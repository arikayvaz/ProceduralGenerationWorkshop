namespace ProceduralGenerationWorkshop.Terrain
{
    using Common;
    using UnityEngine;

    public class MapGenerator : Singleton<MapGenerator>
    {
        [Header("Settings")]
        [SerializeField] MapSettingsSO mapSettings = null;
        public MapSettingsSO MapSettings => mapSettings;

        [Space]
        [SerializeField] MapDisplay mapDisplay = null;
        [SerializeField] DisPlayMode disPlayMode = DisPlayMode.None;

        private void Start()
        {
            GenerateMap();
        }

        public void GenerateMap() 
        {
            MapData mapData = GenerateMapData();

            MeshData meshData = MeshGenerator.Instance.GenerateTerrainMesh(mapData.heightMap
                , mapSettings.MeshHeightMultiplier
                , mapSettings.MeshHeightCurve
                , 1);

            //Texture2D texture = TextureGenerator.Instance.GenerateTextureFromColorMap(mapData.colorMap, mapSettings.mapChunkSize, mapSettings.mapChunkSize);
            //mapDisplay.DrawMesh(meshData, texture);

            switch (disPlayMode)
            {
                case DisPlayMode.HeightMap:
                    mapDisplay.DrawHeightMap(TextureGenerator.Instance.GenerateTextureFromHeightMap(mapData.heightMap));
                    break;
                case DisPlayMode.ColorMap:
                    mapDisplay.DrawColorMap(TextureGenerator.Instance.GenerateTextureFromColorMap(mapData.colorMap, mapSettings.mapChunkSize, mapSettings.mapChunkSize));
                    break;
                case DisPlayMode.FalloffMap:
                    mapDisplay.DrawFalloffMap(TextureGenerator.Instance.GenerateTextureFromFalloffMap(mapData.falloffMap));
                    break;
                case DisPlayMode.Mesh:
                    Texture2D texture = TextureGenerator.Instance.GenerateTextureFromColorMap(mapData.colorMap, mapSettings.mapChunkSize, mapSettings.mapChunkSize);
                    mapDisplay.DrawMesh(meshData, texture);
                    break;
                default:
                    break;
            }
        }

        public void GenerateMapInEditor() 
        {
            MeshGenerator meshGenerator = FindObjectOfType<MeshGenerator>();
            TextureGenerator textureGenerator = FindObjectOfType<TextureGenerator>();

            MapData mapData = GenerateMapData();

            MeshData meshData = meshGenerator.GenerateTerrainMesh(mapData.heightMap
                , mapSettings.MeshHeightMultiplier
                , mapSettings.MeshHeightCurve
                , 1);

            Texture2D texture = textureGenerator.GenerateTextureFromColorMap(mapData.colorMap, mapSettings.mapChunkSize, mapSettings.mapChunkSize);

            mapDisplay.DrawMesh(meshData, texture);
        }

        public MapData GenerateMapData() 
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(mapSettings);
            Color[] colorMap = new Color[mapSettings.mapChunkSize * mapSettings.mapChunkSize];

            float[,] falloffMap = Noise.GenerateFalloffMap(mapSettings.mapChunkSize, mapSettings.FalloffParamA, mapSettings.FalloffParamB);

            for (int y = 0; y < mapSettings.mapChunkSize; y++) 
            {
                for (int x = 0; x < mapSettings.mapChunkSize; x++) 
                {
                    if (mapSettings.UseFalloff)
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);

                    colorMap[y * mapSettings.mapChunkSize + x] = mapSettings.GetMapColor(noiseMap[x, y]);
                }
            }

            return new MapData(noiseMap, colorMap, falloffMap);
        }
    }
}