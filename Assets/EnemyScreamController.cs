using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScreamController : MonoBehaviour
{
    public GameObject targetObject; // Sesin hedef alaca?? obje
    public float maxVolumeDistance = 10f; // Maksimum ses seviyesine ula??lacak mesafe
    public float minVolumeDistance = 20f; // Sesin tamamen duyulmayaca?? mesafe

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (distance > minVolumeDistance)
        {
            audioSource.volume = 0f;
        }
        else if (distance < maxVolumeDistance)
        {
            audioSource.volume = 1f;
        }
        else
        {
            // Mesafe maxVolumeDistance ile minVolumeDistance aras?nda oldu?unda ses seviyesi do?rusal olarak azal?r
            float volume = 1f * (1 - ((distance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance)));
            audioSource.volume = volume;
        }
    }
}
