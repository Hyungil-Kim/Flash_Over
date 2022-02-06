using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EndInfo : MonoBehaviour
{
    public RawImage icon;
    public TextMeshProUGUI chaName;
    public TextMeshProUGUI tired;
    public TextMeshProUGUI characteristic;

    public void Init(CharacterData cd, int index)
    {

        icon.texture = Resources.Load<RenderTexture>($"Icon/icon {index}");

        chaName.text = $"�̸� : {cd.characterName}";
        tired.text = $"�Ƿε� : {cd.tiredScore}";

        characteristic.text = $"Ư�� : ";


    }
}
