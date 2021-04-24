using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public static int globalId = 0;
    
    public List<Player> inactivePlayers = new List<Player>();
    public List<Player> playersList = new List<Player>();
    public List<Sprite> playerPictures, currentPictures = new List<Sprite>();

    public List<Transform> startingPositions,gamePositions = new List<Transform>();
    public Transform spawnPosition;

    public delegate void PlayerLimitReached();
    public event PlayerLimitReached OnPlayerLimitreached;

    public delegate void PlayerSlotAvailable();
    public event PlayerLimitReached OnPlayerSlotAvailable;

    GameManager gm;
    public int maxAmountOfPlayers = 0;


    // Start is called before the first frame update
    void Awake() {
        //Prevention in case another instance would be created
        if (!instance) {
            Debug.Log("Set instance " + " PlayerManager");
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
        maxAmountOfPlayers = gm.maxAmountOfPlayers;
    }

    public void SetPlayerIds() {
        foreach(Player player in playersList) {
            player.id = playersList.IndexOf(player);
        }
    }

    public void AddPlayer(string playerName) {
        //Create new object player
        Player newPlayer = new Player();
        newPlayer.name = playerName;

        //Select a random image
        int randomColor = Random.Range(0, currentPictures.Count - 1);
        newPlayer.image = currentPictures[randomColor];
        currentPictures.RemoveAt(randomColor);

        //Add created object to the list
        playersList.Add(newPlayer);
        Debug.Log(string.Format("Added {0} ", newPlayer.name));

        //Attach player to player card
        PlayerCard pc = PlayerCardManager.instance.CreatePlayerCard(newPlayer);
        pc.transform.position = spawnPosition.position;
        pc.transform.DOMove(startingPositions[playersList.IndexOf(newPlayer)].transform.position,0.3f);
        newPlayer.playerCard = pc;

        if (playersList.Count >= maxAmountOfPlayers)
            OnPlayerLimitreached();
    }

    public void RemovePlayer() {
        //ADD event onplayerspot available.
        //click to remove player
    }


    [ContextMenu("Check List")]
    public void Check() {
        foreach(Player player in playersList) {
            Debug.Log(string.Format("{0}", player.name));
            
        }
    }
}

    

