using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
  private EnemyStore _store;

  private void Awake()
  {
    Debug.Log(gameObject.GetInstanceID());
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    EventManager<OnProjectileHit>.WithId(gameObject.GetInstanceID()).On += OnProjectileHit;
  }

  private void OnDisable()
  {
    EventManager<OnProjectileHit>.WithId(gameObject.GetInstanceID()).On -= OnProjectileHit;
  }

  private void OnProjectileHit(OnProjectileHit data)
  {
    Debug.Log(data.damage);
    _store.currentHealth.Value -= data.damage;
  }
}
