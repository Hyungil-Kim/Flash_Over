using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMaterial()
    {
        Texture2D texture = new Texture2D(128, 128, TextureFormat.ARGB32, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        var colorBuffer = new Color[10];
        var mate = GetComponent<Renderer>().material;
        texture.Apply();
        mate.SetTexture("_MainTex", texture);
    }

}
