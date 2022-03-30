using Newtonsoft.Json;
using UnityEngine;


namespace GameStudioTest1
{
    public class PlayerPrefsSaveSystem : SaveSystem
    {
        private const string KeyPrefix = "Slot";
        public override SavedGameInfo LoadInfo(int slotNum)
        {
            SavedGameInfo savedGameInfo = null;
            var keyName = GetKeyName(slotNum);
            if (PlayerPrefs.HasKey(keyName))
            {
                string json = PlayerPrefs.GetString(keyName);
                savedGameInfo = JsonConvert.DeserializeObject<SavedGameInfo>(json);
            }
                
            return savedGameInfo;
        }

        public override void Save(SavedGameInfo savedGameInfo)
        {
            var json = JsonConvert.SerializeObject(savedGameInfo);
            var keyName = GetKeyName(savedGameInfo.SlotNum);
            PlayerPrefs.SetString(keyName, json);
        }

        private string GetKeyName(int slotNum)
        {
            return $"{KeyPrefix}{slotNum}";
        }
    }
}
