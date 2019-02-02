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
        text = new RenderTexture((int)player.clip.width, (int)player.clip.height, 0);

        player.targetTexture = text;
        image.texture = text;
    }
}
