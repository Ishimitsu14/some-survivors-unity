using AutoDeconstruct;
using Unity.Mathematics;
using UnityEngine;

[AutoDeconstruct]
public partial struct FollowEnemyToPlayerAction
{
  public Transform playerTransform;
  public Transform enemyTransform;
  public EnemyStore enemyStore;
}

[ActionHandler(typeof(FollowEnemyToPlayerAction))]
public class FollowEnemyToPlayerActionHandler : IActionHandler<FollowEnemyToPlayerAction>
{
  public void Execute(FollowEnemyToPlayerAction action)
  {
    action.enemyStore.movement.direction = math.normalize(
        action.playerTransform.position - action.enemyTransform.position
    );
  }
}
