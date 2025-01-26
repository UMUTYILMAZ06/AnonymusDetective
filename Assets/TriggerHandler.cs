using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public JumpScareScript jumpScareScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            if (jumpScareScript != null)
            {
               
                jumpScareScript.ActivateEnemy();
            }
        }
    }
}
