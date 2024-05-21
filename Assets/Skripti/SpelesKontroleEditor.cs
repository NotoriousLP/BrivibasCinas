using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpelesKontrole))]
public class SpelesKontroleEditor : Editor {
    public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    SpelesKontrole targetScript = (SpelesKontrole)target;

    EditorGUILayout.LabelField("Rotas Skaits:");

    var keys = new List<Valstis.Speletaji>(targetScript.rotasSkaitsByPlayer.Keys);

    foreach (var key in keys) { 
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(key.ToString(), GUILayout.Width(100));
        targetScript.rotasSkaitsByPlayer[key] = EditorGUILayout.IntField(targetScript.rotasSkaitsByPlayer[key]);
        EditorGUILayout.EndHorizontal();
    }

    if (GUI.changed) {
        EditorUtility.SetDirty(target);
    }
}

}