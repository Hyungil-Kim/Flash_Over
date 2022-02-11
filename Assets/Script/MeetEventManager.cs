using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MeetEventManager : MonoBehaviour
{
    public UIManager uIManager;
    public Image charImage;
    public TextMeshProUGUI mainText;
    public Button leftButton;
    public Button rightButton;
    public int value1 = 1;
    public int value2 = 2;
    public int returnValue;
    public GameManager gameManager;
	public void Start()
	{
      gameManager = GameManager.instance;
	}
	public void OnClickButton1()
	{
        gameManager.tilemapManager.SaveClaimant.SetState(ClaimantState.Idle);
        gameManager.tilemapManager.SaveClaimant.num = value1;
        gameManager.tilemapManager.SaveClaimant.eventOn = true;
        gameManager.tilemapManager.SaveClaimant.targetPlayer = GameManager.instance.targetPlayer;
    }
    public void OnClickButton2()
    {
        gameManager.tilemapManager.SaveClaimant.SetState(ClaimantState.Idle);
        gameManager.tilemapManager.SaveClaimant.num = value2;
        gameManager.tilemapManager.SaveClaimant.eventOn = true;
        gameManager.tilemapManager.SaveClaimant.targetPlayer = GameManager.instance.targetPlayer;
    }
 
}
