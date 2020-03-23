using Unity.Entities;
using Unity.Mathematics;
using Unity.Authoring.Core;


    

    public struct SnakeHead : IComponentData
    {
        [HideInInspector]
        public float3 Direction;
    }
