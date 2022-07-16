using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] Image _fadeScreen;
    [SerializeField] float _fadeSpeed;

    private static UIController _instance;

    bool _fadingToBlack, _fadingFromBlack;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (_fadingToBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));
            if(_fadeScreen.color.a == 1f)
            {
                _fadingToBlack = false;
            }
        }else if (_fadingFromBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
        }
        if (_fadeScreen.color.a == 1f)
        {
            _fadingFromBlack = false;
        }
    }

    public static UIController GetInstance()
    {
        return _instance;
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthBar.maxValue = maxHealth;
        _healthBar.value = currentHealth;
    }

    public void SetHealthBarLength(int maxHealth)
    {
        RectTransform rectTransform = _healthBar.GetComponent<RectTransform>();
        Rect rect = rectTransform.rect;
        rectTransform.sizeDelta = new Vector2(maxHealth * 30, rect.height);
    }

    public void StartFadeToBlack()
    {
        _fadingToBlack = true;
        _fadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        _fadingToBlack = false;
        _fadingFromBlack = true;

    }
}
