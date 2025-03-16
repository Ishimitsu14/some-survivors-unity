using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
  private Camera _camera;
  private EnemySpawnerStore _store;

  private void Awake()
  {
    TryGetComponent(out _store);
    _camera = Camera.main;
  }

  private void Start()
  {
    StartCoroutine(SpawnCoroutine());
  }

  private IEnumerator SpawnCoroutine()
  {
    while (true)
    {
      var prefab = _store.spawnerConfig.prefab;
      var spawnCount = _store.spawnerConfig.objectsPerTick;
      var spawnInterval = _store.spawnerConfig.spawnRate;

      for (var i = 0; i < spawnCount; i++) SpawnPrefab(prefab);

      yield return new WaitForSeconds(spawnInterval);
    }
  }

  private void SpawnPrefab(GameObject prefab)
  {
    ActionManager.Instance.Execute(new SpawnEnemyOutOfCameraAction
      {
        camera = _camera,
        prefab = prefab
      }
    );
  }
}