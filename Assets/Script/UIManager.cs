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
    public PlayCharacterInfo info;
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
        gameManager.isStart = true;
        gameManager.Init();
        startButton.gameObject.SetActive(false);
        icon.gameObject.SetActive(false);
        info.gameObject.SetActive(false);
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
        info.gameObject.SetActive(true);
    }
    public void OffCharacterInfo()
    {
        info.gameObject.SetActive(false);
    }
}
