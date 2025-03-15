using UnityEngine;
using UnityEngine.InputSystem;
public struct HandleMoveInputAction
{
    public InputAction.CallbackContext inputContext;
    public PlayerStore store;

    public void Deconstruct(out InputAction.CallbackContext inputContext, out PlayerStore store)
    {
        inputContext = this.inputContext;
        store = this.store;
    }
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