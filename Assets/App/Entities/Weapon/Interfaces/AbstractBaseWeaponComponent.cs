using UnityEngine;

public abstract class BaseWeaponComponent : MonoBehaviour
{
    protected abstract void HandleBulletCollision(Collider2D collider, BulletSchema bulletSchema);
}