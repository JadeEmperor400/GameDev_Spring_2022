using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisplayDialogue : MonoBehaviour
{

    [SerializeField]
    private Dialogue_Set firstDialogueSet = null;

    public void displayFirstDialogue()
    {
        firstDialogueSet?.sendDialogue();
    }


}
