using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
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

        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Up)) && currentChunk != null)
        {
            MoveBorder(0, 1);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Down)) && currentChunk != null)
        {
            MoveBorder(0, -1);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Left)) && currentChunk != null)
        {
            MoveBorder(-1, 0);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Right)) && currentChunk != null)
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
                float tileHeight = tile.GetComponent<RectTransform>().rect.height; //Assumes that width = height

                //Now the map canvas needs to be moved in the opposite direction by the amount of pixels that a tile is high/wide,
                //and the same goes for the background

                GameObject grid = transform.parent.Find("Grid").gameObject;
                Vector3 gPos = grid.transform.position;

                GameObject bg = GameObject.Find("Background");
                Vector3 bPos = grid.transform.position;

                Vector3 gTarget = new Vector3(gPos.x + (-xDir * tileHeight), gPos.y + (-zDir * tileHeight), gPos.z);
                Vector3 bTarget = new Vector3(bPos.x + (-xDir * tileHeight), bPos.y + (-zDir * tileHeight), bPos.z);

                iTween.MoveTo(grid, gTarget, animationSpeed);
                iTween.MoveTo(bg, bTarget, animationSpeed);
            }
        }

        
        
    }
}
