using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLines : MonoBehaviour
{
    public Transform parent;
    public RectTransform line;
    GameLogic gameLogic;

    public void Initialize() {
        gameLogic = GameLogic.instance;
        for(int i = 0; i < GameManager.instance.scoreToWin + 1; i++) {
            RectTransform newLine = Instantiate(line);
            
            newLine.transform.SetParent(parent,false);
            newLine.transform.position = PlayerManager.instance.gamePositions[0].transform.position;
            
            Vector3 pos = newLine.transform.position;
            pos.x -= gameLogic.distanceStep * i;
            newLine.transform.position = pos;
        }
    }
}
