using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacter : MonoBehaviour
{
    public RenderTexture rt;
    public RenderTexture icon;
    public Camera iconCamera;
    private void Start()
    {
    }
    public void Init(int index)
    {
        icon = Resources.Load<RenderTexture>($"Icon/icon {index}");
        
        iconCamera.targetTexture = icon;
    }
}
