using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [Header("Infos")]
    public Sprite img;
    public string upgradeName;
    public string effect;
    public int defaultPrice;
    [HideInInspector] public int currentPrice;
    [SerializeField] private AnimationCurve priceCurve;
    [HideInInspector] public int upgradeLevel;
    
    [Header("Components")]
    public Image upgradeImg;
    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeEffectText;
    public TextMeshProUGUI priceText;

    private void Awake()
    {
        currentPrice = defaultPrice;
        upgradeImg.sprite = img;
        upgradeNameText.text = upgradeName;
        upgradeEffectText.text = effect;
        priceText.text = currentPrice + "$";
    }
    
    /// <summary>
    ///     This function calculates and update the new price of the upgrade
    /// </summary>
    /// <remarks>
    ///    This function also update UI
    /// </remarks>
    public void UpdatePrice()
    {
        currentPrice = (int)(defaultPrice + priceCurve.Evaluate(upgradeLevel * 0.01f) * 1000);
        priceText.text = currentPrice + "$";
    }
}
