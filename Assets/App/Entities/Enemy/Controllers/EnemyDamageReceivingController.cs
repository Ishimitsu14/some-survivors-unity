using UnityEngine;

public class EnemyDamageReceivingController : MonoBehaviour
{
  private EnemyStore _store;

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    EventManager<OnProjectileHit>.WithScope(gameObject.GetInstanceID()).On += OnProjectileHit;
  }

  private void OnDisable()
  {
    EventManager<OnProjectileHit>.WithScope(gameObject.GetInstanceID()).On -= OnProjectileHit;
  }

  private void OnProjectileHit(OnProjectileHit data)
  {
    _store.currentHealth.Value -= data.damage;
  }
}