using UnityEngine;

public struct MovePlayerAction
{
    public float delta;
    public PlayerStore store;
    public Transform transform;
    
    public void Deconstruct(out float delta, out PlayerStore store, out Transform transform)
    {
        delta = this.delta;
        store = this.store;
        transform = this.transform;
    }
}


[ActionHandler(typeof(MovePlayerAction))]
public class MovePlayerActionHandler : IActionHandler<MovePlayerAction>
{
    public void Execute(MovePlayerAction action)
    {
        var (delta, store, transform) = action;

        transform.position += store.movementSchema.direction * (delta * store.movementSchema.moveSpeed);

        EventManager<OnPlayerMoveEvent>.Emit(new OnPlayerMoveEvent
            {
                Transform = transform
            }
        );
    }
}