using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareEffect : MonoBehaviour {

    private float bright;
    private float grow;
    private float shrink;
    private float end;
    private float wait;
    private LensFlare f;

	// Use this for initialization
	void Start () {
        f = gameObject.GetComponent<LensFlare>();
	}
	
    public void Flare(float maxBrightness, float growTime, float shrinkTime, float endBrightness, float waitTime)
    {
        bright = maxBrightness;
        grow = growTime;
        shrink = shrinkTime;
        end = endBrightness;
        wait = waitTime;

        IEnumerator co = Effect();
        StartCoroutine(co);
    }

    IEnumerator Effect()
    {
        float growPercent = 0f;
        float timer = 0;
        while(f.brightness < bright)
        {
            growPercent = timer / grow;
            float res = Mathf.Lerp(f.brightness, bright, growPercent);
            timer += Time.deltaTime;
            f.brightness = res;

            yield return null;
        }

        yield return new WaitForSeconds(wait);

        float shrinkPercent = 0f;
        timer = 0f;
        while (f.brightness > end)
        {
            shrinkPercent = timer / shrink;
            float res = Mathf.Lerp(f.brightness, end, shrinkPercent);
            timer += Time.deltaTime;
            f.brightness = res;

            yield return null;
        }

        f.brightness = end;
    }
}
