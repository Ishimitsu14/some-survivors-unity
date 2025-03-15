using System;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    public Action<Collider2D> OnBulletCollisionEnter;
    public Action<Collider2D> OnBulletCollisionExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBulletCollisionEnter?.Invoke(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        OnBulletCollisionExit?.Invoke(other);
    }
}