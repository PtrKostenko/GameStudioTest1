using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStudioTest1
{
    public abstract class LevelLoader
    {
        protected const string MainMenu = "MainMenu";
        protected const string FirstLevel = "Level1";
        public abstract string CurrentLevel { get; }

        public abstract void LoadMainMenu();
        public abstract void LoadFirstLevel();
        public abstract void RestartLevel();
        public abstract bool HasNextLevel();
        public abstract void LoadNextLevel();
        public abstract void LoadScene(string sceneName);
        public abstract void LoadScene(int buildIndex);
    }
}
