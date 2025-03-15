using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct MovePlayerAction
{
  public float delta;
  public PlayerStore store;
  public Transform transform;
}


[ActionHandler(typeof(MovePlayerAction))]
public class MovePlayerActionHandler : IActionHandler<MovePlayerAction>
{
  public void Execute(MovePlayerAction action)
  {
    var (delta, store, transform) = action;

    transform.position += store.movementSchema.direction * (delta * store.movementSchema.moveSpeed);

    EventManager<OnPlayerMove>.Emit(new OnPlayerMove
    {
      Transform = transform
    }
    );
  }
}
