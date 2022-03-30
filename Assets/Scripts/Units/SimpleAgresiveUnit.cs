using GameStudioTest1.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(Sense), typeof(Movement), typeof(DamageOnTouch))]
    public class SimpleAgresiveUnit : AIUnit, ICanSense, IHaveDamageOnTouch
    {
        private Sense _sense;
        private Movement _movement;
        private DamageOnTouch _damageOnTouch;
        public Sense Sense => _sense;

        public DamageOnTouch DamageOnTouch => _damageOnTouch;

        protected override void Init()
        {
            base.Init();
            _sense = GetComponent<Sense>();
            _movement = GetComponent<Movement>();
            _damageOnTouch = GetComponent<DamageOnTouch>();
            _damageOnTouch.Touched.AddListener(OnTouched);
        }

        private void Update()
        {
            var targets = _sense.Sensing();
            if (targets != null && targets.Count > 0)
            {
                var choosedTarget = ChooseTarget(targets).transform.position;
                choosedTarget.y = this.transform.position.y;
                _movement.MoveTowards(choosedTarget);
                LookAt(choosedTarget);
            }
        }

        private void OnTouched()
        {
            Die();
        }
    }
}