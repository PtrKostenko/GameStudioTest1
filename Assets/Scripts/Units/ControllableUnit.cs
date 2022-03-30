using System.Collections;
using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(Movement))]
    public class ControllableUnit : Unit
    {
        [SerializeField] private Movement _movement;

        protected override void Init()
        {
            base.Init();
            _movement = GetComponent<Movement>();
        }

        public Vector3 Move(Vector3 vector) => _movement.Move(vector);
        public Vector3 GetPossiblePosition(Vector3 vector) => _movement.GetPossiblePosition(vector);

    }
}