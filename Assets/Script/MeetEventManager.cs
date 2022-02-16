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
    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;
    public Button leftButton;
    public Button rightButton;
    public int returnValue;
    private int value = 0;
    public GameManager gameManager;
    private EventData eventData;
	public void Start()
	{
      gameManager = GameManager.instance;
    }
	public void OnEnable()
	{
        eventData = MyDataTableMgr.eventTable.GetTable(value);
        mainText.text = eventData.text;
        button1Text.text = eventData.answer1;
        button2Text.text = eventData.answer2;
    }
	public void OnClickButton1()
	{
        gameManager.tilemapManager.SaveClaimant.SetState(ClaimantState.Idle);
        gameManager.tilemapManager.SaveClaimant.claimantState = eventData.answer1state;
        gameManager.tilemapManager.SaveClaimant.eventOn = true;
        gameManager.tilemapManager.SaveClaimant.targetPlayer = GameManager.instance.targetPlayer;
        value++;
    }
    public void OnClickButton2()
    {
        gameManager.tilemapManager.SaveClaimant.SetState(ClaimantState.Idle);
        gameManager.tilemapManager.SaveClaimant.claimantState = eventData.answer2state;
        gameManager.tilemapManager.SaveClaimant.eventOn = true;
        gameManager.tilemapManager.SaveClaimant.targetPlayer = GameManager.instance.targetPlayer;
        value++;
    }
 
}
