using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayCharacterInfo : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI chaName;
    public TextMeshProUGUI chaStat;
    private void Update()
    {
        if(GameManager.instance.targetPlayer != null)
        Init();
    }
    public void Init()
    {
        var cd = GameManager.instance.targetPlayer.cd;
        //icon.sprite
        chaName.text = $"{cd.characterName}";
        string tired = "����";
        if (cd.tiredScore < 30)
        {
            tired = "������";
        }
        else if (cd.tiredScore < 70)
        {
            tired = "�ǰ���";
        }
        else if (cd.tiredScore < 100)
        {
            tired = "�ְ�";
        }
        chaStat.text = $"ü�� : {cd.hp} ������ : {cd.totalStats.dmg}\n�̵� : {cd.totalStats.move} �Ƿ� : {tired}";
    }
}
    