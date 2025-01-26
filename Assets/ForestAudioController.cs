using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip externalAudioClip;
    [SerializeField] private AudioClip internalAudioClip;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Closer"))
        {
            if (audioSource.clip != internalAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = internalAudioClip;
                audioSource.volume = 0.5f;
                audioSource.Play();
            }
           
        }
        else if (other.CompareTag("Opener"))
        {
            if(audioSource.clip != externalAudioClip)
            {
                audioSource.Stop();
                audioSource.clip = externalAudioClip;
                audioSource.Play();
            }
           

        }
    }
}