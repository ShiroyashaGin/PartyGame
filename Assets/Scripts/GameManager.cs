using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// The global GameManager persists through the whole game and is used for global values and actions.
/// </summary>
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

    }

    public void RestartScene() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
