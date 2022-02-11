using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioController : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip[] clips;
    private bool soundPlay;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        soundPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (soundPlay)
        {
            ChangeAudioClip(0);
        }
    }

    public void ChangeAudioClip(int number)
    {
        audio.clip = clips[number];
        audio.Play();
        audio.loop = true;
        soundPlay = false;
    }
}

