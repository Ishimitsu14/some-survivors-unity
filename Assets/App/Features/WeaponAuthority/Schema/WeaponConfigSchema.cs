using System;
using AutoDeconstruct;

namespace WeaponAuthority
{
  [AutoDeconstruct]
  [Serializable]
  public partial struct WeaponConfigSchema
  {
    public Weapon.ConfigScriptableObject Firestaff;
    public Weapon.ConfigScriptableObject Bloodthorn;
  }
}
