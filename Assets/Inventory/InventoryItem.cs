using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    //캐릭터선택창 연결
    public GameObject CharacterSelectPanal;

    public GameObject sellItemPanel;

    //장착버튼을 누르면 캐릭터선택창 열림
    public void OnEquipButton()
    {
        CharacterSelectPanal.SetActive(true);
    }
    public void OnSellButton()
    {
        sellItemPanel.SetActive(true);
    }
}
