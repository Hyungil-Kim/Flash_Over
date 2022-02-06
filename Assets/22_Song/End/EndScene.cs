using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public EndCharacter[] endCharacters;
    private void Start()
    {
        var count = 0;
        if (GameData.userData.fireManList != null)
        {
            foreach (var character in GameData.userData.fireManList)
            {
                endCharacters[count].Init(character.Value, count);
                count++;
            }
        }
    }
}
