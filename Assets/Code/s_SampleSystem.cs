using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public class s_SampleSystem : IEcsInitSystem, IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<c_Renderer>> _filter = default;
    private readonly EcsPoolInject<c_Renderer> _renderersPool = default;
    private readonly EcsCustomInject<LevelSettings> _levelSettings = default;

    public void Init(IEcsSystems systems)
    {
        var renderers = Object.FindObjectsOfType<Renderer>();
        foreach (var renderer in renderers)
        {
            var entity = systems.GetWorld().NewEntity();
            ref var c_renderer = ref _renderersPool.Value.Add(entity);
            c_renderer.Renderer = renderer;
        }
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref var c_renderer = ref _filter.Pools.Inc1.Get(entity);
            c_renderer.Renderer.material.color = _levelSettings.Value.TargetColor * Random.value;
        }
    }
}
