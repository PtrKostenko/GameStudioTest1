using System.Collections.Generic;
using UnityEngine;

namespace GameStudioTest1.AI
{
    public class RadiusSense : Sense
    {
        [SerializeField] private float _radius;

        public override List<Unit> Sensing()
        {
            if (Targets is null)
                return null;
            return Targets.FindAll(t => IsSensing(t));
        }

        private bool IsSensing(Unit unit)
        {
            return Vector3.Distance(this.transform.position, unit.transform.position) < _radius;
        }
    }

}