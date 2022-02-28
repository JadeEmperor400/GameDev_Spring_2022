using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script holds any info needed for a textbox to display text with a profile image 
 */

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    private Sprite profile = null;
    public Sprite Profile {
        set { profile = value; }
        get { return profile; } }

    [SerializeField]
    private string line = "SAMPLE_TEXT";
    public string Line {
        set { line = value; }
        get { return line;}
    }
}
