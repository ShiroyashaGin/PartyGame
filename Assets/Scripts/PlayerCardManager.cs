using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles the Visual card aspect of the player. The playercard represents the player with an image, name and
/// turn indicator.
/// </summary>
public class PlayerCardManager : MonoBehaviour
{
    public static PlayerCardManager instance;

    public Transform playerCardParent;
    public PlayerCard PlayerCardPrefab;

    //Keeping tracking of used and unused player card objects;
    public List<PlayerCard> pool, usedPlayerCards = new List<PlayerCard>();


    void Awake() {
        //Prevention in case another instance would be created
        if (!instance) {
            Debug.Log("Set instance " + " PlayerCardManager");
            instance = this;
        }
        else {
            Destroy(this);
        }

        GameLogic.OnCurrentPlayerChanged += OnCurrentPlayerChanged;
    }

    private void Start() {
        PopulatePool();
    }

    //Subscribed event when the current player changed
    private void OnCurrentPlayerChanged(Player currentPlayer) {
        foreach(PlayerCard pc in usedPlayerCards) {
            pc.SetTurn(pc.linkedPlayer == currentPlayer);
        }
    }

    /// <summary>
    /// Populates the pool of playerCards objects which is in this case a bit redundant as the players can only spawn
    /// in once. It was added so that it is more easily supports adding and removing players from the list.
    /// </summary>
    void PopulatePool() {
        int x = GameManager.instance.maxAmountOfPlayers;
        for (int i = 0; i < x; i++) {
            PlayerCard newPlayerCard = Instantiate(PlayerCardPrefab, playerCardParent);
            newPlayerCard.gameObject.SetActive(false);
            pool.Add(newPlayerCard);
        }
    }

    /// <summary>
    /// Creates a new playercard object with the players data passed down through the parameter.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public PlayerCard CreatePlayerCard(Player player) {
        PlayerCard newPlayerCard = pool[0];
        newPlayerCard.InitializePlayerCard(player);
        newPlayerCard.gameObject.SetActive(true);
        newPlayerCard.gameObject.transform.position = Vector2.zero;
        pool.Remove(newPlayerCard);
        usedPlayerCards.Add(newPlayerCard);
        return newPlayerCard;
    }

    /// Unsubscribe from the static event in case the objects get destroyed to avoid null calls.
    /// (when restarting the game i.e.)
    private void OnDestroy() {
        GameLogic.OnCurrentPlayerChanged -= OnCurrentPlayerChanged;
    }
}

