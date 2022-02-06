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

        chaName.text = $"이름 : {cd.characterName}";
        tired.text = $"피로도 : {cd.tiredScore}";

        characteristic.text = $"특성 : ";


    }
}
