using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    //ĳ���ͼ���â ����
    public GameObject CharacterSelectPanal;

    public GameObject sellItemPanel;

    //������ư�� ������ ĳ���ͼ���â ����
    public void OnEquipButton()
    {
        CharacterSelectPanal.SetActive(true);
    }
    public void OnSellButton()
    {
        sellItemPanel.SetActive(true);
    }
}
