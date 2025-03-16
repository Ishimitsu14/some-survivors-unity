using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerCollisionController))]
[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerMovementSystem))]
[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerAuthority : MonoBehaviour
{
}
