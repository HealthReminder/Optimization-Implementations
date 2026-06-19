using Colosso.Tools.Patterns;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Helper class to pool and render multiple mesh/material groups using GPU instancing.
/// GPU Instancing class is responsible for drawing the meshes every frame*. 
/// This class is responsible to allow user to configure frame skips on transform updates.
/// Meaning that we can configure this class to update transforms at our will.
/// </summary>
[System.Serializable]
public class GlobalRenderer : Singleton<GlobalRenderer>
{
    /// <summary> Reference to the GPU instancing system </summary>                           
    [SerializeField] private GPUInstancing.GPUInstancing _gpuInstancing;

    /// <summary> List of meshes and lists of transforms for updating. </summary>                           
    private List<MeshGroup> _dynamicObjects;

    /// <summary> Defines how many frames to skip before updating dynamic objects. </summary>                           
    //private const int UPDATE_FRAME_SKIP = 0;

    /// <summary>
    /// Data structure to hold a specific configuration of mesh and material
    /// </summary>
    [System.Serializable]
    private class MeshGroup
    {
        public Mesh Mesh;
        public Material Material;
        public List<Transform> Transforms;
        public HashSet<Transform> CulledTransforms;
    }
    public void Start()
    {
        _dynamicObjects = new List<MeshGroup>();
    }

    /// <summary>
    /// Culls or unculls a specific instance based on its transform
    /// </summary>
    public void CullInstance(Mesh mesh, Transform transf, bool isActive)
    {
        var group = _dynamicObjects.Find(g => g.Mesh == mesh);
        if (group != null)
            if (isActive)
            {
                group.CulledTransforms.Remove(transf);
            }
            else
            {
                group.CulledTransforms.Add(transf);
            }

    }

    /// <summary>
    /// Adds or removes a Transform from the dynamic objects list, based on mesh and material.
    /// </summary>
    /// <param name="mesh">The mesh to render.</param>
    /// <param name="transf">The transform of the object to track.</param>
    /// <param name="mat">The material used for rendering this mesh.</param>
    /// <param name="isRendering">If true, adds the transform to the update list; if false, removes/culls it.</param>
    public void AddRemoveInstance(Transform transf, Mesh mesh, Material mat, bool isRendering = true)
    {
        if (mesh == null || transf == null || mat == null)
            return;

        /// Find an existing group with both matching mesh and material
        var group = _dynamicObjects.Find(g => g.Mesh == mesh && g.Material == mat);

        if (isRendering) /// Uncull
        {
            if (group == null)
            {
                /// Create new group for this mesh-material pair
                _dynamicObjects.Add(new MeshGroup
                {
                    Mesh = mesh,
                    Material = mat,
                    Transforms = new List<Transform> { transf },
                    CulledTransforms = new HashSet<Transform> { }
                });
            }
            else if (!group.Transforms.Contains(transf))
            {
                group.Transforms.Add(transf);
            }
            else
            {
                GlobalLogger.Instance?.Warning("[GPU Pooling] Transform already exists in mesh-material group.");
            }
        }
        else /// Cull
        {
            if (group == null)
                return;

            group.Transforms.Remove(transf);

            // Remove group entirely if empty
            if (group.Transforms.Count == 0)
                _dynamicObjects.Remove(group);
        }
        GlobalLogger.Instance?.Log(LogChannel.LOW, $"[GPU Pooling] {(isRendering ? "Unculled" : "Culled")} transform for mesh '{mesh.name}' with material '{mat.name}'. Total transforms in group: {group?.Transforms.Count ?? 0}");
    }

    /// <summary>
    /// Every X frames update the GPU instancing system with the latest transform matrices
    /// </summary>
    private void Update()
    {
        if (_dynamicObjects == null || _gpuInstancing == null) return;

        //if (Time.frameCount % (UPDATE_FRAME_SKIP + 1) != 0)  return;

        /// Clear all existing instances in the GPU instancing system
        /// Ideally we would clear only specific ones that need updating
        _gpuInstancing.ClearAllInstances();

        foreach (var group in _dynamicObjects)
        {
            if (group.Mesh == null || group.Transforms == null || group.Transforms.Count == 0) continue;
            /// Add updated instances
            foreach (var transf in group.Transforms)
            {
                if (group.CulledTransforms.Contains(transf))
                    continue;
                if (transf != null)
                {
                    Matrix4x4 matrix = Matrix4x4.TRS(transf.position, transf.rotation, transf.lossyScale);
                    _gpuInstancing.AddInstance(group.Mesh, group.Material, matrix);
                }
            }
        }

    }
}

