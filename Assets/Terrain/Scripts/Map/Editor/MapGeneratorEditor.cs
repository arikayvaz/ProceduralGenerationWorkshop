namespace ProceduralGenerationWorkshop.Terrain
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        MapGenerator generator = null;

        private void OnEnable()
        {
            generator = target as MapGenerator;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Generate Map"))
            {
                generator.GenerateMapInEditor();
                return;
            }

            GUILayout.Space(10f);
            DrawDefaultInspector();
        }
    }
}