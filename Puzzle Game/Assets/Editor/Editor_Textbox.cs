using UnityEditor.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[CustomEditor(typeof(Textbox))]
[CanEditMultipleObjects]
public class Editor_Textbox : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        //Make Text Continue
        if (GUILayout.Button("Continue")) {
            Debug.Log("ON_GUI: Continue Text");

            if (Textbox.T != null)
            {
                Textbox.T.forcedGo = true;
            }
        }
    }
}
