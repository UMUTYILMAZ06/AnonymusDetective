using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hidding : MonoBehaviour
{
    public InputActionReference hideAction; // Primary buton için InputAction referans?
    public GameObject player; // Saklanacak oyuncu objesi
    public Transform hidePosition; // Saklanacak konum
    public Collider hideAreaCollider; // Saklanma bölgesinin Collider'?

    private Vector3 originalPosition; // Oyuncunun orijinal konumu
    private bool isHidden = false; // Oyuncunun saklanma durumunu takip eden bayrak
    private bool isInHideArea = false; // Oyuncunun saklanma bölgesinde olup olmad???n? takip eden bayrak

    private void OnEnable()
    {
        // InputAction'i etkinle?tir
        hideAction.action.Enable();
        hideAction.action.performed += OnHideAction;
    }

    private void OnDisable()
    {
        // InputAction'i devre d??? b?rak
        hideAction.action.Disable();
        hideAction.action.performed -= OnHideAction;
    }

    private void Start()
    {
        // Oyuncunun ba?lang?ç konumunu kaydet
        if (player != null)
        {
            originalPosition = player.transform.position;
        }
        else
        {
            Debug.LogWarning("Player GameObject is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == hideAreaCollider)
        {
            isInHideArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == hideAreaCollider)
        {
            isInHideArea = false;
        }
    }

    private void OnHideAction(InputAction.CallbackContext context)
    {
        if (player != null && isInHideArea)
        {
            if (isHidden)
            {
                // E?er sakl?ysa, orijinal konumuna geri dön
                player.transform.position = originalPosition;
                isHidden = false;
            }
            else
            {
                // E?er sakl? de?ilse, saklanma konumuna git
                player.transform.position = hidePosition.position;
                isHidden = true;
            }
            Debug.Log("Saklanma durumu de?i?ti: " + isHidden);
        }
    }
}
