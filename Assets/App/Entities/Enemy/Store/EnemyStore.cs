using R3;
using UnityEngine;

public class EnemyStore : Store
{
  public EnemyConfigSchema config = new()
  {
    health = 100
  };

  public MovementSchema movement = new()
  {
    moveSpeed = 2f,
    direction = Vector3.zero
  };

  public ReactiveProperty<float> currentHealth;

  public EnemyStore()
  {
    currentHealth = new ReactiveProperty<float>(config.health);
  }
}
