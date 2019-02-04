using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEmitter : MonoBehaviour {
    public GameObject lazer;
    public float spawnRate;

	// Use this for initialization
	void Start () {
        IEnumerator l = spawnLazer();
        StartCoroutine(l);
	}
	
	IEnumerator spawnLazer()
    {
        while(true)
        {
            GameObject o = Instantiate(lazer, Vector3.zero, Quaternion.identity);
            o.GetComponent<Renderer>().material.SetColor("_Color", CreateColor());
            yield return new WaitForSeconds(spawnRate);
        }
    }

    Color CreateColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
