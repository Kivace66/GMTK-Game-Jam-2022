using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPillController : MonoBehaviour
{

    //TODO: Flash player sprite when pills is in effect
    //Count down effect time
    //Start flashing when reach flash start time
    //Apply effect to player
    //Reset effect when effect time over

    private float _duration;
    private float _effectCounter;
    private float _flashCounter;
    private float _startFlashTime;
    private float _flashInterval;
    [SerializeField] SpriteRenderer[] _playerSprites;
    private bool _canTakePill;
    private PlayerController _playerController;
    private bool _startEffect;
    private Pill _currentPill;
    private int _originMaxHealth;
    private int _originCurrentHealth;

    private Action restoreState;

    public enum PillType
    {
        ChangeSize,
        Invincible,
        ChangeSpeed,
    }

    // Start is called before the first frame update
    void Start()
    {
        _canTakePill = true;
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_startEffect && !_canTakePill)
        {
            if (_effectCounter > 0)
            {
                _effectCounter -= Time.deltaTime;
                if (_effectCounter <= _startFlashTime)
                {
                    Flash();
                }
            }
            else
            {
                restoreState.Invoke();
            }
        }
    }

    private void Flash()
    {
        _flashCounter -= Time.deltaTime;
        if (_flashCounter <= 0)
        {
            foreach (SpriteRenderer sr in _playerSprites)
            {
                sr.enabled = !sr.enabled;
            }
            _flashCounter = _flashInterval;
        }
    }

    public bool CanTakePill()
    {
        return _canTakePill;
    }

    public void TakePill(Pill pill, PillType type)
    {
        _currentPill = pill;
        _canTakePill = false;
        _duration = pill.duration;
        _effectCounter = _duration;
        _startFlashTime = pill.startFlashTime;
        _flashInterval = pill.flashInterval;

        foreach (SpriteRenderer sr in _playerSprites)
        {
            sr.color = pill.targetColor;
        }
        _flashCounter = pill.flashInterval;

        Vector3 scale = gameObject.transform.localScale;
        scale.x = pill.targetXScale;
        scale.y = pill.targetYScale;
        gameObject.transform.localScale = scale;
        _originCurrentHealth = PlayerHealthController.GetInstance().GetCurrentHealth();
        _originMaxHealth = PlayerHealthController.GetInstance().GetMaxHealth();
        _playerController.SetSpeedScale(pill.targetSpeedScale);
        _playerController.SetJumpForceScale(pill.targetJumpForceScale);
        PlayerHealthController.GetInstance().SetHealthScale(pill.targetHPScale);
        _startEffect = true;
        restoreState = ResetState;

        switch (_currentPill.type)
        {
            case PillType.Invincible:
                _playerController.ToggleInvertControl();
                break;
            case PillType.ChangeSpeed:
                if (!pill.canStop)
                {
                    _playerController.ToggleCanStop();
                }
                break;
        }
    }

    public void ResetState()
    {
        Vector3 scale = gameObject.transform.localScale;
        scale.x = 1;
        scale.y = 1;
        gameObject.transform.localScale = scale;
        _playerController.SetSpeedScale(1);
        _playerController.SetJumpForceScale(1);
        PlayerHealthController.GetInstance().SetMaxHealth(_originMaxHealth);
        PlayerHealthController.GetInstance().SetCurrentHealth(_originCurrentHealth);
        _startEffect = false;


        foreach (SpriteRenderer sr in _playerSprites)
        {
            sr.enabled = true;
            sr.color = new Color(255, 255, 255);
        }
        _canTakePill = true;
        switch (_currentPill.type)
        {
            case PillType.Invincible:
                _playerController.ToggleInvertControl();
                break;
            case PillType.ChangeSpeed:
                if (!_currentPill.canStop)
                {
                    _playerController.ToggleCanStop();
                }
                break;
        }
    }

}
