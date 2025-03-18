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
    EventManager<Projectile.OnHit>.WithScope(gameObject.GetInstanceID()).On += OnProjectileHit;
  }

  private void OnDisable()
  {
    EventManager<Projectile.OnHit>.WithScope(gameObject.GetInstanceID()).On -= OnProjectileHit;
  }

  private void OnProjectileHit(Projectile.OnHit data)
  {
    _store.currentHealth.Value -= data.damage;
  }
}
