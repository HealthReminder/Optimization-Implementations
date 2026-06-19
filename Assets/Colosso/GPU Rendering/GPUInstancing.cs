namespace GPUInstancing
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Handles GPU instanced rendering for multiple mesh/material groups
    /// Allows adding instances one at a time and automatically groups them
    /// </summary>
    public class GPUInstancing : MonoBehaviour
    {
        /// <summary>
        /// Holds all rendering groups
        /// </summary>
        private readonly List<MeshInfo> _meshGroups = new();

        private const int batchSize = 1023;

        /// <summary> Defines how many frames to skip rendering objects. Numbers different than 0 may cause flickering. </summary>
        //private const int RENDER_FRAME_SKIP = 0;

        /// <summary>
        /// Represents a mesh/material instancing group
        /// One mesh, one material for many transforms
        /// </summary>
        private class MeshInfo
        {
            public Mesh Mesh;
            public Material Material;
            public List<Matrix4x4> Matrices;

            public MeshInfo(Mesh mesh, Material material)
            {
                Mesh = mesh;
                Material = material;
                Matrices = new List<Matrix4x4>();
            }

            /// <summary>
            /// Checks if this group matches given mesh and material
            /// </summary>
            public bool Matches(Mesh mesh, Material material)
            {
                return Mesh == mesh && Material == material;
            }
        }
        public void ClearAllInstances()
        {
            _meshGroups.Clear();     
        }

        /// <summary>
        /// Adds a single instance to the appropriate group, or creates one if none exists.    /// </summary>
        /// <param name="mesh">The mesh to render./param>
        /// <param name="material">The material to apply./param>
        /// <param name="matrix">The transform matrix of the instance</param>
        public void AddInstance(Mesh mesh, Material material, Matrix4x4 matrix)
        {
            foreach (var group in _meshGroups)
            {
                if (group.Matches(mesh, material))
                {
                    group.Matrices.Add(matrix);
                    return;
                }
            }

            // No existing group found — create a new one
            var newGroup = new MeshInfo(mesh, material);
            newGroup.Matrices.Add(matrix);
            _meshGroups.Add(newGroup);
        }

        /// <summary>
        /// Renders all registered instance groups using GPU instancing
        /// </summary>
        private void Update()
        {
            //if (Time.frameCount % (RENDER_FRAME_SKIP + 1) != 0) return;

            foreach (var instanceGroup in _meshGroups)
            {
                int total = instanceGroup.Matrices.Count;
                if (instanceGroup.Mesh == null || instanceGroup.Material == null || total == 0)
                    continue;

                for (int i = 0; i < total; i += batchSize)
                {
                    int count = Mathf.Min(batchSize, total - i);

                    // Copy batch slice to temp array
                    Matrix4x4[] batch = new Matrix4x4[count];
                    instanceGroup.Matrices.CopyTo(i, batch, 0, count);

                    Graphics.DrawMeshInstanced(
                        instanceGroup.Mesh,
                        0,
                        instanceGroup.Material,
                        batch,
                        count,
                        null,
                        UnityEngine.Rendering.ShadowCastingMode.On,
                        true,
                        0,
                        null
                    );
                }
            }
        }
    }
}
