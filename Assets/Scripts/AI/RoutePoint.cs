using Newtonsoft.Json;
using UnityEngine;

namespace GameStudioTest1.AI
{
    [System.Serializable]
    public class RoutePoint
    {
        [JsonIgnore] public Vector3 Position;
        public bool Reached;

        public RoutePoint(bool reached)
        {
            Reached = reached;
        }
    }
}