using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Explosion : MonoBehaviour {
    private float timer = 0f;
    public float growtime;
    public float maxSize;

    private bool growing = true;
    private Vector3 rotAngle;
    private float percent = 0f;
	// Use this for initialization
	void Start () {
        rotAngle = Random.onUnitSphere;
        IEnumerator co = Explode();
        StartCoroutine(co);
	}
	
    IEnumerator Explode()
    {
        while(timer < growtime)
        {
            percent = timer / growtime;
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(maxSize, maxSize, maxSize), percent);
            transform.Rotate(rotAngle, 2.0f);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;

        while (timer < growtime)
        {
            percent = timer / growtime;
            transform.localScale = Vector3.Lerp(new Vector3(maxSize, maxSize, maxSize), Vector3.zero, percent);
            transform.Rotate(rotAngle, 2.0f);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
