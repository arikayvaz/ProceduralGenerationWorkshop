namespace ProceduralGenerationWorkshop.Terrain
{
    using UnityEngine;

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

        public void DrawMesh(MeshData meshData, Texture2D texture) 
        {
            meshDisplayFilter.mesh = meshData.CreateMesh();
            //meshDisplayRenderer.material.mainTexture = texture;
            meshDisplayRenderer.material.SetTexture("_heightMap", texture);
        }
    }
}