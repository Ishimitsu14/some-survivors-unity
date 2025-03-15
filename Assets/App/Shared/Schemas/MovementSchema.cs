using AutoDeconstruct;
using UnityEngine;

[System.Serializable]
[AutoDeconstruct]
public partial struct MovementSchema
{
  public float moveSpeed;
  public Vector3 direction;
}
