using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minimalist.Effect
{
    [CreateAssetMenu(menuName = "Effects/Vfx Library")]
    public class VfxLibrary : ScriptableObject
    {
        public List<VfxAsset> vfxAssets;
    }
}
