using AutoDeconstruct;
using Unity.Mathematics;
using UnityEngine;

[AutoDeconstruct]
public partial struct AnimatePlayerMoveAction
{
  public Animator animator;
  public PlayerStore store;
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
