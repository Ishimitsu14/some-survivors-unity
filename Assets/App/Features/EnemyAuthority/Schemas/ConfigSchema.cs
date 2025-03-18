using System;
using AutoDeconstruct;
using UnityEngine;

namespace EnemyAuthority
{
  [Serializable]
  [AutoDeconstruct]
  public partial struct ConfigSchema
  {
    public GameObject prefab;
    public float spawnRate;
    public float objectsPerTick;
  }
}
