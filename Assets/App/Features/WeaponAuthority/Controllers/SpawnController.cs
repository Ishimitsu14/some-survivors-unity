using UnityEngine;

namespace WeaponAuthority
{
  public class SpawnController : MonoBehaviour
  {
    private Store _store;

    private void Awake()
    {
      TryGetComponent(out _store);
    }

    // TODO: Implement normal weapons spawn
    private void Start()
    {
      SpawnFirestaff();
      // SpawnBloodthorn();
    }

    private void SpawnFirestaff()
    {
      var config = _store.weaponConfig.Firestaff;
      var weapon = Instantiate(config.prefab, transform);

      var collider = weapon.GetComponent<CircleCollider2D>();
      collider.radius = config.range;

      _store.weapons[Weapon.Kind.Firestaff] = weapon;
    }

    // private void SpawnBloodthorn()
    // {
    //   var config = _store.weaponConfig.Bloodthorn;
    //   var weapon = Instantiate(config.prefab, transform);

    //   var collider = weapon.GetComponent<CircleCollider2D>();
    //   collider.radius = config.range;

    //   _store.weapons[Weapon.Kind.Bloodthorn] = weapon;
    // }
  }
}
