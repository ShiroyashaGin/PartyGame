using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The player data class that is being tracked by the PlayerManager. It is independent from the visual aspect of
/// the player. This Player data can be used in a player card to populate its fields.
/// </summary>
[System.Serializable]
public class Player
{
    public string name;
    public Sprite image;
    public int id;
    public PlayerCard playerCard;
    public int score;
}
