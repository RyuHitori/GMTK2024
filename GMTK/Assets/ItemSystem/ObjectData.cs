using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData_", menuName = "Object/New Object")]
public class ObjectData : ScriptableObject
{
    [Header("Information")]
    public string title = "New Object";
    public string description;
    public long value;
    public long unit;

    [Header("Graphics")]
    public Sprite icon;
    public Mesh mesh;

    
}
