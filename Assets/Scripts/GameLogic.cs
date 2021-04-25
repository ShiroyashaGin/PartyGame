using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    public List<Player> turnList = new List<Player>();

    public Player currentPlayer;
    public bool answerIsCorrect;
    public int turnNumber;
    

    PlayerManager pm;
    GameManager gm;
    QuestionManager qm;
    PlayerCardManager pcm;

    float distanceStep;
    bool answerReceived;

    float playerCardStartingPositionX;

    public delegate void UpdateTurnIndicator();
    public event UpdateTurnIndicator OnNextTurn;

    void Awake() {
        //Prevention in case another instance would be created
        if (!instance) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    void Start()
    {
        pm = PlayerManager.instance;
        gm = GameManager.instance;
        qm = QuestionManager.instance;
        pcm = PlayerCardManager.instance;

        distanceStep = (pm.gamePositions[0].transform.position.x - pm.finishLocations[0].position.x) / gm.scoreToWin;
        playerCardStartingPositionX = pm.gamePositions[0].transform.position.x;
    }

    public void SubmitAnswer(bool correct, int answerIndex) {
        answerIsCorrect = correct;
        answerReceived = true;
        qm.questionCard.HighlightGivenAnswer(answerIndex);
    }

    void HandleAnswer() {
        Debug.Log("Handle answer");
        //show result
        //add score to player
        //update position
        answerReceived = false;
        currentPlayer.score++;
        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition() {
        currentPlayer.playerCard.transform.DOMoveX(playerCardStartingPositionX - distanceStep * currentPlayer.score,1f);
    }

    public void StartGame() {
        if (pm.playersList.Count < 2) {
            //Not enough players
            return;
        }
        PanelManager.instance.SwitchPanel(PanelManager.instance.gamePanel);
        gm.gameStarted = true;
        pm.SetPlayerIds();
        qm.ShufflesStack();
        GivePlayerTurn(0);
        foreach (Player player in pm.playersList) {
            player.playerCard.transform.DOMove(pm.gamePositions[player.id].transform.position, 0.8f, false);
        }

        StartCoroutine(TurnSequence());
    }

    void GivePlayerTurn(int index) {
        currentPlayer = pm.playersList[0];
        OnNextTurn();
    }

    public void NextPlayer() {
        currentPlayer = pm.playersList[pm.playersList.IndexOf(currentPlayer) + 1];
        OnNextTurn();
    }

    void GameEnd() {
        //Game end
        Debug.Log("GAME END");
    }

    public void GameStateCheck() {
        if (currentPlayer.score >= gm.scoreToWin) {
            GameEnd();
        }
        else {
            if(pm.playersList.IndexOf(currentPlayer) >= pm.playersList.Count - 1) {
                GivePlayerTurn(0);
            }
            NextPlayer();
            StartCoroutine(TurnSequence());
        }
    }

    public IEnumerator TurnSequence() {
        yield return new WaitForSeconds(2f);
        qm.ShowQuestion();
        while (!answerReceived) {
            yield return new WaitForEndOfFrame();
        }
        //qm.ShowResult(answerIsCorrect);
        yield return new WaitForSeconds(3f);
        qm.ShowResult(answerIsCorrect);
        
       
        yield return new WaitForSeconds(1f);
        qm.HideQuestion();
        qm.HideResult();
        //animation players before showing answer;
        yield return new WaitForSeconds(2f);
        HandleAnswer();
        yield return null;
        GameStateCheck();
    }
}
