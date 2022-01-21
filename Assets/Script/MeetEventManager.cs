using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MeetEventManager : MonoBehaviour
{
    public UIManager uIManager;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI mainText;
    public Button leftButton;
    public Button rightButton;
    public int value1 = 0;
    public int value2 = 1;
    public int returnValue;
    public void OnClickButton1()
	{
        GameManager.instance.tilemapManager.SaveClaimant.SetState(ClaimantState.Idle);
        GameManager.instance.tilemapManager.SaveClaimant.num = value1;
        GameManager.instance.tilemapManager.SaveClaimant.targetPlayer = GameManager.instance.targetPlayer;
    }
    public void OnClickButton2()
    {
        GameManager.instance.tilemapManager.SaveClaimant.SetState(ClaimantState.Idle);
        GameManager.instance.tilemapManager.SaveClaimant.num = value2;
        GameManager.instance.tilemapManager.SaveClaimant.targetPlayer = GameManager.instance.targetPlayer;
    }
}
