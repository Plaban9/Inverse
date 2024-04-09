using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minimalist.Effect
{
    [CreateAssetMenu(menuName = "Effects/Vfx Asset")]
    public class VfxAsset : ScriptableObject
    {
        public string effectName;
        public GameObject effect;
        public VfxEnum category;
    }
}
