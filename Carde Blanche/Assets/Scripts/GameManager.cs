using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    EventDictionary ed = new EventDictionary();
    CardDictionary cd = new CardDictionary();

    public GameObject cardPrefab;
    public GameObject canvas;
    public Text eventTitleUI;
    public TextMeshProUGUI eventDescUI;

    private GameObject panel;

    public int handSize = 2;
    List<GameObject> hand = new List<GameObject>();

    List<Card> cardsInHand = new List<Card>();
    List<Card> deck = new List<Card>();

    private bool isDeath = false;
    private int nature = 0;
    private int tech = 0;


    //Potential Solution
    //Card[] selected = new Card[2];

    int[] selected = new int[2];
    
	// Use this for initialization
	void Start () {
        
        selected[0] = -1;
        //Arbitrary deck with all current cards
        //deck.Add(cd.Cards[10]);
        //deck.Add(cd.Cards[11]);
        //deck.Add(cd.Cards[10]);
        //deck.Add(cd.Cards[12]);
        //deck.Add(cd.Cards[10]);
        //deck.Add(cd.Cards[15]);
        //deck.Add(cd.Cards[10]);
        //deck.Add(cd.Cards[15]);
        //deck.Add(cd.Cards[10]);
        //deck.Add(cd.Cards[15]);

        for(int i = 0; i < 4; i++)
        {
            deck.Add(cd.Cards[10]);
            deck.Add(cd.Cards[11]);
            deck.Add(cd.Cards[12]);
            deck.Add(cd.Cards[13]);
            deck.Add(cd.Cards[14]);
            deck.Add(cd.Cards[15]);
            deck.Add(cd.Cards[16]);
            deck.Add(cd.Cards[17]);
            deck.Add(cd.Cards[18]);
            deck.Add(cd.Cards[19]);
            deck.Add(cd.Cards[20]);
        }
        Shuffle(deck);

        //Arbitrary selected two cards
        //selected[0] = deck[0].Id;
        //selected[1] = deck[5].Id;

        //Combine the two ids into a string and then convert back to an int. to get the four digit event id
        // int combinedID = Convert.ToInt32("" + selected.Min() + selected.Max());
        // Debug.Log(ed.Events[combinedID].Name);
        // Debug.Log(ed.Events[combinedID].Desc);

        panel = canvas.transform.GetChild(1).GetChild(0).gameObject;

        CreateHand();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateHand()
    {
        //int totalWidth = handSize * 155;

        for (int i = 0; i < handSize; i++)
        {
            DrawCard();
            handSize--;
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
            
            List<int> removed = new List<int>();
            for(int i = 0; i < handSize; i++)
            {
                if (hand[i].GetComponent<Card_UI>().selected == true)
                {
                    hand[i].GetComponent<Card_UI>().selected = false;
                    hand[i].SendMessage("ClearHighlight");
                    removed.Add(i);
                }          
            }
            
            if (ed.Events.ContainsKey(combinedID))
            {
                eventTitleUI.text = ed.Events[combinedID].Name.ToUpper();
                eventDescUI.text = ed.Events[combinedID].Desc;
                if(ed.Events[combinedID].Affinity == "Nature")
                {
                    Debug.Log("Nature");
                }
                else if(ed.Events[combinedID].Affinity == "Tech")
                {
                    Debug.Log("Tech");
                }
                else
                {
                    Debug.Log("Death");
                }
                Destroy(hand[removed[0]]);
                hand.RemoveAt(removed[0]);
                Destroy(hand[removed[1] - 1]);
                hand.RemoveAt(removed[1] - 1);
                handSize -= 2;
                string name = ed.Events[combinedID].Name;
                
             
               
                this.gameObject.GetComponent<EventManager>().SendMessage(name.Replace(" ", ""),null, SendMessageOptions.DontRequireReceiver);
               
               
                
                DrawCard();
                DrawCard();
            }
            else
            {
                eventTitleUI.text = "DUD";
                eventDescUI.text = "*Fart Noise*";
            }
            
            //Debug.Log(ed.Events[combinedID].Name);
            //Debug.Log(ed.Events[combinedID].Desc);
        }
        
    }

    public void ClearSelectedCard()
    {
        selected[0] = -1;
    }

    //Draw a card from the deck and add it to the players hand
    public void DrawCard()
    {
        //int cardIndex = UnityEngine.Random.Range(0, deck.Count - 1);
        int cardIndex = 0;
        CreateCard(cardIndex);
        deck.RemoveAt(cardIndex);
        handSize++;
    }
    //Start of single card creation to reduce code want to wait for panel and grid before integrating
    public void CreateCard(int index)
    {
        GameObject card = GameObject.Instantiate(cardPrefab, panel.transform);
        card.GetComponentInChildren<Text>().text = deck[index].Name;
        card.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>("Beta_" + deck[index].Name);
        card.GetComponent<Card_UI>().cardID = deck[index].Id;
        hand.Add(card);
        cardsInHand.Add(deck[index]);
    }
    public void Shuffle(List<Card> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
