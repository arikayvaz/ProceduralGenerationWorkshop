namespace ProceduralGenerationWorkshop.Terrain
{
    using Common;
    using UnityEngine;

    public class MapGenerator : Singleton<MapGenerator>
    {
        [Header("Settings")]
        [SerializeField] MapSettingsSO mapSettings = null;

        [Space]
        [SerializeField] MapDisplay mapDisplay = null;

        private void Start()
        {
            GenerateMap();
        }

        public void GenerateMap() 
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(mapSettings);

            MeshData meshData = MeshGenerator.Instance.GenerateTerrainMesh(noiseMap
                , mapSettings.MeshHeightMultiplier
                , mapSettings.MeshHeightCurve
                , 1);

            Texture2D texture = TextureGenerator.Instance.GenerateTextureFromHeightMap(noiseMap);

            mapDisplay.DrawMesh(meshData, texture);
        }
    }
}