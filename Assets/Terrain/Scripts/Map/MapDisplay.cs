namespace ProceduralGenerationWorkshop.Terrain
{
    using UnityEngine;

    public enum DisPlayMode { None, HeightMap, ColorMap, FalloffMap, Mesh }

    public class MapDisplay : MonoBehaviour
    {
        [Header("Map")]
        [SerializeField] Renderer mapRenderer;

        [Header("Mesh")]
        [Space]
        [SerializeField] MeshRenderer meshDisplayRenderer;
        [SerializeField] MeshFilter meshDisplayFilter;

        public void DrawTexture(Texture2D texture) 
        {
            mapRenderer.sharedMaterial.mainTexture = texture;
            mapRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }

        public void DrawHeightMap(Texture2D texture) 
        {
            DrawTexture(texture);
            UpdateDisplay(DisPlayMode.HeightMap);
        }

        public void DrawColorMap(Texture2D texture)
        {
            DrawTexture(texture);
            UpdateDisplay(DisPlayMode.ColorMap);
        }

        public void DrawFalloffMap(Texture2D texture) 
        {
            DrawTexture(texture);
            UpdateDisplay(DisPlayMode.FalloffMap);
        }

        public void DrawMesh(MeshData meshData, Texture2D texture) 
        {
            Mesh mesh = meshData.CreateMesh();

            meshDisplayFilter.mesh = mesh;
            meshDisplayRenderer.material.mainTexture = texture;

            UpdateDisplay(DisPlayMode.Mesh);
        }

        private void UpdateDisplay(DisPlayMode displayMode) 
        {
            mapRenderer.gameObject.SetActive(displayMode != DisPlayMode.Mesh);
            meshDisplayRenderer.gameObject.SetActive(displayMode == DisPlayMode.Mesh);
        }
    }
}