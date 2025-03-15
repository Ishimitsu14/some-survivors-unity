using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct BulletSchema
{
  public GameObject enemy;
  public GameObject bullet;
  public BulletCollisionController bulletCollisionController;
}
