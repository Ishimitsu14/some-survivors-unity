using AutoDeconstruct;
using Unity.Mathematics;
using UnityEngine;

namespace EnemyAuthority
{
  [AutoDeconstruct]
  public partial struct FollowToPlayerAction
  {
    public Vector3 playerPosition;
    public Vector3 enemyPosition;
    public EnemyStore enemyStore;
  }

  [ActionHandler(typeof(FollowToPlayerAction))]
  public class FollowToPlayerActionHandler : IActionHandler<FollowToPlayerAction>
  {
    public void Execute(FollowToPlayerAction action)
    {
      action.enemyStore.movement.direction = math.normalize(
        action.playerPosition - action.enemyPosition
      );
    }
  }
}
