using System.Collections;
using UnityEngine;

public class TestWeaponComponent : AbstractBaseWeaponComponent
{
  protected override void OnBulletCreate(BulletSchema bulletSchema)
  {
    StartCoroutine(MoveToEnemy(bulletSchema));
  }

  protected override void OnBulletCollision(Collider2D collider, BulletSchema bulletSchema)
  {
    HitEnemy(collider.gameObject);
    RemoveBullet(bulletSchema);
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

  private void RemoveBullet(BulletSchema bulletSchema)
  {
    _store.bullets.Remove(bulletSchema);
  }
}
