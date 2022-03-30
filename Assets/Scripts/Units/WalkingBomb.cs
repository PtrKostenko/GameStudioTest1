using GameStudioTest1.AI;
using System.Collections;
using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(Route), typeof(Movement), typeof(DamageOnTouch))]
    public class WalkingBomb : AIUnit, IHaveRoute, IHaveDamageOnTouch
    {
        private Route _route;
        private Movement _movement;
        private DamageOnTouch _damageOnTouch;

        public Route Route => _route;
        public DamageOnTouch DamageOnTouch => _damageOnTouch;

        protected override void Init()
        {
            base.Init();
            _route = GetComponent<Route>();
            _movement = GetComponent<Movement>();
            _damageOnTouch = GetComponent<DamageOnTouch>();
            _damageOnTouch.Touched.AddListener(OnTouched);
        }

        private void Update()
        {
            Route.UpdateRouteInHorizontal(transform.position);
            if (Route.GetNextUnreachedPosition(out Vector3 nextUnreachedPos))
            {
                var moveAxis = new Vector3(nextUnreachedPos.x, this.transform.position.y, nextUnreachedPos.z);
                LookAt(moveAxis);
                _movement.MoveTowards(moveAxis);
            }
        }

        private void OnTouched()
        {
            Die();
        }
    }
}