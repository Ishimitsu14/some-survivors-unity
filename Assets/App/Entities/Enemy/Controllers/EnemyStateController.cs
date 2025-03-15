using System;
using System.Collections;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    private EnemyStore _store;
    private SpriteRenderer _spriteRenderer;
    
    private bool _isBlinking;
    private Color _originalColor;

    
    private void Awake()
    {
        TryGetComponent(out _store);
        TryGetComponent(out _spriteRenderer);
        
        _originalColor = _spriteRenderer.material.color;
    }

    private void OnEnable()
    {
        EventManager<OnWeaponDamageEnemy>.WithId(gameObject.GetInstanceID()).On += OnGetDamage;
    }

    private void Update()
    {
        if (_store.baseDataSchema.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnGetDamage(OnWeaponDamageEnemy weaponDamage)
    {
        _store.baseDataSchema.currentHealth -= weaponDamage.damage;
        if (!_isBlinking && _spriteRenderer != null)
        {
            StartCoroutine(BlinkEffect());
        }
    }
    
    private IEnumerator BlinkEffect()
    {
        _isBlinking = true;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.material.color = Color.gray;
            yield return new WaitForSeconds(0.1f);
            
            _spriteRenderer.material.color = _originalColor;
        }

        _isBlinking = false;
    }


    private void OnDisable()
    {
        EventManager<OnWeaponDamageEnemy>.WithId(gameObject.GetInstanceID()).On -= OnGetDamage;
    }
}