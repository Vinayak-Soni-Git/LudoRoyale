using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonsHeight : MonoBehaviour
{
    public float newHeight; // The new height for the button
    private float originalHeight; // The original height of the button

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalHeight = rectTransform.sizeDelta.y; // Store the original height
        }
    }

    public void OnPointerDown()
    {
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight); // Change to new height
        }
    }

    public void OnPointerUp()
    {
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, originalHeight); // Revert to original height
        }
    }
}
