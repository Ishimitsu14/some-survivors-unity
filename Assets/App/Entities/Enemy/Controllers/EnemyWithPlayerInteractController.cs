using System;
using UnityEngine;

public class EnemyWithPlayerInteractController : MonoBehaviour
{

  private EnemyStore _store;
  private PlayerAuthority _player;

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    EventManager<OnPlayerMove>.On += FollowPlayer;
  }

  private void FollowPlayer(OnPlayerMove data)
  {
    ActionManager.Instance.Execute(new EnemyFollowPlayerAction
    {
      playerTransform = data.Transform,
      enemyTransform = gameObject.transform,
      enemyStore = _store,
    }
    );
  }

  private void OnDisable()
  {
    EventManager<OnPlayerMove>.On -= FollowPlayer;
  }
}
