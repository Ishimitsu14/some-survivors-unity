using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
  private PlayerStore _store;
  private SpriteRenderer _spriteRenderer;

  private void Awake()
  {
    gameObject.TryGetComponent(out _store);
    gameObject.TryGetComponent(out _spriteRenderer);
  }

  private void Update()
  {
    HandleMovement();
    HandleFlip();
  }

  private void HandleMovement()
  {
    ActionManager.Instance.Execute(new MovePlayerAction
    {
      store = _store,
      transform = gameObject.transform,
      delta = Time.deltaTime,
    }
    );
  }

  private void HandleFlip()
  {
    ActionManager.Instance.Execute(new SpriteFlipAction
    {
      spriteRenderer = _spriteRenderer,
      movementSchema = _store.movementSchema,
    }
    );
  }
}
