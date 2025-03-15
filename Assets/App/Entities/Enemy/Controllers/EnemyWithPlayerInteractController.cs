using System;
using Pure.DI;
using UnityEngine;

public class EnemyWithPlayerInteractController : MonoBehaviour
{
    [Dependency] public ActionManager ActionManager { private get; set; }

    private EnemyStore _store;
    private PlayerAuthority _player;

    private void Awake()
    {
        Composition.Shared.BuildUp(this);

        TryGetComponent(out _store);
    }

    private void OnEnable()
    {
        EventManager<OnPlayerMoveEvent>.On += FollowPlayer;
    }

    private void FollowPlayer(OnPlayerMoveEvent data)
    {
        ActionManager.Execute(new EnemyFollowPlayerAction
            {
                playerTransform = data.Transform,
                enemyTransform = gameObject.transform,
                enemyStore = _store,
            }
        );
    }

    private void OnDisable()
    {
        EventManager<OnPlayerMoveEvent>.On -= FollowPlayer;
    }
}