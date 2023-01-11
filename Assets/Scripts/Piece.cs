using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    [Header("Terrain settings")] 
    public GameObject terrainPrefab;

    [Header("Border Settings")] 
    public GameObject selectionBorder;
    
    //Borders
    [Header("Borders")]
    
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;

    public Chunk currentUIChunk;
    public GameObject[] uiTiles;
    private Color visibleSprite = new Color(255, 255, 255, 255);
    private Color transparentSprite = new Color(255, 255, 255, 0);

    void Start()
    {
        selectionBorder = GameObject.Find("SelectionBorder");

    }

    void Update()
    {
        //If the piece is being moved (it is in the same position as the selectionborder) and the current chunk selected is not null
        if (transform.position == selectionBorder.transform.position && selectionBorder.GetComponent<MapPieceMover>().currentChunk != null)
        {
            currentUIChunk = selectionBorder.GetComponent<MapPieceMover>().currentChunk;
        }
        
        uiTiles = GameObject.FindGameObjectsWithTag("UITile");

        RenderBorders();
    }
    
    public void RenderBorders()
    {
        RenderInOut(top, true);
        RenderInOut(bottom, true);
        RenderInOut(left, true);
        RenderInOut(right, true);

        foreach(GameObject tile in uiTiles)
        {

            Chunk c = tile.GetComponent<Chunk>();
            
            //Check for above
            if(c.x == currentUIChunk.x && c.z == currentUIChunk.z + 1)
            {
                if(c.GetComponent<Image>().sprite != tile.GetComponent<UITileController>().defaultSprite)
                {
                    //Rendered tile exists above; don't render top border!
                    RenderInOut(top, false);

                }
            }

            //Check for below
            if (c.x == currentUIChunk.x && c.z == currentUIChunk.z - 1)
            {
                if (c.GetComponent<Image>().sprite != tile.GetComponent<UITileController>().defaultSprite)
                {
                    //Rendered tile exists below; don't render below border!
                    RenderInOut(bottom, false);

                }
            }

            //Check for left
            if (c.x == currentUIChunk.x - 1 && c.z == currentUIChunk.z)
            {
                if (c.GetComponent<Image>().sprite != tile.GetComponent<UITileController>().defaultSprite)
                {
                    //Rendered tile exists left; don't render left border!
                    RenderInOut(left, false);

                }
            }

            //Check for right
            if (c.x == currentUIChunk.x + 1 && c.z == currentUIChunk.z)
            {
                if (c.GetComponent<Image>().sprite != tile.GetComponent<UITileController>().defaultSprite)
                {
                    //Rendered tile exists right; don't render right border!
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
