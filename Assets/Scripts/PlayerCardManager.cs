using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public static PlayerCardManager instance;
    public PlayerCard PlayerCardPrefab;

    public List<PlayerCard> pool, usedPlayerCards = new List<PlayerCard>();


    void Awake()
    {
        //Prevention in case another instance would be created
        if (!instance) {
            Debug.Log("Set instance " + " PlayerCardManager");
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        PopulatePool();
    }


    void PopulatePool() {
        int x = GameManager.instance.maxAmountOfPlayers;
        for (int i = 0; i < x; i++) {
            PlayerCard newPlayerCard = Instantiate(PlayerCardPrefab, GameManager.instance.currentPanel.transform);
            newPlayerCard.gameObject.SetActive(false);
            pool.Add(newPlayerCard);
        }
    }

    public PlayerCard CreatePlayerCard(Player player) {
        PlayerCard newPlayerCard = pool[0];
        newPlayerCard.InitializePlayerCard(player);
        newPlayerCard.gameObject.SetActive(true);
        newPlayerCard.gameObject.transform.position = Vector2.zero;
        pool.Remove(newPlayerCard);
        usedPlayerCards.Add(newPlayerCard);
        return newPlayerCard;
    }

    public void ChildAllPlayerCards(Transform parent) {
        foreach(PlayerCard playerCard in usedPlayerCards) {
            playerCard.transform.SetParent(parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
