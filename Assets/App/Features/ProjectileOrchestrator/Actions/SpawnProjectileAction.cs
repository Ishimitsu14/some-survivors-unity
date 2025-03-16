using AutoDeconstruct;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

[AutoDeconstruct]
public partial struct SpawnProjectileAction
{
  public ProjectileConfigSchema config;
  public ProjectileStore store;
}

[ActionHandler(typeof(SpawnProjectileAction))]
public class SpawnProjectileActionHandler : IActionHandler<SpawnProjectileAction>
{
  public void Execute(SpawnProjectileAction action)
  {
    var (config, store) = action;
    var prefab = GetProjectilePrefab(store.prefabConfig, config.kind);

    var projectile = Object.Instantiate(prefab, config.startPoint, Quaternion.identity);

    projectile.name = projectile.GetInstanceID().ToString();

    store.projectiles.Add(projectile.GetInstanceID(), config);
  }

  private GameObject GetProjectilePrefab(ProjectilePrefabConfigSchema config, ProjectileKind kind)
  {
    return kind switch
    {
      ProjectileKind.Fireball => config.Fireball,
      _ => throw new Exception($"Projectile Prefab for {kind} kind not defined"),
    };
  }

}
