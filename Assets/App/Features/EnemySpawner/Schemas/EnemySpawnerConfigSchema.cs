
using System;
using AutoDeconstruct;
using UnityEngine;

[Serializable]
[AutoDeconstruct]
public partial struct EnemySpawnerConfigSchema
{
  public GameObject prefab;
  public float spawnRate;
  public float objectsPerTick;
}
