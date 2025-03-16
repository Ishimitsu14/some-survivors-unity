using System;
using AutoDeconstruct;
using UnityEngine;

[Serializable]
[AutoDeconstruct]
public partial struct GameObjectPrefabSchema
{
  public GameObject prefab;
}
