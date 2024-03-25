using UnityEngine;

namespace Minimalist.SaveSystem
{
    /// <summary>
    /// Save System. (Uses Unity's Pref system)
    /// Note: All the parameters shpuld be a primitive type.
    /// Additional Note: If saving something like JSON, convert it into string.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        #region Read Methods
        public static string ReadData(string key, string defaultValue = "")
        {
            d($"Reading Data: Key: {key}, Deafult Value: {defaultValue}");
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static float ReadData(string key, float defaultValue = 0f)
        {
            return float.TryParse(ReadData(key, defaultValue.ToString()), out float result) ? result : defaultValue;
        }

        public static int ReadData(string key, int defaultValue = 0)
        {
            return int.TryParse(ReadData(key, defaultValue.ToString()), out int result) ? result : defaultValue;
        }
        #endregion

        #region Save Methods
        public static void SaveData(string key, string value)
        {
            d($"Saving Data: Key: {key}, Value: {value}");
            PlayerPrefs.SetString(key, value.ToString());
        }

        public static void SaveData(string key, int value)
        {
            SaveData(key, value.ToString());
        }

        public static void SaveData(string key, float value)
        {
            SaveData(key, value.ToString());
        }
        #endregion

        private static void d(string message)
        {
            Debug.Log("<<SaveManager>> " + message);
        }
    }
}