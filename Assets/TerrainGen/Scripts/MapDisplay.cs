using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    //Turns the noise map into a texture and applies it to a plane
    
    public Renderer texRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawTex(Texture2D tex) 
    {
        texRenderer.sharedMaterial.mainTexture = tex;
        texRenderer.transform.localScale = new Vector3(tex.width, 1, tex.height);
        meshRenderer.sharedMaterial.mainTexture = tex;
    }

    public void DrawMesh(MeshData meshData)//, Texture2D tex) 
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        //meshRenderer.sharedMaterial.mainTexture = tex;
    }
    
}
