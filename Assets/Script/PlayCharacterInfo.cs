using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayCharacterInfo : MonoBehaviour
{

    public TextMeshProUGUI chaName;
    public TextMeshProUGUI chaStat;
    public ClaimantInfo claimantInfo;
    //private void Update()
    //{
    //    if(GameManager.instance.targetPlayer != null)
    //    Init();
    //}
    private void OnEnable()
    {
        //Init();
    }
    private void Update()
    {
        //Init();
    }
    public void Init()
    {
        CharacterData cd = null;
        if (!GameManager.instance.isStart)
        {
            if(GameManager.instance.changePlayer !=null)
            cd = GameManager.instance.changePlayer.cd;
        }
        else
        {
            if (GameManager.instance.targetPlayer != null)
            {
                cd = GameManager.instance.targetPlayer.cd;
                if (GameManager.instance.targetPlayer.handList.Count > 0)
                {
                    var claimant = GameManager.instance.targetPlayer.handList[0].GetComponent<Claimant>();
                    claimantInfo.gameObject.SetActive(true);
                    claimantInfo.Init(claimant);
                }
                else
                {
                    claimantInfo.gameObject.SetActive(false);
                }
            }
        }
        if (cd != null)
        {
            chaName.text = $"{cd.characterName} 산소 : {cd.oxygen}";
            string tired = "ㅈㅈ";
            switch (cd.tiredType)
            {
                case TiredType.Normal:
                    tired = "멀쩡함";
                    break;
                case TiredType.Tired:
                    tired = "피곤함";
                    break;
                case TiredType.BigTired:
                    tired = "빅피곤함";
                    break;
                default:
                    break;
            }
            chaStat.text = $"체력 : {cd.hp} 데미지 : {cd.totalStats.dmg}\n이동 : {cd.totalStats.move} 피로 : {tired}";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
    