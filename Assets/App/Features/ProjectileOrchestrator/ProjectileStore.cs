
using ObservableCollections;

public class ProjectileStore : Store
{
  public ProjectilePrefabConfigSchema prefabConfig = new();

  public ObservableDictionary<int, ProjectileConfigSchema> projectiles = new();
}
