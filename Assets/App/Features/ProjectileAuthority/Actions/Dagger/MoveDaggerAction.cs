using AutoDeconstruct;
using Unity.Mathematics;
using UnityEngine;

namespace ProjectileAuthority
{
  [AutoDeconstruct]
  public partial struct MoveDaggerAction
  {
    public GameObject projectile;
    public DaggerConfigSchema config;
  }

  [ActionHandler(typeof(MoveDaggerAction))]
  public class MoveDaggerActionHandler : IActionHandler<MoveDaggerAction>
  {
    public void Execute(MoveDaggerAction action)
    {
      var (projectile, config) = action;

      projectile.transform.position += config.moveSpeed * Time.deltaTime * config.direction;
      var angle = math.degrees(math.atan2(config.direction.y, config.direction.x)) - 90f;
      projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
  }
}
