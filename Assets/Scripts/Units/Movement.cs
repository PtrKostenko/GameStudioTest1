using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(GuidComponent))]
    public class Movement : MonoBehaviour, IMemorizable
    {
        [SerializeField] private float _speed = 5;
     
        public float Speed => _speed;
        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();

        public Vector3 Move(Vector3 vector)
        {
            var newPos = transform.position;
            newPos += vector * _speed * Time.deltaTime;
            transform.position = newPos;
            return newPos;
        }

        public Vector3 GetPossiblePosition(Vector3 vector)
        {
            var newPos = transform.position;
            newPos += vector * _speed * Time.deltaTime;
            return newPos;
        }

        public Vector3 Forward()
        {
            return Move(transform.forward);
        }


        public Vector3 MoveTowards(Vector3 to)
        {
            var newPos = Vector3.MoveTowards(transform.position, to, _speed * Time.deltaTime);
            transform.position = newPos;
            return newPos;
        }

        public Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(_speed), _speed);
            return mem;
        }

        public void SetFromMemento(Memento memento)
        {
            _speed = System.Convert.ToSingle(memento.TryGetValue(nameof(_speed)));
        }
    }
}