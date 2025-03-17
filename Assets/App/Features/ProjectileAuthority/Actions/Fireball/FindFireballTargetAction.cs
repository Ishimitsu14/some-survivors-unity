using System.Collections.Generic;
using System.Linq;
using AutoDeconstruct;
using UnityEngine;

namespace ProjectileAuthority
{
  [AutoDeconstruct]
  public partial struct FindFireballTargetAction
  {
    public Vector3 startPoint;
    public Dictionary<int, Vector3> possibleTargets;
  }

  [ActionHandler(typeof(FindFireballTargetAction))]
  internal class FindFireballTargetHandler : IActionHandler<FindFireballTargetAction, GameObject>
  {
    public GameObject Execute(FindFireballTargetAction action)
    {
      var (startPoint, possibleTargets) = action;

      var closestTargetId = possibleTargets.First().Key;
      var minDistance = float.MaxValue;

      foreach (var (key, value) in possibleTargets)
      {
        var distance = Vector2.Distance(startPoint, value);
        if (!(distance < minDistance)) continue;
        minDistance = distance;
        closestTargetId = key;
      }

      return GameObject.Find(closestTargetId.ToString());
    }
  }
}
