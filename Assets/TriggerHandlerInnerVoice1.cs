using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandlerInnerVoice1 : MonoBehaviour
{
    private bool OnceTime = true;
    public InnerVoice1 InnerVoice1; // ?lgili kontrolcü script'i referans al
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (InnerVoice1 != null && OnceTime)
            {
                InnerVoice1.Talk();
                OnceTime = false;
            }
        }
    }
}
