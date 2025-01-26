using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GirlTalkTrigger : MonoBehaviour
{
    private bool onceTime = true;
    public Animator animator;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && onceTime)
        {
            animator.SetBool("isEnterRoom", true);
            audioSource.Play();
            onceTime = false;
            StartCoroutine(LoadLevelAfterDelay(5f)); // Start the coroutine with a 10 second delay
        }
    }

    private IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

       
            SceneManager.LoadScene("Level3");
        
    }
}
