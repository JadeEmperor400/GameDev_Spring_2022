using UnityEditor.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[CustomEditor(typeof(Dialogue_Set))]
[CanEditMultipleObjects]
public class Editor_Dialogue_Set : Editor
{
    bool d = false;
    int size = 0;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //Send Dialogue To Textbox.T
        Dialogue_Set ds = (Dialogue_Set)target;

        EditorGUILayout.LabelField(" - Send Dialogue to Textbox in Scene (Playmode) -");
        if (GUILayout.Button("Send Dialogue"))
        {
            Debug.Log("ON_GUI: Dialogue Set sending dialogues");
            ds.sendDialogue();
        }

        EditorGUILayout.LabelField(" - Clears the whole Set -");
        if (GUILayout.Button("Delete All Dialogue"))
        {
            ds.Dialogues.Clear();
        }

        EditorGUILayout.LabelField(" - Add a dialogue to the start of this set -");
        if (GUILayout.Button("Add Dialogue (Start)"))
        {
            ds.Dialogues.Insert(0, new Dialogue());
        }

        //Change List Size
        size = ds.Dialogues.Count;
        EditorGUILayout.Separator();
        d = EditorGUILayout.Foldout(d, "Dialogues - Size : " + size);

        //size = EditorGUILayout.IntField("Size", size);

        //EditorGUILayout.LabelField("Size: " + size);

        /*
        while (size != ds.Dialogues.Count) {
            while (size < ds.Dialogues.Count) {
                ds.Dialogues.RemoveAt(ds.Dialogues.Count - 1);
            }
            while (size > ds.Dialogues.Count)
            {
                ds.Dialogues.Add(new Dialogue());
            }
        }*/

        //Display List

        if (d) {
            EditorGUI.indentLevel++;
            for (int i = 0; i < ds.Dialogues.Count; i++) {
                ds.Dialogues[i].Profile = (Sprite)EditorGUILayout.ObjectField("Profile", ds.Dialogues[i].Profile, typeof(Sprite), false);
                ds.Dialogues[i].Line = EditorGUILayout.TextField("Line", ds.Dialogues[i].Line);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove Dialogue")) {
                    ds.Dialogues.Remove(ds.Dialogues[i]);
                }

                if (GUILayout.Button("Add Dialogue (Next)"))
                {
                    ds.Dialogues.Insert( i+1 , new Dialogue());
                }
                GUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
        }
        
        EditorGUILayout.Separator();
        
        EditorGUILayout.LabelField(" - Add a dialogue to the end of this set -");
        if (GUILayout.Button("Add Dialogue (End)")) {
            ds.Dialogues.Add(new Dialogue());
        }

        EditorGUILayout.Separator();
        if (GUILayout.Button("Add Option"))
        {
            ds.LinkedSet.Add (new LinkSet());
        }
        
        foreach (LinkSet l in ds.LinkedSet) {
            l.option = EditorGUILayout.TextField("Text", l.option);
            l.linkedSet = (Dialogue_Set)EditorGUILayout.ObjectField("Linked Set", l.linkedSet, typeof(Dialogue_Set), false);
        }

        if (GUILayout.Button("Remove Option"))
        {
            ds.LinkedSet.RemoveAt(ds.LinkedSet.Count - 1);
        }

    }
}
