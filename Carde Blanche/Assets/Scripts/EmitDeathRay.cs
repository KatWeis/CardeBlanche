using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitDeathRay : MonoBehaviour {
    private LineRenderer l;
    private Light pLight;
    private float timer = 0f;
    private bool run = false;
    private bool stop = false;
    private bool flare = false;
    public GameObject explosions;
    public GameObject earthExplosion;
    public GameObject ast;
    public GameObject cam;
    RaycastHit hit;
    GameObject g;
    // Use this for initialization
    void Start () {
        l = gameObject.GetComponent<LineRenderer>();
        pLight = gameObject.GetComponent<Light>();
        Physics.Raycast(transform.position, -transform.position.normalized, out hit, 100f);
        cam.GetComponent<DeathCam>().SetLook(hit.point);
        GameObject.Find("Earth").GetComponent<SphereCollider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > 10f)
        {
            Destroy(GameObject.Find("Earth"));
            GameObject[] props = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < 7; i++)
            {
                Instantiate(earthExplosion, Random.insideUnitSphere * 5f, Quaternion.identity);
                Instantiate(ast, Random.insideUnitSphere * 5f, Quaternion.identity);
            }
            for(int i = 0; i < props.Length; i++)
            {
                Destroy(props[i]);
            }
            props = GameObject.FindGameObjectsWithTag("Tree");
            for (int i = 0; i < props.Length; i++)
            {
                Destroy(props[i]);
            }
            Destroy(this);
        }
        if(timer > 7f && !flare)
        {
            g.GetComponent<FlareEffect>().Flare(11f, 3f, 0.25f, 0f, 0f, 0f);
            flare = true;
        }
        if(timer > 8f && !stop)
        {
            l.enabled = false;
            g.GetComponent<ParticleSystem>().Stop();
            stop = true;
        }
        if(timer > 6f && timer < 8f)
        {
            l.widthMultiplier = Random.Range(0.7f, 1.3f);
            l.SetPosition(1, Vector3.Lerp(transform.position, hit.point, (timer - 6f) / 2f));
        }
        // wait for death ray to reach earth
        if(timer > 4f && timer < 6f)
        {
            l.widthMultiplier = Random.Range(0.7f, 1.3f);
        }
        if (timer > 4f && !run)
        {
            g = Instantiate(explosions, hit.point, Quaternion.identity);
            g.transform.LookAt(transform.position);
            run = true;
        }
        // wait for lens flare to light up
        if(timer > 2f && timer < 4f)
        {
            l.widthMultiplier = Mathf.Lerp(Random.Range(0f, 0.3f), Random.Range(0.7f, 1f), (timer - 2f) / 2f);
            l.SetPosition(1, transform.position);
            l.SetPosition(0, Vector3.Lerp(transform.position, hit.point, (timer - 2f) / 2f));
        }
        if(timer < 2f)
        {
            pLight.intensity = Mathf.Lerp(0f, 2f, timer / 2f);
        }
	}
}
