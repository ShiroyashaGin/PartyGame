using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// The playercard represent that player data in a visual way on the field. Taking its values and displaying it
/// on the screen with the name and an image.
/// </summary>
public class PlayerCard : MonoBehaviour
{
    public Player linkedPlayer;

    [SerializeField]
    TMP_Text playerName;
    [SerializeField]
    Image playerImage, turnIdicator;

    public void SetTurn(bool turn) {
        turnIdicator.gameObject.SetActive(turn);
    }

    public void InitializePlayerCard(Player _player) {
        linkedPlayer = _player;
        playerImage.sprite = _player.image;
        playerName.text = _player.name;
    }
}
