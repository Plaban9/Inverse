using Minimalist.Inverse;
using Minimalist.SaveSystem;

using UnityEngine;

public class GameAttributes
{
    public static void OnInit()
    {
        Stat_LaunchCount = SaveManager.ReadData(Constants.SaveSystem.STAT_LAUNCH_COUNT, 0);
        SaveManager.SaveData(Constants.SaveSystem.STAT_LAUNCH_COUNT, ++Stat_LaunchCount);

        Stat_LastLevelUnlocked = SaveManager.ReadData(Constants.SaveSystem.GAMEPLAY_LAST_LEVEL_UNLOCKED, 0);
        Stat_LastLevelPlayed = SaveManager.ReadData(Constants.SaveSystem.GAMEPLAY_LAST_LEVEL_PLAYED, 0);

        Settings_SFXVolume = SaveManager.ReadData(Constants.SaveSystem.SETTINGS_SOUND_VOLUME, Constants.General.Audio.MAX_SOUND_VOLUME);
        Settings_MusicVolume = SaveManager.ReadData(Constants.SaveSystem.SETTINGS_MUSIC_VOLUME, Constants.General.Audio.MAX_MUSIC_VOLUME);

        d($"---- Attributes ----");
        d($"Launch Count: {Stat_LaunchCount}, Last Level Unlocked: {Stat_LastLevelUnlocked}, Last Level Played: {Stat_LastLevelPlayed}, SFX Volume: {Settings_SFXVolume}, Music Volume: {Settings_MusicVolume}");
    }

    public static int Stat_LaunchCount { get; set; }
    public static int Stat_LastLevelUnlocked { get; set; }
    public static int Stat_LastLevelPlayed { get; set; }
    public static float Settings_SFXVolume { get; set; }
    public static float Settings_MusicVolume { get; set; }

    private static void d(string message)
    {
        //Debug.Log("<<GameAttributes>> " + message);
    }
}
