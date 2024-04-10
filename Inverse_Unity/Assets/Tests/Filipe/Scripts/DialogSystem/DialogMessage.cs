using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minimalist.DialogSystem
{
    [Serializable]
    [CreateAssetMenu(menuName = "Minimalist/Dialog/DialogMessage")]
    public class DialogMessage : ScriptableObject
    {
        public string speaker = "No Name Specified";
        public string text = "Message missing!";
        public float timeToNext = 1000f;
        public float timeTillNextCharacter = 0.1f;
    }
}
