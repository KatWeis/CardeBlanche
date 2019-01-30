using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScroll : MonoBehaviour {

    public float scrollSpeed = 10;

    private TextMeshProUGUI textMesh;
    private TextMeshProUGUI cloneTextObject;

    private RectTransform textRect;
    private string sourceText;
    private string tempText;
    

    private void Awake()
    {
        textMesh = gameObject.transform.GetComponentInChildren<TextMeshProUGUI>();
        textRect = textMesh.GetComponent<RectTransform>();

        cloneTextObject = Instantiate(textMesh) as TextMeshProUGUI;
        RectTransform cloneRect = cloneTextObject.GetComponent<RectTransform>();
        cloneRect.SetParent(textRect);
        cloneRect.anchorMin = new Vector2(1, textRect.anchorMin.y);
        cloneRect.position = new Vector3(cloneRect.position.x, textRect.position.y, cloneRect.position.z);
        cloneRect.localScale = new Vector3(1,1,1);

        StartCoroutine("Scroll");
    }

    // Use this for initialization
    IEnumerator Scroll()
    {
        float width = textMesh.preferredWidth;
        Vector3 startPos = textRect.position;

        float scrollPosition = 0;

        while(true)
        {
            // Re-compute the width of the RectTransform if the text object has changed
            if(textMesh.text != cloneTextObject.text)
            {
                width = textMesh.preferredWidth;
                cloneTextObject.text = textMesh.text;
            }

            // Scroll the text across the screen by moving the RectTransform
            textRect.position = new Vector3(-scrollPosition % width, startPos.y, startPos.z);

            scrollPosition += scrollSpeed * 20 * Time.deltaTime;

            yield return null;
        }
	}
}
