using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Story/New Dialogue")]
public class StorySO : ScriptableObject
{
    [field: SerializeField] public float textSize = 45;
    [field: SerializeField] public float waitTime = 1f;
    [field: SerializeField] public float writeTimeMultiplier = 1f;
    [field:SerializeField, TextArea(5,20)] public string dialogue { get; private set; }
}
