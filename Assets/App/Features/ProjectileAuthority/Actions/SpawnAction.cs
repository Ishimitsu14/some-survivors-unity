using System;
using AutoDeconstruct;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectileAuthority
{
  [AutoDeconstruct]
  public partial struct SpawnAction
  {
    public Vector3 startPoint;
    public Projectile.Kind kind;
    public PrefabConfigSchema prefabConfig;
  }

  [ActionHandler(typeof(SpawnAction))]
  public class SpawnProjectileActionHandler : IActionHandler<SpawnAction, GameObject>
  {
    public GameObject Execute(SpawnAction action)
    {
      var (startPoint, kind, prefabConfig) = action;
      var prefab = GetProjectilePrefab(prefabConfig, kind);

      var projectile = Object.Instantiate(prefab, startPoint, Quaternion.identity);

      projectile.name = projectile.GetInstanceID().ToString();

      return projectile;
    }

    private static GameObject GetProjectilePrefab(PrefabConfigSchema config, Projectile.Kind kind)
    {
      return kind switch
      {
        Projectile.Kind.Fireball => config.Fireball,
        Projectile.Kind.Dagger => config.Dagger,
        _ => throw new Exception($"Projectile Prefab for {kind} kind not defined")
      };
    }
  }
}
