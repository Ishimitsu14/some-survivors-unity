using System.Collections.Specialized;
using UnityEngine;

public abstract class AbstractBaseWeaponComponent : MonoBehaviour
{
  protected WeaponStore _store;

  protected virtual void Awake()
  {
    TryGetComponent(out _store);
  }

  protected virtual void Start()
  {
    _store.bullets.CollectionChanged += OnBulltsChange;
  }

  private void OnBulltsChange(object sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.NewItems != null)
    {
      foreach (BulletSchema bulletSchema in e.NewItems)
      {
        OnBulletCreate(bulletSchema);
        bulletSchema.collisionRelay.OnTriggerEnter2DRelay +=
            C => OnBulletCollisionEnter(C, bulletSchema);
      }
    }

    if (e.OldItems != null)
    {
      foreach (BulletSchema bulletSchema in e.OldItems)
      {
        bulletSchema
            .collisionRelay
            .OnTriggerEnter2DRelay -= C => OnBulletCollisionEnter(C, bulletSchema);

        Destroy(bulletSchema.bullet);
      }
    }
  }

  private void OnBulletCollisionEnter(Collider2D collider, BulletSchema bulletSchema)
  {
    if (collider.CompareTag(GameTags.Enemy))
    {
      OnBulletCollision(collider, bulletSchema);
    }
  }

  protected void HitEnemy(GameObject enemy)
  {
    EventManager<OnBulletHitEnemy>.Emit(
        new OnBulletHitEnemy
        {
          enemy = enemy,
        },
        gameObject.GetInstanceID()
    );
  }

  private void OnDisable()
  {
    _store.bullets.CollectionChanged -= OnBulltsChange;
  }

  protected abstract void OnBulletCreate(BulletSchema bulletSchema);

  protected abstract void OnBulletCollision(Collider2D collider, BulletSchema bulletSchema);
}
