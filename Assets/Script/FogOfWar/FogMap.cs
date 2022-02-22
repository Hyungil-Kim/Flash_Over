using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMap
{
    //private List<GroundTile> map = new List<GroundTile>();
    private int mapLength;

    private Color[] colorBuffer;
    private Material blurMat;
        
    private Texture2D texBuffer;
    private RenderTexture blurBuffer; // 렌더텍스쳐 여러장 쓰는 이유 : 안개를 더 부드럽게 보이게 하기 위해
    private RenderTexture blurBuffer2;

    private RenderTexture curTexture;
    private RenderTexture lerpBuffer;
    private RenderTexture nextTexture;

    public Texture FogTexture => curTexture;

    //public FowManager FM => FowManager.I;
    //public FowManager.FogAlpha AlphaData => FowManager.I._fogAlpha;

    public void InitMap(float[,] heightMap)
    {
        //map.Clear();
        //map = AllTile.allTile;
        var tileSize = GameManager.instance.tilemap.size;
        var tileWidth = tileSize.x;
        var tileHeight = tileSize.y;
        mapLength = tileSize.x * tileSize.y;


        //visit = new float[mapLength];
        colorBuffer = new Color[mapLength];

        //for (int i = 0; i < mapLength; i++)
        //    visit[i] = AlphaData.never;

        blurMat = new Material(Shader.Find("FogOfWar/AverageBlur"));
        texBuffer = new Texture2D(tileWidth, tileHeight, TextureFormat.ARGB32, false);
        texBuffer.wrapMode = TextureWrapMode.Clamp;

        int width = (int)(tileWidth * 1.5f);
        int height = (int)(tileHeight * 1.5f);

        blurBuffer = RenderTexture.GetTemporary(width, height, 0);
        blurBuffer2 = RenderTexture.GetTemporary(width, height, 0);

        curTexture = RenderTexture.GetTemporary(width, height, 0);
        nextTexture = RenderTexture.GetTemporary(width, height, 0);
        lerpBuffer = RenderTexture.GetTemporary(width, height, 0);

    }
    public void LerpBlur()
    {
        // CurTexture  -> LerpBuffer
        // LerpBuffer  -> "_LastTex"
        // NextTexture -> FogTexture [Pass 1 : Lerp]

        Graphics.Blit(curTexture, lerpBuffer);
        blurMat.SetTexture("_LastTex", lerpBuffer);

        Graphics.Blit(nextTexture, curTexture, blurMat, 1);
    }
    public void ApplyFogAlpha()
    {
#if DETAILED_PROFILE
            Profiler.BeginSample("ApplyFogAlpha_CalculateColorBuffer");
#endif
        for (int i = 0; i < colorBuffer.Length/2; i++)
        {
            colorBuffer[i].a = 1f;
        }

#if DETAILED_PROFILE
            Profiler.EndSample();
            Profiler.BeginSample("ApplyFogAlpha_SetPixels");
#endif
        // ColorBuffer -> TexBuffer
        texBuffer.SetPixels(colorBuffer);
        texBuffer.Apply();

#if DETAILED_PROFILE
            Profiler.EndSample();
            Profiler.BeginSample("ApplyFogAlpha_Blits");
#endif
        // TexBuffer -> nextTexture

        // Pass 0 : Blur
        Graphics.Blit(texBuffer, blurBuffer, blurMat, 0);
        Graphics.Blit(blurBuffer, blurBuffer2, blurMat, 0);
        Graphics.Blit(blurBuffer2, blurBuffer, blurMat, 0);

        Graphics.Blit(blurBuffer, nextTexture);

#if DETAILED_PROFILE
            Profiler.EndSample();
#endif
    }

}
