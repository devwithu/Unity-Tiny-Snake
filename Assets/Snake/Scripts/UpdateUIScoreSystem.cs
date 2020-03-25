using Unity.Entities;
using Unity.Tiny.Text;
using Unity.Tiny.Core;

public class UpdateUIScoreSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var tailCount = 0;
        Entities.WithAll<SnakeTail>().ForEach((Entity entity) => { tailCount++; });
        
        Entities.WithAll<UIScoreTag>().ForEach((Entity entity) =>
        {
            EntityManager.SetBufferFromString<TextString>(entity, "Score: " + tailCount.ToString());
            
        });
    }
}
