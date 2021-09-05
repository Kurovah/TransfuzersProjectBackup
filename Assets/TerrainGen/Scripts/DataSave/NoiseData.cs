using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class NoiseData : UpdateData
{
    public float noiseScale;
    public int octaves, seed;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public Vector2 offset;

    protected override void OnValidate()
    {
       
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        base.OnValidate();
    }
}
