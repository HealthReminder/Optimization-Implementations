namespace GPUInstancing
{
    using UnityEngine;

    /// <summary>
    /// Spawns GameObjects using MeshRenderer and MeshFilter for benchmarking against GPU instancing.
    /// Supports multiple mesh/material groups.
    /// </summary>
    public class NonGPUInstancing : MonoBehaviour
    {
        [SerializeField] private Vector3 _spawnRange;
        [SerializeField] private MeshGroup[] _meshGroups;

        [System.Serializable]
        private class MeshGroup
        {
            public Mesh Mesh;
            public Material Material;
            public int objCount = 100;
        }

        private void Start()
        {
            foreach (var group in _meshGroups)
            {
                if (group.Mesh == null || group.Material == null || group.objCount <= 0)
                    continue;

                for (int i = 0; i < group.objCount; i++)
                {
                    Vector3 pos = new Vector3(
                        Random.Range(-_spawnRange.x, _spawnRange.x),
                        Random.Range(-_spawnRange.y, _spawnRange.y),
                        Random.Range(-_spawnRange.z, _spawnRange.z)
                    );

                    // Create GameObject
                    GameObject obj = new GameObject($"Instance_{group.Mesh.name}_{i}");
                    obj.transform.SetParent(transform);

                    // Set transform
                    obj.transform.position = pos;
                    obj.transform.rotation = Quaternion.identity;
                    obj.transform.localScale = Vector3.one;

                    // Attach components
                    var filter = obj.AddComponent<MeshFilter>();
                    filter.sharedMesh = group.Mesh;

                    var renderer = obj.AddComponent<MeshRenderer>();
                    renderer.sharedMaterial = group.Material;
                }
            }
        }
    }
}
