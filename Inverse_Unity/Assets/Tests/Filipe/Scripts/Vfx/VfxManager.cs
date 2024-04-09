using Minimalist.Effect.Animations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Minimalist.Effect.Animations
{
    public class VfxManager : MonoBehaviour
    {
        public static VfxManager Instance { get; private set; }

        [SerializeField] private VfxLibrary vfxLibrary;

        private List<GameObject> _inactiveEffects;

            private void Awake()
            {
                if (Instance != null && Instance != this)
                {
                    Destroy(this);
                }
                else
                {
                    Instance = this;
                }

                _inactiveEffects = new List<GameObject>();

            }

        /// <summary>
        /// Creates an effect at given position.
        /// VFX Asset is required with an animation and a proper name
        /// VFX Asset GameObject *MUST* contain an VfxController AND
        /// the animation must call AlertObserver("animationEnded") at the end.
        /// This must be done through the animation editor, add an Event at the end
        /// dropdown to VfxController/AlertObserver and add String parameter of
        /// "animationEnded".
        /// </summary>
        /// <param name="vfxAssetCategory">Consult VfxEnum</param>
        /// <param name="position">Position the object must be in</param>
        public void CreateEffect(VfxEnum vfxAssetCategory, Vector3 position)
        {

            GameObject reusable = _inactiveEffects.Find(o =>
            o.GetComponent<VfxController>().GetAsset()
            .category == vfxAssetCategory);

            if (reusable != null)
            {
                _inactiveEffects.Remove(reusable);
                reusable.transform.position = position;
                reusable.SetActive(true);
                if (reusable.TryGetComponent(out Animation effAnim))
                {
                    effAnim.Rewind();
                    effAnim.Play();
                }
            }
            //else if (_inactiveEffects.Count > 0)
            //{
            //    GameObject oldEffect = _inactiveEffects[0];
            //    _inactiveEffects.RemoveAt(0);
            //    oldEffect.transform.position = position;
            //    oldEffect.SetActive(true);
            //    if (oldEffect.TryGetComponent(out VfxController contr))
            //    {
            //        contr.SetAsset(vfxAssetCategory);
            //    }
            //    if (oldEffect.TryGetComponent<Animation>(out Animation effAnim))
            //    {
            //        effAnim = vfxAsset.GetComponent<Animation>();
            //        effAnim.Rewind();
            //        effAnim.Play();
            //    }
            //}
            else
            {
                VfxAsset asset = GetVfxAsset(vfxAssetCategory);
                GameObject obj = Instantiate(asset.effect,
                    position, Quaternion.identity, null);
                if (obj.TryGetComponent(out VfxController vfxController))
                {
                    vfxController.SetAsset(asset);
                }
            }
        }

        /// <summary>
        /// Disables the current effect and add them to be reused later.
        /// Just use it if you believe this effect will eventually be reused.
        /// Also make sure to use this only on objects that are meant to be used by the 
        /// vfx manager to begin with.
        /// </summary>
        /// <param name="disabledEffect">Effect to be disabled</param>
        public void AddToInactiveEffect(GameObject disabledEffect)
        {
            disabledEffect.SetActive(false);
            _inactiveEffects.Add(disabledEffect);
        }

        private VfxAsset GetVfxAsset(VfxEnum vfxEnum)
        {
            List<VfxAsset> assets = vfxLibrary.vfxAssets;
            switch (vfxEnum)
            {
                case VfxEnum.PLAYER_DAMAGEDBLOOD:
                    List<VfxAsset> assetList = assets.FindAll(q => q.name.Contains("Blood"));
                    return assetList[Mathf.RoundToInt(Random.Range(0, assetList.Count))];
                    break;
                case VfxEnum.PLAYER_JUMPDUST:
                    VfxAsset asset = assets.Find(q => q.name.Contains("Jump"));;
                    return asset;
                default:
                    break;
            }
            return null;
        }

    }

}