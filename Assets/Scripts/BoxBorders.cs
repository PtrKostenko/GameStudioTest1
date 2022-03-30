using UnityEngine;

namespace GameStudioTest1
{
    [System.Serializable()]
    public sealed class BoxBorders : AreaBorders
    {
        public Bounds Borders;

        public override bool IsInArea(Unit unit)
        {
            return Borders.Contains(unit.transform.position);
        }
    }
}
