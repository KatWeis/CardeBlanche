using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMario : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        IEnumerator l = changeColor();
        StartCoroutine(l);
    }

    IEnumerator changeColor()
    {
        while (true)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", CreateColor());
            yield return new WaitForSeconds(0.07f);
        }
    }

    Color CreateColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
