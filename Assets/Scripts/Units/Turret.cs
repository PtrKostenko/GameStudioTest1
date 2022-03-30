using GameStudioTest1.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(Sense), typeof(RangeWeapon))]
    public class Turret : AIUnit, ICanShoot, ICanSense
    {
        private Sense _sense;
        private RangeWeapon _rangeWeapon;

        public Sense Sense => _sense;
        public RangeWeapon RangeWeapon => _rangeWeapon;


        private void Update()
        {
            var targets = _sense.Sensing();
            if (targets != null && targets.Count > 0)
            {
                LookAt(ChooseTarget(targets).transform.position);
                if (RangeWeapon.CanShoot)
                    RangeWeapon.Shoot();
            }
        }

        protected override void Init()
        {
            base.Init();
            _sense = GetComponent<Sense>();
            _rangeWeapon = GetComponent<RangeWeapon>();
        }

    }
}