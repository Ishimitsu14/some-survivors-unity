using AutoDeconstruct;
using Projectile;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectileAuthority
{
  [AutoDeconstruct]
  public partial struct SpawnAction
  {
    public Vector3 startPoint;
    public Kind kind;
    public GameObject prefab;
  }

  [ActionHandler(typeof(SpawnAction))]
  public class SpawnProjectileActionHandler : IActionHandler<SpawnAction, GameObject>
  {
    public GameObject Execute(SpawnAction action)
    {
      var (startPoint, kind, prefab) = action;

      var projectile = Object.Instantiate(prefab, startPoint, Quaternion.identity);

      projectile.name = projectile.GetInstanceID().ToString();

      return projectile;
    }
  }
}