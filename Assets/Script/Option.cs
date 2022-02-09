using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
	public AudioMixer audioMixer;
	public Slider backGroundSlider;
	public Slider effectSlider;
	public Button helpButton;
	public Button creditButton;
	public Button questionButton;
	public Button cancleButton;

	private float backGroundVolume;
	private float effectVolume;

	public void Start()
	{
		backGroundVolume = backGroundSlider.value;
		effectVolume = effectSlider.value;
	}

	public void Update()
	{
		if (backGroundVolume == -50f) audioMixer.SetFloat("BGM", -80);
		else audioMixer.SetFloat("BGM", backGroundVolume);

		if (effectVolume == -50f) audioMixer.SetFloat("Effect", -80);
		else audioMixer.SetFloat("Effect", effectVolume);
	}

	public void OnClickEndOption()
	{
		gameObject.SetActive(false);
	}
}
