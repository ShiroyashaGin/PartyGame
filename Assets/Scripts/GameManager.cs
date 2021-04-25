using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentPanel;
    public bool gameStarted = false;


    [Header("Settings")]
    public int maxAmountOfPlayers;
    public int scoreToWin;

    PlayerManager pm;
     
    // Start is called before the first frame update
    void Awake()
    {
        //Prevention in case another instance would be created
        if (!instance) {
            instance = this;
            Debug.Log("Set instance " + " GameManager");
        }
        else {
            Destroy(this);
        }
    }

    public void StartGame() {
        pm = PlayerManager.instance;
        pm.SetPlayerIds();

        foreach(Player player in pm.playersList) {
            //player.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
