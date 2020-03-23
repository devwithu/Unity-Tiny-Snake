 using Unity.Entities;

public struct SnakeTail : IComponentData
{
    public bool IsCreated;
    public int Number;
    
    public static SnakeTail Defalut
    {
        get
        {
            var tail = new SnakeTail
            {
                IsCreated = false
            };
            return tail;
        }
        //set {  }
    }
}

