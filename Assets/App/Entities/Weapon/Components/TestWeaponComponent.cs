using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

public class TestWeaponComponent : BaseWeaponComponent
{
    private WeaponStore _store;

    private void Awake()
    {
        TryGetComponent(out _store);
    }

    private void Start()
    {
        _store.bullets.CollectionChanged += OnChangeBullets;
    }

    private void OnChangeBullets(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (BulletSchema bulletSchema in e.NewItems)
            {
                StartCoroutine(MoveToEnemy(bulletSchema));
                bulletSchema.bulletCollisionController.OnBulletCollisionEnter +=
                    (C => HandleBulletCollision(C, bulletSchema));
            }
        }
    }

    private IEnumerator MoveToEnemy(BulletSchema bulletSchema)
    {
        while (_store.bullets.Contains(bulletSchema))
        {
            var bulletTransform = bulletSchema.bullet.transform;
            var direction = bulletSchema.enemy.transform.position - bulletTransform.position;
            bulletTransform.position += direction * (5f * Time.deltaTime);
            yield return new WaitForSeconds(0f);
        }
    }

    protected override void HandleBulletCollision(Collider2D collider, BulletSchema bulletSchema)
    {
        if (collider.CompareTag(GameTags.Enemy))
        {
            EventManager<OnBulletHitEnemy>.Emit(new OnBulletHitEnemy
            {
                enemy = collider.gameObject,
            }, gameObject.GetInstanceID());
            
            DestroyBullet(bulletSchema);
        }
    }

    private void DestroyBullet(BulletSchema bulletSchema)
    {
        _store.bullets.Remove(bulletSchema);
        
        bulletSchema.bulletCollisionController.OnBulletCollisionEnter -=
            (C => HandleBulletCollision(C, bulletSchema));
        
        Destroy(bulletSchema.bullet);
    }
}