using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Reference UI elements so we can pass data into them to display the winner. 
//Should be part of the Panel class via inheritance in the future.
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
