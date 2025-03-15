
using AutoDeconstruct;
using UnityEngine;

[System.Serializable]
[AutoDeconstruct]
public partial struct SpawnerConfigSchema
{
  public GameObject prefab;
  public float spawnEverySeconds;
  public float countObjectsPerSecond;
}
