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
        _store.bullets.CollectionChanged += OnChangeBullets;
    }

    private void OnChangeBullets(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (BulletSchema bulletSchema in e.NewItems)
            {
                HandleBulletCreated(bulletSchema);
                bulletSchema.bulletCollisionController.OnBulletCollisionEnter +=
                    (C => OnBulletCollisionEnter(C, bulletSchema));
            }
        }

        if (e.OldItems != null)
        {
            foreach (BulletSchema bulletSchema in e.OldItems)
            {
                bulletSchema
                    .bulletCollisionController
                    .OnBulletCollisionEnter -= (C => OnBulletCollisionEnter(C, bulletSchema));
                
                Destroy(bulletSchema.bullet);
            }
        }
    }

    private void OnBulletCollisionEnter(Collider2D collider, BulletSchema bulletSchema)
    {
        if (collider.CompareTag(GameTags.Enemy))
        {
            HandleBulletCollision(collider, bulletSchema);
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
        _store.bullets.CollectionChanged -= OnChangeBullets;
    }

    protected abstract void HandleBulletCreated(BulletSchema bulletSchema);

    protected abstract void HandleBulletCollision(Collider2D collider, BulletSchema bulletSchema);
}