using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinnerScreen : MonoBehaviour
{

    public TMP_Text playerName;
    public Image playerImage;


    public void SetData(Player winner)
    {
        playerName.text = winner.name;
        playerImage.sprite = winner.image;
    }
}
