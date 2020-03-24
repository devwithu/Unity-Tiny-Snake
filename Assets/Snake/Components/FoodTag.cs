using Unity.Entities;

public struct FoodTag : IComponentData
{
    public bool ShoudMove;
    public static FoodTag Default {get; } = new FoodTag
    {
      ShoudMove = true
    };
}
