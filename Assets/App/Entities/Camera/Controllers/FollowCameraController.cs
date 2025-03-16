using UnityEngine;

[DisallowMultipleComponent]
public class FollowCameraController : MonoBehaviour
{
  private void OnEnable()
  {
    EventManager<OnPlayerMove>.On += OnPlayerMove;
  }

  private void OnDisable()
  {
    EventManager<OnPlayerMove>.On -= OnPlayerMove;
  }

  private void OnPlayerMove(OnPlayerMove data)
  {
    transform.position = new Vector3(data.position.x, data.position.y, transform.position.z);
  }
}