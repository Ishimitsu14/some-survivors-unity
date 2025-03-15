
using AutoDeconstruct;

[System.Serializable]
[AutoDeconstruct]
public partial struct EnemyBaseDataSchema
{
  public float maxHealth;
  public float currentHealth;
  public float damage;
}
