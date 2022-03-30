using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameStudioTest1
{
    public sealed class ExitArea : MonoBehaviour
    {
        [SerializeField] private AreaTrigger _areaTrigger;

        public UnitEvent AreaEntered => _areaTrigger.AreaEntered;
        public UnitEvent AreaExited => _areaTrigger.AreaExited;
        public bool IsInArea(Unit unit) => _areaTrigger.IsInTrigger(unit);
    } 
}
