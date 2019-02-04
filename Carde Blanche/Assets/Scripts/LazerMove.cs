using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMove : MonoBehaviour {
    public float speed;
    public float lifetime;
    private Vector3 direction;
    private float timer = 0f;
	// Use this for initialization
	void Start () {
        direction = Random.onUnitSphere;
        transform.up = direction.normalized;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * Time.deltaTime * speed;
        timer += Time.deltaTime;

        if (timer > lifetime)
            Destroy(gameObject);
	}
}
