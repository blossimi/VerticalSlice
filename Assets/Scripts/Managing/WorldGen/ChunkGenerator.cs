using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Settings")]
    public bool autoGenerateRandom = false;
    public bool autoGenerateAdvanced = true;
    public GameObject[] chunkPrefabs;
    [Range(0.0f, 10.0f)] public float heightOffset = 5f;
    public GameObject terrainParent;

    [Header("Chunk to generate in")]
    public Chunk chunk;
    public bool generate = false;

    //private variables
    private GridController gc;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GridController").GetComponent<GridController>();
        terrainParent = GameObject.Find("Terrains");

        if (autoGenerateRandom)
        {
            foreach (GameObject chunk in GameObject.FindGameObjectsWithTag("BaseTile"))
            {
                LoadTerrain(chunk.GetComponent<Chunk>());
            }
        }

        if (autoGenerateAdvanced)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (generate && !autoGenerateRandom)
        {
            generate = false;
            LoadTerrain(chunk);
        }
    }

    void LoadTerrain(Chunk chunk)
    {
        GameObject newTerrain = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        Vector3 newTerrainPos = new Vector3(chunk.gameObject.transform.position.x, chunk.gameObject.transform.position.y + heightOffset, chunk.gameObject.transform.position.z);

        GameObject terrain = Instantiate(newTerrain, newTerrainPos, new Quaternion(0, 0, 0, 0), terrainParent.transform); //Create the new terrain, at the newTerrainPos position, with TerrainParent as its parent for organisation
        terrain.name = "(" + chunk.x.ToString() + ", " + chunk.z.ToString() + ") " + terrain.name; //Set the name of the new terrain
    }
}
