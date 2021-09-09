using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu()]
public class TextureData : UpdateData
{
    

    public TerrainTypes[] regions;

    float savedMinH;
    float savedMaxH;
    public void ApplyMat(Material mat) 
    {
        mat.SetInt("colCount", regions.Length);
        mat.SetColorArray("colors", regions.Select(x => x.cols).ToArray());
        mat.SetFloatArray("baseHeights", regions.Select(x => x.baseHeights).ToArray());
        mat.SetFloatArray("blends", regions.Select(x => x.blends).ToArray());

        UpdateMeshHeight(mat, savedMinH, savedMaxH);
    }

    public void UpdateMeshHeight(Material mat, float minH, float maxH) 
    {
        savedMaxH = maxH;
        savedMinH = minH;
        mat.SetFloat("minHeight", minH);
        mat.SetFloat("maxHeight", maxH);
    }
    [System.Serializable]
    public class TerrainTypes 
    {
        public string type;
        public float baseHeights;
        public Color cols;
        public float blends;
    }
}
