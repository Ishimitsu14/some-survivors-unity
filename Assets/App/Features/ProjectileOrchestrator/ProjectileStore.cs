using ObservableCollections;

public class ProjectileStore : Store
{
  public ProjectilePrefabConfigSchema prefabConfig;

  public readonly ObservableDictionary<int, ProjectileConfigSchema> projectiles = new();
}