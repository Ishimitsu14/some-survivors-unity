using System;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

  private EnemyStore _store;

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void FixedUpdate()
  {
    HandleMove();
  }

  private void HandleMove()
  {
    ActionManager.Instance.Execute(new MoveEnemyAction
    {
      transform = gameObject.transform,
      store = _store,
      delta = Time.deltaTime,
    });
  }
}
