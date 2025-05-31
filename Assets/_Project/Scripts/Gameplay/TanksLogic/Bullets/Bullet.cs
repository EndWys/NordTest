using Assets._Project.Scripts.ObjectPoolSytem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.TanksLogic.Bullets
{
    public class Bullet : PoolObject
    {
        private Rigidbody2D _rb;

        public event Action<Bullet> OnHit;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
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
            OnHit?.Invoke(this);
        }
    }
}