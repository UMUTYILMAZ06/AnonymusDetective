using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPushOpenDoor : MonoBehaviour
{
    public Animator animator;
    public string boolName = "Open";

    // Method to toggle the door's open/closed state
    public void ToggleDoorOpen()
    {
        bool isOpen = animator.GetBool(boolName);
        animator.SetBool(boolName, !isOpen);
    }

    // When another object enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // You can add a tag check or other conditions here if needed
        if (other.CompareTag("Card")) // Example condition, change "Player" to the tag of your triggering object
        {
            ToggleDoorOpen();
        }
    }

    // When another object exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        // You can add a tag check or other conditions here if needed
        if (other.CompareTag("Card")) // Example condition, change "Player" to the tag of your triggering object
        {
            ToggleDoorOpen();
        }
    }
}
