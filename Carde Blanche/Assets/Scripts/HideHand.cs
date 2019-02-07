using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideHand : MonoBehaviour{

    private GameObject hand;
    private bool isHandVis;
    private bool isHandMoving = false;

    private float startY;
    private float endY;

    public float upY = 135.5f; // Y coordinate when hand is visible
    public float downY = -210; // Y coordinate when hand is hidden

    public bool IsHandVis { get { return isHandVis; } }

    private bool waiting = false;
    // Use this for initialization
    void Start () {
        isHandVis = true;

        hand = transform.parent.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        MoveHand();
	}

    /// <summary>
    /// Checks if the click was using the right mouse button
    /// Slides the hand panel down or up depending on its current state
    /// </summary>
    public void MoveHand()
    {
        if(!Input.GetMouseButtonDown(1) || isHandMoving || waiting)
        {
            return;
        }
        isHandVis = !isHandVis;

        if(!isHandVis) DeselectAllCards();

        IEnumerator sh = SlideHand(isHandVis, -1f);
        StartCoroutine(sh);
    }

    public void ToggleHand(bool up, float time)
    {
        isHandVis = (time < 0 ? up : isHandVis);
        IEnumerator sh = SlideHand(up, time);
        StartCoroutine(sh);
    }

    public void RemoveHand(bool up, float time)
    {

    }
    /// <summary>
    /// Bumps the Card up above the others to emphasize selection
    /// </summary>
    IEnumerator SlideHand(bool slideUp, float waitToDoOpposite)
    {
        isHandMoving = true;

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
            
            currentTime += Time.deltaTime;
            float yPos = Mathf.Lerp(startY, endY, currentTime / maxTime);
            hand.transform.position = new Vector3(hand.transform.position.x, yPos, hand.transform.position.z);
            yield return null;
        }
        isHandMoving = false;

        if (waitToDoOpposite >= 0)
        {
            waiting = true;
            yield return new WaitForSeconds(waitToDoOpposite);
        }
        else yield break;

        if (!slideUp)
        {
            startY = downY;
            endY = upY;
        }
        else
        {
            startY = upY;
            endY = downY;
        }


        currentTime = 0f;
        // get the total angle we are traversing * degreesPerSecond
        // to get totalTime we need to Lerp
        maxTime = .5f;

        while (currentTime < maxTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            float yPos = Mathf.Lerp(startY, endY, currentTime / maxTime);
            hand.transform.position = new Vector3(hand.transform.position.x, yPos, hand.transform.position.z);
        }

        waiting = false;
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
            hand.transform.GetChild(i).transform.localPosition = new Vector3(hand.transform.GetChild(i).transform.localPosition.x, hand.transform.GetChild(i).GetComponent<Card_UI>().inactiveY, hand.transform.GetChild(i).transform.localPosition.z);
        }
    }
}
