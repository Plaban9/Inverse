using Minimalist.Interfaces;
using Minimalist.Level;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Minimalist.Manager
{
    public class RealmManager : MonoBehaviour
    {
        [SerializeField] LevelType _currentLevelType;
        [SerializeField] private List<LevelData> _levelData = new List<LevelData>();
        private Dictionary<LevelType, ILevelSwitch> _levelDataDictionary = new Dictionary<LevelType, ILevelSwitch>();
        private List<ILevelListener<LevelType>> _levelListeners = new List<ILevelListener<LevelType>>();

        public void InitializeRealmManager(LevelType defaultLevelType)
        {
            foreach (var _data in _levelData)
            {
                ILevelSwitch levelSwitch = _data.levelObject.GetComponent<ILevelSwitch>();
                levelSwitch.OnSwitchInitialized();

                _levelDataDictionary.Add(_data.levelType, levelSwitch);
                levelSwitch.HideLevel();
            }

            _currentLevelType = defaultLevelType;
            _levelDataDictionary[defaultLevelType].OnLevelSwitch(defaultLevelType);
            NotifyListeners(defaultLevelType);
        }

        public void OnLevelSwitch(LevelType levelType)
        {
            _currentLevelType = levelType;

            _levelDataDictionary.ToList().ForEach(x =>
            {
                x.Value.OnLevelSwitch(levelType);
            });
            NotifyListeners(levelType);
        }

        public LevelType GetCurrentLevelType()
        {
            return _currentLevelType;
        }

        public void AddListener(ILevelListener<LevelType> listener)
        {
            _levelListeners.Add(listener);
        }

        public void RemoveListener(ILevelListener<LevelType> listener)
        {
            _levelListeners.Remove(listener);

        }

        public void NotifyListeners(LevelType type)
        {
            foreach(ILevelListener<LevelType> listener in _levelListeners)
            {
                listener.OnNotify(type);
            }
        }
    }
}
