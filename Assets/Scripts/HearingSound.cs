using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HearingSound : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Voice"))
        {
            Debug.Log("I think I heard something");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Voice"))
        {
            Debug.Log("I think I heard something");
        }
    }
}
