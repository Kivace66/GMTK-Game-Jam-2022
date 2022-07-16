using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizePill : MonoBehaviour
{
    public float duration;
    public float startFlashTime ;
    public float flashInterval ;
    public float targetXScale ;
    public float targetYScale ;
    public float targetJumpForceScale ;
    public float targetHPScale ;
    public float targetSpeedScale ;
    public Color targetColor ;

    private float _flashCounter;
    private float _effectCounter;

    private bool _interactable;
    private PlayerPillController _playerPillController;

    private void Update()
    {
        if (_interactable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _playerPillController.TakePill(gameObject, PlayerPillController.PillType.ChangeSize);
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerPillController = other.GetComponentInParent<PlayerPillController>();
            UIController.GetInstance().EnableInteractText();
            _interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerPillController = null;
            UIController.GetInstance().DisableInteractText();
            _interactable = false;
        }
    }

}
