using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace GameStudioTest1.UI
{
    public class SaveLoadMenu : Menu
    {
        [SerializeField] private SaveLoadSlot[] _saveLoadSlots;

        [Inject] private SaveSystem _saveSystem;

        public LoadGameEvent LoadGameClicked = new LoadGameEvent();
        public IntEvent SaveGameClicked = new IntEvent();
        public bool NoSavesAllowed { get; set; }

        private void Start()
        {
            for (int i = 0; i < _saveLoadSlots.Length; i++)
            {
                SaveLoadSlot saveLoadSlot = _saveLoadSlots[i];
                saveLoadSlot.SaveClicked.AddListener(OnSaveClicked);
                saveLoadSlot.LoadClicked.AddListener(OnLoadClicked);
            }
        }

        private void OnSaveClicked(int slotNum)
        {
            SaveGameClicked?.Invoke(slotNum);
        }

        private void OnLoadClicked(SavedGameInfo savedGame)
        {
            LoadGameClicked?.Invoke(savedGame);
        }

        public override void Activate()
        {
            base.Activate();
            UpdateMenu();
        }

        public void UpdateMenu()
        {
            int slotsNum = _saveLoadSlots.Length;
            for (int i = 0; i < slotsNum; i++)
            {
                _saveLoadSlots[i].SetSavedGameInfo(_saveSystem.LoadInfo(i), defaultSlotNum: i);
                _saveLoadSlots[i].NoSavesAllowed = NoSavesAllowed;
            }
        }
    }
}