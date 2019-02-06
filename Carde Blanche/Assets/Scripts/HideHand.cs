using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideHand : MonoBehaviour{

    private GameObject hand;
    private bool isHandVis;

    private float startY;
    private float endY;

    private float upY = 135; // Y coordinate when hand is visible
    private float downY = -210; // Y coordinate when hand is hidden

    public bool IsHandVis { get { return isHandVis; } }

    // Use this for initialization
    void Start () {
        isHandVis = true;

        hand = transform.parent.GetChild(2).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        MoveHand();
	}

    /// <summary>
    /// Checks if the click was using the right mouse button
    /// Slides the hand panel down or up depending on its current state
    /// </summary>
    public void MoveHand()
    {
        if(!Input.GetMouseButtonDown(1) || (hand.transform.position.y != upY && hand.transform.position.y != downY))
        {
            return;
        }
        isHandVis = !isHandVis;

        //DeselectAllCards();

        IEnumerator sh = SlideHand(isHandVis);
        StartCoroutine(sh);
    }

    /// <summary>
    /// Bumps the Card up above the others to emphasize selection
    /// </summary>
    IEnumerator SlideHand(bool slideUp)
    {
        //DeselectAllCards();
        //StopCoroutine("BumpCard");
        //StopCoroutine("UnBumpCard");


        if (slideUp)
        {
            startY = downY;
            endY = upY;
        }
        else
        {
            startY = upY;
            endY = downY;
        }


        float currentTime = 0f;
        // get the total angle we are traversing * degreesPerSecond
        // to get totalTime we need to Lerp
        float maxTime = .5f;

        while (currentTime < maxTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            float yPos = Mathf.Lerp(startY, endY, currentTime / maxTime);
            hand.transform.position = new Vector3(hand.transform.position.x, yPos, hand.transform.position.z);
        }
    }

    /// <summary>
    /// Sets all cards' selected variable to false
    /// Clears Highlights
    /// </summary>
    private void DeselectAllCards()
    {
        for (int i = 0; i < hand.transform.childCount; i++)
        {
            hand.transform.GetChild(i).GetComponent<Card_UI>().selected = false;
            hand.transform.GetChild(i).GetComponent<Card_UI>().SendMessage("ClearHighlight");
            hand.transform.GetChild(i).position = new Vector3(hand.transform.GetChild(i).position.x, 
                hand.transform.GetChild(i).GetComponent<Card_UI>().inactiveY, hand.transform.GetChild(i).position.z);
        }
    }
}
