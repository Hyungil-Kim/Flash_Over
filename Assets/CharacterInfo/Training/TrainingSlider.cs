using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSlider : MonoBehaviour
{
    public Slider slider;
    public RectTransform rectTransform; 
    public Image increase;
    public GameObject levelUp;
    private int exp = 0;
    private int normalExp = 0;
    private void Start()
    {
        Back();
    }
    public void Init(TrainingStatType type , int increaseExp = 0)
    {
        var character = GetComponentInParent<TrainingCharacter>().curCharacter;
        var sliderWidth = rectTransform.sizeDelta.x;
        float statMaxExp = 0;
        float statExp = 0;
        var oldIncrease = increase.rectTransform.sizeDelta.x;
        var oldIncreaseNomalize = increase.rectTransform.sizeDelta.x / sliderWidth;
        var critical = 0.5f;
        var random = Random.value;
        if (character == null)
        {
            slider.value = 0;
            increase.rectTransform.sizeDelta =
                new Vector2(0, increase.rectTransform.sizeDelta.y);
            return;
        }
        switch (type)
        {
            case TrainingStatType.Str:
                statMaxExp = character.totalStats.str.maxExp;
                statExp = character.totalStats.str.exp;
                slider.value = statExp / statMaxExp;
                break;
            case TrainingStatType.Lung:
                statMaxExp = character.totalStats.lung.maxExp;
                statExp = character.totalStats.lung.exp;
                slider.value = statExp / statMaxExp;
                break;
            case TrainingStatType.Hp:
                statMaxExp = character.totalStats.hp.maxExp;
                statExp = character.totalStats.hp.exp;
                slider.value = statExp / statMaxExp;
                break;
            default:
                break;
        }
        exp = critical < random ? Mathf.RoundToInt(exp + increaseExp * 1.5f) : exp + increaseExp;
        //exp += increaseExp;
        normalExp += increaseExp;

        var increaseNomalize = normalExp / statMaxExp;
        increaseNomalize = Mathf.Clamp(increaseNomalize, 0, 1 - (slider.value/* + oldIncreaseNomalize*/));
        increase.rectTransform.sizeDelta = new Vector2(/*oldIncrease +*/ sliderWidth * increaseNomalize, increase.rectTransform.sizeDelta.y);

        if(exp + statExp >= statMaxExp)
        {
            levelUp.SetActive(true);
        }
    }
    public void Back()
    {
        exp = 0;
        normalExp = 0;
        increase.rectTransform.sizeDelta = new Vector2(0, increase.rectTransform.sizeDelta.y);
        levelUp.SetActive(false);
    }
    public void SetExp(TrainingStatType type)
    {
        levelUp.SetActive(false);
        var character = GetComponentInParent<TrainingCharacter>().curCharacter;
        switch (type)
        {
            case TrainingStatType.Str:
                var stat = character.totalStats.str;
                stat.exp += exp;
                stat.CheakExp();
                break;
            case TrainingStatType.Lung:
                stat = character.totalStats.lung;
                stat.exp += exp;
                stat.CheakExp();
                break;
            case TrainingStatType.Hp:
                stat = character.totalStats.hp;
                stat.exp += exp;
                stat.CheakExp();
                break;
            default:
                break;
        }
        exp = 0;
        normalExp = 0;
        Init(type);
    }

}
