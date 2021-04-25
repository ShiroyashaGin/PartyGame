using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCard : MonoBehaviour
{
    public Player linkedPlayer;

    [SerializeField]
    TMP_Text playerName;
    [SerializeField]
    Image playerImage, turnIdicator;
    GameLogic gameLogic;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameLogic.instance;
        gameLogic.OnNextTurn += NextTurn;
    }

    void NextTurn() {
        Debug.Log("Next Turn");
        turnIdicator.gameObject.SetActive(gameLogic.currentPlayer == linkedPlayer);
    }

    public void InitializePlayerCard(Player _player) {
        linkedPlayer = _player;
        playerImage.sprite = _player.image;
        playerName.text = _player.name;
    }
}
