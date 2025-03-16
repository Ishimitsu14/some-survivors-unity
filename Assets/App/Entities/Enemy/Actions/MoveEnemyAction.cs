using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct MoveEnemyAction
{
  public EnemyStore store;
  public Transform transform;
  public float delta;
}

[ActionHandler(typeof(MoveEnemyAction))]
public class MoveEnemyActionHandler : IActionHandler<MoveEnemyAction>
{
  public void Execute(MoveEnemyAction action)
  {
    action.transform.position += action.delta * action.store.movement.moveSpeed *
                                 action.store.movement.direction;
  }
}
