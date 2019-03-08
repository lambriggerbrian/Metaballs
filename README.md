# Metaballs
Marching Cubes implementation of Metaballs in Unity 2018.3.0f2 based off of the work of Sehyun Av Kim and Inigo Quilez.

Calculations are deferred to GPU with compute shader to improve runtime speeds.

To Use:
1) Create GameObject with BlobContainer, Mesh Renderer, and Mesh Filter components
2) Choose the Marching Cubes compute shader for the Compute Shader field in the BlobContainer component
3) Add Material to Mesh Renderer component, which determines how blobs will render
4) Add child GameObject with Blob component to GameObject with BlobContainer component


References:
1) Ray Marching Metaball in Unity 3D: https://medium.com/@avseoul/ray-marching-metaball-in-unity3d-fc6f83766c5d
