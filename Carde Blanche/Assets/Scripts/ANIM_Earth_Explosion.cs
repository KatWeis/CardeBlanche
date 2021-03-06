﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Earth_Explosion : MonoBehaviour {
    bool growing = true;
    float maxSize;
    float timer = 0f;
    float scaleInitial;
    float timeStep;
    Vector3 angle;
    LensFlare lf;
	// Use this for initialization
	void Start () {
        angle = Random.onUnitSphere;
        scaleInitial = Random.Range(0f, 3f);
        transform.localScale = new Vector3(scaleInitial, scaleInitial, scaleInitial);
        maxSize = scaleInitial + 5f;
        timeStep = 3f - scaleInitial;
        lf = gameObject.GetComponent<LensFlare>();
	}
	
	// Update is called once per frame
	void Update () {
		if(growing)
        {
            float size = Mathf.Lerp(scaleInitial, maxSize, timer / timeStep);
            transform.localScale = new Vector3(size, size, size);
            lf.brightness = Mathf.Lerp(0.5f, 2f, timer / timeStep);
            transform.Rotate(angle, 2.0f);
            if(timer / timeStep > 1)
            {
                growing = false;
                timer = 0f;
            }
        }
        else
        {
            float size = Mathf.Lerp(maxSize, 0, timer / 3f);
            transform.localScale = new Vector3(size, size, size);
            transform.Rotate(angle, 2.0f);
            lf.brightness = Mathf.Lerp(2f, 0f, timer / timeStep);
            if (timer > 3f)
            {
                Destroy(gameObject);
            }
        }
        timer += Time.deltaTime;
	}
}
