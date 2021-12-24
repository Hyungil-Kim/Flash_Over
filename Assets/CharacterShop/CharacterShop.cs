using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour
{
    public HireInfo hireInfo;
    public int maxChaList;
    public GameObject shopChaPrefab;
    public GameObject content;
    private int currentPoint;
    List<GameObject> shopChaList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < maxChaList; i++)
        {
            var index = i;
            var shopCha = Instantiate(shopChaPrefab, content.transform);
            var button = shopCha.GetComponent<Button>();
            button.onClick.AddListener(()=>OnChaButton(index));
            shopCha.SetActive(false);
            shopChaList.Add(shopCha);
        }
        if (GameData.userData.shopChaList.Count == 0)
        {
            ShopListUpdate();
        }
        else
        {
            for (int i = 0; i < maxChaList; i++)
            {
                if (GameData.userData.shopChaList[i].isHire == false)
                {
                    SetList(GameData.userData.shopChaList[i], i);
                }
            }
        }
        hireInfo.Init(null);
    }
    private void OnEnable()
    {
        
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
            shopChaList[i].SetActive(true);
        }
    }

    public void SetList(CharacterData cd, int index)
    {
        var shopChaPrefab = shopChaList[index].GetComponent<ShopChaPrefab>();
        shopChaList[index].SetActive(true);
        shopChaPrefab.SetValue(cd);
    }

    public void OnChaButton(int index)
    {
        currentPoint = index;
        hireInfo.Init(GameData.userData.shopChaList[currentPoint]);
    }
    public void OnHireButton()
    {
        var cheak = GameData.userData.characterList.Count < GameData.userData.maxCharacter;
        if (!cheak)
        {
            return;
        }
        if (currentPoint < maxChaList)
        {
            shopChaList[currentPoint].SetActive(false);
            GameData.userData.characterList.Add(GameData.userData.shopChaList[currentPoint]);
            GameData.userData.shopChaList[currentPoint].isHire = true;
            currentPoint = maxChaList + 1;
            hireInfo.Init(null);
        }
        //test
        GameData.userData.SaveUserData(1);
    }

    public void OnTest()
    {
        ShopListUpdate();
    }
}
