using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Direction
    {
        up,
        left,
        right
    }

    [SerializeField] float _bulletSpeed;
    [SerializeField] ParticleSystem _impactEffect;
    [SerializeField] float _lifeTime = 10f;
    [SerializeField] int _damageAmount = 1;


    ObjectPool<Bullet> _pool;

    Rigidbody2D _rb;
    Direction _direction;
    Vector2 _moveDir = Vector2.right;

    public void SetObjectPool(ObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    public ObjectPool<Bullet> GetObjectPool()
    {
        return _pool;
    }

    public void SetDirection(Direction direction)
    {
        _direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (_facingRight) _moveDir = Vector2.right;
        //else _moveDir = Vector2.left;
        switch (_direction)
        {
            case Direction.right:
                _moveDir = Vector2.right;
                break;
            case Direction.left:
                _moveDir = Vector2.left;
                break;
            case Direction.up:
                _moveDir = Vector2.up;
                break;
        }
        _rb.velocity = _moveDir * _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>()?.Damage(_damageAmount);
        }
        _pool.Enqueue(this);
        gameObject.SetActive(false);
        if (_impactEffect != null)
        {
            Instantiate(_impactEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnBecameInvisible()
    {
        _pool.Enqueue(this);
        gameObject.SetActive(false);
    }
}
