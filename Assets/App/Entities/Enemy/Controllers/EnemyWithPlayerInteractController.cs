using UnityEngine;

public class EnemyWithPlayerInteractController : MonoBehaviour
{
  private EnemyStore _store;

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    EventManager<OnPlayerMove>.On += FollowPlayer;
  }

  private void OnDisable()
  {
    EventManager<OnPlayerMove>.On -= FollowPlayer;
  }

  private void FollowPlayer(OnPlayerMove data)
  {
    ActionManager.Instance.Execute(new FollowEnemyToPlayerAction
      {
        playerPosition = data.position,
        enemyPosition = gameObject.transform.position,
        enemyStore = _store
      }
    );
  }
}