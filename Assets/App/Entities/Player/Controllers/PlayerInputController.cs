using Pure.DI;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerInputController : MonoBehaviour
{
    private InputSystem_Actions _actions;

    [Dependency] public ActionManager ActionManager { private get; set; }

    private PlayerStore _store;

    private void Awake()
    {
        Composition.Shared.BuildUp(this);

        gameObject.TryGetComponent(out _store);

        _actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _actions.Enable();
        _actions.Player.Move.performed += OnMove;
        _actions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        _actions.Disable();
        _actions.Player.Move.performed -= OnMove;
        _actions.Player.Move.canceled -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        ActionManager.Execute(new HandleMoveInputAction
            {
                inputContext = ctx,
                store = _store,
            }
        );
    }
}