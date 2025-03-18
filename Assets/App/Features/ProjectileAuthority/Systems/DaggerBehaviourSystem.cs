using UnityEngine;

namespace ProjectileAuthority
{
  public class DaggerBehaviourSystem : BehaviorSystemBase<DaggerConfigSchema>
  {
    protected override Projectile.Kind Kind { get; } = Projectile.Kind.Dagger;

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
}
