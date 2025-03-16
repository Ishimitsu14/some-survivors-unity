
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(EnemyMovementController))]
[RequireComponent(typeof(EnemyWithPlayerInteractController))]
[RequireComponent(typeof(EnemyLifeController))]
[RequireComponent(typeof(EnemyDamageController))]
public class EnemyAuthority : MonoBehaviour
{
  protected void Awake()
  {
    gameObject.tag = GameTags.Enemy;
  }
}
