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
        Init();
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
            chaName.text = $"{cd.characterName} ��� : {cd.oxygen}";
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
        else
        {
            gameObject.SetActive(false);
        }
    }
}
    