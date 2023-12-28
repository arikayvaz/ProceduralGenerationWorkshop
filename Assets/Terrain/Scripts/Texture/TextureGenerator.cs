namespace ProceduralGenerationWorkshop.Terrain
{
    using Common;
    using UnityEngine;

    public class TextureGenerator : Singleton<TextureGenerator>
    {
        public Texture2D GenerateTextureFromHeightMap(float[,] heightMap) 
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);

            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;

            Color[] colorMap = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                }//for (int x = 0; x < width; x++)
            }//for (int y = 0; y < height; y++)

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }

        public Texture2D GenerateTextureFromColorMap(Color[] colorMap, int width, int height) 
        {
            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }

        public Texture2D GenerateTextureFromFalloffMap(float[,] falloffMap) 
        {
            int width = falloffMap.GetLength(0);
            int height = falloffMap.GetLength(1);

            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;

            Color[] colorMap = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, falloffMap[x, y]);
                }//for (int x = 0; x < width; x++)
            }//for (int y = 0; y < height; y++)

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }
    }
}