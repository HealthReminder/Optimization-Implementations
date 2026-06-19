using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Helper class to pool and render multiple mesh/material groups using GPU instancing.
/// GPU Instancing class is responsible for drawing the meshes every frame*. 
/// This class is responsible to allow user to configure frame skips on transform updates.
/// Meaning that we can configure this class to update transforms at our will.
/// </summary>
[System.Serializable]
public class GPUPooling : MonoBehaviour
{
    /// <summary> Reference to the GPU instancing system </summary>                           
    [SerializeField] private GPUInstancing.GPUInstancing _gpuInstancing;

    /// <summary> List of meshes and lists of transforms for updating. </summary>                           
    private List<MeshGroup> _dynamicObjects;

    /// <summary> Defines how many frames to skip before updating dynamic objects. </summary>                           
    private const int UPDATE_FRAME_SKIP = 0;
     
    /// <summary>
    /// Data structure to hold a specific configuration of mesh and material
    /// </summary>
    private class MeshGroup
    {
        public Mesh Mesh;
        public Material Material;
        public List<Transform> Transforms;
    }
    public void Start()
    {
        _dynamicObjects = new List<MeshGroup>();
    }
    /// <summary> Adds a mesh to the dynamic objects list to be updated every X frame 
    /// <param name="mesh">The mesh to render.</param>
    /// <param name="transf">The transform of the object to track.</param>
    /// <paramref name="isAdding"/> If true, adds the transform to the update list; if false, removes/culls it.
    /// </summary>
    public void AddDynamicMesh(Mesh mesh, Transform transf, bool isAdding = true)
    {
        var group = _dynamicObjects.Find(g => g.Mesh == mesh);

        if (isAdding) /// Uncull
        {
            if (group == null)
            {
                _dynamicObjects.Add(new MeshGroup
                {
                    Mesh = mesh,
                    Transforms = new List<Transform> { transf }
                });
            }
            else if (!group.Transforms.Contains(transf))
            {
                group.Transforms.Add(transf);
            }
            else
            {
                GlobalLogger.Instance?.Warning("[GPU Pooling] Transform already exists in mesh group.");
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
    }
    /// <summary>
    /// Every X frames update the GPU instancing system with the latest transform matrices
    /// </summary>
    private void Update()
    {
        if (_dynamicObjects == null || _gpuInstancing == null) return;

        if (Time.frameCount % (UPDATE_FRAME_SKIP + 1) != 0)  return;

        /// Clear all existing instances in the GPU instancing system
        /// Ideally we would clear only specific ones that need updating
        _gpuInstancing.ClearAllInstances();

        foreach (var group in _dynamicObjects)
        {
            if (group.Mesh == null || group.Transforms == null || group.Transforms.Count == 0) continue;
            // Add updated instances
            foreach (var transf in group.Transforms)
            {
                if (transf != null)
                {
                    Matrix4x4 matrix = Matrix4x4.TRS(transf.position, transf.rotation, transf.localScale);
                    _gpuInstancing.AddInstance(group.Mesh, group.Material, matrix);
                }
            }
        }

    }
}

