using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public sealed class LevelECS : MonoBehaviour
{
    EcsSystems _systems;

    void Start()
    {
        var world = new EcsWorld();
        _systems = new EcsSystems(world);

        _systems
            .Add(new s_SampleSystem())
            .Inject(FindObjectOfType<LevelSettings>())
            .Init();
    }

    void Update()
    {
        _systems?.Run();
    }

    void OnDestroy()
    {
        _systems?.Destroy();
        _systems?.GetWorld()?.Destroy();
        _systems = null;
    }
}
