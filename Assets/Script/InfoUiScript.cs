using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUiScript : MonoBehaviour
{
    public CharInfo charaterInfo;
    public TileInfo tileInfo;
    public ClaimantInfo2 claimantInfo;
    public CharacterInfoSmall smallInfo;

    public List<GameObject> infoList;
    /// <summary>
    /// 0.charaterInfo, 1.tileInfo, 2.claimantInfo, 3.smallInfo
    /// </summary>
    /// <param name="index"></param>
    public void ChangeInfoUi(int index)//
	{
        foreach(var elem in infoList)
		{
            if(elem.activeSelf)
			{
                elem.SetActive(false);
			}
		}
        infoList[index].SetActive(true);
	}
}
