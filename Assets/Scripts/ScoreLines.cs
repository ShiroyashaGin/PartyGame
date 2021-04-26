using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLines : MonoBehaviour
{
    public Transform parent;
    public RectTransform line;
    GameLogic gameLogic;

    /// <summary>
    /// Creates the lines across the board to indicate the current score of the player and the score required to win.
    /// Is dynamically placed based on the score required to win.
    /// one line for each score point.
    /// </summary>
    public void Initialize() {
        gameLogic = GameLogic.instance;
        for(int i = 0; i < GameManager.instance.scoreToWin + 1; i++) {
            RectTransform newLine = Instantiate(line);
            
            //Set the right starting position
            newLine.transform.SetParent(parent,false);
            newLine.transform.position = PlayerManager.instance.gamePositions[0].transform.position;
            
            //calculate y offset
            Vector3 pos = newLine.transform.position;
            pos.x -= gameLogic.distanceStep * i;

            //assign final positions
            newLine.transform.position = pos;
        }
    }
}
