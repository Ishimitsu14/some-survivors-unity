using Unity.Mathematics;
using UnityEngine;

public struct AnimatePlayerMoveAction
{
    public Animator animator;
    public PlayerStore store;
    
    public void Deconstruct(out Animator animator, out PlayerStore store)
    {
        animator = this.animator;
        store = this.store;
    }
}

[ActionHandler(typeof(AnimatePlayerMoveAction))]
public class AnimatePlayerMoveActionHandler : IActionHandler<AnimatePlayerMoveAction>
{
    public void Execute(AnimatePlayerMoveAction action)
    {
        var (animator, store) = action;
        
        animator.SetFloat("Speed", math.cmax(math.abs(store.movementSchema.direction)));
    }
}