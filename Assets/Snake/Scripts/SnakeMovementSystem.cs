using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Input;
using Unity.Tiny.Scenes;


public class SnakeMovementSystem : ComponentSystem
{
    private const float OrthographicSize = 10;

    protected override void OnUpdate()
    {
        bool shoudSpawn = false;
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

            if (inputSystem.GetKeyDown(KeyCode.Space))
            {
                shoudSpawn = true;
               
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

            snake.LastPosition = translation.Value;
            translation.Value = position;
            MoveTail();
        });
        
        if(shoudSpawn)
            SceneService.LoadSceneAsync(World.TinyEnvironment().GetConfigData<GameConfig>()
                .SnakeTailSceneReference);
    }

    private void MoveTail()
    {
        Entities.ForEach( (DynamicBuffer<SnakeSegment> segments, ref SnakeHead snake) =>
        {
            for (int i = 0; i < segments.Length; i++)
            {
                var tail = EntityManager.GetComponentData<SnakeTail>(segments[i].Reference);
                if (tail.Number == segments.Length)
                {
                    var translation = EntityManager.GetComponentData<Translation>(segments[i].Reference);
                    translation.Value = snake.LastPosition;
                    EntityManager.SetComponentData(segments[i].Reference, translation);
                    tail.Number = 1;
                    EntityManager.SetComponentData(segments[i].Reference, tail);

                }
                else
                {
                    tail.Number += 1;
                    EntityManager.SetComponentData(segments[i].Reference, tail);
                }
            }
        });
    }
}
