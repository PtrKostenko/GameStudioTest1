using UnityEngine;

namespace GameStudioTest1
{
    public abstract class AreaBorders : MonoBehaviour
    {
        public abstract bool IsInArea(Unit unit);
        public abstract bool IsInArea(Vector3 point);
    }
}