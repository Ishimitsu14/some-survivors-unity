using AutoDeconstruct;
using UnityEngine;


[System.Serializable]
[AutoDeconstruct]
public partial struct WeaponConfigSchema
{
  public float damage;
  public float range;
  public float fireRate;
  public float punchPower;

  public MonoBehaviour weaponComponent;
  public GameObject bulletPrefab;
}
