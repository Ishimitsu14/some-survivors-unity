using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAnimatorController : MonoBehaviour
{

  private PlayerStore _store;
  private Animator _animator;

  private void Awake()
  {
    gameObject.TryGetComponent(out _store);
    gameObject.TryGetComponent(out _animator);
  }

  private void FixedUpdate()
  {
    OnAnimatorMove();
  }

  private void OnAnimatorMove()
  {
    ActionManager.Instance.Execute(new AnimatePlayerMoveAction
    {
      store = _store,
      animator = _animator,
    }
    );
  }
}
