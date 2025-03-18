using System.Collections.Generic;
using ObservableCollections;
using R3;
using R3.Triggers;
using UnityEngine;

namespace ProjectileAuthority
{
  public abstract class BehaviorSystemBase<T> : MonoBehaviour where T : struct
  {
    protected readonly Dictionary<GameObject, T> items = new();
    private DisposableBag _disposableBag;
    private Store _store;
    protected abstract Projectile.Kind Kind { get; }

    private void Awake()
    {
      TryGetComponent(out _store);
    }

    private void OnEnable()
    {
      _disposableBag = new();

      _store.projectiles
        .ObserveDictionaryAdd()
        .Where(e => e.Value.kind == Kind)
        .Subscribe(OnItemAdd)
        .AddTo(ref _disposableBag);

      _store.projectiles
        .ObserveDictionaryRemove()
        .Where(e => e.Value.kind == Kind)
        .Subscribe(OnItemRemove)
        .AddTo(ref _disposableBag);
    }

    private void OnDisable()
    {
      _disposableBag.Dispose();
    }

    private void OnItemAdd(DictionaryAddEvent<GameObject, ProjectileConfigSchema> item)
    {
      var (projectile, config) = item;

      projectile
        .OnTriggerEnter2DAsObservable()
        .Select(other => (projectile, other.gameObject, config))
        .Subscribe(OnHit)
        .AddTo(ref _disposableBag);

      items.TryAdd(projectile, PrepareConfig(projectile, config));
    }

    private void OnItemRemove(DictionaryRemoveEvent<GameObject, ProjectileConfigSchema> item)
    {
      items.Remove(item.Key);
    }

    private void OnHit((GameObject projectile, GameObject other, ProjectileConfigSchema config) data)
    {
      var (projectile, other, config) = data;

      _store.projectiles.Remove(projectile);
      Destroy(projectile);

      EventManager<Projectile.OnHit>.Emit(new Projectile.OnHit
      {
        damage = config.damage
      }, other.GetInstanceID());
    }

    protected abstract T PrepareConfig(GameObject projectile, ProjectileConfigSchema config);
  }
}
