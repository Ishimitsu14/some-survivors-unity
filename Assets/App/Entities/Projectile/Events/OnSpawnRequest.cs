using System.Collections.Generic;
using AutoDeconstruct;
using UnityEngine;

namespace Projectile
{
  [AutoDeconstruct]
  public partial struct OnSpawnRequest
  {
    public Kind kind;
    public float damage;
    public float moveSpeed;
    public float destructionRadius;
    public float lifeTime;
    public Vector3 startPoint;
    public Dictionary<int, Vector3> possibleTargets;
    public Vector3 weaponDirection;
    public GameObject prefab;
  }
}