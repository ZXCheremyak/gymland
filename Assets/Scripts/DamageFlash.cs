using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime = 0.25f;

    private SpriteRenderer _spriteRenderer;
    private Material material;

    private Coroutine _damageFlashCoroutine;

    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        material = _spriteRenderer.material;
    }

    public void CallDamageFlash()
    {
        _damageFlashCoroutine = StartCoroutine("DamageFlasher");
    }

    private IEnumerator DamageFlasher()
    {
        SetFlashColor();

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while(elapsedTime < _flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / _flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    void SetFlashColor()
    {
        material.SetColor("_FlashColor", _flashColor);
    }

    void SetFlashAmount(float amount)
    {
        material.SetFloat("_FlashAmount", amount);
    }
}
