using UnityEngine;
using UnityEngine.InputSystem;

namespace WeaponAuthority
{
  public class DirectionController : MonoBehaviour
  {
    private InputSystem_Actions _actions;

    private Store _store;

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
}
