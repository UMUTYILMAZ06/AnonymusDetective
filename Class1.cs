using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RunningChecker : MonoBehaviour
{
    public ContinuousMoveProviderBase moveProvider; // Continuous Move Provider script referansı
    public InputActionReference increaseSpeedAction; // InputActionReference referansı
    public float speedIncreaseAmount = 7f; // Artırmak istediğiniz hız miktarı
    private float originalMoveSpeed; // Orijinal hız değeri
    public bool isSpeedIncreased = false; // Hızın artırılıp artırılmadığını kontrol eden bayrak
    private Vector3 previousPosition;
    [SerializeField] private AudioSource audioSource;
    private float stopDelay = 2.0f; // Sesin durdurulması için bekleme süresi
    private Coroutine checkPositionCoroutine;

    private void Awake()
    {
        if (moveProvider != null)
        {
            originalMoveSpeed = moveProvider.moveSpeed; // Orijinal hızı sakla
        }

        // InputAction'ı etkinleştir
        increaseSpeedAction.action.Enable();
        increaseSpeedAction.action.performed += OnIncreaseSpeedAction;
    }

    private void OnDestroy()
    {
        // InputAction'ı devre dışı bırak
        increaseSpeedAction.action.Disable();
        increaseSpeedAction.action.performed -= OnIncreaseSpeedAction;
    }

    private void OnIncreaseSpeedAction(InputAction.CallbackContext context)
    {
        if (moveProvider != null)
        {
            if (isSpeedIncreased)
            {
                // Hız artırıldıysa, eski haline döndür
                moveProvider.moveSpeed = originalMoveSpeed;
                if (checkPositionCoroutine != null)
                {
                    StopCoroutine(checkPositionCoroutine);
                }
            }
            else
            {
                // Hız artırılmadıysa, artır
                moveProvider.moveSpeed = speedIncreaseAmount;
                checkPositionCoroutine = StartCoroutine(CheckPositionCoroutine());
            }
            // Hız artırılıp artırılmadığını değiştir
            isSpeedIncreased = !isSpeedIncreased;
        }
        else
        {
            Debug.LogWarning("MoveProvider script is not assigned.");
        }
    }

    private IEnumerator CheckPositionCoroutine()
    {
        while (true)
        {
            Vector3 startPosition = transform.position;
            yield return new WaitForSeconds(2f);
            Vector3 endPosition = transform.position;

            if (isSpeedIncreased && startPosition == endPosition)
            {
                audioSource.Stop();
            }
        }
    }
}
