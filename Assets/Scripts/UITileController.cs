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

    //Borders
    [Header("Borders")]
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    [Space]
    public Sprite defaultSprite;
    public GameObject[] uiTiles;

    private GameObject canvas;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        thisChunk = gameObject.GetComponent<Chunk>();
        canvas = GameObject.Find("Canvas");
        uiTiles = GameObject.FindGameObjectsWithTag("UITile");
        defaultColor = gameObject.GetComponent<Image>().color;

        if (canvas.activeInHierarchy)
        {
            baseTileGroup = GameObject.Find("BaseGrid");

            foreach (Transform child in baseTileGroup.transform)
            {
                chunksInWorld.Add(child.gameObject);
            }
        }

        top = transform.Find("border_top").gameObject;
        bottom = transform.Find("border_bottom").gameObject;
        left = transform.Find("border_left").gameObject;
        right = transform.Find("border_right").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.activeInHierarchy)
        {
            inWorldChunk = SynchroniseChunk();

            LoadUIImage();
        }

        RenderBorders();

        if(gameObject.GetComponent<Image>().sprite == defaultSprite)
        {
            RenderInOut(top, false);
            RenderInOut(bottom, false);
            RenderInOut(left, false);
            RenderInOut(right, false);

            gameObject.GetComponent<Image>().color = new Color(0, 0, 100, 0);
        }
        else
        {
            gameObject.GetComponent<Image>().color = defaultColor;
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
    void RenderBorders()
    {
        /*//Chunk above
        Chunk chunkAbove = new Chunk(thisChunk.x, thisChunk.z + 1);
        //Chunk below
        Chunk chunkBelow = new Chunk(thisChunk.x, thisChunk.z - 1);
        //Chunk left
        Chunk chunkLeft = new Chunk(thisChunk.x - 1, thisChunk.z);
        //Chunk left
        Chunk chunkRight = new Chunk(thisChunk.x + 1, thisChunk.z);*/

        foreach (GameObject tile in uiTiles)
        {
            Chunk selectedChunk = tile.GetComponent<Chunk>();
            if (selectedChunk.x == thisChunk.x && selectedChunk.z == thisChunk.z + 1 && tile.GetComponent<Image>().sprite != defaultSprite)
            {
                //There is a laoded chunk above this chunk, so dont render the top border
                RenderInOut(top, false);
            }
            if (selectedChunk.x == thisChunk.x && selectedChunk.z == thisChunk.z - 1 && tile.GetComponent<Image>().sprite != defaultSprite)
            {
                //There is a laoded chunk below this chunk, so dont render the bottom border
                RenderInOut(bottom, false);
            }
            if (selectedChunk.x == thisChunk.x - 1 && selectedChunk.z == thisChunk.z && tile.GetComponent<Image>().sprite != defaultSprite)
            {
                //There is a laoded chunk left of this chunk, so dont render the left border
                RenderInOut(left, false);
            }
            if (selectedChunk.x == thisChunk.x + 1 && selectedChunk.z == thisChunk.z && tile.GetComponent<Image>().sprite != defaultSprite)
            {
                //There is a laoded chunk right of this chunk, so dont render the right border
                RenderInOut(right, false);
            }
        }

    }
    void RenderInOut(GameObject border, bool render)
    {
        if (render == true)
        {
            border.gameObject.SetActive(true);
        }
        if (render == false)
        {
            border.gameObject.SetActive(false);
        }
    }

}
