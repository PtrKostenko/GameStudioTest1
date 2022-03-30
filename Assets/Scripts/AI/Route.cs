using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameStudioTest1.AI
{
    [RequireComponent(typeof(GuidComponent))]
    public class Route : MonoBehaviour, IMemorizable
    {
        [SerializeField] private RoutePoint[] _points;
        [SerializeField] private bool _looped;
        private bool _isFinished = false;
        private const float ReachedThreshold = 0.5f;

        public RoutePoint[] RoutePoints => _points;
        public bool IsFinished => !_looped && _isFinished;
        public bool IsLooped => _looped;
        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();


        private void OnValidate()
        {
            Debug.Assert(_points != null, $"{nameof(_points)} need to be assigned", this);
        }

        public void UpdateRoute(Vector3 currentPos)
        {
            if (GetNextUnreachedPoint(out RoutePoint nextPoint))
            {
                var distance = Vector3.Distance(nextPoint.Position, currentPos);
                if (distance < ReachedThreshold)
                {
                    nextPoint.Reached = true;
                }
            }
        }

        public void UpdateRouteInHorizontal(Vector3 currentPos)
        {
            if (GetNextUnreachedPoint(out RoutePoint nextPoint))
            {
                var curXZ = currentPos;
                curXZ.y = 0;
                var nextXZ = nextPoint.Position;
                nextXZ.y = 0;

                var distance = Vector3.Distance(curXZ, nextXZ);
                if (distance < ReachedThreshold)
                {
                    nextPoint.Reached = true;
                }
            }
        }

        public bool GetNextUnreachedPosition(out Vector3 unreachedPosition)
        {
            bool getted = GetNextUnreachedPoint(out RoutePoint routePoint);
            unreachedPosition = getted ? routePoint.Position : Vector3.zero;
            return getted;
        }

        private bool GetNextUnreachedPoint(out RoutePoint unreachedPoint)
        {
            unreachedPoint = null;
            foreach (RoutePoint rp in _points)
            {
                if (!rp.Reached)
                {
                    unreachedPoint = rp;
                    return true;
                }
            }

            if (_looped)
            {
                foreach (RoutePoint rp in _points)
                {
                    rp.Reached = false;
                }
                unreachedPoint = _points[0];
                return true;
            }

            //route finished
            return false;
        }

        public Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(_points), _points.Select(p => p.Reached).ToList());
            return mem;
        }

        public void SetFromMemento(Memento memento)
        {
            var rps = ((Newtonsoft.Json.Linq.JArray)memento.TryGetValue(nameof(_points))).ToObject<List<bool>>();
            var i = 0;
            foreach (var item in rps)
            {
                _points[i].Reached = item;
                i++;
            }
        }
    }
}