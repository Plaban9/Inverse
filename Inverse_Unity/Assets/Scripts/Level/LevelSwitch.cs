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
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.enabled = true;
            }

            //SetLevelEnabledStatus(true);
        }

        public override void HideLevel()
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.enabled = false;
            }

            //SetLevelEnabledStatus(false);
        }

        private void SetRealmData()
        {
            if (_spriteRenderers != null)
            {
                _spriteRenderers.Clear();
            }
            else
            {
                _spriteRenderers = new List<SpriteRenderer>();
            }

            Debug.Log("Child Count: " + transform.childCount);

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                {
                    spriteRenderer.enabled = false;
                    spriteRenderer.name = spriteRenderer.name + "_" + levelType.ToString();
                    _spriteRenderers.Add(spriteRenderer);
                }
            }
        }
    }
}

