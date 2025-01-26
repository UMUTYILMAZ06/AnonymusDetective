using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloserDoor : MonoBehaviour
{
    public Animator animator;
    public string boolName = "Open";
    private bool onceInTime = true;
    // Start is called before the first frame update
    public void ToggleDoorOpen()
    {
        bool isOpen = animator.GetBool(boolName);
        animator.SetBool(boolName, !isOpen);
    }

    // When another object enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // You can add a tag check or other conditions here if needed
        if (other.CompareTag("Player") && onceInTime) // Example condition, change "Player" to the tag of your triggering object
        {
            ToggleDoorOpen();
            onceInTime = false;
        }
    }
}
