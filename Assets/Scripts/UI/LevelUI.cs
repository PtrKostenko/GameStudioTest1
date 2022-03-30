using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameStudioTest1.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private UnitPanel _activeUnitPanel;
        [SerializeField] private Button _pauseButton;

        public UnityEvent PauseClicked;
        public UnityEvent ContinueClicked;
        public UnityEvent RestartClicked => _pauseMenu.RestartClicked;
        public UnityEvent ExitToMainMenuClicked => _pauseMenu.ExitToMainMenuClicked;
        public LoadGameEvent LoadGameClicked => _pauseMenu.LoadGameClicked;
        public IntEvent SaveGameClicked => _pauseMenu.SaveGameClicked;
        public UnityEvent OnNextUnitClicked => _activeUnitPanel.NextUnitClicked;

        private void OnValidate()
        {
            Debug.Assert(_pauseMenu != null, $"{nameof(_pauseMenu)} need to be assigned", this);
            Debug.Assert(_activeUnitPanel != null, $"{nameof(_activeUnitPanel)} need to be assigned", this);
            Debug.Assert(_pauseButton != null, $"{nameof(_pauseButton)} need to be assigned", this);
        }
        private void Start()
        {
            _pauseButton.onClick.AddListener(OnPauseClicked);
            _pauseMenu.ContinueClicked.AddListener(OnContinueClicked);
        }

        private void OnContinueClicked()
        {
            _pauseButton.gameObject.SetActive(true);
            _pauseMenu.Deactivate();
            ContinueClicked?.Invoke();
        }

        private void OnPauseClicked()
        {
            _pauseButton.gameObject.SetActive(false);
            _pauseMenu.Activate();
            PauseClicked?.Invoke();
        }

        public void SetActiveUnit(Unit unit)
        {
            _activeUnitPanel.SetUnit(unit);
        }
        public void UpdateSaveLoadMenu()
        {
            _pauseMenu.UpdateSaveLoadMenu();
        }
    }
}