using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStudioTest1
{
    public abstract class SaveSystem
    {
        public abstract void Save(SavedGameInfo savedGameInfo);
        public abstract SavedGameInfo LoadInfo(int slotNum);
    } 
}
