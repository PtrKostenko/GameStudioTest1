using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace GameStudioTest1.UI
{
    public class SaveLoadSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNameTMP;
        [SerializeField] private TMP_Text _dateTMP;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _saveButton;

        private SavedGameInfo _saved;
        private int _slotNum;

        public LoadGameEvent LoadClicked = new LoadGameEvent();
        public IntEvent SaveClicked = new IntEvent();
        public bool NoSavesAllowed { get; set; }

        private void OnValidate()
        {
            Debug.Assert(_levelNameTMP != null, $"{nameof(_levelNameTMP)} need to be assigned", this);
            Debug.Assert(_dateTMP != null, $"{nameof(_dateTMP)} need to be assigned", this);
            Debug.Assert(_loadButton != null, $"{nameof(_loadButton)} need to be assigned", this);
            Debug.Assert(_saveButton != null, $"{nameof(_saveButton)} need to be assigned", this);
        }

        private void Start()
        {
            _loadButton.onClick.AddListener(OnLoadClicked);
            _saveButton.onClick.AddListener(OnSaveClicked);
        }

        private void OnSaveClicked()
        {
            SaveClicked?.Invoke(_slotNum);
        }

        private void OnLoadClicked()
        {
            LoadClicked?.Invoke(_saved);
        }

        public void SetSavedGameInfo(SavedGameInfo saved, int defaultSlotNum)
        {
            _slotNum = defaultSlotNum;
            _saved = saved;
            if (saved is null)
            {
                _loadButton.interactable = false;
                _levelNameTMP.text = "Empty";
                _dateTMP.text = "-";
            }
            else
            {
                _loadButton.interactable = !NoSavesAllowed;
                _levelNameTMP.text = saved.CurrentLevelName;
                _dateTMP.text = saved.Date;
            }
        }
    }
}