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
                text.text = $"{cd.characterName}이(가)\n{cd.restCount}차 휴식중입니다";
            }
            else
            {
                button.interactable = true;
                text.text = $"휴식할 대원을\n선택해 주세요.";
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
