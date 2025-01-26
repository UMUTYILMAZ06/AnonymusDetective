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
            // Döngü modunu etkinle?tir
            audioSource.loop = true;

            // Sesi çalmaya ba?la
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
            // Döngü modunu devre d??? b?rak ve sesi durdur
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
}
