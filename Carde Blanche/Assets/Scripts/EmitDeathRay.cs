using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitDeathRay : MonoBehaviour {
    private LineRenderer l;
    private float timer = 0f;
    private bool run = false;
    public GameObject explosions;
    public GameObject cam;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        l = gameObject.GetComponent<LineRenderer>();
        Physics.Raycast(transform.position, -transform.position.normalized, out hit, 100f);
        cam.GetComponent<DeathCam>().SetLook(hit.point);
	}
	
	// Update is called once per frame
	void Update () {
        if(run)
        {
            l.SetPosition(1, transform.position);
            l.widthMultiplier = Random.Range(0.7f, 1.3f);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 4f)
            {
                run = true;
                GameObject g = Instantiate(explosions, hit.point, Quaternion.identity);
                g.transform.LookAt(transform.position);
            }
            if(timer > 2f)
            {
                l.widthMultiplier = Mathf.Lerp(0f, 1f, (timer - 2f) / 2f);
                l.SetPosition(1, transform.position);
                l.SetPosition(0, Vector3.Lerp(transform.position, hit.point, (timer - 2f) / 2f));
            }
        }
	}
}
