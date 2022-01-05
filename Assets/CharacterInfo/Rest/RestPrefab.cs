using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestPrefab : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    
    public void init(CharacterData cd, bool isActive, int index)
    {
        var button = icon.GetComponent<Button>();
        button.onClick.AddListener(() => OnClick(index));
        if (!isActive)
        {
            button.interactable = false;
            icon.sprite = null;
            text.text = "";
        }
        else
        {
            if(cd != null)
            {
                button.interactable = false;
                //icon.sprite
                text.text = $"{cd.characterName}��(��)\n{cd.restCount}�� �޽����Դϴ�";
            }
            else
            {
                button.interactable = true;
                text.text = $"�޽��� �����\n������ �ּ���.";
            }
        }
    }
    public void OnClick(int index)
    {
        var rest = GetComponentInParent<Rest>();
        rest.CurIndex = index;
        rest.OnClickRestRoom();
    }
}
