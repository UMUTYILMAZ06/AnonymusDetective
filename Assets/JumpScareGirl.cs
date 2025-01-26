using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareGirl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void Activate()
    {
        // Objeyi aktif yap ve ses çal
        gameObject.SetActive(true);
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
            return;
        if (audioSource != null && !audioSource.isPlaying )
        {
            gameObject.SetActive(false);
        }
    }
}
