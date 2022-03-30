using UnityEngine;
using UnityEngine.Events;

namespace GameStudioTest1
{
    [RequireComponent(typeof(Collider), typeof(GuidComponent))]
    public class DamageOnTouch : MonoBehaviour, IMemorizable
    {
        [SerializeField] private int _damage = 10;
        private Collider _collider;
        public UnityEvent Touched;

        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();


        public int Damage => _damage;
        public Collider Collider 
        { 
            get
            {
                if (_collider is null) _collider = GetComponent<Collider>();
                return _collider;
            } 
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<Unit>(out Unit unit))
            {
                unit.Health.ChangeBy(-Damage);
                Touched?.Invoke();
            }
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<Unit>(out Unit unit))
            {
                unit.Health.ChangeBy(-Damage);
                Touched?.Invoke();
            }
        }

        public Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(_damage), _damage);
            return mem;
        }

        public void SetFromMemento(Memento memento)
        {
            _damage = System.Convert.ToInt32(memento.TryGetValue(nameof(_damage)));
        }
    }
}