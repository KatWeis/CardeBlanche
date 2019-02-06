using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Cam_Supernova : MonoBehaviour {
    public Transform target;
    public float timeToLook;
    Quaternion init;
    private float percent = 0f;
    private float timer = 0f;
	// Use this for initialization
	void Start () {
        init = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if(timer < timeToLook)
        {
            percent = timer / timeToLook;
            transform.rotation = Quaternion.Slerp(init, Quaternion.LookRotation(target.position - transform.position), percent);
            timer += Time.deltaTime;
        }
    }
}
