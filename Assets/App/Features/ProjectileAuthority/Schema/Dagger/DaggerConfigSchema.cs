using AutoDeconstruct;
using UnityEngine;

namespace ProjectileAuthority
{
  [AutoDeconstruct]
  public partial struct DaggerConfigSchema
  {
    public float damage;
    public float moveSpeed;
    public float destructionRadius;
    public float lifeTime;
    public Vector3 direction;
  }
}
