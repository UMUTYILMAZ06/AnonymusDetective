using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.InputSystem;
public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private InputActionReference startConversationAction;

    private void Awake()
    {
        // InputAction'? etkinle?tir
        startConversationAction.action.Enable();
    }

    private void OnDestroy()
    {
        // InputAction'? devre d??? b?rak
        startConversationAction.action.Disable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            if (startConversationAction.action.triggered)
            {
                ConversationManager.Instance.StartConversation(myConversation);
            }
        }
    }
}
