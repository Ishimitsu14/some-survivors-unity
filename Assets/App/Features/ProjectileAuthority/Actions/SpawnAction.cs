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
    public PrefabConfigSchema prefabConfig;
  }

  [ActionHandler(typeof(SpawnAction))]
  public class SpawnProjectileActionHandler : IActionHandler<SpawnAction, GameObject>
  {
    public GameObject Execute(SpawnAction action)
    {
      var (startPoint, kind, prefabConfig) = action;
      var prefab = prefabConfig.Get(kind);

      var projectile = Object.Instantiate(prefab, startPoint, Quaternion.identity);

      projectile.name = projectile.GetInstanceID().ToString();

      return projectile;
    }
  }
}