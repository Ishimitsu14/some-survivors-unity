using UnityEngine;

namespace WeaponAuthority
{
  [DisallowMultipleComponent]
  [RequireComponent(typeof(FireController))]
  [RequireComponent(typeof(DirectionController))]
  [RequireComponent(typeof(SpawnController))]
  [RequireComponent(typeof(Store))]
  public class Entry : MonoBehaviour
  {
  }
}
