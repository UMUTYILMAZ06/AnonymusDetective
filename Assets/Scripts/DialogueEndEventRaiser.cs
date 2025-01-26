using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEndEventRaiser : MonoBehaviour
{
    public Action OnDialogueEnd;
    public void DialogueEndEvent()
    {
        OnDialogueEnd?.Invoke();
    }
}
