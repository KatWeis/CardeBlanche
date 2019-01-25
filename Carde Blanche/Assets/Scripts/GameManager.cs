using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    EventDictionary ed = new EventDictionary();
    CardDictionary cd = new CardDictionary();

    public GameObject cardPrefab;
    public GameObject canvas;

    public int handSize = 7;// only amount that looks nice, DON'T CHANGE
    List<GameObject> hand = new List<GameObject>();

    List<Card> deck = new List<Card>();
    
    //Potential Solution
    //Card[] selected = new Card[2];

    int[] selected = new int[2];
    
	// Use this for initialization
	void Start () {
        selected[0] = -1;
        //Arbitrary deck with all current cards
        deck.Add(cd.Cards[10]);
        deck.Add(cd.Cards[11]);
        deck.Add(cd.Cards[12]);
        deck.Add(cd.Cards[13]);
        deck.Add(cd.Cards[14]);
        deck.Add(cd.Cards[15]);
        deck.Add(cd.Cards[16]);

        //Arbitrary selected two cards
        //selected[0] = deck[0].Id;
        //selected[1] = deck[5].Id;

        //Combine the two ids into a string and then convert back to an int. to get the four digit event id
       // int combinedID = Convert.ToInt32("" + selected.Min() + selected.Max());
       // Debug.Log(ed.Events[combinedID].Name);
       // Debug.Log(ed.Events[combinedID].Desc);

        CreateHand();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateHand()
    {
        int totalWidth = handSize * 155;

        for (int i = 0; i < handSize; i++)
        {
            //Random selection
            //int cardIndex = UnityEngine.Random.Range(0, deck.Count - 1);
            int cardIndex = i;
            GameObject card = GameObject.Instantiate(cardPrefab, canvas.transform);
            card.GetComponentInChildren<Text>().text = deck[cardIndex].Name;
            card.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>("Beta_" + deck[cardIndex].Name);

            //offset the card
            float offset = (totalWidth / handSize) + 155 * i;
            card.transform.position = new Vector3(transform.position.x + offset, card.transform.position.y, card.transform.position.z);
            card.GetComponent<Card_UI>().cardID = deck[cardIndex].Id;
            hand.Add(card);
        }
        
    }

    public void AddSelectedCard(int Id)
    {
        
        if(selected[0] == -1)
        {
            selected[0] = Id;
        }
        else
        {
            selected[1] = Id;

            int combinedID = Convert.ToInt32("" + selected.Min() + selected.Max());
            try
            {
                canvas.transform.GetChild(0).GetComponent<Text>().text = ed.Events[combinedID].Name;
                canvas.transform.GetChild(1).GetComponent<Text>().text = ed.Events[combinedID].Desc;
            }catch(Exception e)
            {
                canvas.transform.GetChild(0).GetComponent<Text>().text = "Dud";
                canvas.transform.GetChild(1).GetComponent<Text>().text = "*Fart Noise*";
            }

            for(int i = 0; i < handSize; i++)
            {
                if (hand[i].GetComponent<Card_UI>().selected == true)
                {
                    hand[i].GetComponent<Card_UI>().selected = false;
                    hand[i].SendMessage("ClearHighlight");
                }
                

            }
            
            //Debug.Log(ed.Events[combinedID].Name);
            //Debug.Log(ed.Events[combinedID].Desc);
        }
        
    }

    public void ClearSelectedCard()
    {
        selected[0] = -1;
    }
}
