using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

    private Vector3 offset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.2f;

    [Range(0.01f, 1.0f)]
    public float idleOrbitSpeed = 0.15f;

    [Range(0.5f, 5.0f)]
    public float orbitSpeed = 3f;


    // Use this for initialization
    void Start () {
        offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(!Input.GetMouseButton(0))
            offset = IdleOrbit() * offset;
        if(Input.GetMouseButton(0))
            offset = MouseOrbit() * offset;

        transform.position = Vector3.Slerp(transform.position, offset, smoothFactor);

        transform.LookAt(Vector3.zero);
	}

    public Quaternion IdleOrbit()
    {
        Quaternion turnAngle = Quaternion.AngleAxis(idleOrbitSpeed, Vector3.up);
        return turnAngle;
    }

    public Quaternion MouseOrbit()
    {
        Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * orbitSpeed, Vector3.up);
        return turnAngle;
    }
}
