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
  public class WeaponData
  {
    public HashSet<GameObject> enemies = new();
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

      _weaponData.TryAdd(kind, new WeaponData());
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

      if (weaponData.enemies.Add(enemy)) weaponData.fireCoroutine ??= StartCoroutine(FireCoroutine(kind));
    }

    private void OnEnemyOutWeaponRange((GameObject weapon, GameObject enemy, Kind kind) data)
    {
      var (weapon, enemy, kind) = data;

      if (!_weaponData.TryGetValue(kind, out var weaponData)) return;

      weaponData.enemies.Remove(enemy);

      if (weaponData.enemies.Count != 0 || _weaponData[kind].fireCoroutine == null) return;

      StopCoroutine(weaponData.fireCoroutine);
      weaponData.fireCoroutine = null;
    }

    private IEnumerator FireCoroutine(Kind kind)
    {
      var weaponData = _weaponData[kind];
      var config = _store.weaponConfig.Get(kind);

      while (weaponData.enemies.Count > 0)
      {
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
            weaponDirection = _store.direction
          });
          yield return new WaitForSeconds(config.projectileDelay);
        }

        yield return new WaitForSeconds(config.fireRate);
      }

      weaponData.fireCoroutine = null;
    }
  }
}