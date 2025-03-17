using UnityEngine;

public class PlayerStore : StoreBase
{
  public MovementSchema movement = new() { moveSpeed = 5f, direction = Vector3.zero };
}
