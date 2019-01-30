using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_UI : MonoBehaviour {

    private Outline ol;
    public bool selected;
    private bool hovered;

    public float inactiveY = 135.0f;
    public float activeY = 210.0f;
    private float riseSpeed = 50.0f;

    private float percent;
    private float startTime;

    private Vector3 downCardPos;
    private Vector3 upCardPos;

    public int cardID;

    // Use this for initialization
    void Start () {
        ol = gameObject.GetComponent<Outline>();
        selected = false;
        hovered = false;

        upCardPos = new Vector3(transform.position.x, activeY, transform.position.z);
        downCardPos = new Vector3(transform.position.x, inactiveY, transform.position.z);
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
            GameObject.Find("GameManager").SendMessage("AddSelectedCard", cardID);
        }
        else
        {
            ol.effectColor = Color.black;
            ol.effectDistance = new Vector2(1, -1);
            GameObject.Find("GameManager").SendMessage("ClearSelectedCard");
        }
        
    }

    public void ClearHighlight()
    {
        ol.effectColor = Color.black;
        ol.effectDistance = new Vector2(1, -1);
        GameObject.Find("GameManager").SendMessage("ClearSelectedCard");
        StopHover();
    }

    /// <summary>
    /// Set hover to on
    /// </summary>
    public void StartHover()
    {
        if (selected) return;
        hovered = true;
        startTime = Time.time;
        HoverHighlight();
        StopCoroutine("BumpCard");
        StopCoroutine("UnBumpCard");
        StartCoroutine("BumpCard");
    }

    /// <summary>
    /// Set hover to off
    /// </summary>
    public void StopHover()
    {
        if (selected) return;
        hovered = false;
        startTime = Time.time;
        HoverHighlight();
        StopCoroutine("BumpCard");
        StopCoroutine("UnBumpCard");
        StartCoroutine("UnBumpCard");
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

    /// <summary>
    /// Bumps the Card up above the others to emphasize selection
    /// </summary>
    IEnumerator BumpCard()
    {

        float currentTime = 0f;
        // get the total angle we are traversing * degreesPerSecond
        // to get totalTime we need to Lerp
        float maxTime = .3f;

        while (currentTime < maxTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            float yPos = Mathf.Lerp(inactiveY, activeY, currentTime / maxTime);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }

    /// <summary>
    /// Bumps the Card up above the others to emphasize selection
    /// </summary>
    /// <returns></returns>
    IEnumerator UnBumpCard()
    {
        float currentTime = 0f;
        // get the total angle we are traversing * degreesPerSecond
        // to get totalTime we need to Lerp
        float maxTime = .3f;

        while (currentTime < maxTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            float yPos = Mathf.Lerp(activeY, inactiveY, currentTime / maxTime);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }
}
