using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class GameManager : MonoBehaviour {
    EventDictionary ed = new EventDictionary();
    CardDictionary cd = new CardDictionary();

    List<Card> deck = new List<Card>();
    int[] selected = new int[2];

	// Use this for initialization
	void Start () {
        //Arbitrary deck with all current cards
        deck.Add(cd.Cards[10]);
        deck.Add(cd.Cards[11]);
        deck.Add(cd.Cards[12]);
        deck.Add(cd.Cards[13]);
        deck.Add(cd.Cards[14]);
        deck.Add(cd.Cards[15]);
        deck.Add(cd.Cards[16]);

        //Arbitrary selected two cards
        selected[0] = deck[0].Id;
        selected[1] = deck[5].Id;

        //Combine the two ids into a string and then convert back to an int. to get the four digit event id
        int combinedID = Convert.ToInt32("" + selected.Min() + selected.Max());
        Debug.Log(ed.Events[combinedID].Name);
        Debug.Log(ed.Events[combinedID].Desc);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
