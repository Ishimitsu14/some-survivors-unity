using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct SpriteFlipAction
{
  public SpriteRenderer spriteRenderer;
  public MovementSchema movement;
}

[ActionHandler(typeof(SpriteFlipAction))]
public class SpriteFlipActionHandler : IActionHandler<SpriteFlipAction>
{
  public void Execute(SpriteFlipAction action)
  {
    var (spriteRenderer, movementSchema) = action;

    spriteRenderer.flipX = movementSchema.direction.x switch
    {
      < 0 => true,
      > 0 => false,
      _ => spriteRenderer.flipX
    };
  }
}
