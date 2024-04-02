using Minimalist.Effect;
using Minimalist.Effect.Animations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Minimalist.Effect.Animations
{
    public class VfxController : MonoBehaviour
    {
        [SerializeField] private VfxAsset asset;

        public void SetAsset(VfxAsset asset)
        {
            this.asset = asset;

        }

        public VfxAsset GetAsset()
        {
            return asset;
        }

        public void Kill()
        {
            VfxManager.Instance.AddToInactiveEffect(this.gameObject);
        }

        public void AlertObserver(string message)
        {
            if (message.Equals("animationEnded"))
            {
                VfxManager.Instance.AddToInactiveEffect(this.gameObject);
            }
        }
    }
}