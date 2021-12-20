using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour
{
    public int maxChaList;
    public GameObject shopChaPrefab;
    public GameObject content;
    private int currentPoint;
    List<GameObject> shopChaList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maxChaList; i++)
        {
            var index = i;
            var shopCha = Instantiate(shopChaPrefab, content.transform);
            var button = shopCha.GetComponent<Button>();
            button.onClick.AddListener(()=>OnChaButton(index));
            shopChaList.Add(shopCha);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShopListUpdate();
        }
    }
    public void ShopListUpdate()
    {
        GameData.userData.shopChaList.Clear();
        for (int i = 0; i < maxChaList; i++)
        {
            CharacterData cd = new CharacterData();
            cd.SetCharacter();
            var prefab = shopChaList[i].GetComponent<ShopChaPrefab>();
            prefab.SetValue(cd);
            GameData.userData.shopChaList.Add(cd);
        }
    }

    public void OnChaButton(int index)
    {
        currentPoint = index;
    }
    public void OnHireButton()
    {
        GameData.userData.characterList.Add(GameData.userData.shopChaList[currentPoint]);

        //test
        GameData.userData.SaveUserData(1);
    }
}
