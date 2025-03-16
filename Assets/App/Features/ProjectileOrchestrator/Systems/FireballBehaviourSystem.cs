using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using R3;
using R3.Triggers;
using UnityEngine;

public class FireballBehaviourSystem : MonoBehaviour
{
  private const ProjectileKind Kind = ProjectileKind.Fireball;
  private readonly Dictionary<GameObject, FireballConfigSchema> _items = new();

  private DisposableBag _disposableBag;
  private ProjectileStore _store;

  private void Awake()
  {
    TryGetComponent(out _store);
  }

  private void Update()
  {
    foreach (var key in _items.Keys.ToList())
      _items[key] = ActionManager.Instance.Execute<MoveFireballAction, FireballConfigSchema>(new MoveFireballAction
      {
        projectile = key,
        config = _items[key]
      });
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

    _items.TryAdd(projectile, new FireballConfigSchema
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
    _items.Remove(projectile);
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