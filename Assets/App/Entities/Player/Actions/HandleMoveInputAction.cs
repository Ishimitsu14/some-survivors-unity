using AutoDeconstruct;
using UnityEngine;
using UnityEngine.InputSystem;

[AutoDeconstruct]
public partial struct HandleMoveInputAction
{
  public InputAction.CallbackContext inputContext;
  public PlayerStore store;
}

[ActionHandler(typeof(HandleMoveInputAction))]
public class HandleMoveInputActionHandler : IActionHandler<HandleMoveInputAction>
{
  public void Execute(HandleMoveInputAction action)
  {
    var (inputContext, store) = action;

    store.movementSchema.direction = inputContext.ReadValue<Vector2>();
  }
}
