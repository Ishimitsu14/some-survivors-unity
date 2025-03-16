using R3;
using UnityEngine;
using ObservableCollections;
using System.Collections.Generic;
using System.Linq;
using R3.Triggers;

public class FireballBehaviourSystem : MonoBehaviour
{
  private ProjectileStore _store;

  private DisposableBag _disposableBag;

  public Dictionary<GameObject, FireballConfigSchema> items = new();

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void OnEnable()
  {
    _disposableBag = new DisposableBag();

    _store.projectiles
      .ObserveDictionaryAdd()
      .Where(e => e.Value.kind == ProjectileKind.Fireball)
      .Subscribe(OnAdd)
      .AddTo(ref _disposableBag);

    _store.projectiles
      .ObserveDictionaryRemove()
      .Where(e => e.Value.kind == ProjectileKind.Fireball)
      .Subscribe(OnRemove)
      .AddTo(ref _disposableBag);
  }

  private void OnDisable()
  {
    _disposableBag.Dispose();
  }

  private void Update()
  {
    foreach (var key in items.Keys.ToList())
    {
      items[key] = ActionManager.Instance.Execute<MoveFireballAction, FireballConfigSchema>(new MoveFireballAction
      {
        projectile = key,
        config = items[key]
      });
    }
  }

  private void OnAdd(DictionaryAddEvent<int, ProjectileConfigSchema> item)
  {
    var projectile = GameObject.Find(item.Key.ToString());

    var target = ActionManager.Instance.Execute<FindFireballTargetAction, GameObject>(new FindFireballTargetAction
    {
      startPoint = item.Value.startPoint,
      possibleTargets = item.Value.possibleTargets
    });

    projectile
      .OnTriggerEnter2DAsObservable()
      .Select(other => (projectile, other.gameObject, item.Value))
      .Subscribe(OnHit)
      .AddTo(ref _disposableBag);

    items.TryAdd(projectile, new FireballConfigSchema
    {
      damage = item.Value.damage,
      moveSpeed = item.Value.moveSpeed,
      destructionRadius = item.Value.destructionRadius,
      quantity = item.Value.quantity,
      lifeTime = item.Value.lifeTime,
      target = target
    });
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
}
