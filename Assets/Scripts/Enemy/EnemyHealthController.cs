using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] int _totalHealth = 2;
    [SerializeField] GameObject _deathEffect;

    public void Damage(int damageAmount)
    {
        if((_totalHealth -= damageAmount) <= 0)
        {
            if (_deathEffect != null)
            {
                Instantiate(_deathEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
