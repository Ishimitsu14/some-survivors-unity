using System;
using UnityEngine;

public class WeaponCollisionController : MonoBehaviour
{
    public Action<GameObject> OnEnemyInWeaponRange;
    public Action<GameObject> OnEnemyOutWeaponRange;
    
    private WeaponStore _store;
    private BoxCollider2D _collider;
    
    private void Awake()
    {
        TryGetComponent(out _collider);
        TryGetComponent(out _store);
    }

    private void Start()
    {
        _collider.size = new Vector2(_store.weaponConfig.range, _store.weaponConfig.range);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_collider != null && collider.CompareTag(GameTags.Enemy))
        {
            OnEnemyInWeaponRange.Invoke(collider.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider != null && collider.CompareTag(GameTags.Enemy))
        {
            OnEnemyOutWeaponRange?.Invoke(collider.gameObject);
        }
    }
}