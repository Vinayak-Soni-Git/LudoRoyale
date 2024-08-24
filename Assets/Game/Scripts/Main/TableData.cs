using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TableData : MonoBehaviour
{
    public TextMeshProUGUI EnrtyText,Wintext;
    private int Entry, Win;
    public TextMeshProUGUI Heading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetData(int enrty, int win, int head)
    {
        EnrtyText.text = "₹"+ enrty.ToString();
        Wintext.text = "₹" + win.ToString();
        Entry = enrty;
        Win = win;
        if (head == 1)
        {
            Heading.text = "1 vs 1";
        }
        else
            Heading.text = "1 vs 3";


    }


    public void StartGame()
    {
        InitMenuScript.inst.List.SetActive(false);
        GameManager.Instance.payoutCoins = Entry;
        PlayerPrefs.SetInt("EN", Entry);
        PlayerPrefs.SetInt("WINAMT", Win);
        if (GameManager.Instance.TotalCoins >= GameManager.Instance.payoutCoins)
        {
            // GameManager.Instance.initMenuScript.BackgroundSound.Stop();
            if (GameManager.Instance.type != MyGameType.Private)
            {
                InitMenuScript.inst.startTimer();
                GameManager.Instance.facebookManager.startRandomGame();
            }
            else
            {
                InitMenuScript.inst.botTimer.SetActive(false);
                if (GameManager.Instance.JoinedByID)
                {
                    Debug.Log("Joined by id!");
                    GameManager.Instance.matchPlayerObject.GetComponent<SetMyData>().MatchPlayer();
                }
                else
                {
                    Debug.Log("Joined and created");
                    GameManager.Instance.playfabManager.CreatePrivateRoom();
                    GameManager.Instance.matchPlayerObject.GetComponent<SetMyData>().MatchPlayer();
                }

            }
        }
        else
        {
            GameManager.Instance.dialog.SetActive(true);
        }
    }
}
