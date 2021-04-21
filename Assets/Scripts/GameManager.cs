using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentPanel;


    [Header("Settings")]
    public int maxAmountOfPlayers;
     
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
