using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ModelPlacerEditor : EditorWindow
{
    private string folderPath = "Assets/";
    private float xSpacing = 2f;
    private float zSpacing = 2f;
    private int modelsPerRow = 5;

    [MenuItem("Tools/Model Placer")]
    public static void ShowWindow()
    {
        GetWindow<ModelPlacerEditor>("Model Placer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Model Placement Settings", EditorStyles.boldLabel);

        folderPath = EditorGUILayout.TextField("Folder Path (e.g., Assets/Models)", folderPath);
        xSpacing = EditorGUILayout.FloatField("X Spacing", xSpacing);
        zSpacing = EditorGUILayout.FloatField("Z Spacing", zSpacing);
        modelsPerRow = EditorGUILayout.IntField("Models Per Row", modelsPerRow);

        if (GUILayout.Button("Place Models"))
        {
            PlaceModels();
        }
    }

    private void PlaceModels()
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogError("Folder path cannot be empty.");
            return;
        }

        // Ensure the path starts with "Assets/"
        if (!folderPath.StartsWith("Assets/"))
        {
            folderPath = "Assets/" + folderPath;
        }

        // Remove trailing slash if present, unless it's just "Assets/"
        if (folderPath.Length > "Assets/".Length && folderPath.EndsWith("/"))
        {
            folderPath = folderPath.Substring(0, folderPath.Length - 1);
        }

        string[] guids = AssetDatabase.FindAssets("t:Model", new[] { folderPath });
        List<GameObject> modelsToPlace = new List<GameObject>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (model != null)
            {
                modelsToPlace.Add(model);
            }
        }

        if (modelsToPlace.Count == 0)
        {
            Debug.LogWarning($"No models found in folder: {folderPath}");
            return;
        }

        int count = 0;
        for (int i = 0; i < modelsToPlace.Count; i++)
        {
            GameObject modelPrefab = modelsToPlace[i];
            Vector3 position = new Vector3(
                (count % modelsPerRow) * xSpacing,
                0f, // Y-coordinate is always 0
                (count / modelsPerRow) * zSpacing
            );

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(modelPrefab);
            instance.transform.position = position;
            instance.name = modelPrefab.name; // Keep original name

            count++;
        }

        Debug.Log($"Placed {modelsToPlace.Count} models from {folderPath}");
    }
}
