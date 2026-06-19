namespace GPUInstancing
{
    using UnityEngine;

    /// <summary>
    /// Test class to initialize a GPU instancing system by generating a number of
    /// instance transforms for multiple mesh/material groups.
    /// </summary>
    public class GPUInstancingSampleUsage : MonoBehaviour
    {
        [SerializeField] private Vector3 _spawnRange;           /// Defines the space confining rendered meshes
        [SerializeField] private MeshGroup[] _meshGroups;       /// Configurable mesh/material groups
        [SerializeField] private GPUInstancing _gpuInstancing;  /// Reference to the instancing system

        [System.Serializable]
        private class MeshGroup
        {
            public Mesh Mesh;
            public Material Material;
            public int objCount = 10;
        }

        /// <summary>
        /// Generates instance transforms for each mesh group and registers them with GPUInstancing.
        /// </summary>
        public void Start()
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

                    Matrix4x4 matrix = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
                    _gpuInstancing.AddInstance(group.Mesh, group.Material, matrix);
                }
            }
        }
    }
}
