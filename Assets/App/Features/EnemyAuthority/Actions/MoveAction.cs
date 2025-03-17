using AutoDeconstruct;
using UnityEngine;

namespace EnemyAuthority
{
  [AutoDeconstruct]
  public partial struct MoveAction
  {
    public EnemyStore store;
    public Transform transform;
    public float delta;
  }

  [ActionHandler(typeof(MoveAction))]
  public class MoveEnemyActionHandler : IActionHandler<MoveAction>
  {
    public void Execute(MoveAction action)
    {
      action.transform.position += action.delta * action.store.movement.moveSpeed *
                                   action.store.movement.direction;
    }
  }
}
