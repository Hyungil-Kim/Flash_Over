using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPortrait : MonoBehaviour
{
    public RenderTexture icon;
    public Camera iconCamera;
    public int index;
    void Start()
    {
        icon = Resources.Load<RenderTexture>($"Icon/MainIcon");
        if(index >0)
        {
            icon = Resources.Load<RenderTexture>($"Icon/MainIcon {index}");
        }
        iconCamera.targetTexture = icon;
    }

}
