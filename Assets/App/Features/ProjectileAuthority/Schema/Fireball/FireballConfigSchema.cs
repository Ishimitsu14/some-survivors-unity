using AutoDeconstruct;
using UnityEngine;

namespace ProjectileAuthority
{
  [AutoDeconstruct]
  public partial struct FireballConfigSchema
  {
    public float damage;
    public float moveSpeed;
    public float destructionRadius;
    public float lifeTime;
    public GameObject target;
    public Vector3 lastDirection;
  }
}
