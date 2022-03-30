using System.Collections.Generic;
using UnityEngine;

namespace GameStudioTest1
{
    [RequireComponent(typeof(GuidComponent))]
    public class RangeWeapon : MonoBehaviour, IMemorizable
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private float _reloadTime = 2;

        private List<Projectile> _projectiles = new List<Projectile>();
        private float _reloadTimer = 0;

        public bool CanShoot => _reloadTimer > _reloadTime;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public Vector3 ProjectileSpawnPoint => _projectileSpawnPoint.position;
        public Quaternion ProjectileSpawnRotation => _projectileSpawnPoint.rotation;
        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();


        private void OnValidate()
        {
            Debug.Assert(_projectilePrefab != null, $"{nameof(_projectilePrefab)} need to be assigned", this);
            Debug.Assert(_projectileSpawnPoint != null, $"{nameof(_projectileSpawnPoint)} need to be assigned", this);
        }

        private void Update()
        {
            _reloadTimer += Time.deltaTime;
        }

        public void Shoot()
        {
            var proj = GetProjectile();
            proj.transform.position = ProjectileSpawnPoint;
            proj.transform.rotation = ProjectileSpawnRotation;
            proj.Activate();
            _reloadTimer = 0;
        }

        private Projectile GetProjectile()
        {
            Projectile proj;
            proj = _projectiles.Find(p => !p.IsActive);

            if (proj == null)
            {
                proj = Instantiate<Projectile>(ProjectilePrefab, position: ProjectileSpawnPoint, rotation: Quaternion.identity);
                _projectiles.Add(proj);
            }

            return proj;
        }

        public Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(_reloadTime), _reloadTime);
            mem.AddKeyValue(nameof(_reloadTimer), _reloadTimer);
            return mem;
        }

        public void SetFromMemento(Memento memento)
        {
            _reloadTime = System.Convert.ToSingle(memento.TryGetValue(nameof(_reloadTime)));
            _reloadTimer = System.Convert.ToSingle(memento.TryGetValue(nameof(_reloadTimer)));
        }
    }
}