using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Animator _ballAnimator;
    [SerializeField] RuntimeAnimatorController _shotUpOverride;

    private RuntimeAnimatorController _originAnimController;
    bool _isBall;

    Animator _currentAnimator;

    private void Start()
    {
        _currentAnimator = _playerAnimator;
        _originAnimController = _currentAnimator.runtimeAnimatorController;
    }

    public void Move(float move)
    {
        _currentAnimator.SetFloat("run", Mathf.Abs(move));
    }
    public void Jump(bool jump)
    {
        //if (_isBall) return;
        _currentAnimator.SetBool("jump", jump);
    }

    public void DoubleJump()
    {
        //if (_isBall) return;
        _currentAnimator.SetTrigger("doubleJump");
    }

    public void Shoot()
    {
        //if (_isBall) return;
        _currentAnimator.SetTrigger("shoot");
    }

    public void ShootUp()
    {
        //if (_isBall) return;
        //_currentAnimator.SetTrigger("shootUp");
        _currentAnimator.SetTrigger("shoot");
    }

    public void OverrideAnimator()
    {
        _currentAnimator.runtimeAnimatorController = _shotUpOverride;

    }

    public void RestoreAnimator()
    {
        _currentAnimator.runtimeAnimatorController = _originAnimController;

    }

    public void ActivateBall()
    {
        _isBall = true;
        _currentAnimator = _ballAnimator;
    }

    public void DeativateBall()
    {
        _isBall = false;
        _currentAnimator = _playerAnimator;
    }

    public void DisableAnimator()
    {
        _playerAnimator.enabled = false;
    }

    public void EnableAnimator()
    {
        _playerAnimator.enabled = true;
    }
}
