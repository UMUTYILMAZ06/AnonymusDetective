using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingingEnd : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource bile?enini al
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            // D�ng� modunu etkinle?tir
            audioSource.loop = true;

            // Sesi �almaya ba?la
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource bileseni bulunamad?!");
        }
    }

    public void StopAudio()
    {
        if (audioSource != null)
        {
            // D�ng� modunu devre d??? b?rak ve sesi durdur
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
}
