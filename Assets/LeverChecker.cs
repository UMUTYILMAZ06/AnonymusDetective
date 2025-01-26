using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverChecker : MonoBehaviour
{
    private bool _isLevelCheckerReady;
    [SerializeField] private DialogueEndEventRaiser dialogueEndEventRaiser;

    private void Start()
    {
        dialogueEndEventRaiser.OnDialogueEnd += OnDialogueEnd;
    }
    private void OnDialogueEnd()
    {
       
        _isLevelCheckerReady = true;
    }
    void Update()
    {
        if (!_isLevelCheckerReady)
            return;
        // Kendi transform bile?eninden rotation de?erini al
        Vector3 rotation = GetComponent<Transform>().localEulerAngles;
       // Debug.Log(rotation.x);

        // Rotation.x de?erini -40 derece ile kontrol et
        if (rotation.x < 320 && rotation.x > 180)
        {
            // Rotation.x -40'?n alt?na inerse yap?lacak i?lemler
            SceneManager.LoadScene("Level2");
           

            // Buraya ekleyece?iniz i?lemleri yapabilirsiniz
            // Örne?in:
            // Ba?ka bir nesnenin rengini de?i?tirmek gibi:
            // GetComponent<Renderer>().material.color = Color.red;
            // veya
            // Animasyon ba?latmak gibi:
            // GetComponent<Animator>().SetTrigger("TriggerName");
        }
    }

   
       
    
}
