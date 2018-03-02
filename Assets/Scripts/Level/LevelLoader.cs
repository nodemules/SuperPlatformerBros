using System;
using Interfaces;
using TriggerArea;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelLoader : MonoBehaviour
    {
        private static readonly string[] Levels =
            {"Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7"};

        private static readonly int HighestLevel = Levels.Length;
        public static readonly string FirstLevel = Levels[0];

        public static void ChangeLevel(string levelName)
        {
            int level = Array.IndexOf(Levels, levelName);
            if (level == -1)
            {
                print("Can't change level to " + level);
                print("Going to next level");
                NextLevel();
                return;
            }

            print("Changing level to Level=" + levelName);
            GlobalGameState.CurrentLevel = levelName;
            GlobalGameState.EnableSceneListener();
            SceneManager.LoadScene(levelName);
        }

        public static void NextLevel()
        {
            int level = Array.IndexOf(Levels, GlobalGameState.CurrentLevel) + 1;
            if (level < HighestLevel)
            {
                int nextLevel = level + 1;
                string levelName = Levels[nextLevel - 1];
                ChangeLevel(levelName);
                return;
            }

            if (level == HighestLevel)
            {
                ApplicationState.Ending = -100;
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}