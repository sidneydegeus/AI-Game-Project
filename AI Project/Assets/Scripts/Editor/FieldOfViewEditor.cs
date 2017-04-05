using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Human))]
public class FieldOfViewEditor : Editor {

    void OnSceneGUI() {
        Human fow = (Human)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.TargetsInRadius) {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}