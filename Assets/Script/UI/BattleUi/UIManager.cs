using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public BattleUiManager battleUiManager;
    public MeetEventManager meetEventManager;
    public Button startButton;
    public Image icon;
    public UpperUIManager upperUIManager;
    public MenuManager menuManager;
    public InfoUiScript InfoUiScript;
    public BetweenPlaying betweenPlaying;
    public GameObject option;
    public CheckUi checkUI;
    public AudioController audio;
    public bool restart;
    public bool title;

    public GameClearUI gameclearUI;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
		if (!gameManager.isStart)
		{
            var offset = new Vector3(0, 6, -1.5f);
            var pos = Turn.players[0].transform.position + offset;
            gameManager.mousePos = pos;
            gameManager.prevPos = pos;
        }
        gameManager.isStart = true;
        
        gameManager.Init();
        startButton.gameObject.SetActive(false);
        icon.gameObject.SetActive(false);
        audio.ChangeAudioClip(1);
        betweenPlaying.ShowStartPanel();
        
    }
 
    public void OnCharacterIcon()
    {
        icon.gameObject.SetActive(true);
    }
    public void OffCharacterIcon()
    {
        icon.gameObject.SetActive(false);
    }
    public void OnCharacterInfo()
    {
        InfoUiScript.ChangeInfoUi(3);
    }
    public void OffCharacterInfo()
    {
        InfoUiScript.smallInfo.gameObject.SetActive(false);
    }
}
