using UnityEngine;
using Weapon;

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
      Spawn(Kind.Firestaff);
      Spawn(Kind.Bloodthorn);
    }

    private void Spawn(Kind kind)
    {
      var config = _store.weaponConfig.Get(kind);
      var weapon = Instantiate(config.prefab, transform);

      var circleCollider2D = weapon.GetComponent<CircleCollider2D>();
      circleCollider2D.radius = config.range;

      _store.weapons[kind] = weapon;
    }
  }
}