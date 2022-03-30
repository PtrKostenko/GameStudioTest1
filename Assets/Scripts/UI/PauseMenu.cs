using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameStudioTest1.UI
{
    public class PauseMenu : Menu
    {
        [SerializeField] private SaveLoadMenu _saveLoadMenu;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _exitToMainMenu;

        public UnityEvent ContinueClicked => _continueButton.onClick;
        public UnityEvent RestartClicked => _restartLevelButton.onClick;
        public UnityEvent ExitToMainMenuClicked => _exitToMainMenu.onClick;
        public LoadGameEvent LoadGameClicked => _saveLoadMenu.LoadGameClicked;
        public IntEvent SaveGameClicked => _saveLoadMenu.SaveGameClicked;


        private void OnValidate()
        {
            Debug.Assert(_continueButton != null, $"{nameof(_continueButton)} need to be assigned", this);
            Debug.Assert(_restartLevelButton != null, $"{nameof(_restartLevelButton)} need to be assigned", this);
            Debug.Assert(_exitToMainMenu != null, $"{nameof(_exitToMainMenu)} need to be assigned", this);
        }

        public override void Activate()
        {
            base.Activate();
            _saveLoadMenu.Activate();
        }

        public void UpdateSaveLoadMenu()
        {
            _saveLoadMenu.UpdateMenu();
        }
    } 
}
