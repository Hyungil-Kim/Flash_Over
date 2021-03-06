using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingSlider : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;
    public Slider increaseSlider;
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
        var critical = 0.01f;
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
                text.text = $"??:{character.baseStats.str.stat}\n???? {character.baseStats.str.level}";
                statMaxExp = character.baseStats.str.statData.maxexp;
                statExp = character.baseStats.str.exp;
                slider.value = statExp / statMaxExp;
                break;
            case TrainingStatType.Lung:
                text.text = $"??:{character.baseStats.lung.stat}\n???? {character.baseStats.lung.level}";
                statMaxExp = character.baseStats.lung.statData.maxexp;
                statExp = character.baseStats.lung.exp;
                slider.value = statExp / statMaxExp;
                break;
            case TrainingStatType.Hp:
                text.text = $"ü??:{character.baseStats.hp.stat}\n???? {character.baseStats.hp.level}";
                statMaxExp = character.baseStats.hp.statData.maxexp;
                statExp = character.baseStats.hp.exp;
                slider.value = statExp / statMaxExp;
                break;
            default:
                break;
        }
        exp = critical > random ? Mathf.RoundToInt(exp + increaseExp * 1.5f) : exp + increaseExp;
        //exp += increaseExp;
        normalExp += increaseExp;

        var increaseNomalize = normalExp / statMaxExp;
        increaseNomalize = Mathf.Clamp(increaseNomalize, 0, 1 - (slider.value/* + oldIncreaseNomalize*/));
        increaseSlider.value = increaseNomalize + slider.value;
        //increase.rectTransform.sizeDelta = new Vector2(/*oldIncrease +*/ sliderWidth * increaseNomalize, increase.rectTransform.sizeDelta.y);
        if(normalExp + statExp >= statMaxExp)
        {
            levelUp.SetActive(true);
        }
    }
    public void Back()
    {
        exp = 0;
        normalExp = 0;
        increaseSlider.value = 0;
        //increase.rectTransform.sizeDelta = new Vector2(0, increase.rectTransform.sizeDelta.y);
        levelUp.SetActive(false);
    }
    public void SetExp(TrainingStatType type)
    {
        levelUp.SetActive(false);
        var character = GetComponentInParent<TrainingCharacter>().curCharacter;
        switch (type)
        {
            case TrainingStatType.Str:
                var stat = character.baseStats.str;
                stat.IncreaseExp(exp);
                //stat.exp += exp;
                //stat.CheakExp();
                break;
            case TrainingStatType.Lung:
                stat = character.baseStats.lung;
                stat.IncreaseExp(exp);
                //stat.exp += exp;
                //stat.CheakExp();
                break;
            case TrainingStatType.Hp:
                stat = character.baseStats.hp;
                stat.IncreaseExp(exp);
                //stat.exp += exp;
                //stat.CheakExp();
                break;
            default:
                break;
        }
        character.StatInit();
        exp = 0;
        normalExp = 0;
        Init(type);
    }

}
