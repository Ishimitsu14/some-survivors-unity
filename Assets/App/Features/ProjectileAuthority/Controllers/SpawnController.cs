using Projectile;
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
      EventManager<OnSpawnRequest>.On += OnProjectileSpawn;
    }

    private void OnDisable()
    {
      EventManager<OnSpawnRequest>.On -= OnProjectileSpawn;
    }

    private void OnProjectileSpawn(OnSpawnRequest data)
    {
      var (kind, damage, moveSpeed, destructionRadius, lifeTime, startPoint, possibleTargets, weaponDirection, prefab) =
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
        weaponDirection = weaponDirection,
        prefab = prefab
      };

      var projectile = ActionManager.Instance.Execute<SpawnAction, GameObject>(new SpawnAction
      {
        startPoint = startPoint,
        kind = kind,
        prefab = prefab
      });

      _store.projectiles.Add(projectile, projectileConfig);
    }
  }
}