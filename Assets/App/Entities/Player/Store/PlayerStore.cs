using UnityEngine;

public class PlayerStore : Store
{
  public MovementSchema movement = new() { moveSpeed = 5f, direction = Vector3.zero };
}
