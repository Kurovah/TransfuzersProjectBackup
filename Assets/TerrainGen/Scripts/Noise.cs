using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise 
{
    public float[,] GenNoiseMap(int width, int height, float scale, int octaves, float persistance, float lacunarity, int seed, Vector2 offset) //scale - scale of the noise
    {
        float[,] noiseMap = new float[width, height];

        System.Random rnd = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = rnd.Next(-100000, 100000) + offset.x;
            float offsetY = rnd.Next(-100000, 100000) + offset.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0) 
        {
            scale = 0.001f;
        }
        float maxNoiseH = float.MinValue;
        float minNoiseH = float.MaxValue;

        float halfW = width / 2f;
        float halfH = height / 2f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amp = 1;
                float freq = 1;
                float noiseHeight = 0;
                //loop through octaves
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfW) / scale * freq + octaveOffsets[i].x; //higher freq - 
                    float sampleY = (y - halfH)/ scale * freq + octaveOffsets[i].y;

                    float perlinVal = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    //noiseMap[x, y] = perlinVal; // apply to noise map

                    noiseHeight += perlinVal * amp;
                    amp *= persistance; // decreases with each octave
                    freq *= lacunarity; // increases with each octave

                }
                if (noiseHeight > maxNoiseH) 
                {
                    maxNoiseH = noiseHeight;
                }
                else if (noiseHeight < minNoiseH)
                {
                    minNoiseH = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseH, maxNoiseH, noiseMap[x, y]); // returns value between 0 and 1 - normalising the noise map
            }

        }   
        return noiseMap;
    }
}
