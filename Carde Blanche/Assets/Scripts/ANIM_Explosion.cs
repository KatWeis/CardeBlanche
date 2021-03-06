﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Explosion : MonoBehaviour {
    private float timer = 0f;
    public float growtime;
    public float maxSize;

    private bool growing = true;
    private Vector3 rotAngle;
    private float percent = 0f;
    private LensFlare lf;
	// Use this for initialization
	void Start () {
        rotAngle = Random.onUnitSphere;
        lf = gameObject.GetComponent<LensFlare>();
        IEnumerator co = Explode();
        StartCoroutine(co);
	}
	
    IEnumerator Explode()
    {
        while(timer < growtime)
        {
            percent = timer / growtime;
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(maxSize, maxSize, maxSize), percent);
            lf.brightness = Mathf.Lerp(0f, 0.5f, percent);
            transform.Rotate(rotAngle, 2.0f);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;

        while (timer < growtime)
        {
            percent = timer / growtime;
            transform.localScale = Vector3.Lerp(new Vector3(maxSize, maxSize, maxSize), Vector3.zero, percent);
            lf.brightness = Mathf.Lerp(0.5f, 0f, percent);
            transform.Rotate(rotAngle, 2.0f);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
