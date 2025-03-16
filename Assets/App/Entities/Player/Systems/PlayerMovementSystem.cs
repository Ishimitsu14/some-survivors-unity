using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
  private SpriteRenderer _spriteRenderer;
  private PlayerStore _store;

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
    if (Vector3.zero.Equals(_store.movement.direction)) return;

    ActionManager.Instance.Execute(new MovePlayerAction
      {
        store = _store,
        transform = gameObject.transform,
        delta = Time.deltaTime
      }
    );
  }

  private void HandleFlip()
  {
    ActionManager.Instance.Execute(new SpriteFlipAction
      {
        spriteRenderer = _spriteRenderer,
        movement = _store.movement
      }
    );
  }
}