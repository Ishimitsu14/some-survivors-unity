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
    var pos = new Vector3(data.position.x, data.position.y, transform.position.z); 
    pos.x = Mathf.Round(pos.x * 100) / 100;
    pos.y = Mathf.Round(pos.y * 100) / 100;
    transform.position = pos;
  }
}