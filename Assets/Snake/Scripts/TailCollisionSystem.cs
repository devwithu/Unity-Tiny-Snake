using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core2D;

public class TailCollisionSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((DynamicBuffer<SnakeSegment> segments, ref Translation translation, ref SnakeHead snake) =>
        {
            var segs = segments.Reinterpret<Entity>().ToNativeArray((Unity.Collections.Allocator.Temp));
            for (int i = 0; i < segs.Length; i++)
            {
                var tailTranslation = EntityManager.GetComponentData<Translation>(segs[i]);
                if (math.distance(translation.Value, tailTranslation.Value) < 1)
                {
                    snake.RemoveTail = true;
                    segments.Clear();
                } 
            }
            segs.Dispose();
        });
    }
}
