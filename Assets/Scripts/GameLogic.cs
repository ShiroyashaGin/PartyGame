using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    public List<Player> turnList = new List<Player>();

    public Player currentPlayer;
    public bool answerIsCorrect;
    public int turnNumber = 1;

    public Panel winnerPanel;
    public WinnerScreen winnerScreen;
    public ScoreLines scoreLines;

    [Header("Interval Settings")]
    public float showQuestionResult;
    public float timeBeforeShowingAnswer;

    PlayerManager pm;
    GameManager gm;
    QuestionManager qm;

    public float distanceStep;
    bool answerReceived;

    float playerCardStartingPositionX;

    public static event Action<Player> OnCurrentPlayerChanged = delegate { };

    void Awake() {
        //Prevention in case another instance would be created
        if (!instance) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    void Start() {
        pm = PlayerManager.instance;
        gm = GameManager.instance;
        qm = QuestionManager.instance;

        distanceStep = (pm.gamePositions[0].transform.position.x - pm.finishLocations[0].position.x) / gm.scoreToWin;
        playerCardStartingPositionX = pm.gamePositions[0].transform.position.x;
        scoreLines.Initialize();
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
        if (answerIsCorrect) {
            currentPlayer.score++;
        }

        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition() {
        currentPlayer.playerCard.transform.DOMoveX(playerCardStartingPositionX - distanceStep * currentPlayer.score, 1f);
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
        currentPlayer = pm.playersList[index];
    }

    public void NextPlayer() {
        if (pm.playersList.IndexOf(currentPlayer) >= pm.playersList.Count - 1) {
            currentPlayer = pm.playersList[0];
        }
        currentPlayer = pm.playersList[pm.playersList.IndexOf(currentPlayer) + 1];
    }

    void GameEnd() {
        PanelManager.instance.SwitchPanel(winnerPanel);
        winnerScreen.SetData(currentPlayer);
        foreach (Player player in pm.playersList) {
            player.playerCard.gameObject.SetActive(false);
        }
    }

    public void GameStateCheck() {
        if (currentPlayer.score >= gm.scoreToWin) {
            Invoke("GameEnd", 3f);
        }
        else {
            //Last player of the round played, so finish a turn and loop back to player one
            if (pm.playersList.IndexOf(currentPlayer) >= pm.playersList.Count - 1) {
                GivePlayerTurn(0);
                turnNumber++;
            }
            else {
                NextPlayer();
            }
            StartCoroutine(TurnSequence());
        }
    }

    public IEnumerator TurnSequence() {
        OnCurrentPlayerChanged(currentPlayer);
        yield return new WaitForSeconds(2f);
        qm.ShowQuestion();
        while (!answerReceived) {
            yield return new WaitForEndOfFrame();
        }
        //qm.ShowResult(answerIsCorrect);
        yield return new WaitForSeconds(timeBeforeShowingAnswer);
        qm.ShowResult(answerIsCorrect);


        yield return new WaitForSeconds(showQuestionResult);
        qm.HideQuestion();
        qm.HideResult();
        //animation players before showing answer;
        yield return new WaitForSeconds(0.5f);
        HandleAnswer();
        yield return null;
        GameStateCheck();
    }
}
