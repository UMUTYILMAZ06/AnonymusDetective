using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public List<GameObject> breakablePieces;
    public AudioClip breakSound;
    public AudioSource audioSource;

    // Olay tan?mlamas?
    public delegate void StoneBreakHandler(Vector3 breakPosition);
    public static event StoneBreakHandler OnStoneBreak;

    void Start()
    {
        foreach (var item in breakablePieces)
        {
            item.SetActive(false);
        }
        audioSource.playOnAwake = false;
        audioSource.clip = breakSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the stone has collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Call the Break function
            Break();
        }
    }

    public void Break()
    {
        foreach (var item in breakablePieces)
        {
            item.SetActive(true);
            item.transform.parent = null;
        }

        if (breakSound != null)
        {
            audioSource.Play();
        }

        // Ta? k?r?ld???nda olay tetiklenir
        OnStoneBreak?.Invoke(transform.position);

        // Deactivate the game object after a delay to allow the sound to play
        gameObject.SetActive(false);
    }
}
