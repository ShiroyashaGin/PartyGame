using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public static int globalId = 0;

    int maxAmountOfPlayers = 0;
    public List<Player> inactivePlayers = new List<Player>();
    public List<Player> playersList = new List<Player>();
    public List<Image> playerPictures, currentPictures = new List<Image>();

    GameManager gm;

    // Start is called before the first frame update
    void Awake() {
        //Prevention in case another instance would be created
        if (!instance) {
            Debug.Log("set instance");
            instance = this;
        }
        else {
            Destroy(this);
        }

        currentPictures = new List<Sprite>(playerPictures);
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        maxAmountOfPlayers = gm.amountOfPlayers;
    }


    public void AddPlayer(string playerName) {
        Player newPlayer = new Player();
        newPlayer.name = playerName;
        //newPlayer.image = 
      
        
        playersList.Add(newPlayer);
        Debug.Log(string.Format("Added {0} ", newPlayer.name));
    }


    [ContextMenu("Check List")]
    public void Check() {
        foreach(Player player in playersList) {
            Debug.Log(string.Format("{0}", player.name));
            
        }
    }
}

    

