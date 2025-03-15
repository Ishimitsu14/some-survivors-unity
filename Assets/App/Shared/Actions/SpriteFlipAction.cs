using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct SpriteFlipAction
{
  public SpriteRenderer spriteRenderer;
  public MovementSchema movementSchema;
}

[ActionHandler(typeof(SpriteFlipAction))]
public class SpriteFlipActionHandler : IActionHandler<SpriteFlipAction>
{
  public void Execute(SpriteFlipAction action)
  {
    if (action.spriteRenderer)
    {
      if (action.movementSchema.direction.x < 0)
      {
        action.spriteRenderer.flipX = true;
      }

      if (action.movementSchema.direction.x > 0)
      {
        action.spriteRenderer.flipX = false;
      }
    }
  }
}
