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
        string tired = "ㅈㅈ";
        if (cd.tiredScore < 30)
        {
            tired = "멀쩡함";
        }
        else if (cd.tiredScore < 70)
        {
            tired = "피곤함";
        }
        else if (cd.tiredScore < 100)
        {
            tired = "주거";
        }
        chaStat.text = $"체력 : {cd.hp} 데미지 : {cd.totalStats.dmg}\n이동 : {cd.totalStats.move} 피로 : {tired}";
    }
}
    