using System.Collections.Generic;
using ObservableCollections;
using R3;
using R3.Triggers;
using UnityEngine;

public abstract class ProjectileBehaviorSystem<T> : MonoBehaviour where T : struct
{
  protected readonly Dictionary<GameObject, T> items = new();
  private DisposableBag _disposableBag;
  private ProjectileStore _store;
  protected abstract ProjectileKind Kind { get; }

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    _disposableBag = new DisposableBag();

    _store.projectiles
      .ObserveDictionaryAdd()
      .Where(e => e.Value.kind == Kind)
      .Subscribe(OnAdd)
      .AddTo(ref _disposableBag);

    _store.projectiles
      .ObserveDictionaryRemove()
      .Where(e => e.Value.kind == Kind)
      .Subscribe(OnRemove)
      .AddTo(ref _disposableBag);
  }

  private void OnDisable()
  {
    _disposableBag.Dispose();
  }

  private void OnAdd(DictionaryAddEvent<int, ProjectileConfigSchema> item)
  {
    var (projectileId, config) = item;
    var projectile = GameObject.Find(projectileId.ToString());

    projectile
      .OnTriggerEnter2DAsObservable()
      .Select(other => (projectile, other.gameObject, config))
      .Subscribe(OnHit)
      .AddTo(ref _disposableBag);

    items.TryAdd(projectile, PrepareConfig(projectile, config));
  }

  private void OnRemove(DictionaryRemoveEvent<int, ProjectileConfigSchema> item)
  {
    var projectile = GameObject.Find(item.Key.ToString());
    items.Remove(projectile);
  }

  private void OnHit((GameObject projectile, GameObject other, ProjectileConfigSchema config) data)
  {
    var (projectile, other, config) = data;

    _store.projectiles.Remove(projectile.GetInstanceID());
    Destroy(projectile);

    EventManager<OnProjectileHit>.Emit(new OnProjectileHit
    {
      damage = config.damage
    }, other.GetInstanceID());
  }

  protected abstract T PrepareConfig(GameObject projectile, ProjectileConfigSchema config);
}