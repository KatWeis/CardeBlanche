using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitDeathRay : MonoBehaviour {
    private LineRenderer l;
	// Use this for initialization
	void Start () {
        l = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        l.SetPosition(1, transform.position);
	}
}
