using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapPieceMover : MonoBehaviour
{
    public GameObject selectedPiece;
    public GameObject selectedTile;
    public GameObject[] uiTiles;

    [Header("Settings")] public GameObject tileSelectionBorder;
    [Range(0.0f, 1.0f)] public float animationSpeed = 0.1f;

    public Chunk currentChunk;
    
    // Start is called before the first frame update
    void Start()
    {
        uiTiles = GameObject.FindGameObjectsWithTag("UITile");
    }

    // Update is called once per frame
    void Update()
    {
        currentChunk = GetCurrentChunk();
        
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveBorder(selectedTile);
        }*/

        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Up)))
        {
            MoveBorder(0, 1);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Down)))
        {
            MoveBorder(0, -1);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Left)))
        {
            MoveBorder(-1, 0);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Right)))
        {
            MoveBorder(1, 0);
        }
    }

    private Chunk GetCurrentChunk()
    {
        foreach (GameObject tile in uiTiles)
        {
            if (tile.transform.position == gameObject.transform.position)
            {
                return tile.GetComponent<Chunk>();
            }
        }

        return null;
    }
    
    void MoveBorder(int xDir, int zDir)
    {

        foreach (GameObject tile in uiTiles)
        {
            Chunk selChunk = tile.GetComponent<Chunk>();
            Chunk thisChunk = currentChunk;

            if (selChunk.x == thisChunk.x + xDir && selChunk.z == thisChunk.z + zDir)
            {
                float tileHeight = 100;

                //The tile exists! The border can now move towards it
                Vector3 targetLoc = new Vector3(transform.position.x + (tileHeight * xDir),
                    transform.position.y + (tileHeight * zDir), transform.position.z);
                //iTween.MoveTo(tileSelectionBorder, targetLoc, animationSpeed);

                
                //Now the map canvas needs to be moved in the opposite direction by the amount of pixels that a tile is high/wide

                GameObject grid = transform.parent.Find("Grid").gameObject;
                Vector3 gPos = grid.transform.position;
                targetLoc = new Vector3(gPos.x + (-xDir * tileHeight), gPos.y + (-zDir * tileHeight), gPos.z);
                
                iTween.MoveTo(grid, targetLoc, animationSpeed);
            }
        }

        
        
    }
}
