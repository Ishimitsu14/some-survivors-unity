using System.Linq;
using UnityEngine;

namespace ProjectileAuthority
{
  public class FireballBehaviourSystem : BehaviorSystemBase<FireballConfigSchema>
  {
    protected override Projectile.Kind Kind { get; } = Projectile.Kind.Fireball;

    private void Update()
    {
      foreach (var key in items.Keys.ToList())
        items[key] = ActionManager.Instance.Execute<MoveFireballAction, FireballConfigSchema>(new MoveFireballAction
        {
          projectile = key,
          config = items[key]
        });
    }

    protected override FireballConfigSchema PrepareConfig(GameObject projectile, ProjectileConfigSchema config)
    {
      var target = ActionManager.Instance.Execute<FindFireballTargetAction, GameObject>(new FindFireballTargetAction
      {
        startPoint = config.startPoint,
        possibleTargets = config.possibleTargets
      });

      return new FireballConfigSchema
      {
        damage = config.damage,
        moveSpeed = config.moveSpeed,
        destructionRadius = config.destructionRadius,
        lifeTime = config.lifeTime,
        target = target
      };
    }
  }
}
