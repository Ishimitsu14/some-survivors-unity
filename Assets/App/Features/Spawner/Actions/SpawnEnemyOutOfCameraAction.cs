using UnityEngine;

public struct SpawnEnemyOutOfCameraAction
{
    public Camera camera;
    public GameObject prefab;

    public void Deconstruct(out Camera camera, out GameObject prefab)
    {
        camera = this.camera;
        prefab = this.prefab;
    }
}

[ActionHandler(typeof(SpawnEnemyOutOfCameraAction))]
public class SpawnEnemyOutOfCameraActionHandler : IActionHandler<SpawnEnemyOutOfCameraAction>
{
    public void Execute(SpawnEnemyOutOfCameraAction action)
    {
        var (camera, prefab) = action;

        var screenLeft = camera.transform.position.x - camera.orthographicSize * camera.aspect;
        var screenRight = camera.transform.position.x + camera.orthographicSize * camera.aspect;
        var screenBottom = camera.transform.position.y - camera.orthographicSize;
        var screenTop = camera.transform.position.y + camera.orthographicSize;

        var spawnOffset = 5f;

        var spawnHorizontally = Random.value > 0.5f;
        Vector3 spawnPosition;

        if (spawnHorizontally)
        {
            var xPos = Random.value > 0.5f ? screenRight + spawnOffset : screenLeft - spawnOffset;
            var yPos = Random.Range(screenBottom, screenTop);
            spawnPosition = new Vector3(xPos, yPos, 0);
        }
        else
        {
            var yPos = Random.value > 0.5f ? screenTop + spawnOffset : screenBottom - spawnOffset;
            var xPos = Random.Range(screenLeft, screenRight);
            spawnPosition = new Vector3(xPos, yPos, 0);
        }

        Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}