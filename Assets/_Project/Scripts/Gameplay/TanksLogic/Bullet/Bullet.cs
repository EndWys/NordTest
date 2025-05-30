using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Bullet
{
    public class Bullet : PoolObject
    {
        private BulletPool _pool;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetPool(BulletPool pool)
        {
            _pool = pool;
        }

        public void Init(Vector2 position, Vector2 direction, float speed)
        {
            transform.position = position;
            transform.up = direction;
            _rb.velocity = direction * speed;
        }

        public override void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public override void OnReleaseToPool()
        {
            gameObject.SetActive(false);
            _rb.velocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _pool.ReleaseObject(this);
        }
    }
}