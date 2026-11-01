using UnityEngine;

namespace NightRacer.Persistence
{
    public static class HighScoreStorage
    {
        private const string Key = "NightRacer_HighScore";

        public static int Load()
        {
            return PlayerPrefs.GetInt(Key, 0);
        }

        public static void Save(int score)
        {
            PlayerPrefs.SetInt(Key, score);
            PlayerPrefs.Save();
        }
    }
}

