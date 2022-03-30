using UnityEngine;
using UnityEngine.Events;

namespace GameStudioTest1
{
    [RequireComponent(typeof(Health), typeof(Collider), typeof(Rigidbody)), RequireComponent(typeof(GuidComponent))]
    public class Unit : MonoBehaviour, IMemorizable
    {
        [SerializeField] private string _unitName;
        private Health _health;
        private Collider _collider;
        private Rigidbody _rigidBody;
        private bool _isDead = false;

        private const string PositionXKey = "positionX";
        private const string PositionYKey = "positionY";
        private const string PositionZKey = "positionZ";
        private const string RotationXKey = "rotationX";
        private const string RotationYKey = "rotationY";
        private const string RotationZKey = "rotationZ";


        public UnitEvent Died = new UnitEvent();


        public string UnitName => _unitName;
        public Health Health => _health;
        public Collider Collider => _collider;
        public Rigidbody RigidBody => _rigidBody;
        public bool IsDead => _isDead;
        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();


        protected void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _health = GetComponent<Health>();
            _collider = GetComponent<Collider>();
            _rigidBody = GetComponent<Rigidbody>();
            _health.ValueExpired.AddListener(Die);
        }


        protected void LookAt(Vector3 pos)
        {
            var aimVector3 = pos - transform.position;
            aimVector3.y = 0;
            var rotation = Quaternion.LookRotation(aimVector3);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            transform.LookAt(pos);
        }

        protected virtual void Die()
        {
            gameObject.SetActive(false);
            _isDead = true;
            Died?.Invoke(this);
        }


        public virtual Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(_isDead), _isDead);
            mem.AddKeyValue(PositionXKey, transform.position.x);
            mem.AddKeyValue(PositionYKey, transform.position.y);
            mem.AddKeyValue(PositionZKey, transform.position.z);
            Vector3 rot = transform.rotation.eulerAngles;
            mem.AddKeyValue(RotationXKey, rot.x);
            mem.AddKeyValue(RotationYKey, rot.y);
            mem.AddKeyValue(RotationZKey, rot.z);
            return mem;
        }

        public virtual void SetFromMemento(Memento memento)
        {
            _isDead = System.Convert.ToBoolean(memento.TryGetValue(nameof(_isDead)));
            gameObject.SetActive(!_isDead);
            var px = System.Convert.ToSingle(memento.TryGetValue(PositionXKey));
            var py = System.Convert.ToSingle(memento.TryGetValue(PositionYKey));
            var pz = System.Convert.ToSingle(memento.TryGetValue(PositionZKey));
            var rx = System.Convert.ToSingle(memento.TryGetValue(RotationXKey));
            var ry = System.Convert.ToSingle(memento.TryGetValue(RotationYKey));
            var rz = System.Convert.ToSingle(memento.TryGetValue(RotationZKey));
            transform.position = new Vector3(px, py, pz);
            transform.rotation = Quaternion.Euler(rx, ry, rz);
        }
    } 
}
