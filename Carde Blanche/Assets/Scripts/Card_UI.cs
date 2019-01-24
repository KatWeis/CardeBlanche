using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_UI : MonoBehaviour {

    private Outline ol;
    private bool selected;
    private bool hovered;

	// Use this for initialization
	void Start () {
        ol = gameObject.GetComponent<Outline>();
        selected = false;
        hovered = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Turns a red highlight on if the card is selected
    /// Turns the highlight off if the card is deselected
    /// </summary>
    public void SelectHighlight()
    {
        selected = !selected;
        if(selected)
        {
            ol.effectColor = Color.red;
            ol.effectDistance = new Vector2(6, 6);
        }
        else
        {
            ol.effectColor = Color.black;
            ol.effectDistance = new Vector2(1, -1);
        }
        
    }

    /// <summary>
    /// Set hover to on
    /// </summary>
    public void StartHover()
    {
        hovered = true;
        HoverHighlight();
    }

    /// <summary>
    /// Set hover to off
    /// </summary>
    public void StopHover()
    {
        hovered = false;
        HoverHighlight();
    }

    /// <summary>
    /// Returns without doing anything if the card is already selected
    /// Otherwise it will give the card a green highlight when hovered over
    /// </summary>
    private void HoverHighlight()
    {
        if (selected) return;
        if (hovered)
        {
            ol.effectColor = Color.green;
            ol.effectDistance = new Vector2(6, 6);
        }
        else
        {
            ol.effectColor = Color.black;
            ol.effectDistance = new Vector2(1, -1);
        }
    }
}
