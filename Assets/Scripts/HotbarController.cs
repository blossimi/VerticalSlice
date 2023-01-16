using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarController : MonoBehaviour
{
    [Header("Information")]
    
    public List<GameObject> piecesInBar;
    public GameObject firstPiece;
    
    //Private variables
    public GameObject grid;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set-up variables
        grid = transform.GetChild(0).gameObject;
        
        //Get variables
        piecesInBar = GetPieces();
        firstPiece = piecesInBar[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Get variables
        piecesInBar = GetPieces();
        firstPiece = GetFirstPiece();
    }

    List<GameObject> GetPieces()
    {
        List<GameObject> list = new List<GameObject>();
        
        //For each child (piece) in the grid
        foreach (Transform piece in grid.transform)
        {
            //If the list does not (yet) contain the piece in question:
            if (!list.Contains(piece.gameObject))
            {
                list.Add(piece.gameObject);
            }
        }

        return list;
    }

    GameObject GetFirstPiece()
    {
        
        foreach (GameObject piece in piecesInBar)
        {
            //If the current piece in question has a lower x value than the current firstPiece:
            if (piece.GetComponent<RectTransform>().position.x < firstPiece.GetComponent<RectTransform>().position.x)
            {
                //Set the current piece in question to be the firstPiece
                return piece;
            }
        }

        return firstPiece;

    }
}
