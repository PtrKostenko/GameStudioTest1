using System.Collections.Generic;
using UnityEngine;

namespace GameStudioTest1.AI
{
    public class SectorSense : Sense
    {
        [SerializeField, Range(0, 360)] private float _angle;
        [SerializeField, Min(0)] private float _radius;
        [SerializeField] private float dotToMinAngle;
        [SerializeField] private float dotToMaxAngle;
        public float Angle => _angle;
        public float Radius => _radius;

        public override List<Unit> Sensing()
        {
            if (Targets is null)
                return null;
            return Targets.FindAll(u => IsSensing(u));
        }

        private bool IsSensing(Unit unit)
        {
            bool inRadius = Vector3.Distance(this.transform.position, unit.transform.position) < _radius;
            var toOther = unit.transform.position - this.transform.position;
            dotToMinAngle = Vector3.Dot(Quaternion.Euler(0, -Angle / 2f + 90, 0) * this.transform.forward, toOther);
            dotToMaxAngle = Vector3.Dot(Quaternion.Euler(0, Angle / 2f - 90, 0) * this.transform.forward, toOther);
            bool inSector = dotToMinAngle > 0 && dotToMaxAngle > 0;

            return inRadius && inSector;
        }
    }

}