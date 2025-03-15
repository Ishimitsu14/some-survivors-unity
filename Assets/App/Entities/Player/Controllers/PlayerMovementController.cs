using Pure.DI;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Dependency] public ActionManager ActionManager { private get; set; }

    private PlayerStore _store;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Composition.Shared.BuildUp(this);
        
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
        ActionManager.Execute(new MovePlayerAction
            {
                store = _store,
                transform = gameObject.transform,
                delta = Time.deltaTime,
            }
        );
    }

    private void HandleFlip()
    {
        ActionManager.Execute(new SpriteFlipAction
            {
                spriteRenderer = _spriteRenderer,
                movementSchema = _store.movementSchema,
            }
        );
    }
}