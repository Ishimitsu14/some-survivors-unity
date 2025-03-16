using UnityEngine;

public class ProjectileSpawnController : MonoBehaviour
{
  private ProjectileStore _store;

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    EventManager<OnProjectileSpawn>.On += OnProjectileSpawn;
  }

  private void OnDisable()
  {
    EventManager<OnProjectileSpawn>.On -= OnProjectileSpawn;
  }

  private void OnProjectileSpawn(OnProjectileSpawn data)
  {
    var (kind, attackType, damage, moveSpeed, destructionRadius, lifeTime, startPoint, quantity, possibleTargets) = data;

    ActionManager.Instance.Execute(new SpawnProjectileAction
    {
      config = new ProjectileConfigSchema
      {
        kind = kind,
        attackType = attackType,
        damage = damage,
        moveSpeed = moveSpeed,
        destructionRadius = destructionRadius,
        lifeTime = lifeTime,
        startPoint = startPoint,
        quantity = quantity,
        possibleTargets = possibleTargets
      },
      store = _store
    });
  }
}
