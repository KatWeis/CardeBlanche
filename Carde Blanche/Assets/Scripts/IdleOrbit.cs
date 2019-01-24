using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleOrbit : MonoBehaviour
{

    // Vector for the new position of the camera
    private Vector3 offset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.2f;

    
    public float idleOrbitSpeed = -0.15f;


    // Use this for initialization
    void Start()
    {
        // set initial position
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        offset = Orbit() * offset;

        // slerp to new position
        transform.position = Vector3.Slerp(transform.position, offset, smoothFactor);

        // reset the camera to look at the earth
        transform.LookAt(Vector3.zero);
    }

    // Slowly rotate the camera
    public Quaternion Orbit()
    {
        Quaternion turnAngle = Quaternion.AngleAxis(idleOrbitSpeed, Vector3.up);
        return turnAngle;
    }
}
