using System;
using AutoDeconstruct;
using Projectile;
using UnityEngine;

namespace ProjectileAuthority
{
  [Serializable]
  [AutoDeconstruct]
  public partial struct PrefabConfigSchema
  {
    public GameObject Fireball;
    public GameObject Dagger;

    public GameObject Get(Kind kind)
    {
      return kind switch
      {
        Kind.Fireball => Fireball,
        Kind.Dagger => Dagger,
        _ => throw new Exception($"Projectile Prefab for {kind} kind not defined")
      };
    }
  }
}