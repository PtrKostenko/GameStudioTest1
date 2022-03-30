using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(DamageOnTouch), typeof(Movement), typeof(GuidComponent))]
    public class Projectile : MonoBehaviour, IHaveDamageOnTouch, IMemorizable
    {
        [SerializeField] private float _lifetime = 15;

        private Movement _movement;
        private DamageOnTouch _damageOnTouch;
        private float _lifeTimer = 0;

        private const string PositionXKey = "positionX";
        private const string PositionYKey = "positionY";
        private const string PositionZKey = "positionZ";
        private const string RotationXKey = "rotationX";
        private const string RotationYKey = "rotationY";
        private const string RotationZKey = "rotationZ";

        public DamageOnTouch DamageOnTouch => _damageOnTouch;
        public bool IsActive => gameObject.activeInHierarchy;

        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();

        private void Start()
        {
            _movement = GetComponent<Movement>();
            _damageOnTouch = GetComponent<DamageOnTouch>();
            _damageOnTouch.Touched.AddListener(OnTouched);
        }

        private void OnTouched()
        {
            Deactivate();
        }

        private void Update()
        {
            _lifeTimer += Time.deltaTime;
            _movement.Forward();
            if (_lifeTimer > _lifetime)
            {
                Deactivate();
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _lifeTimer = 0;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public virtual Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(IsActive), IsActive);
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
            gameObject.SetActive(System.Convert.ToBoolean(memento.TryGetValue(nameof(IsActive))));
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