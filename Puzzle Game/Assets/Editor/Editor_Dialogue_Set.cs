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
    Dialogue_Set ds;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //Send Dialogue To Textbox.T
        ds = (Dialogue_Set)target;

        EditorGUILayout.LabelField(" - Send Dialogue to Textbox in Scene (Playmode) -");
        if (GUILayout.Button("Send Dialogue"))
        {
            Debug.Log("ON_GUI: Dialogue Set sending dialogues");
            ds.sendDialogue();
        }

        EditorGUILayout.LabelField(" - Clears the whole Set -");
        if (GUILayout.Button("Delete All Dialogue"))
        {
            if (EditorUtility.DisplayDialog("Delete all Dialogue?", "Would you really like to delete all dialogues from '" + target.name + "'?", "Yes", "No"))
            {
                Undo.RecordObject(ds, "Clear Dialogues");
                ds.Dialogues.Clear();
                EditorUtility.SetDirty(ds);
            }

        }

        EditorGUILayout.LabelField(" - Add a dialogue to the start of this set -");
        if (GUILayout.Button("Add Dialogue (Start)"))
        {
            Undo.RecordObject(ds, "Add Dialogue");
            ds.Dialogues.Insert(0, new Dialogue());
            EditorUtility.SetDirty(ds);
        }


        //Change List Size
        size = ds.Dialogues.Count;
        EditorGUILayout.Separator();
        d = EditorGUILayout.Foldout(d, "Dialogues - Size : " + size);

        //size = EditorGUILayout.IntField("Size", size);

        //EditorGUILayout.LabelField("Size: " + size);

        //Display List
        SerializedProperty dialogueList = serializedObject.FindProperty("Dialogues");

        if (d)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < ds.Dialogues.Count; i++)
            {
                EditorGUI.BeginChangeCheck();
                Sprite newSprite = (Sprite)EditorGUILayout.ObjectField("Profile", ds.Dialogues[i].Profile, typeof(Sprite), false);
                string newLine = EditorGUILayout.TextField("Line", ds.Dialogues[i].Line);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ds, "Change Dialogue");
                    ds.Dialogues[i].Profile = newSprite;
                    ds.Dialogues[i].Line = newLine;
                    EditorUtility.SetDirty(ds);
                }

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove Dialogue"))
                {
                    Undo.RecordObject(ds, "Delete Dialogue");
                    ds.Dialogues.Remove(ds.Dialogues[i]);
                    EditorUtility.SetDirty(ds);
                }

                if (GUILayout.Button("Add Dialogue (Next)"))
                {
                    Undo.RecordObject(ds, "Add Dialogue");
                    ds.Dialogues.Insert(i + 1, new Dialogue());
                    EditorUtility.SetDirty(ds);
                }
                GUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField(" - Add a dialogue to the end of this set -");
        if (GUILayout.Button("Add Dialogue (End)"))
        {
            Undo.RecordObject(ds, "Add Dialogue");
            ds.Dialogues.Add(new Dialogue());
            EditorUtility.SetDirty(ds);

        }

        EditorGUILayout.Separator();
        if (GUILayout.Button("Add Option"))
        {
            Undo.RecordObject(ds, "Add Option");
            ds.LinkedSet.Add(new LinkSet());
            EditorUtility.SetDirty(ds);
        }

        int lc = 0;
        foreach (LinkSet l in ds.LinkedSet)
        {
            EditorGUI.BeginChangeCheck();
            string option = EditorGUILayout.TextField("Text", l.option);
            Dialogue_Set linkedSet = (Dialogue_Set)EditorGUILayout.ObjectField("Linked Set", l.linkedSet, typeof(Dialogue_Set), false);


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ds, "Change LinkSet");
                l.option = option;
                l.linkedSet = linkedSet;
                EditorUtility.SetDirty(ds);
            }

            lc++;
        }

        if (GUILayout.Button("Remove Option") && ds.LinkedSet.Count > 0)
        {
            Undo.RecordObject(ds, "Remove Option");
            ds.LinkedSet.RemoveAt(ds.LinkedSet.Count - 1);
            EditorUtility.SetDirty(ds);
        }

    }
}
