using UnityEngine;

// The only script that spells a PlayerPrefs key.
public static class SaveData
{
    const string UnlockedKey = "unlocked";
    const string MusicKey = "MusicVolume";
    const int LastLevel = 5;
    static string BestKey(int level) => "best" + level;

    public static int GetUnlocked() => PlayerPrefs.GetInt(UnlockedKey, 1);
    public static bool IsUnlocked(int level) => level <= GetUnlocked();

    public static void Unlock(int level)
    {
        level = Mathf.Clamp(level, 1, LastLevel);
        if (level <= GetUnlocked()) return;
        PlayerPrefs.SetInt(UnlockedKey, level);
        PlayerPrefs.Save();
    }

    public static bool HasBest(int level) => PlayerPrefs.HasKey(BestKey(level));
    public static float GetBest(int level) => PlayerPrefs.GetFloat(BestKey(level), 0f);

    // true when this time is a new best
    public static bool Record(int level, float seconds)
    {
        if (HasBest(level) && GetBest(level) <= seconds) return false;
        PlayerPrefs.SetFloat(BestKey(level), seconds);
        PlayerPrefs.Save();
        return true;
    }

    public static float GetMusicVolume() => Mathf.Clamp(PlayerPrefs.GetFloat(MusicKey, 1f), 0.0001f, 1f);
    public static void SetMusicVolume(float v)
    {
        PlayerPrefs.SetFloat(MusicKey, Mathf.Clamp(v, 0.0001f, 1f));
        PlayerPrefs.Save();
    }
}
