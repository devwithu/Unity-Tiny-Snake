using Unity.Entities;

public class InitTailSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach( (Entity entity, ref SnakeTail tail) =>
        {
            var count = 0;
            if (tail.IsCreated)
                return;
            tail.IsCreated = true;
            Entities.ForEach((DynamicBuffer<SnakeSegment> segments) =>
            {
                segments.Add(new SnakeSegment
                {
                    Reference = entity
                });
                count = segments.Length;
            });
            tail.Number = count;
        });
    }
}
