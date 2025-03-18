using System;
using AutoDeconstruct;
using UnityEngine;
using Weapon;

namespace WeaponAuthority
{
  [AutoDeconstruct]
  [Serializable]
  public partial struct WeaponConfigSchema
  {
    [SerializeField] private ConfigScriptableObject firestaff;
    [SerializeField] private ConfigScriptableObject bloodthorn;

    public ConfigScriptableObject Get(Kind kind)
    {
      return kind switch
      {
        Kind.Firestaff => firestaff,
        Kind.Bloodthorn => bloodthorn,
        _ => throw new Exception($"Weapon Config for {kind} kind not defined")
      };
    }
  }
}