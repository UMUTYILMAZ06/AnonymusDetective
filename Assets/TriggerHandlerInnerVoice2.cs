using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandlerInnerVoice2 : MonoBehaviour
{
    private bool OnceTime = true;
    public InnerVoice2 InnerVoice2; // ?lgili kontrolcü script'i referans al

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (InnerVoice2 != null && OnceTime)
            {
                InnerVoice2.Talk();
                OnceTime = false;
            }
        }
    }
}
