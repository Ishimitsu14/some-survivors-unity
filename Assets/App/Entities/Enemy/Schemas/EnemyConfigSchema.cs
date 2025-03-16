
using System;
using AutoDeconstruct;

[Serializable]
[AutoDeconstruct]
public partial struct EnemyConfigSchema
{
  public float health;
  public float damage;
}
