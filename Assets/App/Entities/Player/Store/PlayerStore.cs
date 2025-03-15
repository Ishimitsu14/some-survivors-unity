using UnityEngine;

public class PlayerStore : Store
{
      public MovementSchema movementSchema = new() { moveSpeed = 5f, direction = Vector3.zero };
}