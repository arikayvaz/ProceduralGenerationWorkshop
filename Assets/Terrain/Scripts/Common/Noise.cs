namespace ProceduralGenerationWorkshop.Terrain
{
    using UnityEngine;

    public static class Noise
    {
        public static float[,] GenerateNoiseMap(MapSettingsSO mapSettings) 
        {
            return GenerateNoiseMap(mapSettings.mapChunkSize
                , mapSettings.mapChunkSize
                , mapSettings.Seed
                , mapSettings.MapNoiseScale
                , mapSettings.Octaves
                , mapSettings.Persistance
                , mapSettings.Lacuranity
                , mapSettings.Offset);
        }

        public static float[,] GenerateNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacuranity, Vector2 offset) 
        {
            float[,] noiseMap = new float[width, height];

            if (scale <= 0)
                scale = 0.0001f;

            System.Random random = new System.Random(seed);

            Vector2[] octaveOffsets = new Vector2[octaves];

            for (int i = 0; i < octaveOffsets.Length; i++)
            {
                float offsetX = random.Next(-100000, 100000) + offset.x;
                float offsetY = random.Next(-100000, 100000) + offset.y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }//for (int i = 0; i < octaveOffsets.Length; i++)


            float maxNoiseHeight = float.MinValue;
            float minNoseHeight = float.MaxValue;

            float halfWidth = width / 2f;
            float halfHeight = height / 2f;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float amplitude = 1f;
                    float frequency = 1f;
                    float noiseHeight = 0;

                    for (int i = 0; i < octaves; i++)
                    {
                        float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x * frequency;
                        float sampleY = (y - halfHeight) / scale * frequency - octaveOffsets[i].y * frequency;

                        float perlin = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseHeight += perlin * amplitude;

                        amplitude *= persistance;
                        frequency *= lacuranity;
                    }//for (int i = 0; i < octaves; i++)

                    if (noiseHeight > maxNoiseHeight)
                        maxNoiseHeight = noiseHeight;
                    else if (noiseHeight < minNoseHeight)
                        minNoseHeight = noiseHeight;

                    noiseMap[x, y] = noiseHeight;

                }//for (int x = 0; x < width; x++)

            }//for (int y = 0; y < height; y++)

            //Normalize noise map
            for (int y = 0; y < height; y++) 
                for (int x = 0; x < width; x++)
                    noiseMap[x, y] = Mathf.InverseLerp(minNoseHeight, maxNoiseHeight, noiseMap[x, y]);


            return noiseMap;
        }
    }
}