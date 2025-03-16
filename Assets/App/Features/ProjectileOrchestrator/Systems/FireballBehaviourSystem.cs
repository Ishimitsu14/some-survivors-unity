using System.Linq;
using ObservableCollections;
using R3;
using R3.Triggers;
using UnityEngine;

public class FireballBehaviourSystem : ProjectileBehaviorSystem<FireballConfigSchema>
{
  protected override ProjectileKind Kind { get; } = ProjectileKind.Fireball;

  private void Update()
  {
    foreach (var key in items.Keys.ToList())
      items[key] = ActionManager.Instance.Execute<MoveFireballAction, FireballConfigSchema>(new MoveFireballAction
      {
        projectile = key,
        config = items[key]
      });
  }

  protected override void OnAdd(DictionaryAddEvent<int, ProjectileConfigSchema> item)
  {
    var (projectileId, config) = item;
    var projectile = GameObject.Find(projectileId.ToString());

    var target = ActionManager.Instance.Execute<FindFireballTargetAction, GameObject>(new FindFireballTargetAction
    {
      startPoint = config.startPoint,
      possibleTargets = config.possibleTargets
    });

    projectile
      .OnTriggerEnter2DAsObservable()
      .Select(other => (projectile, other.gameObject, config))
      .Subscribe(OnHit)
      .AddTo(ref disposableBag);

    items.TryAdd(projectile, new FireballConfigSchema
    {
      damage = config.damage,
      moveSpeed = config.moveSpeed,
      destructionRadius = config.destructionRadius,
      quantity = config.quantity,
      lifeTime = config.lifeTime,
      target = target
    });
  }

  protected override void OnRemove(DictionaryRemoveEvent<int, ProjectileConfigSchema> item)
  {
    var projectile = GameObject.Find(item.Key.ToString());
    items.Remove(projectile);
  }

  private void OnHit((GameObject projectile, GameObject other, ProjectileConfigSchema config) data)
  {
    var (projectile, other, config) = data;

    store.projectiles.Remove(projectile.GetInstanceID());
    Destroy(projectile);

    EventManager<OnProjectileHit>.Emit(new OnProjectileHit
    {
      damage = config.damage
    }, other.GetInstanceID());
  }
}