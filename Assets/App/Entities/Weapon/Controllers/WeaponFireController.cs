using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFireController : MonoBehaviour
{
  private WeaponStore _store;
  private WeaponCollisionController _collisionController;

  private readonly Dictionary<int, GameObject> _enemies = new();
  private Coroutine _fireCoroutine;

  private void Awake()
  {
    TryGetComponent(out _store);
    TryGetComponent(out _collisionController);
  }

  private void OnEnable()
  {
    EventManager<OnBulletHitEnemy>.WithId(gameObject.GetInstanceID()).On += OnBulletHitEnemy;
  }

  private void Start()
  {
    _collisionController.OnEnemyInWeaponRange += HandleEnemyInWeaponRange;
    _collisionController.OnEnemyOutWeaponRange += HandleEnemyOutWeaponRange;
  }

  private void HandleEnemyInWeaponRange(GameObject enemy)
  {
    if (_enemies.TryAdd(enemy.GetInstanceID(), enemy))
    {
      if (_fireCoroutine == null)
      {
        _fireCoroutine = StartCoroutine(FireCoroutine());
      }
    }
  }

  private void HandleEnemyOutWeaponRange(GameObject enemy)
  {
    _enemies.Remove(enemy.GetInstanceID());

    if (_enemies.Count == 0 && _fireCoroutine != null)
    {
      StopCoroutine(_fireCoroutine);
      _fireCoroutine = null;
    }
  }

  private IEnumerator FireCoroutine()
  {
    while (_enemies.Count > 0)
    {
      GameObject closestEnemy = null;
      float minDistance = float.MaxValue;

      foreach (var enemy in _enemies.Values)
      {
        float distance = Vector2.Distance(_store.weaponHolder.transform.position, enemy.transform.position);
        if (distance < minDistance)
        {
          minDistance = distance;
          closestEnemy = enemy;
        }
      }


      var holderTransform = _store.weaponHolder.transform;
      var bullet = Instantiate(_store.weaponConfig.bulletPrefab, holderTransform.position, Quaternion.identity);

      var collisionRelay = bullet.AddComponent<CollisionRelay>();

      _store.bullets.Add(new BulletSchema
      {
        enemy = closestEnemy,
        bullet = bullet,
        collisionRelay = collisionRelay,
      });


      yield return new WaitForSeconds(_store.weaponConfig.fireRate);
    }

    _fireCoroutine = null;
  }

  private void OnBulletHitEnemy(OnBulletHitEnemy bulletHitEnemy)
  {
    EventManager<OnWeaponDamageEnemy>.Emit(new OnWeaponDamageEnemy
    {
      damage = _store.weaponConfig.damage,
      punchPower = _store.weaponConfig.punchPower,
    }, bulletHitEnemy.enemy.GetInstanceID());
  }

  private void OnDisable()
  {
    _collisionController.OnEnemyInWeaponRange -= HandleEnemyInWeaponRange;
    _collisionController.OnEnemyOutWeaponRange -= HandleEnemyOutWeaponRange;
    EventManager<OnBulletHitEnemy>.WithId(gameObject.GetInstanceID()).On -= OnBulletHitEnemy;

    if (_fireCoroutine != null)
    {
      StopCoroutine(_fireCoroutine);
      _fireCoroutine = null;
    }
  }
}
