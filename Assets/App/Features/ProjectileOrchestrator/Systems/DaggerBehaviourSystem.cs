using UnityEngine;

public class DaggerBehaviourSystem : ProjectileBehaviorSystem<DaggerConfigSchema>
{
  protected override ProjectileKind Kind { get; } = ProjectileKind.Dagger;

  private void Update()
  {
    foreach (var (projectile, config) in items)
      ActionManager.Instance.Execute(new MoveDaggerAction
      {
        projectile = projectile,
        config = config
      });
  }

  protected override DaggerConfigSchema PrepareConfig(GameObject projectile, ProjectileConfigSchema config)
  {
    return new DaggerConfigSchema
    {
      damage = config.damage,
      moveSpeed = config.moveSpeed,
      destructionRadius = config.destructionRadius,
      lifeTime = config.lifeTime,
      direction = config.weaponDirection
    };
  }
}