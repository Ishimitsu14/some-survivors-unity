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
internal class FindFireballTargetHandler : IActionHandler<FindFireballTargetAction, GameObject>
{
  public GameObject Execute(FindFireballTargetAction action)
  {
    var (startPoint, possibleTargets) = action;

    var (closestTargetId, position) = possibleTargets.ElementAt(Random.Range(0, possibleTargets.Count));

    return GameObject.Find(closestTargetId.ToString());
  }
}