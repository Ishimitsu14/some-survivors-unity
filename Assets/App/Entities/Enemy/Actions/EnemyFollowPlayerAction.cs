using Unity.Mathematics;
using UnityEngine;

public struct EnemyFollowPlayerAction
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public EnemyStore enemyStore;
}

[ActionHandler(typeof(EnemyFollowPlayerAction))]
public class EnemyFollowPlayerActionHandler : IActionHandler<EnemyFollowPlayerAction>
{
    public void Execute(EnemyFollowPlayerAction action)
    {
        action.enemyStore.movementSchema.direction = math.normalize(
            action.playerTransform.position - action.enemyTransform.position
        );
    }
}