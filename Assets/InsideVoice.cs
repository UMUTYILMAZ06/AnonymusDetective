using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideVoice : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OpenerInside")) // Tag kontrol� yaparak sadece belirli bir objeye tepki verir
        {
            if (!audioSource.isPlaying)
            {
       

                audioSource.loop = true; // D�ng� modunu etkinle?tir
                audioSource.Play(); // Sesi ba?lat


            }
        }
    }

}
