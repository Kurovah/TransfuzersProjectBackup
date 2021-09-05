using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{

    //[SerializeField]
    //private Transform testTree, rock, grass;

    //[SerializeField]
    //private int treeNum = 500;

    //[SerializeField]
    //private float level = 20f;


    [SerializeField]
    private Foliage[] foliage;


    [SerializeField]
    private bool genFoliage;

    public MeshFilter mesh;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;

    private TextureGen texGen;
    private MeshGen meshGen;
    private Noise noise;

    public Material terrainMat;

    public enum DrawMode
    {
        noise,
        color,
        draw
    };

    public DrawMode drawMode;

    public int mapWidth, mapHeight;

    public bool autoUpdate;



    public MeshRenderer meshRend;

    private HashSet<Vector3> trees;
    private void Awake()
    {

        //GenMap();
        
    }

    [System.Serializable]
    public struct Foliage
    {
        public string type;
        public Transform prefab;
        public int amount;
        public float level;

    }

    private void FoliageGen(string name, Transform prefab, int num, float level)
    {
        
        Vector3 pos = new Vector3();
        if (transform.Find(name))
        {
            Destroy(transform.Find(name).gameObject);
            trees.Clear();
        }
        Transform newObject = new GameObject(name).transform;
       

        for (int i = 0; i < num; i++)
        {
            while (true)
            {
                float randomX = Random.Range(meshRend.bounds.min.x, meshRend.bounds.max.x);
                float randomZ = Random.Range(meshRend.bounds.min.z, meshRend.bounds.max.z);

                RaycastHit hit;
                if (Physics.Raycast(new Vector3(randomX, level, randomZ), -Vector3.up, out hit))
                {
                    if (!trees.Contains(hit.point))
                    {
                        pos = hit.point;


                        break;
                    }
                    ///sfa

                }
                trees.Add(pos);
                Transform clone = Instantiate(prefab, pos, Quaternion.identity);

                clone.parent = newObject;
            }



        }
        Debug.Log("Trees " + trees.Count);
    }
    void OnValuesUpdated() 
    {
        if (!Application.isPlaying) 
        {
            GenMap();
        }
    }

    void OnTextureUpdared() 
    {
        textureData.ApplyMat(terrainMat);
    }

    
    public void GenMap() 
    {
        noise = new Noise();
        texGen = new TextureGen();
        meshGen = new MeshGen();

        float[,] noiseMap = noise.GenNoiseMap(mapWidth, mapHeight, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.seed, noiseData.offset);
       
        textureData.UpdateMeshHeight(terrainMat, terrainData.minHeight, terrainData.maxHeight);
        MapDisplay mapDis = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.noise) 
        {
            mapDis.DrawTex(texGen.TexFromHeightMap(noiseMap));
        }
       
        else if (drawMode == DrawMode.draw)
        {


            mapDis.DrawMesh(meshGen.GenTerrainMesh(noiseMap, terrainData.meshHMult, terrainData.meshHCurve));
            
        }
        //mesh.gameObject.AddComponent<MeshCollider>();
        mesh.GetComponent<MeshFilter>().sharedMesh = mesh.sharedMesh;
        mesh.GetComponent<MeshCollider>().sharedMesh = mesh.sharedMesh;
        trees = new HashSet<Vector3>();
        if (genFoliage) 
        {
            for (int i = 0; i < foliage.Length; i++)
            {
                FoliageGen(foliage[i].type, foliage[i].prefab, foliage[i].amount, foliage[i].level);
            }
           
           
        }
       
    }

    private void OnValidate()
    {
        if (terrainData != null) 
        {
            terrainData.OnvaluesUpdate -= OnValuesUpdated;
            terrainData.OnvaluesUpdate += OnValuesUpdated;
        }
        if (noiseData != null)
        {
            noiseData.OnvaluesUpdate -= OnValuesUpdated;
            noiseData.OnvaluesUpdate += OnValuesUpdated;
        }

        if (textureData != null)
        {
            textureData.OnvaluesUpdate -= OnTextureUpdared;
            textureData.OnvaluesUpdate += OnTextureUpdared;
        }

        if (mapWidth < 1) 
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        

    }



    

}
