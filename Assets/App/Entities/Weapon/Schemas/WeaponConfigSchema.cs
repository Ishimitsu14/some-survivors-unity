using UnityEngine;


[System.Serializable]
public struct WeaponConfigSchema
{
    public float damage;
    public float range;
    public float fireRate;
    public float punchPower;
    
    public MonoBehaviour weaponComponent;
    public GameObject bulletPrefab;
}