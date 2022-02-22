using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlreadyEquip : MonoBehaviour
{
    public Image chaIcon;
    public TextMeshProUGUI description;

    private CharacterData cd;
    private ItemDataBase id;
    private ItemType it;
    public delegate void isExit();
    public isExit exit;
    public void Init(CharacterData character, ItemDataBase itemData, ItemType itemType)
    {
        exit = null;
        cd = character;
        id = itemData;
        it = itemType;
        //chaIcon.sprite = character.
        description.text = $"'{cd.characterName}'��(��) �������Դϴ�.\n�����ϰ� �����Ͻðڽ��ϱ�?";
    }
    public void OnYes()
    {
        //�ӽ÷� ó�� ���߿� UI�߰��ؾ��ҵ� ?
        if (cd.weight > id.dataTable.weight)
        {
            cd.EquipItem(id, it);
        }
        if(exit != null)
        {
            exit();
        }
        gameObject.SetActive(false);
    }
    public void OnNo()
    {

        gameObject.SetActive(false);
    }
}
