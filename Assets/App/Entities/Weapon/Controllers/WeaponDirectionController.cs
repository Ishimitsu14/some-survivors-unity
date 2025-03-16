using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponDirectionController : MonoBehaviour
{
  private InputSystem_Actions _actions;

  private WeaponStore _store;

  private void Awake()
  {
    gameObject.TryGetComponent(out _store);

    _actions = new InputSystem_Actions();
  }

  private void OnEnable()
  {
    _actions.Enable();
    _actions.Player.Move.performed += OnMove;
  }

  private void OnDisable()
  {
    _actions.Disable();
    _actions.Player.Move.performed -= OnMove;
  }

  private void OnMove(InputAction.CallbackContext ctx)
  {
    _store.direction = ctx.ReadValue<Vector2>();
  }
}