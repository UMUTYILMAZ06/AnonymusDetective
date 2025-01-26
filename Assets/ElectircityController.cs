using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityController : MonoBehaviour
{
    public List<GameObject> targetObjects; // Layer'?n? de?i?tirmek istedi?in objelerin listesi
    public int newLayer; // De?i?tirmek istedi?in layer numaras?
    [SerializeField] private AudioClip GeneratorVoice;
    [SerializeField] private AudioSource audioSource;
    public GameObject particles;

    
    public Animator animator;
    public string boolName = "Open";
   
    private bool onceInTime = true;

    public void ElectricityCome()
    {
       
        audioSource.Play();
        ToggleDoorOpen();
        if (targetObjects != null && targetObjects.Count > 0)
        {
            particles.SetActive(true);
            foreach (GameObject targetObject in targetObjects)
            {
                if (targetObject != null)
                {
                    targetObject.layer = newLayer; // Objeyi yeni layer'a ata
                }
            }
        }

        StartCoroutine(PlayAudioAfterDelay(9f)); // 9 saniye bekledikten sonra ses klibini çal
    }

    private IEnumerator PlayAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null)
        {
            audioSource.clip = GeneratorVoice;
            audioSource.loop = true; // Loop'u ba?lat
            audioSource.Play();
        }
    }

    public void ToggleDoorOpen()
    {
        bool isOpen = animator.GetBool(boolName);
        animator.SetBool(boolName, !isOpen);
    }
}
