using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData_", menuName = "Object/New Object")]
public class ObjectData : ScriptableObject
{
    [Header("Information")]
    public string title;
    public string description;
    public float value;
    public float scale;

    [Header("Graphics")]
    public Sprite icon;
    public Mesh mesh;

    [Header("Fix")]
    public Vector3 insta3DScale, insta3DOffset;


}
