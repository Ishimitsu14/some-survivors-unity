using System;
using UnityEngine;

public class CollisionRelay : MonoBehaviour
{
  public Action<Collider2D> OnTriggerEnter2DRelay;
  public Action<Collider2D> OnTriggerExit2DRelay;

  private void OnTriggerEnter2D(Collider2D other)
  {
    OnTriggerEnter2DRelay?.Invoke(other);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    OnTriggerExit2DRelay?.Invoke(other);
  }
}
