using System;
using UnityEngine;

public class WeaponCollisionController : MonoBehaviour
{
  private CircleCollider2D _collider;

  private WeaponStore _store;

  private void Awake()
  {
    TryGetComponent(out _collider);
    TryGetComponent(out _store);
  }

  private void Start()
  {
    _collider.radius = _store.weaponConfig.range;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (_collider == null || !other.CompareTag(GameTags.Enemy)) return;
    OnEnemyInWeaponRange?.Invoke(other.gameObject);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other != null && other.CompareTag(GameTags.Enemy)) OnEnemyOutWeaponRange?.Invoke(other.gameObject);
  }

  public event Action<GameObject> OnEnemyInWeaponRange;
  public event Action<GameObject> OnEnemyOutWeaponRange;
}