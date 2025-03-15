using UnityEngine;

public class EnemyStore : Store
{
    public EnemyBaseDataSchema baseDataSchema = new() { maxHealth = 100, currentHealth = 100, damage = 5 };

    public MovementSchema movementSchema = new() { moveSpeed = 2f, direction = Vector3.zero };
}