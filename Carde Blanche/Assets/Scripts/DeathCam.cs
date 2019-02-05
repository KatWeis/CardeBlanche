using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCam : MonoBehaviour {
    private float timer = 0f;
    private bool run = false;
    private Vector3 lookAt;
    private Quaternion initial;
    // Use this for initialization
    void Start () {
        initial = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > 2f)
        {
            run = true;
        }
        if(run)
        {
            transform.rotation = Quaternion.Slerp(initial, Quaternion.LookRotation(lookAt - transform.position), timer - 2f);
            if(timer > 3f)
            {
                Destroy(this);
            }
        }
	}

    public void SetLook(Vector3 target)
    {
        lookAt = target;
    }
}
