using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace Minimalist.Level
{
    public abstract class ILevelSwitch : MonoBehaviour
    {
        [SerializeField] protected bool levelEnabledStatus;

        [SerializeField] protected LevelType levelType;

        [SerializeField] protected List<GameObject> _switchObjects;

        public abstract void OnSwitchInitialized();
        public abstract void OnSwitchInitialized(bool enableLevel);
        public abstract void OnLevelSwitch(LevelType levelType);
        public abstract void ShowLevel();
        public abstract void HideLevel();

        public LevelType GetLevelType()
        {
            return levelType;
        }

        public bool GetLevelEnabledStatus()
        {
            return levelEnabledStatus;
        }

        protected void SetLevelEnabledStatus(bool shouldEnable)
        {
            if (shouldEnable)
            {
                if (!levelEnabledStatus)
                {
                    ShowLevel();
                }
            }
            else
            {
                if (levelEnabledStatus)
                {
                    HideLevel();
                }
            }

            levelEnabledStatus = shouldEnable;
        }
    }
}
