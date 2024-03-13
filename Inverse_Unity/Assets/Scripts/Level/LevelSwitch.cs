    using Minimalist.Effect.Level.Parallax;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Minimalist.Level
{
    [ExecuteInEditMode]
    public class LevelSwitch : ILevelSwitch
    {
        public override void OnLevelSwitch(LevelType levelType)
        {
            //if (this.GetLevelType() == levelType)
            //{
            //    if (!GetLevelEnabledStatus())
            //    {
            //        ShowLevel();
            //    }

            //}
            //else
            //{
            //    if (GetLevelEnabledStatus())
            //    {
            //        HideLevel();
            //    }
            //}

            SwitchLevel(this.GetLevelType() == levelType);
        }

        private void SwitchLevel(bool enableLevel)
        {
            SetLevelEnabledStatus(enableLevel);
        }

        public override void OnSwitchInitialized()
        {
            OnSwitchInitialized(false);
        }

        public override void OnSwitchInitialized(bool enableLevel)
        {
            SetRealmData();
            SetLevelEnabledStatus(enableLevel);
        }

        public override void ShowLevel()
        {
            foreach (var gameObject in _switchObjects)
            {
                if (gameObject.TryGetComponent(out Collider2D cl))
                {
                    cl.enabled = true;
                }
                if (gameObject.TryGetComponent(out SpriteRenderer sr))
                {
                    sr.enabled = true;
                }
            }

            //SetLevelEnabledStatus(true);
        }

        public override void HideLevel()
        {
            foreach (var gameObject in _switchObjects)
            {
                HideObject(gameObject);
            }

            //SetLevelEnabledStatus(false);
        }

        private void HideObject(GameObject gmObj)
        {

                if (gmObj.TryGetComponent(out Collider2D cl))
                {
                    cl.enabled = false;
                }
                if (gmObj.TryGetComponent(out SpriteRenderer sr))
                {
                    sr.enabled = false;
                }

        }

        private void SetRealmData()
        {
            if (_switchObjects != null)
            {
                _switchObjects.Clear();
            }
            else
            {
                _switchObjects = new List<GameObject>();
            }

            Debug.Log("Child Count: " + transform.childCount);

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child != null)
                {

                    HideObject(child);
                    child.name = child.name + "_" + levelType.ToString();
                    _switchObjects.Add(child);
                }
            }
        }
    }
}

