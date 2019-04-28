using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobContainer : MonoBehaviour
{
    public enum OptimizationType
    {
        Unoptimized,
        RunOnChange
    }

    public bool ShowGrid = false;

    // Grid properties
    private MarchingGrid Grid;
    public Vector3 Size; // Recommended Size:Resolution ratio is 1:0.025
    [Range(1,30)]
    public int Subdivisions;
    public float Threshold;
    public OptimizationType optimization = OptimizationType.RunOnChange;

    // Compute Shader properties
    public ComputeShader ComputeShader;
    public bool CalculateNormals;

    public Blob[] Blobs;

    [HideInInspector]
    public bool NeedsUpdate = true;

    private void Awake()
    {
        Blobs = GetComponentsInChildren<Blob>();
        foreach (Blob blob in Blobs) blob.Container = this;
    }

    private void Start()
    {
        Grid = new MarchingGrid(this, ComputeShader);
    }

    // Update is called once per frame
    private void Update()
    {
        // If there's no change, we don't need to update mesh
        if (optimization == OptimizationType.RunOnChange && !NeedsUpdate) return;

        Grid.EvaluateAll(Blobs);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = Grid.Vertices.ToArray();
        mesh.triangles = GridUtils.GetTriangles(Grid.Vertices.Count, Grid.Triangles);

        if (CalculateNormals)
        {
            mesh.RecalculateNormals();
        }

        NeedsUpdate = false;
    }

    private void OnDestroy()
    {
        Grid.Destroy();
    }

    // ============================================================================================
    // Debug Draw Functions -----------------------------------------------------------------------
    // ============================================================================================

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.99f, 0.4f, 0);
        Gizmos.DrawWireCube(transform.position, Vector3.Scale(Size, transform.localScale));
    }

    public Material GridMaterial
    {
        get
        {
            if (!_gridMaterial)
            {
                _gridMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
                _gridMaterial.color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
                _gridMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return _gridMaterial;
        }
    }
    private Material _gridMaterial;

    public Material VertMaterial
    {
        get
        {
            if (!_vertMaterial)
            {
                _vertMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
                _vertMaterial.color = new Color(1, 0, 0, 0.5f);
                _vertMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return _vertMaterial;
        }
    }
    private Material _vertMaterial;

    private void OnRenderObject()
    {
        if (ShowGrid) drawGrid();
    }

    private void drawGrid()
    {
        if (Grid == null) return;

        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);

        GridMaterial.SetPass(0);
        GridUtils.DrawGridLines(Grid.GridLines);
        GL.PopMatrix();
    }

    
}
