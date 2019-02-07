using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

    private int currentInstruct;

	// Use this for initialization
	void Start () {
        currentInstruct = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            currentInstruct++;
            transform.GetChild(currentInstruct - 1).gameObject.SetActive(false);

            if (currentInstruct >= transform.childCount)
            {
                gameObject.SetActive(false);
                return;
            }
            
            transform.GetChild(currentInstruct).gameObject.SetActive(true);
            
        }
    }
}
