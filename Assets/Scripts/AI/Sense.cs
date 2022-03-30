using System.Collections.Generic;
using UnityEngine;

namespace GameStudioTest1.AI
{
    public abstract class Sense : MonoBehaviour
    {
        protected List<Unit> Targets;
        public void SetTargets(List<Unit> units)
        {
            Targets = units;
        }
        public abstract List<Unit> Sensing();

    }
}