using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStudioTest1
{
    public class SavedGameInfo
    {
        public int SlotNum;
        public string CurrentLevelName;
        public string Date;
        public List<Memento> Mementos;

        public SavedGameInfo(int slotNum, string levelName, List<Memento> mementos)
        {
            SlotNum = slotNum;
            CurrentLevelName = levelName;
            Mementos = mementos;
            Date = DateTime.Now.ToString();
        }

        public override string ToString()
        {
            return $"{SlotNum}: {CurrentLevelName} {Date} {Environment.NewLine}{Mementos}";
        }
    }
}
