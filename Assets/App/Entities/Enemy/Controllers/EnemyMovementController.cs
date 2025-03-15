using System;
using Pure.DI;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [Dependency] public ActionManager ActionManager { private get; set; }
    
    private EnemyStore _store;

    private void Awake()
    {
        Composition.Shared.BuildUp(this);
        TryGetComponent(out _store);
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        ActionManager.Execute(new MoveEnemyAction
        {
            transform = gameObject.transform,
            store = _store,
            delta = Time.deltaTime,
        });
    }
}