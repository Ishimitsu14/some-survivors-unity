using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using Projectile;
using R3;
using R3.Triggers;
using UnityEngine;
using Kind = Weapon.Kind;

namespace WeaponAuthority
{
  public struct WeaponData
  {
    public HashSet<GameObject> enemies;
    public Coroutine fireCoroutine;
  }

  public class FireController : MonoBehaviour
  {
    private readonly Dictionary<Kind, WeaponData> _weaponData = new();
    private DisposableBag _disposableBag;
    private Store _store;

    private void Awake()
    {
      TryGetComponent(out _store);
    }

    private void OnEnable()
    {
      _disposableBag = new DisposableBag();

      _store.weapons
        .ObserveDictionaryAdd()
        .Subscribe(OnAdd)
        .AddTo(ref _disposableBag);

      _store.weapons
        .ObserveDictionaryRemove()
        .Subscribe(OnRemove)
        .AddTo(ref _disposableBag);
    }

    private void OnDisable()
    {
      _disposableBag.Dispose();
    }

    private void OnAdd(DictionaryAddEvent<Kind, GameObject> item)
    {
      var (kind, weapon) = item;

      weapon
        .OnTriggerEnter2DAsObservable()
        .Select(other => (weapon, other.gameObject, kind))
        .Subscribe(OnEnemyInWeaponRange)
        .AddTo(ref _disposableBag);

      weapon
        .OnTriggerExit2DAsObservable()
        .Select(other => (weapon, other.gameObject, kind))
        .Subscribe(OnEnemyOutWeaponRange)
        .AddTo(ref _disposableBag);

      var weaponData = new WeaponData { enemies = new HashSet<GameObject>() };

      if (!_weaponData.TryAdd(kind, weaponData)) return;

      weaponData.fireCoroutine = StartCoroutine(FireCoroutine(kind));
    }

    private void OnRemove(DictionaryRemoveEvent<Kind, GameObject> item)
    {
      var (kind, weapon) = item;

      if (!_weaponData.TryGetValue(kind, out var weaponData)) return;

      if (weaponData.fireCoroutine != null) StopCoroutine(weaponData.fireCoroutine);
      _weaponData.Remove(kind);
    }

    private void OnEnemyInWeaponRange((GameObject weapon, GameObject enemy, Kind kind) data)
    {
      var (weapon, enemy, kind) = data;

      if (!_weaponData.TryGetValue(kind, out var weaponData)) return;

      weaponData.enemies.Add(enemy);
    }

    private void OnEnemyOutWeaponRange((GameObject weapon, GameObject enemy, Kind kind) data)
    {
      var (weapon, enemy, kind) = data;

      if (!_weaponData.TryGetValue(kind, out var weaponData)) return;

      weaponData.enemies.Remove(enemy);
    }

    private IEnumerator FireCoroutine(Kind kind)
    {
      var weaponData = _weaponData[kind];
      var config = _store.weaponConfig.Get(kind);

      while (true)
      {
        if (weaponData.enemies.Count > 0)
        {
          var lastDirection = _store.direction;

          for (var i = 0; i < config.projectileQuantity; i++)
          {
            var possibleTargets = weaponData.enemies.ToDictionary(
              item => item.GetInstanceID(),
              item => item.transform.position
            );

            EventManager<OnSpawnRequest>.Emit(new OnSpawnRequest
            {
              kind = config.projectileKind,
              damage = config.damage,
              moveSpeed = config.projectileSpeed,
              destructionRadius = config.destructionRadius,
              lifeTime = config.projectileLifetime,
              startPoint = transform.position,
              possibleTargets = possibleTargets,
              weaponDirection = lastDirection,
              prefab = config.projectilePrefab
            });

            yield return new WaitForSeconds(config.projectileDelay);
          }
        }

        yield return new WaitForSeconds(config.fireRate);
      }
    }
  }
}