using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GameStudioTest1.AI
{
    public class AiDirector : MonoBehaviour
    {
        [Inject] private List<ControllableUnit> _playerUnits;
        [Inject] private List<AIUnit> _aiUnits;

        public void Init()
        {
            foreach (AIUnit ai in _aiUnits)
            {
                if (ai is ICanSense iCanSense) iCanSense.Sense.SetTargets(_playerUnits.Cast<Unit>().ToList());
            }
        }
    }
}