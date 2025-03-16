using System;
using AutoDeconstruct;
using UnityEngine;

[Serializable]
[AutoDeconstruct]
public partial struct MovementSchema
{
  public float moveSpeed;
  public Vector3 direction;
}
