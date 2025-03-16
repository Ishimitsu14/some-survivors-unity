using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEngine;

public abstract class ProjectileBehaviorSystem<T> : MonoBehaviour where T : struct
{
  protected readonly Dictionary<GameObject, T> items = new();
  protected DisposableBag disposableBag;
  protected ProjectileStore store;
  protected abstract ProjectileKind Kind { get; }

  private void Awake()
  {
    TryGetComponent(out store);
  }

  private void OnEnable()
  {
    disposableBag = new DisposableBag();

    store.projectiles
      .ObserveDictionaryAdd()
      .Where(e => e.Value.kind == Kind)
      .Subscribe(OnAdd)
      .AddTo(ref disposableBag);

    store.projectiles
      .ObserveDictionaryRemove()
      .Where(e => e.Value.kind == Kind)
      .Subscribe(OnRemove)
      .AddTo(ref disposableBag);
  }

  private void OnDisable()
  {
    disposableBag.Dispose();
  }

  protected abstract void OnAdd(DictionaryAddEvent<int, ProjectileConfigSchema> item);
  protected abstract void OnRemove(DictionaryRemoveEvent<int, ProjectileConfigSchema> item);
}