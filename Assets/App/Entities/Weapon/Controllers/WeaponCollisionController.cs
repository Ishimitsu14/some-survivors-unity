using System;
using UnityEngine;

public class WeaponCollisionController : MonoBehaviour
{
  private CircleCollider2D _collider;

  private WeaponStore _store;
  public Action<GameObject> OnEnemyInWeaponRange;
  public Action<GameObject> OnEnemyOutWeaponRange;

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
    if (_collider != null && other.CompareTag(GameTags.Enemy)) OnEnemyInWeaponRange.Invoke(other.gameObject);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other != null && other.CompareTag(GameTags.Enemy)) OnEnemyOutWeaponRange?.Invoke(other.gameObject);
  }
}