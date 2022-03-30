using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameStudioTest1
{
    public class LevelArea : MonoBehaviour
    {
        [SerializeField] private AreaBorders _areaBorders;
        [SerializeField] private ExitArea _exitArea;

        public bool IsInsideArea(Unit unit) => _areaBorders.IsInArea(unit);
        public bool IsInsideArea(Vector3 point) => _areaBorders.IsInArea(point);
        public bool IsInExitArea(Unit unit) => _exitArea.IsInArea(unit);

        public UnitEvent ExitAreaEntered => _exitArea.AreaEntered;
        public UnitEvent ExitAreaExited => _exitArea.AreaExited;
    } 
}
