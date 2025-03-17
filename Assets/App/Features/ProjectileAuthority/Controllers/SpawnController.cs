using UnityEngine;

namespace ProjectileAuthority
{
  public class SpawnController : MonoBehaviour
  {
    private Store _store;

    private void Awake()
    {
      TryGetComponent(out _store);
    }

    private void OnEnable()
    {
      EventManager<Projectile.OnSpawnRequest>.On += OnProjectileSpawn;
    }

    private void OnDisable()
    {
      EventManager<Projectile.OnSpawnRequest>.On -= OnProjectileSpawn;
    }

    private void OnProjectileSpawn(Projectile.OnSpawnRequest data)
    {
      var (kind, damage, moveSpeed, destructionRadius, lifeTime, startPoint, possibleTargets, weaponDirection) =
        data;

      var projectileConfig = new ProjectileConfigSchema
      {
        kind = kind,
        damage = damage,
        moveSpeed = moveSpeed,
        destructionRadius = destructionRadius,
        lifeTime = lifeTime,
        startPoint = startPoint,
        possibleTargets = possibleTargets,
        weaponDirection = weaponDirection
      };

      var projectile = ActionManager.Instance.Execute<SpawnAction, GameObject>(new SpawnAction
      {
        startPoint = startPoint,
        kind = kind,
        prefabConfig = _store.prefabConfig
      });

      _store.projectiles.Add(projectile, projectileConfig);
    }
  }
}
