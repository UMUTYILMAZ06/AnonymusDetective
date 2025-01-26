using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandlerInnerVoice4 : MonoBehaviour
{
    private bool OnceTime = true;
    
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && OnceTime)
        {
            audioSource.Play();
            OnceTime = false;
        }
    }
}
