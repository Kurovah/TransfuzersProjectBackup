using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextureGen 
{
    public Texture2D TexFromColMap(Color[] colorMap, int width, int height ) 
    {
        Texture2D tex = new Texture2D(width, height);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.SetPixels(colorMap);
        
        tex.Apply();

        //SaveTexture(tex);
        return tex;
    }

    public  Texture2D TexFromHeightMap(float[,] heightMap) 
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        //set the color of pixels
        Color[] colMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                
            }
        }
        

        return TexFromColMap(colMap, width, height);


    }

//    public static void SaveTexture(Texture2D texture)
//    {
//        byte[] bytes = texture.EncodeToPNG();
//        var dirPath = Application.dataPath + "/RenderOutput";
//        if (!System.IO.Directory.Exists(dirPath))
//        {
//            System.IO.Directory.CreateDirectory(dirPath);
//        }
//        System.IO.File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".png", bytes);
//        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
//#if UNITY_EDITOR
//        UnityEditor.AssetDatabase.Refresh();
//#endif

//    }
}
