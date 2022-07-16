using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bullet;

public class FireController : MonoBehaviour
{
    [SerializeField] Bullet _bullectPrefab;
    [SerializeField] Transform _firePosition;
    [SerializeField] Transform _fireUpPosition;
    [SerializeField] GameObject _bomb;
    [SerializeField] Transform _bombPosition;
    [SerializeField] int _bulletPoolInitialCount = 10;

    public int poolCount = 0;

    ObjectPool<Bullet> _bulletPool;
    PlayerAnimation _playerAnimation;
    Transform _whereToFire;

    AbilitiesController _abilitiesController;

    public bool _isStanding { get; set; } = true;

    private Direction _direction = Direction.right;

    public void SetDirection(Direction direction)
    {
        _direction = direction;
    }

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _abilitiesController = GetComponent<AbilitiesController>();
        _whereToFire = _firePosition;
    }

    private void OnEnable()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, _bullectPrefab, _firePosition, _bulletPoolInitialCount);
    }

    private void OnDisable()
    {
        _bulletPool.ClearPool();
    }

    private void Update()
    {
        poolCount = _bulletPool.GetCount();
        DecideFirePosition();
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (_isStanding)
            {
                Bullet bullet = _bulletPool.Dequeue();
                bullet.SetDirection(_direction);
                bullet.transform.position = _whereToFire.position;
                if(bullet.GetObjectPool() == null)
                {
                    bullet.SetObjectPool(_bulletPool);
                }
                if (Input.GetKey(KeyCode.W))
                {
                    _playerAnimation.ShootUp();
                }
                else
                {
                    _playerAnimation.Shoot();
                }
            }
            else if(_abilitiesController.canDropBomb)
            {
                Instantiate(_bomb, _bombPosition.position, _bombPosition.rotation);
            }
        }
    }

    private void DecideFirePosition()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _whereToFire = _fireUpPosition;
        }
        else
        {
            _whereToFire = _firePosition;
        }
    }

    private Bullet CreateBullet(Bullet gameObject, Transform transform)
    {
        Bullet bullet = GameObject.Instantiate(gameObject, transform.position, transform.rotation);
        bullet.gameObject.SetActive(false);
        bullet.SetObjectPool(_bulletPool);
        return bullet;
    }
}
