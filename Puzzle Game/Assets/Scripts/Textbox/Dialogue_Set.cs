using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue_Set : ScriptableObject
{
    [SerializeField]
    private List<Dialogue> dialogues = new List<Dialogue>();
    [SerializeField]
    private List<LinkSet> linkedSet = new List<LinkSet>();

    public List<LinkSet> LinkedSet {
        get { return linkedSet; }
    }

    public List<Dialogue> Dialogues { get { return dialogues; } }
    public void sendDialogue() {
        if (Textbox.T != null)
        {
            Textbox.T.read(this);
            Debug.Log("Dialogue Sent to Textbox");
        }
        else {
            Debug.Log("DialogueSet cannot be sent, no textbox active \nGame may not be running");
        }
    }

    public void sendLinkedDialogue(int goTo) {
        if (linkedSet != null && linkedSet.Count > goTo) {
            Textbox.T.nextDialogues.Insert(0, linkedSet[goTo].linkedSet);
        }
    }
}
