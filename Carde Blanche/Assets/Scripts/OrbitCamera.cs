using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

    // Vector for the new position of the camera
    private Vector3 offset;
    
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.2f;

    [Range(0.01f, 1.0f)]
    public float idleOrbitSpeed = 0.15f;

    [Range(0.5f, 5.0f)]
    public float orbitSpeed = 3f;

    public float minZoom = 7f;
    public float maxZoom = 18f;
    public float zoomAmt = 2f;

    private bool autoOrbit = true;
    // Use this for initialization
    void Start () {
        // set initial position
        offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // only idle orbit when not holding left click
        if(!Input.GetMouseButton(0) && autoOrbit)
            offset = IdleOrbit() * offset;
        // if holding left click, rotate the cam
        if(Input.GetMouseButton(0))
            offset = MouseOrbit() * offset;

        if (Input.GetKeyDown(KeyCode.O)) autoOrbit = !autoOrbit;
        // check for zooming
        Zoom();

        // slerp to new position
        transform.position = Vector3.Slerp(transform.position, offset, smoothFactor);

        // reset the camera to look at the earth
        transform.LookAt(Vector3.zero);
	}

    // Slowly rotate the camera
    public Quaternion IdleOrbit()
    {
        Quaternion turnAngle = Quaternion.AngleAxis(idleOrbitSpeed, Vector3.up);
        return turnAngle;
    }

    // rotate the cam based on mouse swiping
    public Quaternion MouseOrbit()
    {
        Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * orbitSpeed, Vector3.up);
        turnAngle = turnAngle * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * -orbitSpeed, transform.right);
        return turnAngle;
    }

    // move the camera in and out based on scrollwheel
    public void Zoom()
    {
        Vector3 newPos = Input.GetAxis("Mouse ScrollWheel") * transform.forward * zoomAmt;
        offset = ClampMagnitude(newPos + offset, maxZoom, minZoom); 
    }

    // Helper method to clamp zoom between two values
    public static Vector3 ClampMagnitude(Vector3 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }
}
