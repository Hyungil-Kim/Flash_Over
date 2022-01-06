using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    public GameObject characterPrefab;
    public int characterIndex;
    private GameObject character;
    private void Start()
    {

    }
    public void Create()
    {
        if (GameData.userData.fireManList.ContainsKey(characterIndex))
        {
            character = Instantiate(characterPrefab, transform);
            character.GetComponent<Player>().cd = GameData.userData.fireManList[characterIndex];
        }
    }
}
