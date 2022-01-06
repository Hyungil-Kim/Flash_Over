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
        startButton.gameObject.SetActive(false);
    }
}
