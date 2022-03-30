using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;
using GameStudioTest1.UI;

namespace GameStudioTest1
{
    public class MainMenu : Menu
    {
        [SerializeField] private Button _startNewGameButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private SaveLoadMenu _saveLoadMenu;
        [Inject] private Game _game;

        public UnityEvent StartNewGameClicked => _startNewGameButton.onClick;
        public UnityEvent ExitClicked => _exitButton.onClick;
        public LoadGameEvent LoadGameClicked => _saveLoadMenu.LoadGameClicked;

        private void OnValidate()
        {
            Debug.Assert(_startNewGameButton != null, $"{nameof(_startNewGameButton)} need to be assigned", this);
            Debug.Assert(_exitButton != null, $"{nameof(_exitButton)} need to be assigned", this);
            Debug.Assert(_saveLoadMenu != null, $"{nameof(_saveLoadMenu)} need to be assigned", this);
        }

        private void Start()
        {
            _game.SubscribeToMainMenu(this);
            Activate();
        }

        public override void Activate()
        {
            base.Activate();
            _saveLoadMenu.Activate();
            _saveLoadMenu.NoSavesAllowed = true;
        }
    }
}