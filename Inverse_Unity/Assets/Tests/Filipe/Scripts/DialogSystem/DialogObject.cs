using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.DialogSystem
{
    [Serializable]
    [CreateAssetMenu(menuName = "Minimalist/Dialog/DialogObject")]
    public class DialogObject : ScriptableObject
    {
        [SerializeField] public List<DialogMessage> messages;
    }
}
