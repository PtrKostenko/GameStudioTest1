using UnityEngine.SceneManagement;

namespace GameStudioTest1
{
    public class SyncLevelLoader : LevelLoader
    {
        public override string CurrentLevel => SceneManager.GetActiveScene().name;


        public override void LoadMainMenu()
        {
            LoadScene(MainMenu);
        }
        public override void LoadFirstLevel()
        {
            LoadScene(FirstLevel);
        }
        public override void RestartLevel()
        {
            var current = SceneManager.GetActiveScene().buildIndex;
            LoadScene(current);
        }

        public override bool HasNextLevel()
        {
            bool exist = false;
            var current = SceneManager.GetActiveScene().buildIndex;
            try
            {
                var newScene = SceneManager.GetSceneByBuildIndex(current + 1);
                exist = true;
            }
            catch (System.ArgumentException exception)
            {
                exist = false;
            }
            return exist;
        }

        public override void LoadNextLevel()
        {
            var current = SceneManager.GetActiveScene().buildIndex;
            var newScene = SceneManager.GetSceneByBuildIndex(current + 1);
            LoadScene(newScene.buildIndex);
        }

        public override void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public override void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        
    }
}
