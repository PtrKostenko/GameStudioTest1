using Newtonsoft.Json;
using System.IO;
using UnityEngine;


namespace GameStudioTest1
{
    public class JsonSaveSystem : SaveSystem
    {
        private string _directory => Path.Combine(Application.persistentDataPath, "Saves");
        private const string FilePrefix = "Slot";
        public override SavedGameInfo LoadInfo(int slotNum)
        {
            SavedGameInfo savedGameInfo = null;
            var path = Path.Combine(_directory, GetFileName(slotNum));
            
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                savedGameInfo = JsonConvert.DeserializeObject<SavedGameInfo>(json);
            }
            return savedGameInfo;
        }

        public override void Save(SavedGameInfo savedGameInfo)
        {
            var json = JsonConvert.SerializeObject(savedGameInfo);
            var path = Path.Combine(_directory, GetFileName(savedGameInfo.SlotNum));
            if (!Directory.Exists(_directory)) Directory.CreateDirectory(_directory);
            File.WriteAllText(path, json);
        }

        private string GetFileName(int slotNum)
        {
            return $"{FilePrefix}{slotNum}";
        }
    }
}
