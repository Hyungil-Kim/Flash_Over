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
            case TrainingRoomUpgrade.Basic:
                trainingUpgrade.text = $"���׷��̵�\n��� : 1000";
                if (GameData.userData.traingShopData.balence >= 2)
                {
                    button.interactable = false;
                    trainingUpgrade.text = $"�Ϸ�";
                }

                break;
            case TrainingRoomUpgrade.Balence:
                trainingUpgrade.text = $"���׷��̵�\n��� : 600";
                break;
            case TrainingRoomUpgrade.Random:
                trainingUpgrade.text = $"���׷��̵�\n��� : 500";
                break;
            default:
                break;
        }

    }
}
