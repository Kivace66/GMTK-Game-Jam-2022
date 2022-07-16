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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: Prompt to take pill
            PlayerPillController controller = other.GetComponentInParent<PlayerPillController>();
            controller.TakePill(gameObject, PlayerPillController.PillType.ChangeSize);

        }
    }

}
