using System.Collections.Generic;
using System.Linq;
using AutoDeconstruct;
using UnityEngine;

[AutoDeconstruct]
public partial struct FindFireballTargetAction
{
  public Vector3 startPoint;
  public Dictionary<int, Vector3> possibleTargets;
}

[ActionHandler(typeof(FindFireballTargetAction))]
class FindFireballTargetHandler : IActionHandler<FindFireballTargetAction, GameObject>
{
  public GameObject Execute(FindFireballTargetAction action)
  {
    var (startPoint, possibleTargets) = action;

    int closestTargetId = possibleTargets.First().Key;
    float minDistance = float.MaxValue;

    foreach (var (Key, Value) in possibleTargets)
    {
      float distance = Vector2.Distance(startPoint, Value);
      if (distance < minDistance)
      {
        minDistance = distance;
        closestTargetId = Key;
      }
    }

    return GameObject.Find(closestTargetId.ToString());
  }
}
