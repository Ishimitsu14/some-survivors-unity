using UnityEngine;

namespace Weapon
{
  [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
  public class ConfigScriptableObject : ScriptableObject
  {
    public GameObject prefab;
    public float damage;
    public float range;
    public float fireRate;
    public float destructionRadius;
    public AttackType attackType;
    public float projectileSpeed;
    public float projectileDelay;
    public Projectile.Kind projectileKind;
    public int projectileQuantity;
    public float projectileLifetime;
  }
}
