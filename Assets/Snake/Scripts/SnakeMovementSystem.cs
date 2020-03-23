using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Input;



public class SnakeMovementSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity Entity, ref SnakeHead snake, ref Translation translation) =>
        {
            var position = translation.Value;
            var inputSystem = World.GetExistingSystem<InputSystem>();
            if (inputSystem.GetKey(KeyCode.W))
            {
                snake.Direction = new float3(0,1,0);
            }
            else if (inputSystem.GetKey(KeyCode.A))
            {
                snake.Direction = new float3(-1,0,0);
            }
            else if (inputSystem.GetKey(KeyCode.S))
            {
                snake.Direction = new float3(0,-1,0);
            }
            else if (inputSystem.GetKey(KeyCode.D))
            {
                snake.Direction = new float3(1,0,0);
            }

            var tinyEnv = World.TinyEnvironment();
            var gameConfig = tinyEnv.GetConfigData<GameConfig>();
            if (tinyEnv.frameTime - gameConfig.LastFrameTime < gameConfig.TickRate)
                return;

            gameConfig.LastFrameTime = (float)tinyEnv.frameTime;
            tinyEnv.SetConfigData(gameConfig);
            position += snake.Direction ;
            translation.Value = position;
        });
    }
}
