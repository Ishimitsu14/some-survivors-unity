using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponFireController : MonoBehaviour
{
  private readonly Dictionary<int, GameObject> _enemies = new();
  private WeaponCollisionController _collisionController;
  private Coroutine _fireCoroutine;
  private WeaponStore _store;

  private void Awake()
  {
    TryGetComponent(out _store);
    TryGetComponent(out _collisionController);
  }

  private void Start()
  {
    _collisionController.OnEnemyInWeaponRange += HandleEnemyInWeaponRange;
    _collisionController.OnEnemyOutWeaponRange += HandleEnemyOutWeaponRange;
  }

  private void OnDisable()
  {
    _collisionController.OnEnemyInWeaponRange -= HandleEnemyInWeaponRange;
    _collisionController.OnEnemyOutWeaponRange -= HandleEnemyOutWeaponRange;

    if (_fireCoroutine == null) return;
    StopCoroutine(_fireCoroutine);
    _fireCoroutine = null;
  }

  private void HandleEnemyInWeaponRange(GameObject enemy)
  {
    if (_enemies.TryAdd(enemy.GetInstanceID(), enemy)) _fireCoroutine ??= StartCoroutine(FireCoroutine());
  }

  private void HandleEnemyOutWeaponRange(GameObject enemy)
  {
    _enemies.Remove(enemy.GetInstanceID());

    if (_enemies.Count != 0 || _fireCoroutine == null) return;
    StopCoroutine(_fireCoroutine);
    _fireCoroutine = null;
  }

  private IEnumerator FireCoroutine()
  {
    while (_enemies.Count > 0)
    {
      var possibleTargets = _enemies.ToDictionary(
        item => item.Key,
        item => item.Value.transform.position
      );

      EventManager<OnProjectileSpawn>.Emit(new OnProjectileSpawn
      {
        kind = _store.weaponConfig.projectileKind,
        damage = _store.weaponConfig.damage,
        moveSpeed = _store.weaponConfig.moveSpeed,
        destructionRadius = _store.weaponConfig.destructionRadius,
        lifeTime = _store.weaponConfig.projectileLifetime,
        startPoint = transform.position,
        quantity = _store.weaponConfig.projectileQuantity,
        possibleTargets = possibleTargets
      });

      yield return new WaitForSeconds(_store.weaponConfig.fireRate);
    }

    _fireCoroutine = null;
  }
}