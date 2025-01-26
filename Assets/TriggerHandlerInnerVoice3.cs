using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandlerInnerVoice3 : MonoBehaviour
{
    private bool OnceTime = true;
    public ManVoice ManVoice; // ?lgili kontrolcü script'i referans al

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ManVoice != null && OnceTime)
            {
                ManVoice.Talk();
                OnceTime = false;
            }
        }
    }
}
