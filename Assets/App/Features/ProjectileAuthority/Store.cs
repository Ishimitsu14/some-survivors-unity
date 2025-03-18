using ObservableCollections;
using UnityEngine;

namespace ProjectileAuthority
{
  public class Store : StoreBase
  {
    public readonly ObservableDictionary<GameObject, ProjectileConfigSchema> projectiles = new();
  }
}
