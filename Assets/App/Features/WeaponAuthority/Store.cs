using ObservableCollections;
using UnityEngine;

namespace WeaponAuthority
{
  public class Store : StoreBase
  {
    public Vector3 direction = Vector3.zero;
    public WeaponConfigSchema weaponConfig;
    public readonly ObservableDictionary<Weapon.Kind, GameObject> weapons = new();
  }
}
