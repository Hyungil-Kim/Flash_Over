using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
	public GameObject soundPanel;
	public GameObject creditPanel;
	public GameObject qaPanel;
	public AudioMixer audioMixer;
	public Slider backGroundSlider;
	public Slider effectSlider;
	public Button helpButton;
	public Button creditButton;
	public Button questionButton;
	public Button cancleButton;

	private float backGroundVolume;
	private float effectVolume;

	public void OnEnable()
	{
		float bgmValue;
		float effectValue;
		bool effect = audioMixer.GetFloat("Effect", out effectValue);
		bool bgm = audioMixer.GetFloat("BGM", out bgmValue);
		if (bgm)
		{
			backGroundSlider.value = bgmValue;
		}
		else
		{
			backGroundSlider.value = 0f;
		}
		if (effect)
		{
			effectSlider.value = effectValue;
		}
		else
		{
			effectSlider.value = 0f;
		}
	}
	public void BGMControl()
	{
		if (backGroundSlider.value == -50f) audioMixer.SetFloat("BGM", -80);
		else audioMixer.SetFloat("BGM", backGroundSlider.value);
	}
	public void EffectControl()
	{
		if (effectSlider.value == -50f) audioMixer.SetFloat("Effect", -80);
		else audioMixer.SetFloat("Effect", effectSlider.value);
	}
	public void OnClickEndOption()
	{
		gameObject.SetActive(false);

	}
	public void OnclickCreditButton()
	{
		creditPanel.SetActive(true);
		soundPanel.SetActive(false);
	}
	public void OnclickCreditEnd()
	{
		creditPanel.SetActive(false);
		soundPanel.SetActive(true);
	}
	public void OnclickQAButton()
	{
		qaPanel.SetActive(true);
		soundPanel.SetActive(false);
	}
	public void OnclickQAEnd()
	{
		qaPanel.SetActive(false);
		soundPanel.SetActive(true);
	}
}
