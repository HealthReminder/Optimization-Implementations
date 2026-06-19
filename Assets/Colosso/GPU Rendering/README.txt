# GPU Instancing Package for Unity

Thank you for purchasing the GPU Instancing Package.  
Written by Eduardo Martinelli, 2025.

## Overview

This package provides an efficient way to render many instances of the same mesh using GPU instancing in Unity. It’s meant to help you improve performance in scenes with large numbers of repeated objects.

## Contents

- `GPUInstancing.cs`: Renders meshes directly in the CPU.
- `GPUInstancingSampleUsage.cs`: Uses the GPUInstancing class to render objects within a confined space.. Replace this with your implementation.
- Example prefabs and demo scene.

## Requirements

- URP made, but may work with other pipelines with minimal effort.

## How to Use

1. Import the package into your Unity project.
2. Attach `GPUInstancing.cs` to a GameObject.
3. Assign meshes, materials and the number of instances.
4. Attach `GPUInstancingSampleUsage.cs` to a GameObject. Or make calls to GPUInstancing on a custom script.
5. Press Play to see instancing in action.
6. Use `NonGPUInstancing.cs` for performance comparison.

## Notes

- Make sure the material has "Enable GPU Instancing" checked.
- Unity batches up to 1023 instances per draw call.

## Contact

For questions or support: edu.f.martinelli@gmail.com	
