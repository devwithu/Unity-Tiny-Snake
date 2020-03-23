using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Input;



public class SnakeMovementSystem : ComponentSystem
{
    private const float OrthographicSize = 10;

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

            var displayInfo = tinyEnv.GetConfigData<DisplayInfo>();
            var aspectRatio = (float) displayInfo.width / displayInfo.height;
            var xScreenSize = aspectRatio * OrthographicSize;
            
            position += snake.Direction ;

            if (position.x > xScreenSize)
                position.x = -xScreenSize;
            else if (position.x < -xScreenSize)
                position.x = xScreenSize;
            else if (position.y > OrthographicSize)
                position.y = -OrthographicSize;
            else if (position.y < -OrthographicSize)
                position.y = OrthographicSize;
            
            
            translation.Value = position;
        });
    }
}
