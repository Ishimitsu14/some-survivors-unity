using System;
using AutoDeconstruct;

[Serializable]
[AutoDeconstruct]
public partial struct WeaponConfigSchema
{
  public float damage;
  public float range;
  public float fireRate;
  public float punchPower;
  public float moveSpeed;
  public float destructionRadius;
  public float projectileLifetime;
  public ProjectileKind projectileKind;
  public WeaponAttackType attackType;
  public int projectileQuantity;
}