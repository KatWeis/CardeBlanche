using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EventManager : MonoBehaviour {
    // references to objects to spawn
    public GameObject deathStar;
    public GameObject forestPrefab;
    public GameObject solarFlare;
    public CanvasGroup SkyrimVideoGroup;
    public CanvasGroup AllCanvas;
    public VideoPlayer video;
    public GameObject lazers;

    public GameObject mario;

    public GameObject explosion;



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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Earthquake();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Supernova();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Lazers();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            StarMario();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            WorldWar3();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeathRay();
        }
    }

    public void DeathStar()
    {
        Instantiate(deathStar, new Vector3(12f, 0, 0), Quaternion.identity);
    }

    public void DeathRay()
    {
        GameObject death = GameObject.FindGameObjectWithTag("DeathStar");
        if(death != null)
        {
            death.transform.GetChild(0).gameObject.SetActive(true);
            death.transform.GetChild(0).GetComponent<FlareEffect>().Flare(0.75f, 2f, 1f, 0.75f, 1f, 0f);
            death.GetComponent<IdleOrbit>().enabled = false;
            death.transform.GetChild(1).gameObject.SetActive(true);
        }
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

    public void Earthquake()
    {
        GameObject.Find("CamParent").GetComponent<ScreenShake>().ShakeCamera(30f, 5f);
    }

    public void Supernova()
    {
        // disable the camera script and enable the cutscene camera
        GameObject cam = GameObject.Find("Main Camera");
        cam.GetComponent<OrbitCamera>().enabled = false;
        cam.GetComponent<ANIM_Cam_Supernova>().enabled = true;
        GameObject sun = GameObject.Find("Sun");
        sun.GetComponent<IdleOrbit>().enabled = false;
        sun.GetComponentInChildren<FlareEffect>().Flare(150f, 6f, 5f, 0f, 0f);
        GameObject.Find("Earth").GetComponent<SphereCollider>().enabled = false;
        GameObject.Find("CamParent").GetComponent<ScreenShake>().ShakeCamera(5f, 10f);
        //IEnumerator fade = FadeCanvas(2f);
        //StartCoroutine(fade);
        IEnumerator show = ShowVideo(6f, 7f);
        StartCoroutine(show);
    }

    public void Lazers()
    {
        Instantiate(lazers, Vector3.zero, Quaternion.identity);
    }
    public void StarMario()
    {
        Instantiate(mario, Vector3.zero, Quaternion.identity);
    }

    public void WorldWar3()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
        IEnumerator co = Explosions(spawns);
        StartCoroutine(co);
    }

    IEnumerator Explosions(GameObject[] spawn)
    {
        while(true)
        {
            int rand = Random.Range(0, spawn.Length);
            Instantiate(explosion, spawn[rand].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator FadeCanvas(float time)
    {
        float timer = 0f;
        float amount = 0f;
        
        while (timer < time)
        {
            amount = timer / time;
            AllCanvas.alpha = Mathf.Lerp(1f, 0f, amount);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ShowVideo(float waitTime, float fadeTime)
    {
        yield return new WaitForSeconds(waitTime);
        SkyrimVideoGroup.alpha = 1;

        Image i = GameObject.Find("SkyrimOverlay").GetComponent<Image>();
        float timer = 0f;
        float percent = 0f;
        video.Play();
        while(timer < fadeTime)
        {
            percent = timer / fadeTime;
            i.color = new Color(255f, 255f, 255f, Mathf.Lerp(1f, 0f, percent));
            timer += Time.deltaTime;
            yield return null;
        }
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
