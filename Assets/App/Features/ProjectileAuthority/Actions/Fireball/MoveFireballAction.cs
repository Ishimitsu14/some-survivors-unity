using System;
using AutoDeconstruct;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectileAuthority
{
  [AutoDeconstruct]
  public partial struct MoveFireballAction
  {
    public GameObject projectile;
    public FireballConfigSchema config;
  }

  [ActionHandler(typeof(MoveFireballAction))]
  public class MoveFireballActionHandler : IActionHandler<MoveFireballAction, FireballConfigSchema>
  {
    public FireballConfigSchema Execute(MoveFireballAction action)
    {
      var (projectile, config) = action;

      Vector3 direction;

      if (config.target && !config.target.IsDestroyed())
      {
        direction = math.normalize(config.target.transform.position - projectile.transform.position);
        config.lastDirection = direction;
      }
      else
      {
        if (config.lastDirection.Equals(Vector3.zero))
          throw new Exception("Impossible to move. Target has been destroyed, last target position not defined");

        direction = config.lastDirection;
      }

      projectile.transform.position += config.moveSpeed * Time.deltaTime * direction;

      return config;
    }
  }
}