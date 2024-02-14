using Minimalist.Level;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.VisualScripting;

using UnityEngine;

namespace Minimalist.Manager
{
    public class RealmManager : MonoBehaviour
    {
        [SerializeField] LevelType _currentLevelType;
        [SerializeField] private List<LevelData> _levelData = new List<LevelData>();
        private Dictionary<LevelType, ILevelSwitch> _levelDataDictionary = new Dictionary<LevelType, ILevelSwitch>();

        public void InitializeRealmManager(LevelType defaultLevelType)
        {
            foreach (var _data in _levelData)
            {
                ILevelSwitch levelSwitch = _data.levelObject.GetComponent<ILevelSwitch>();
                levelSwitch.OnSwitchInitialized();

                _levelDataDictionary.Add(_data.levelType, levelSwitch);
            }

            _currentLevelType = defaultLevelType;
            _levelDataDictionary[defaultLevelType].OnLevelSwitch(defaultLevelType);
        }

        public void OnLevelSwitch(LevelType levelType)
        {
            _currentLevelType = levelType;

            _levelDataDictionary.ToList<KeyValuePair<LevelType, ILevelSwitch>>().ForEach(x =>
            {
                x.Value.OnLevelSwitch(levelType);
            });
        }
    }
}
