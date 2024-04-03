using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.DialogSystem
{
    [Serializable]
    [CreateAssetMenu(menuName = "Minimalist/Dialog/DialogMessage")]
    public class DialogMessage : ScriptableObject
    {
        [SerializeField] public String speaker = "No Name Specified";
        [SerializeField] public String text = "Message missing!";
        [SerializeField] public float timeToNext = 1000f;
        [SerializeField] public float timeTillNextCharacter = 0.1f;
    }
}
