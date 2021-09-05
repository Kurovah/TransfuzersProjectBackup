using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdateData
{
    public float scale = 2.5f;
    public float meshHMult;

    public AnimationCurve meshHCurve;

    public float minHeight
    {
        get { return scale *meshHMult * meshHCurve.Evaluate(0); }
    }
    public float maxHeight
    {
        get { return scale * meshHMult * meshHCurve.Evaluate(1); }
    }


}
