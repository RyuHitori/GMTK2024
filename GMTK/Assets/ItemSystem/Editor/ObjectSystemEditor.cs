using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(ObjectSystem))]
public class ObjectSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObjectSystem objSystem = (ObjectSystem)target;

        objSystem.UpdateData();

    }
}
