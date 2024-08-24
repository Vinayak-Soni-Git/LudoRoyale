using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTranform;
    Toggle toggle;
    public Sprite handlerOnImage;
    public Image handler;
    Image image;
    Sprite handlerOffImage;
    Vector2 handlePosition;

    void Awake(){
        toggle = GetComponent<Toggle>();
        image = handler.GetComponent<Image>();
        handlerOffImage = image.sprite;
        handlePosition = uiHandleRectTranform.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        if(toggle.isOn){
            OnSwitch(true);
        }
    }

    void OnSwitch(bool on){
        // if(on){
        //     uiHandleRectTranform.anchoredPosition = handlePosition * -1;
        // }else{
        //     uiHandleRectTranform.anchoredPosition = handlePosition
        // }

        // uiHandleRectTranform.anchoredPosition = on ? handlePosition * -1: handlePosition;
        // image.sprite = on ? handlerOnImage : handlerOffImage;

        uiHandleRectTranform.DOAnchorPos(on ? handlePosition * -1: handlePosition, .4f).SetEase(Ease.InOutBack);
        image.sprite = on ? handlerOnImage : handlerOffImage;
    }

    void OnDestroy(){
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
