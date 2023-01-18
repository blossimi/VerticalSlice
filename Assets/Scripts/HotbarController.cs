using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour
{
    [Header("Settings")] 
    
    public GameObject emptyBar;

    private float fadeTimePerUnit;

    [FormerlySerializedAs("fadeTime")] public float totalFadeTime;
    
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
        
        //Settings
        fadeTimePerUnit = totalFadeTime / 255;
        
        SetSprite();
    }

    void SetSprite()
    {
        //If there are no pieces in the bar
        if (firstPiece == null)
        {
            //Fade in the emptyBar
            
            //StartCoroutine(FadeBar(false, emptyBar));
            
            emptyBar.SetActive(true);
        }
        else //If there are pieces in the bar
        {
            
            /*if (emptyBar.GetComponent<Image>().color != new Color(255, 255, 255, 0))
            {
                //Make it transparent
                StartCoroutine(FadeBar(true, emptyBar));
            }*/
            
            emptyBar.SetActive(false);
        }
    }

    /*IEnumerator FadeBar(bool makeTrans, GameObject bar)
    {
        

        if (makeTrans) //Make transparent
        {
            
            //While the bar is not yet transparent
            while (emptyBar.GetComponent<Image>().color != new Color(255, 255, 255, 0))
            {
                Color c = emptyBar.GetComponent<Image>().color;
            
                emptyBar.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a - 1); //Set color
            
                yield return new WaitForSeconds(fadeTimePerUnit); //Wait
            }
        }
        else if (!makeTrans) //Make visible
        {
            
            //While the bar is not yet transparent
            while (emptyBar.GetComponent<Image>().color != new Color(255, 255, 255, 255))
            {
                Color c = emptyBar.GetComponent<Image>().color;
            
                emptyBar.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a + 1); //Set color
            
                yield return new WaitForSeconds(fadeTimePerUnit); //Wait
            }
        }
        
        
        
        yield return null;
    }*/

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
