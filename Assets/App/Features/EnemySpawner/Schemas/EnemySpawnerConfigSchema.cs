
using AutoDeconstruct;
using UnityEngine;

[System.Serializable]
[AutoDeconstruct]
public partial struct EnemySpawnerConfigSchema
{
  public GameObject prefab;
  public float spawnRate;
  public float objectsPerTick;
}
