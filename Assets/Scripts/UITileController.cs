using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITileController : MonoBehaviour
{
    [Tooltip("The in-world chunk that the tile is bound to")] public Chunk inWorldChunk; //The in-world chunk that the tile is bound to
    //public GameObject UITilePrefab; //The prefab (including the image) for the current used tile
    public Chunk thisChunk; //The chunk script attached to this UI tile
    public List<GameObject> chunksInWorld;
    public GameObject baseTileGroup;

    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        thisChunk = gameObject.GetComponent<Chunk>();
        canvas = GameObject.Find("Canvas");

        if (canvas.activeInHierarchy)
        {
            baseTileGroup = GameObject.Find("BaseGrid");

            foreach (Transform child in baseTileGroup.transform)
            {
                chunksInWorld.Add(child.gameObject);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.activeInHierarchy)
        {
            inWorldChunk = SynchroniseChunk();

            LoadUIImage();
        }
    }

    Chunk SynchroniseChunk()
    {

        foreach(GameObject chunkObj in chunksInWorld) //For every base chunk in the world;
        {
            if (chunkObj.GetComponent<Chunk>().x == thisChunk.x && chunkObj.GetComponent<Chunk>().z == thisChunk.z)
            { //If the chunk coordinates are equal to this UI tile's chunk coordinates;
                return chunkObj.GetComponent<Chunk>();
            }
        }

        return null;
    }

    void LoadUIImage()
    {
        if(inWorldChunk != null && inWorldChunk.gameObject.transform.childCount > 0) //If there is an in world chunk synchronised, and it has a child;
        {
            GameObject terrain = inWorldChunk.gameObject.transform.GetChild(0).gameObject;
            Sprite uiTileImage = terrain.GetComponent<TerrainObject>().uiTileImage;

            gameObject.GetComponent<Image>().sprite = uiTileImage; //Set the UI tile image

        }
        
    }
}
