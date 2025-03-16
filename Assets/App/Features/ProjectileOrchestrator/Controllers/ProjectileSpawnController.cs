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
    var (kind, damage, moveSpeed, destructionRadius, lifeTime, startPoint, possibleTargets, weaponDirection) =
      data;

    ActionManager.Instance.Execute(new SpawnProjectileAction
    {
      config = new ProjectileConfigSchema
      {
        kind = kind,
        damage = damage,
        moveSpeed = moveSpeed,
        destructionRadius = destructionRadius,
        lifeTime = lifeTime,
        startPoint = startPoint,
        possibleTargets = possibleTargets,
        weaponDirection = weaponDirection
      },
      store = _store
    });
  }
}