using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Settings")]
    public GameObject prefab;
    [Range(0.0f, 10.0f)] public float heightOffset = 1f;

    [Header("Chunk to generate in")]
    public Chunk chunk;
    public bool generate = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (generate)
        {
            LoadTerrain(chunk, prefab);
        }
    }

    public void LoadTerrain(Chunk chunk, GameObject prefab)
    {
        GameObject newTerrain = prefab;
        Vector3 newTerrainPos = new Vector3
            (chunk.gameObject.transform.position.x, chunk.gameObject.transform.position.y + heightOffset, 
            chunk.gameObject.transform.position.z);

        GameObject terrain = Instantiate(newTerrain, newTerrainPos, 
            new Quaternion(0, 0, 0, 0), chunk.gameObject.transform); 
        //Create the new terrain, at the newTerrainPos position, with TerrainParent as its parent for organisation
        terrain.name = "(" + chunk.x.ToString()
            + ", " + chunk.z.ToString() + ") " + terrain.name; //Set the name of the new terrain
    }
}
