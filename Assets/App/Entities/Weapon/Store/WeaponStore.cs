using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class WeaponStore : Store
{
  public WeaponConfigSchema weaponConfig;
  public GameObject weaponHolder;
  public ObservableCollection<BulletSchema> bullets = new();
}
