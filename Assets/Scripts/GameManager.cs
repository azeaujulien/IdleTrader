using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Info")]
    public ulong money;
    public int level = 1;
    public float defaultXpToNextLevel = 100;
    [HideInInspector] public float xpToNextLevel;
    [HideInInspector] public float currentXp;

    [Header("Trader Info")]
    [SerializeField] private GameObject traderPrefab;
    [SerializeField] private Transform traderParent;
    public ulong defaultGain = 10;
    public float defaultMul = 1;
    public float defaultSpeed = 5;
    public ulong defaultTapGain = 10;
    [SerializeField] private Button traderZone;
    [HideInInspector] public int numberOfTrader;
    [SerializeField] private AnimationCurve maxTraderForLevel;
    private int _maxTrader;
    
    [HideInInspector] public ulong currentGain;
    [HideInInspector] public float currentMul;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public ulong currentTapGain;

    [Header("Upgrade")]
    [SerializeField] private Button traderButton;

    [Header("Other")]
    [SerializeField] private Transform tapHolder;
    [SerializeField] private GameObject tapPrefab;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        // Create Singleton
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        currentGain = defaultGain;
        currentMul = defaultMul;
        currentSpeed = defaultSpeed;
        currentTapGain = defaultTapGain;
        xpToNextLevel = defaultXpToNextLevel;
        traderZone.onClick.AddListener(OnTraderZoneClick);
        _maxTrader = (int) (maxTraderForLevel.Evaluate(level * 0.01f) * 100);
    }

    private void Update()
    {
        UIManager.Instance.UpdateUI();
    }

    /// <summary>
    ///     This function add money after user tap and intantiate visual alert
    /// </summary>
    /// <remarks>
    ///    Call function when clicking on the trader area
    /// </remarks>
    private void OnTraderZoneClick()
    {
        AddMoney(currentTapGain);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        Vector3 point = mainCamera.ScreenToWorldPoint(mousePos);
        GameObject tap = Instantiate(tapPrefab, tapHolder);
        tap.transform.position = point;
        Destroy(tap, 1.5f);
    }

    /// <summary>
    ///     This function add one level to the player and calculates new xp objective and new max trader number
    /// </summary>
    public void LevelUp()
    {
        money += (ulong)xpToNextLevel;
        level += 1;
        currentXp = 0;
        xpToNextLevel = defaultXpToNextLevel + (defaultXpToNextLevel * level) / 3;
        _maxTrader = (int) (maxTraderForLevel.Evaluate(level * 0.01f) * 100);
        traderButton.interactable = true;
    }

    /// <summary>
    ///     This function add one level to the player and calculates new xp objective and new max trader number
    /// </summary>
    /// <param name="button">Button who call function</param>
    public void AddTrader(UpgradeButton button)
    {
        if (money >= (ulong)button.currentPrice) {
            money -= (ulong)button.currentPrice;
            numberOfTrader += 1;
            button.upgradeLevel += 1;
            TraderUI trader = Instantiate(traderPrefab, traderParent).GetComponent<TraderUI>();
            trader.InitUI(numberOfTrader);
            button.UpdatePrice();
        }

        if (numberOfTrader == _maxTrader) {
            button.GetComponent<Button>().interactable = false;
        }
    }

    /// <summary>
    ///     This function calculate and add money make by trader every seconds
    /// </summary>
    public void AddMoney()
    {
        money += (ulong)(currentGain * currentMul);
        currentXp += currentGain * currentMul;
    }
    
    /// <summary>
    ///     This function add money given in parameter
    /// </summary>
    /// <param name="amount">Amount of money who will be add to user</param>
    public void AddMoney(ulong amount)
    {
        money += amount;
        currentXp += amount;
    }
    
    /// <summary>
    ///     This function increase gain generate by trader
    /// </summary>
    /// <param name="button">Button who call function</param>
    public void IncreaseGain(UpgradeButton button)
    {
        if (money >= (ulong)button.currentPrice) {
            money -= (ulong)button.currentPrice;
            button.upgradeLevel += 1;

            currentGain = (ulong)(defaultGain + (defaultGain * (ulong) button.upgradeLevel) * 0.2f);
                
            button.UpdatePrice();
        }
    }

    /// <summary>
    ///     This function reduce the time between each trader's gain
    /// </summary>
    /// <param name="button">Button who call function</param>
    public void LowerSpeed(UpgradeButton button)
    {
        if (money >= (ulong)button.currentPrice) {
            money -= (ulong)button.currentPrice;
            button.upgradeLevel += 1;
            
            if (currentSpeed <= 0.25f) {
                currentSpeed = 0.1f;
                button.GetComponent<Button>().interactable = false;
            } else {
                currentSpeed = (defaultSpeed - (defaultSpeed * button.upgradeLevel) * 0.02f);
            }
            
            button.UpdatePrice();
        }
    }
    
    /// <summary>
    ///     This function increase gain generate by tap
    /// </summary>
    /// <param name="button">Button who call function</param>
    public void IncreaseTapGain(UpgradeButton button)
    {
        if (money >= (ulong)button.currentPrice) {
            money -= (ulong)button.currentPrice;
            button.upgradeLevel += 1;

            currentTapGain = (ulong)(defaultTapGain + (defaultTapGain * (ulong) button.upgradeLevel) * 0.4f);
                
            button.UpdatePrice();
        }
    }
}
