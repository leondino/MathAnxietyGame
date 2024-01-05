using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource singleSound1;
    [SerializeField]
    private AudioSource singleSound2;
    [Header("Sounds")]
    [SerializeField]
    private AudioClip correctSound;

    private void Awake()
    {

    }

    public void playCorrectSound()
    {
        PlaySound(correctSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (!singleSound1.isPlaying)
        {
            singleSound1.clip = clip;
            singleSound1.Play();
        }
        else
        {
            singleSound2.clip = clip;
            singleSound2.Play();
        }
    }
}
