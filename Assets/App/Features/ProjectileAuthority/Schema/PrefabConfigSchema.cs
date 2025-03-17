using System;
using AutoDeconstruct;
using UnityEngine;

namespace ProjectileAuthority
{
  [Serializable]
  [AutoDeconstruct]
  public partial struct PrefabConfigSchema
  {
    public GameObject Fireball;
    public GameObject Dagger;
  }
}
