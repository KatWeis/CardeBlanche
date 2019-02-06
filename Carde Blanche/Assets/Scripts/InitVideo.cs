using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class InitVideo : MonoBehaviour {
    public VideoPlayer player;
    public RawImage image;

    
    RenderTexture text;

    void Start()
    {
        text = new RenderTexture((int)Screen.width, (int)Screen.height, 0);
        //image.SetNativeSize();
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        player.targetTexture = text;
        image.texture = text;
    }
}
