namespace ProceduralGenerationWorkshop.Terrain
{
    using UnityEngine;

    public struct MapData
    {
        public float[,] heightMap;
        public Color[] colorMap;
        public float[,] falloffMap;

        public MapData(float[,] heightMap, Color[] colorMap, float[,] falloffMap)
        {
            this.heightMap = heightMap;
            this.colorMap = colorMap;
            this.falloffMap = falloffMap;
        }
    }
}