using AutoDeconstruct;
using Unity.Mathematics;
using UnityEngine;

[AutoDeconstruct]
public partial struct FollowEnemyToPlayerAction
{
  public Vector3 playerPosition;
  public Vector3 enemyPosition;
  public EnemyStore enemyStore;
}

[ActionHandler(typeof(FollowEnemyToPlayerAction))]
public class FollowEnemyToPlayerActionHandler : IActionHandler<FollowEnemyToPlayerAction>
{
  public void Execute(FollowEnemyToPlayerAction action)
  {
    action.enemyStore.movement.direction = math.normalize(
      action.playerPosition - action.enemyPosition
    );
  }
}