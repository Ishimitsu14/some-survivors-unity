using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerCollisionController))]
[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerAuthority : MonoBehaviour
{
}