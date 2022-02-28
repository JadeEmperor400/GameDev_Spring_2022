using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinkSet
{
    public Dialogue_Set linkedSet;
    public string option = "Option";

    public LinkSet() {
        option = "Option";
        linkedSet = null;
    }
    
}
