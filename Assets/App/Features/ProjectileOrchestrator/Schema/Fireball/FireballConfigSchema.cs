using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct FireballConfigSchema
{
  public float damage;
  public float moveSpeed;
  public float destructionRadius;
  public int quantity;
  public float lifeTime;
  public GameObject target;
  public Vector3 lastDirection;
}
