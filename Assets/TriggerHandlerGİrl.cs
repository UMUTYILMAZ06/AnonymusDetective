using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandlerGİrl : MonoBehaviour
{
    private bool OnceScare = true;
    public JumpScareGirl jumpScareController; // İlgili kontrolcü script'i referans al

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (jumpScareController != null && OnceScare)
            {
                jumpScareController.Activate();
                OnceScare = false;
            }
        }
    }
}
