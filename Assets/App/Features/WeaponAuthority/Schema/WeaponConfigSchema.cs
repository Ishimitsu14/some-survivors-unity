using System;
using AutoDeconstruct;
using Weapon;

namespace WeaponAuthority
{
  [AutoDeconstruct]
  [Serializable]
  public partial struct WeaponConfigSchema
  {
    public ConfigScriptableObject Firestaff;
    public ConfigScriptableObject Bloodthorn;

    public ConfigScriptableObject Get(Kind kind)
    {
      return kind switch
      {
        Kind.Firestaff => Firestaff,
        Kind.Bloodthorn => Bloodthorn,
        _ => throw new Exception($"Weapon Config for {kind} kind not defined")
      };
    }
  }
}