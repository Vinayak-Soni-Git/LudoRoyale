using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBarColorsScript : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TextMeshProUGUI topPanelText;
    public Image topBarPanel;
    public Image notificatinoButton;
    public Image walletButton;
    public TextMeshProUGUI walletBalance;
    public Image addMoneyButton;
    public Color startColor = new Color(1f, 1f, 1f, 1f);
    public Color endColor = new Color(255f, 255f, 255f, 100f);

    Color walletBalanceStartColor = new Color(14f, 0f, 255f, 255f);
    Color walletBalanceEndColor = new Color(1f, 1f, 1f, 1f);
    public Color topBarPanelStartColor = new Color(14f, 0f, 255f, 255f);
    public Color topBarPanelEndColor = new Color(255f, 255f, 255f, 100f);

    void Start()
    {
        // Subscribe to the ScrollRect's onValueChanged event
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    // This method is called whenever the scroll position changes
    private void OnScrollValueChanged(Vector2 scrollPosition)
    {
        // Get the vertical scroll position (0 at the bottom, 1 at the top)
        float verticalScrollValue = scrollRect.verticalNormalizedPosition;
        // topBarPanel.color = Color.Lerp(topBarPanelEndColor, topBarPanelStartColor, verticalScrollValue);

        // Interpolate the color of the top panel based on the scroll position

        notificatinoButton.color = Color.Lerp(endColor, startColor, verticalScrollValue);
        walletButton.color = Color.Lerp(endColor, startColor, verticalScrollValue);
        walletBalance.color = Color.Lerp(walletBalanceEndColor, walletBalanceStartColor, verticalScrollValue);
        addMoneyButton.color = Color.Lerp(endColor, startColor, verticalScrollValue);
        
        topPanelText.color = Color.Lerp(endColor, startColor, verticalScrollValue);
        topBarPanel.color = Color.Lerp(topBarPanelEndColor, topBarPanelStartColor, verticalScrollValue);
    }

    void OnDestroy()
    {
        // Unsubscribe from the ScrollRect's onValueChanged event when the object is destroyed
        scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
    }
}
