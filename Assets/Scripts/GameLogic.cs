using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


/// <summary>
/// GameLogic handles the main sequence of the game. It controls the main game loop by running through coroutine
/// TurnSequence().
/// 
/// </summary>
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

    //Delegate Action to create an Event so that the PlayerManager can hook into it.
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

    //Gets the result of the answer passed down from QuestionManager.
    public void SubmitAnswer(bool correct, int answerIndex) {
        answerIsCorrect = correct;
        answerReceived = true;
        //Currently not in use but a planned feature. qm.questionCard.HighlightGivenAnswer(answerIndex); 
    }

    //Result of the answer outcome
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

    //Updates the player position based on the amount of points needed to win and the distance to the end.
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

    //Gives the turn to the next player and loops it back to the first player within the list in case everyone had
    //their turn.
    public void NextPlayer() {
        if (pm.playersList.IndexOf(currentPlayer) >= pm.playersList.Count - 1) {
            currentPlayer = pm.playersList[0];
        }
        currentPlayer = pm.playersList[pm.playersList.IndexOf(currentPlayer) + 1];
    }

    /// <summary>
    /// Prepares the states and handled data for when the game ends
    /// </summary>
    void GameEnd() {
        PanelManager.instance.SwitchPanel(winnerPanel);
        winnerScreen.SetData(currentPlayer);

        //Disable all player objects
        foreach (Player player in pm.playersList) {
            player.playerCard.gameObject.SetActive(false);
        }
    }

    //Checks for the current game state if it should continue to play the next round
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

    /// <summary>
    /// If the game runs out of questions it checks for the highest scoring player and ends the game.
    /// This is flawed since the game doesn't handle draws at the moment.
    /// </summary>
    public void OutOfCards() {
        Player highestScorePlayer = null;
        foreach (Player player in pm.playersList) {
           
            if (highestScorePlayer == null) {
                highestScorePlayer = player;
            }
            else if(highestScorePlayer.score <= player.score){
                highestScorePlayer = player;
            }
        }
        currentPlayer = highestScorePlayer;
        GameEnd();
    }

    /// <summary>
    /// The main sequence of the game. Waits for each individual breakpoint during a single turn, such as:
    /// Showing the question, waiting for the player answer and short interval before showing the answer.
    /// The intervals are built in to allow some room for animation and sounds (for futurue update ). 
    /// </summary>
    /// <returns></returns>
    public IEnumerator TurnSequence() {
        OnCurrentPlayerChanged(currentPlayer);
        yield return new WaitForSeconds(2f);
        if (qm.questionStack.Count < 1) {
            OutOfCards();
            yield break;
        }
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
