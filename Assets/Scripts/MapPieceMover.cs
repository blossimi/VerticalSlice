using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MapPieceMover : MonoBehaviour
{
    [Header("Information")]
    
    public GameObject selectedPiece;
    public GameObject selectedTile;
    public Chunk currentChunk;
    public Chunk currentRealWorldChunk;
    public GameObject[] uiTiles;

    [Header("Settings")]
    
    public GameObject tileSelectionBorder;
    [Range(0.0f, 1.0f)] public float borderAnimationSpeed = 0.1f;
    [Range(0.0f, 2.0f)] public float pieceAnimationSpeed = 0.5f;
    
    //Private variables
    private InputManager im;
    private HotbarController hc;
    private ChunkGenerator cg;

    private Vector3 oldFirstPiecePos;
    
    // Start is called before the first frame update
    void Start()
    {

        im = GameObject.Find("GameManager").GetComponent<InputManager>();
        hc = GameObject.Find("Bar").GetComponent<HotbarController>();
        
        uiTiles = GameObject.FindGameObjectsWithTag("UITile");
        cg = GameObject.Find("GameManager").GetComponent<ChunkGenerator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        currentChunk = GetCurrentChunk();
        if (currentChunk != null)
        {
            currentRealWorldChunk = currentChunk.gameObject.GetComponent<UITileController>().inWorldChunk;
        }
        
        //
        //Map movement inputs
        //
        
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Up)) && currentChunk != null)
        { //Up
            MoveBorder(0, 1);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Down)) && currentChunk != null)
        { //Down
            MoveBorder(0, -1);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Left)) && currentChunk != null)
        { //Left
            MoveBorder(-1, 0);
        }
        if (Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Right)) && currentChunk != null)
        { //Right
            MoveBorder(1, 0);
        }

        //
        //Map control inputs
        //
        
        //If the current input state is MovingPiece and NewPiece input is pressed
        
        if (im.currentState == InputManager.States.MovingPiece && Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Cancel)))
        { //CANCEL NewPiece
            
            //Move piece in hotbar to OLD position
            Vector3 target = oldFirstPiecePos;
            iTween.MoveTo(selectedPiece, target, pieceAnimationSpeed);

            im.SetState(InputManager.States.InInventory);
            
            //Debug.Log("Esc pressed, state is now " + im.currentState.ToString());
            
        }
        
        //If the current input state is InInventory and NewPiece input is pressed
        if (im.currentState == InputManager.States.InInventory && Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.NewPiece)))
        { //New Piece
            
            
            //Select first piece in hotbar
            selectedPiece = hc.firstPiece;

            oldFirstPiecePos = selectedPiece.transform.position;
            
            //Set input state to MovingPiece
            im.SetState(InputManager.States.MovingPiece);
            
            //Debug.Log("W pressed, state is now " + im.currentState.ToString());

            //Move piece in hotbar to THIS position
            Vector3 target = transform.position;
            iTween.MoveTo(selectedPiece, target, pieceAnimationSpeed);

        }
        
        //If the current input is MovingPiece and Conform input is pressed
        if (im.currentState == InputManager.States.MovingPiece && Input.GetKeyDown(InputManager.UIInputs.GetValue(InputManager.InputTypes.Confirm)))
        {
            Debug.Log("Space pressed");
            
            //Generate the terrain
            GameObject terrainPrefab = selectedPiece.GetComponent<Piece>().terrainPrefab;
            
            
            cg.LoadTerrain(currentRealWorldChunk, terrainPrefab);

            //Destroy the piece
            Destroy(selectedPiece);
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

                iTween.MoveTo(grid, gTarget, borderAnimationSpeed);
                iTween.MoveTo(bg, bTarget, borderAnimationSpeed);
            }
        }

        
        
    }
}
