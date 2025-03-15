using System.Collections;
using Pure.DI;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Dependency] public ActionManager ActionManager { private get; set; }

    private EnemySpawnerStore _store;
    private Camera _camera;

    private void Awake()
    {
        Composition.Shared.BuildUp(this);

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
            var prefab = _store.spawnerConfigSchema.prefab;
            var spawnCount = _store.spawnerConfigSchema.countObjectsPerSecond;
            var spawnInterval = _store.spawnerConfigSchema.spawnEverySeconds;

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnPrefab(prefab);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPrefab(GameObject prefab)
    {
        ActionManager.Execute(new SpawnEnemyOutOfCameraAction
            {
                camera = _camera,
                prefab = prefab,
            }
        );
    }
}