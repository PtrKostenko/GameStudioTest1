using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GameStudioTest1
{
    public class Game : MonoBehaviour
    {
        [Inject] private SaveSystem _saveSystem;
        [Inject] private LevelLoader _levelLoader;

        private Level _сurrentLevel;
        private MainMenu _currentMainMenu;
        private Action _onLevelInited;

        public void SubscribeToCurrentLevel(Level level)
        {
            if (_сurrentLevel != null)
            {
                _сurrentLevel.SaveGameRequested.RemoveListener(OnSaveGameRequest);
                _сurrentLevel.LevelInited.RemoveListener(OnLevelInited);
                _сurrentLevel.LevelFinished.RemoveListener(OnLevelFinished);
                _сurrentLevel.LoadGameRequested.RemoveListener(OnLoadGameRequest);
                _сurrentLevel.ExitToMainMenuRequested.RemoveListener(ExitToMainMenu);
                _сurrentLevel.RestartRequested.RemoveListener(Restart);
            }

            _сurrentLevel = level;
            _сurrentLevel.SaveGameRequested.AddListener(OnSaveGameRequest);
            _сurrentLevel.LevelInited.AddListener(OnLevelInited);
            _сurrentLevel.LevelFailed.AddListener(OnLevelFailed);
            _сurrentLevel.LevelFinished.AddListener(OnLevelFinished);
            _сurrentLevel.LoadGameRequested.AddListener(OnLoadGameRequest);
            _сurrentLevel.ExitToMainMenuRequested.AddListener(ExitToMainMenu);
            _сurrentLevel.RestartRequested.AddListener(Restart);
            
        }

        public void SubscribeToMainMenu(MainMenu mainMenu)
        {
            if (_currentMainMenu != null)
            {
                _currentMainMenu.StartNewGameClicked.RemoveListener(OnStartNewGameRequest);
                _currentMainMenu.LoadGameClicked.RemoveListener(OnLoadGameRequest);
                _currentMainMenu.ExitClicked.RemoveListener(OnExitRequested);
            }

            _currentMainMenu = mainMenu;
            _currentMainMenu.StartNewGameClicked.AddListener(OnStartNewGameRequest);
            _currentMainMenu.LoadGameClicked.AddListener(OnLoadGameRequest);
            _currentMainMenu.ExitClicked.AddListener(OnExitRequested);
        }

        private void OnStartNewGameRequest()
        {
            _levelLoader.LoadFirstLevel();
        }

        private void OnLevelFailed()
        {
            Restart();
        }

        private void OnLevelFinished()
        {
            if (_levelLoader.HasNextLevel())
            {
                _levelLoader.LoadNextLevel();
            }
            else
            {
                _levelLoader.LoadMainMenu();
            }
        }

        private void OnLevelInited()
        {
            Debug.Log("Level Inited");
            _onLevelInited?.Invoke();
            _onLevelInited = null;
        }

        private void OnLoadGameRequest(SavedGameInfo savedGame)
        {
            _сurrentLevel?.Unpause();
            _levelLoader.LoadScene(savedGame.CurrentLevelName);
            _onLevelInited = () => _сurrentLevel.SetLevelState(savedGame.Mementos);
        }

        private void OnSaveGameRequest(int slotNum)
        {
            var mementos = _сurrentLevel.GetMementos();
            SavedGameInfo savedGameInfo = new SavedGameInfo(slotNum, _levelLoader.CurrentLevel, mementos);
            _saveSystem.Save(savedGameInfo);
            _сurrentLevel.UpdateUI();
            Debug.Log($"Game saved in slot {slotNum}. {savedGameInfo}");
        }


        private void ExitToMainMenu()
        {
            _сurrentLevel.Unpause();
            _levelLoader.LoadMainMenu();
        }
        private void Restart()
        {
            _сurrentLevel.Unpause();
            _levelLoader.RestartLevel();
        }

        private void OnExitRequested()
        {
            Application.Quit();
        }
    } 
}
