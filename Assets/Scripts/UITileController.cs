using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITileController : MonoBehaviour
{
    [Tooltip("The in-world chunk that the tile is bound to")] public Chunk inWorldChunk; //The in-world chunk that the tile is bound to
    //public GameObject UITilePrefab; //The prefab (including the image) for the current used tile
    public Chunk thisChunk; //The chunk script attached to this UI tile
    public GameObject[] chunksInWorld;

    //Borders
    [Header("Borders")]
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    [Space]
    public bool existAbove = false;
    [Space]
    public Sprite defaultSprite;
    public GameObject[] uiTiles;

    private GameObject canvas;
    private Color visibleSprite = new Color(255, 255, 255, 255);
    private Color transparentSprite = new Color(255, 255, 255, 0);

    // Start is called before the first frame update
    void Start()
    {
        thisChunk = gameObject.GetComponent<Chunk>();
        canvas = GameObject.Find("Canvas");
        uiTiles = GameObject.FindGameObjectsWithTag("UITile");
        //defaultColor = gameObject.GetComponent<Image>().color;

        top = transform.Find("border_top").gameObject;
        bottom = transform.Find("border_bottom").gameObject;
        left = transform.Find("border_left").gameObject;
        right = transform.Find("border_right").gameObject;

        chunksInWorld = GameObject.FindGameObjectsWithTag("BaseTile");
    }

    // Update is called once per frame
    void Update()
    {
        //chunksInWorld.AddRange(GameObject.FindGameObjectsWithTag("BaseTile"));
        
        inWorldChunk = SynchroniseChunk();

        LoadUIImage();

        RenderBorders();
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
        

        //If there is a in-world chunk with the same chunk coords:
        if(inWorldChunk.transform.childCount > 0)
        {
            Sprite tileImage = inWorldChunk.gameObject.transform.GetChild(0).GetComponent<TerrainObject>().uiTileImage;

            gameObject.GetComponent<Image>().sprite = tileImage; //Set the correct uitile image
            gameObject.GetComponent<Image>().color = visibleSprite; //Make the tile image no longer transparent
        }
        //If there is NOT an in-world chunk with the same chunk coords:
        else
        {
            gameObject.GetComponent<Image>().sprite = null; //Set the correct empty image
            gameObject.GetComponent<Image>().color = transparentSprite; //Make the tile image transparent
        }
        
    }
    public void RenderBorders()
    {

        if (gameObject.GetComponent<Image>().sprite == defaultSprite)
        {
            RenderInOut(top, false);
            RenderInOut(bottom, false);
            RenderInOut(left, false);
            RenderInOut(right, false);
        }
        else
        {
            RenderInOut(top, true);
            RenderInOut(bottom, true);
            RenderInOut(left, true);
            RenderInOut(right, true);

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
    }

    void RenderInOut(GameObject border, bool render)
    {
        if (render == true)
        {
            border.gameObject.GetComponent<Image>().color = visibleSprite;
        }
        if (render == false)
        {
            border.gameObject.GetComponent<Image>().color = transparentSprite;
        }
    }

}
