using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    public Chunk chunk;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTerrain()
    {
        Debug.Log("clicked");

        ChunkGenerator cg = GameObject.Find("GameManager").GetComponent<ChunkGenerator>();
        cg.LoadTerrain(chunk, prefab);
    }

    public void RenderUITileBorders()
    {
        foreach(GameObject UITile in GameObject.FindGameObjectsWithTag("UITile"))
        {
            UITile.GetComponent<UITileController>().RenderBorders();
        }
    }
}
