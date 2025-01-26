using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RunningChecker : MonoBehaviour
{
    public ContinuousMoveProviderBase moveProvider; // Continuous Move Provider script referans?
    public InputActionReference increaseSpeedAction; // InputActionReference referans?
    public float speedIncreaseAmount = 7f; // Art?rmak istedi?iniz h?z miktar?
    private float originalMoveSpeed; // Orijinal h?z de?eri
    public bool isSpeedIncreased = false; // H?z?n art?r?l?p art?r?lmad???n? kontrol eden bayrak
    private Vector3 previousPosition;
    [SerializeField] private Transform objectToTrack;

    [SerializeField] private AudioSource audioSource;
    private float checkInterval = 2.0f; // Pozisyon kontrolü için bekleme süresi
    private float checkTimer = 0f;
    public bool HasPositionChanged = false;
    private Coroutine checkPositionCoroutine;

    private void Awake()
    {
        if (moveProvider != null)
        {
            originalMoveSpeed = moveProvider.moveSpeed; // Orijinal h?z? sakla
        }

        // InputAction'? etkinle?tir
        increaseSpeedAction.action.Enable();
        increaseSpeedAction.action.performed += OnIncreaseSpeedAction;
    }

    private void OnDestroy()
    {
        // InputAction'? devre d??? b?rak
        increaseSpeedAction.action.Disable();
        increaseSpeedAction.action.performed -= OnIncreaseSpeedAction;
    }

    private void OnIncreaseSpeedAction(InputAction.CallbackContext context)
    {
        if (moveProvider != null)
        {
            if (isSpeedIncreased)
            {
                // H?z art?r?ld?ysa, eski haline döndür
                moveProvider.moveSpeed = originalMoveSpeed;
            
            }
            else
            {
                // H?z art?r?lmad?ysa, art?r
                moveProvider.moveSpeed = speedIncreaseAmount;
                
            }
            // H?z art?r?l?p art?r?lmad???n? de?i?tir
            isSpeedIncreased = !isSpeedIncreased;
        }
        else
        {
            Debug.LogWarning("MoveProvider script is not assigned.");
        }
    }

    private void Start()
    {
        previousPosition = objectToTrack.position;
    }

    private void Update()
    {
        checkPositionCoroutine = StartCoroutine(CheckPositionCoroutine());
        if (audioSource == null)
            return;
        if (isSpeedIncreased && HasPositionChanged && !audioSource.isPlaying)
            {
           
                audioSource.Play();
            }
            else if (audioSource.isPlaying && !HasPositionChanged || (audioSource.isPlaying && !isSpeedIncreased) )
            {
                audioSource.Stop();
            }
        
        
    }

    private IEnumerator CheckPositionCoroutine()
    {
        while (true)
        {
            Vector3 startPosition = objectToTrack.position;


            yield return new WaitForSeconds(0.2f);
            Vector3 endPosition = objectToTrack.position;

            if (endPosition == startPosition)
            {
                HasPositionChanged = false;
            }
            else
            {
                HasPositionChanged = true;
            }
            
        }
    }
}
