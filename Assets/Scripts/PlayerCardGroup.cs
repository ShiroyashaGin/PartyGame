using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardGroup : MonoBehaviour
{
    public List<Transform> positions3 = new List<Transform>();
    public List<PlayerCard> positions = new List<PlayerCard>();
    //Dictionary<int, PlayerCard> positions = new Dictionary<int, PlayerCard>();
    // Start is called before the first frame update
    void Awake()
    {
        //Create empty value entries in the dictionary for each position
       foreach(Transform pos in positions3) {
           //positions.Add(positions3.IndexOf(pos), null);
        }
       //positions.
    }

    public Transform AssignPlayerCardToPosition(PlayerCard player) {
        //Loop through the list to check for an open position.
        for(int i = 0; i < positions.Count-1; i++) {
            //Check if the value of the key value pair is open.
            if (positions[i] == null) {
                positions[i] = player;
                return positions3[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
