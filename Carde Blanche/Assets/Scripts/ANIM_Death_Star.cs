using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Death_Star : MonoBehaviour {

    public float buildStep;
    public float fadeStep;
    public float maxBrightness;
    public float shakeAmt;
    public float shakeDur;
    
    private LensFlare lf;
    
    private bool building = true;
    private bool fading = false;
    private float brightness = 0f;
	// Use this for initialization
	void Start () {
        lf = gameObject.GetComponent<LensFlare>();
	}
	
	// Update is called once per frame
	void Update () {
        if (building)
        {
            if(brightness >= maxBrightness)
            {
                building = false;
                fading = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("CamParent").GetComponent<ScreenShake>().ShakeCamera(shakeAmt, shakeDur);
            }
            else
            {
                brightness += buildStep;
                lf.brightness = brightness;
            }
        }

        if (fading)
        {
            brightness -= fadeStep;

            if(brightness <= 0)
            {
                brightness = 0;
                fading = false;
                gameObject.GetComponent<IdleOrbit>().enabled = true;
            }

            lf.brightness = brightness;
        }


        if(!fading && !building)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            Destroy(this);
        }
	}
}
