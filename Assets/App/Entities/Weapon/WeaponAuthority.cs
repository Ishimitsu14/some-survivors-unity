using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(WeaponFireController))]
[RequireComponent(typeof(WeaponCollisionController))]
[RequireComponent(typeof(WeaponDirectionController))]
[RequireComponent(typeof(WeaponStore))]
public class WeaponAuthority : MonoBehaviour
{
}