using System.Collections;
using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(Collider))]
    public sealed class AreaTrigger : MonoBehaviour
    {
        private Collider _collider;

        public UnitEvent AreaEntered = new UnitEvent();
        public UnitEvent AreaExited = new UnitEvent();

        private void Reset()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        public bool IsInTrigger(Unit unit)
        {
            return _collider.bounds.Intersects(unit.Collider.bounds);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Unit>(out Unit unit))
            {
                AreaEntered?.Invoke(unit);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Unit>(out Unit unit))
            {
                AreaExited?.Invoke(unit);
            }
        }
    }
}