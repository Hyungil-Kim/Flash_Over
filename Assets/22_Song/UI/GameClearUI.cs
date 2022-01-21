using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;
public class GameClearUI : MonoBehaviour
{
    public GameObject[] resultGo;

    public TextMeshProUGUI title;
    public TextMeshProUGUI saveperson;
    public TextMeshProUGUI playturn;
    public TextMeshProUGUI totalresult;
    public TextMeshProUGUI personality;
    public TextMeshProUGUI tired;
    
    private void OnEnable()
    {
        StopAllCoroutines();
        Init();  
        StartCoroutine(CoResultPrint());
    }
    public void Init()
    {
        title.text = $"STAGE{1} CLEAR!!";
        var resque = 0;
        foreach (var claimant in Turn.claimants)
        {
            if(claimant.exit)
            {
                resque++;
            }
        }
        saveperson.text = $"�θ� ���� {resque}/{Turn.claimants.Count} => {100}���";
        playturn.text = $"�Ҹ��� �� �� {GameManager.instance.turnCount} => {100}���";
        totalresult.text = $"�ջ� = {100 + 100}��� !!";
        
        var sba = new StringBuilder();
        //Dictionay<string, Personality.CharacterPersonality>;
        for (int i = 0; i < 10; i++)
        {
            sba.Append(string.Format($"��� test.key �� < test.value >�� ȹ���Ͽ����ϴ�.\n"));
        }
        personality.text = sba.ToString();
        var sbb = new StringBuilder();
        //Dictionay<string, >
        for (int i = 0; i < Turn.players.Count; i++)
        {
            if (Turn.players[i].cd.GetTired())
            {
                sbb.Append(string.Format($"��� {Turn.players[i].cd.characterName} �� {Turn.players[i].cd.tiredType}���°� �Ǿ����ϴ�.\n"));
            }
        }
        tired.text = sbb.ToString();
        foreach (var result in resultGo)
        {
            result.SetActive(false);
        }
        StartCoroutine(CoResultPrint());
    }
    IEnumerator CoResultPrint()
    {
        for (int i = 0; i < resultGo.Length; i++)
        {
            resultGo[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
    public void SkipResult(InputAction.CallbackContext asd)
    {
        switch (asd.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                switch (asd.interaction)
                {
                    case MultiTapInteraction:
                        StopCoroutine(CoResultPrint());
                        for (int i = 0; i < resultGo.Length; i++)
                        {
                            resultGo[i].SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }

        //StopCoroutine(CoResultPrint());
        //for (int i = 0; i < resultGo.Length; i++)
        //{
        //    resultGo[i].SetActive(true);
        //}
    }
}
