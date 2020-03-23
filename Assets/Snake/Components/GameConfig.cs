using Unity.Entities;
using Unity.Tiny.Scenes;


public struct GameConfig : IComponentData
    {
        public float TickRate;
        public float LastFrameTime;
        public SceneReference SnakeTailSceneReference;
    }


