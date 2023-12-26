namespace ProceduralGenerationWorkshop.Terrain
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "MapSettings", menuName = "Workshop/Terrain/Map/Settings")]
    public class MapSettingsSO : ScriptableObject
    {
        public readonly int mapChunkSize = 241;
        [field: SerializeField] public float MapNoiseScale { get; private set; } = 0f;
        [field: SerializeField, Min(0)] public int Octaves { get; private set; } = 0;
        [field: SerializeField, Range(0f, 1f)] public float Persistance { get; private set; } = 0f;
        [field: SerializeField, Min(1f)] public float Lacuranity { get; private set; } = 0f;
        [field: SerializeField] public int Seed { get; private set; } = 0;
        [field: SerializeField] public Vector2 Offset { get; private set; } = Vector2.zero;
        [field: SerializeField, Min(1f)] public float MeshHeightMultiplier { get; private set; } = 1f;
        [field: SerializeField] public AnimationCurve MeshHeightCurve { get; private set; } = null;
    }
}