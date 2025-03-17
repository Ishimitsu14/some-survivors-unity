using AutoDeconstruct;
using UnityEngine;

namespace EnemyAuthority
{
  [AutoDeconstruct]
  public partial struct SpawnOutOfBoundsAction
  {
    public Camera camera;
    public GameObject prefab;
  }

  [ActionHandler(typeof(SpawnOutOfBoundsAction))]
  public class SpawnOutOfBoundsActionHandler : IActionHandler<SpawnOutOfBoundsAction>
  {
    public void Execute(SpawnOutOfBoundsAction action)
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

      var enemy = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);

      enemy.name = enemy.GetInstanceID().ToString();
    }
  }
}
