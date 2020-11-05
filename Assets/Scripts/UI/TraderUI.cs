using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraderUI : MonoBehaviour
{
    private Sprite _sprite;
    
    public Image img;
    public Image progressBar;
    public TextMeshProUGUI traderNbText;

    /// <summary>
    ///     This function init trader's UI
    /// </summary>
    public void InitUI(int nb)
    {
        img.sprite = _sprite;
        traderNbText.text = "Trader n°" + nb;
        ResetProgressBar();
    }

    /// <summary>
    ///     This function calculates progress of bar to animate it
    /// </summary>
    public void UpdateProgressBar(float currentTime, float maxTime)
    {
        progressBar.fillAmount = (100 - (currentTime * 100 / maxTime)) / 100;
    }

    /// <summary>
    ///     This function reset the progress bar
    /// </summary>
    public void ResetProgressBar()
    {
        progressBar.fillAmount = 1;
    }
}
