using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingShutDown : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioClip innerVoice3Clip;
    public Animator animator;

    // Start is called before the first frame update
    private bool OnceTime = true;

    // Update is called once per frame
    void Update()
    {
        if (audioSource1 != null && !audioSource1.isPlaying && OnceTime && audioSource1.clip == innerVoice3Clip)
        {
            OnceTime = false;
            Invoke("StopAudioSource2", 1.0f); // 1 saniye sonra StopAudioSource2 metodunu ça??r
        }
    }

    void StopAudioSource2()
    {
        if (audioSource2 != null)
        {
            audioSource2.Stop();
            animator.SetBool("IsDead", true);
        }
    }
}
