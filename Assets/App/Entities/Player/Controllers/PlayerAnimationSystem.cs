using Pure.DI;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimatorController : MonoBehaviour
{
    [Dependency] public ActionManager ActionManager { private get; set; }

    private PlayerStore _store;
    private Animator _animator;

    private void Awake()
    {
        Composition.Shared.BuildUp(this);

        gameObject.TryGetComponent(out _store);
        gameObject.TryGetComponent(out _animator);
    }

    private void FixedUpdate()
    {
        OnAnimatorMove();
    }

    private void OnAnimatorMove()
    {
        ActionManager.Execute(new AnimatePlayerMoveAction
            {
                store = _store,
                animator = _animator,
            }
        );
    }
}