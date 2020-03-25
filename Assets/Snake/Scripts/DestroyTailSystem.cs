using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Scenes;

public class DestroyTailSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        bool destoryAll = false;
        Entities.ForEach((Entity EntityManager, ref SnakeHead snake, ref Translation translation) =>
        {
            if(!snake.RemoveTail) 
                return;
            destoryAll = true;
            snake.Direction = float3.zero;
            translation.Value = float3.zero;
            snake.RemoveTail = false;
        });
        if (destoryAll)
        {
            SceneService.UnloadAllSceneInstances(World.TinyEnvironment().GetConfigData<GameConfig>().SnakeTailSceneReference);
        }
    }
}
