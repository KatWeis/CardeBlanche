using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCars : MonoBehaviour {

    float startTime;
    float growthTime;
    float rotateScalar;
    bool checkTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        growthTime = 0.5f;
        rotateScalar = 15f;
        checkTime = true;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * Time.deltaTime * rotateScalar);

        if(checkTime)
        {
            if (Time.time < startTime + growthTime)
            {
                transform.localScale = new Vector3(
                    0.85f + ((Time.time - startTime) / growthTime * 0.15f),
                    0.85f + ((Time.time - startTime) / growthTime * 0.15f),
                    0.85f + ((Time.time - startTime) / growthTime * 0.15f)
                    );
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                checkTime = false;
            }
        }
	}
}
