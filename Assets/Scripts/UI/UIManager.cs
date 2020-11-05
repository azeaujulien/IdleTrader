using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelInfo;
    [SerializeField] private TextMeshProUGUI moneyInfo;
    [SerializeField] private TextMeshProUGUI moneyPerSecond;
    [SerializeField] private Image xpBar;
    [SerializeField] private Button nextLevelButton;
    
    public static UIManager Instance { get; private set; }
    
    private void Awake()
    {
        // Create singleton
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     This function update all info display in UI
    /// </summary>
    public void UpdateUI()
    {
        GameManager gameInfo = GameManager.Instance;
        levelInfo.text = "Level : " + gameInfo.level;
        moneyInfo.text = gameInfo.money + " $";
        xpBar.fillAmount = gameInfo.currentXp * 100 / gameInfo.xpToNextLevel / 100;
        nextLevelButton.interactable = gameInfo.currentXp >= gameInfo.xpToNextLevel;
        float gainPerSecond = gameInfo.currentGain * gameInfo.currentMul / gameInfo.currentSpeed * gameInfo.numberOfTrader;
        moneyPerSecond.text = gainPerSecond.ToString("F2") + " $/s";
    }
}
