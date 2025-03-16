using R3;
using UnityEngine;

public class EnemyLifeController : MonoBehaviour
{
  private EnemyStore _store;
  private DisposableBag _disposableBag;

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    _disposableBag = new DisposableBag();
    _store.currentHealth.Subscribe(OnHealthChange).AddTo(ref _disposableBag);
  }

  private void OnDisable()
  {
    _disposableBag.Dispose();
  }

  private void OnHealthChange(float currentHealth)
  {
    if (currentHealth <= 0)
    {
      Destroy(gameObject);
    }
  }

}
