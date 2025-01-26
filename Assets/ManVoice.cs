using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManVoice : MonoBehaviour
{
    [SerializeField] private AudioClip innerVoice2Clip;
    [SerializeField] private AudioSource audioSource;


    public void Talk()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {

            audioSource.clip = innerVoice2Clip;
            audioSource.Play();
        }
    }
}
