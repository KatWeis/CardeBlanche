using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    // references to objects to spawn
    public GameObject deathStar;
    public GameObject forestPrefab;
    public GameObject solarFlare;

    // vars used for different animations


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Reforestation();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeathStar();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SolarFlare();
        }
    }

    public void DeathStar()
    {
        Instantiate(deathStar, new Vector3(8, 0, 0), Quaternion.identity);
    }

    public void Reforestation()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Tree");
        IEnumerator makeForest = SpawnForests(objs);
        StartCoroutine(makeForest);
    }

    public void SolarFlare()
    {
        ParticleSystem p = solarFlare.GetComponent<ParticleSystem>();
        solarFlare.GetComponent<FlareEffect>().Flare(2.5f, 0.5f, 0.5f, 1f, 5f);
        p.Play();
        IEnumerator co = PlaySolarFlare(p);
        StartCoroutine(co);
    }

    IEnumerator PlaySolarFlare(ParticleSystem pSys)
    {
        yield return new WaitForSeconds(5f);

        pSys.Stop();
    }

    IEnumerator SpawnForests(GameObject[] trees)
    {
        float scale = 1f;

        while(scale > 0)
        {
            Vector3 currentScale = new Vector3(scale, scale, scale);
            foreach(GameObject tree in trees)
            {
                tree.transform.localScale = currentScale;
            }

            scale -= 0.025f;

            yield return null;
        }

        List<GameObject> forest = new List<GameObject>();

        foreach (GameObject tree in trees)
        {
            forest.Add((GameObject)Instantiate(forestPrefab, tree.transform.position, tree.transform.rotation));
            Destroy(tree);
        }

        scale = 0f;

        while(scale < 1.5)
        {
            Vector3 currentScale = new Vector3(scale, scale, scale);
            foreach (GameObject tree in forest)
            {
                tree.transform.localScale = currentScale;
            }

            scale += 0.05f;

            yield return null;
        }
    }
}
