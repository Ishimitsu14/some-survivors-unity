using System.Collections.Generic;
using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct ProjectileConfigSchema
{
  public ProjectileKind kind;
  public WeaponAttackType attackType;
  public float damage;
  public float moveSpeed;
  public float destructionRadius;
  public float lifeTime;
  public int quantity;
  public Vector3 startPoint;
  public Dictionary<int, Vector3> possibleTargets;
}
