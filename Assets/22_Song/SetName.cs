using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SetName : MonoBehaviour
{
    public TextMeshProUGUI setName;
    public void OnNext()
    {
        GameData.userData.userName = setName.text;
        if(GameData.userData.userName == "")
        {
            GameData.userData.userName = "¼Ò¹æ´ë";
        }
        SceneManager.LoadScene("MainScene");
    }
}
