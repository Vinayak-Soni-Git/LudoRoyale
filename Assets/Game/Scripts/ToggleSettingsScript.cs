using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSettingsScript : MonoBehaviour
{
    public GameObject settingsMenu;
    bool show = false;

    public void toggleSettings(){
        if(!show){
            show = true;
        }else{
            show = false;
        }
        settingsMenu.SetActive(show);
    }
}
