using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice1 : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void Talk()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
