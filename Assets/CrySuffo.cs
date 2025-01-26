using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrySuffo : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Player's transform
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float maxDistance = 5f; // Maximum distance at which the sound is heard
    [SerializeField] private float minVolume = 0f; // Minimum volume level
    [SerializeField] private float maxVolume = 1f; // Maximum volume level
    [SerializeField] private Slider suffocationSlider; // UI Slider to show suffocation progress
    [SerializeField] private GameObject suffocationSliderGameObject;
    private float distance;
    public bool isHeard;
    public bool isPlayingCry;

    private float stayTimer = 0f; // Timer to track how long the object has been in the trigger
    [SerializeField] private float requiredStayTime = 10f; // Required time to trigger the function

    private void Start()
    {
        distance = 1000f;
        AdjustVolumeBasedOnDistance();
        isPlayingCry = false;
        isPlayingCry = true;
        suffocationSlider.minValue = 0f;
        suffocationSlider.maxValue = requiredStayTime;
        suffocationSlider.value = stayTimer;
    }

    private void Update()
    {
         
        distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance < 8f)
        {
            AdjustVolumeBasedOnDistance();
        }
            

        suffocationSlider.value = stayTimer; // Update the slider value
    }

    private void AdjustVolumeBasedOnDistance()
    {
        
        
        if(distance >= 8f)
        {
           
            audioSource.volume = 0;
            return;
        }
        float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
        audioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            suffocationSliderGameObject.SetActive(true);
            stayTimer += Time.deltaTime;

            if (stayTimer >= requiredStayTime)
            {
                DieBreathlessFunction();
                stayTimer = 0f; // Reset the timer after triggering the function
                suffocationSliderGameObject.SetActive(false);
            }

            if (audioSource.isPlaying)
            {
                CryStop();
            }
        }
        else if (other.gameObject.CompareTag("SafeRoom"))
        {
            isHeard = false;
            
        }

      
    }

    private void OnTriggerExit(Collider other)
    {
        if (!audioSource.isPlaying && other.gameObject.CompareTag("Hand"))//?
        {
            Cry();
        }
        if (other.gameObject.CompareTag("Hand"))
        {
            stayTimer = 0f;
            suffocationSliderGameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("SafeRoom"))
        {
            isHeard = true;
        }

        
    }

    private void CryStop()
    {
        audioSource.loop = false;
        audioSource.Stop();
        isPlayingCry = false;
    }

    private void Cry()
    {
       
        audioSource.loop = true;
        audioSource.Play();
        isPlayingCry = true;

    }

    private void DieBreathlessFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
