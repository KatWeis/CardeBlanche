using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Asteroid : MonoBehaviour {
    Vector3 dir;
    Vector3 angle;
    float speed;
	// Use this for initialization
	void Start () {
        dir = Random.onUnitSphere;
        angle = Random.onUnitSphere;
        speed = Random.Range(0.5f, 1f);
        float r = Random.Range(0.3f, 1.3f);
        transform.localScale = new Vector3(r, r, r);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += dir * speed * Time.deltaTime;
        transform.Rotate(angle, speed);
	}
}
