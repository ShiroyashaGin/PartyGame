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
    Image playerImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePlayerCard(Player _player) {
        linkedPlayer = _player;
        playerImage.sprite = _player.image;
        playerName.text = _player.name;
    }
}
