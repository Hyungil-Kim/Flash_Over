using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingUpgradePrefab : MonoBehaviour
{
    public int index;
    public Button button;
    public Text trainingName;
    public Text trainingDesc;
    public Text trainingUpgrade;
    public void Init()
    {
        var upgradeIndex = (TrainingRoomUpgrade)index;
        switch (upgradeIndex)
        {
            case TrainingRoomUpgrade.Str:
                trainingUpgrade.text = $"���׷��̵�\n��� : 1000";
                if (GameData.userData.traingShopData.str >= 2)
                {
                    button.interactable = false;
                    trainingUpgrade.text = $"�Ϸ�";
                }

                break;
            case TrainingRoomUpgrade.Lung:
                trainingUpgrade.text = $"���׷��̵�\n��� : 600";
                break;
            case TrainingRoomUpgrade.Hp:
                trainingUpgrade.text = $"���׷��̵�\n��� : 500";
                break;
            default:
                break;
        }

    }
}
