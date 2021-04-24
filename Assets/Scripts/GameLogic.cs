using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameLogic : MonoBehaviour
{

    public List<Player> turnList = new List<Player>();

    public Player playerTurn;
    public int turnNumber;

    PlayerManager pm;
    GameManager gm;
    QuestionManager qm;
 
    void Start()
    {
        pm = PlayerManager.instance;
        gm = GameManager.instance;
        qm = QuestionManager.instance;
    }

  
    void Update()
    {
        
    }

    public void StartGame() {
        gm.gameStarted = true;
        pm.SetPlayerIds();
        qm.ShufflesStack();
        foreach(Player player in pm.playersList) {
            player.playerCard.transform.DOMove(pm.gamePositions[player.id].transform.position, 0.8f, false);
        }
    }
}
