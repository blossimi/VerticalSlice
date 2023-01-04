using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum States
    {
        InWorld,
        InInventory,
        MovingPiece
    }
    
    public KeyCode UI;
    public GameObject UICanvas;
    public MapPieceMover mpm;
    public States currentState;
    

    

    [SerializeField]
    private Dictionary<States, bool> InputState = new Dictionary<States, bool>()
    {
        { States.InWorld , false},
        { States.InInventory , false},
        { States.MovingPiece , false}
    };

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.Find("Canvas");
        mpm = GameObject.Find("MapUI").GetComponent<MapPieceMover>();
        
        UICanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(UI))
        {
            if (UICanvas.activeInHierarchy) //If UI is ON:
            {
                //DISABLE UI
                UICanvas.SetActive(false);
            }
            else if (!UICanvas.activeInHierarchy) //If UI is OFF:
            {
                //ENABLE UI
                UICanvas.SetActive(true);
            }
        }

        GetInputState();
    }

    void GetInputState()
    {
        if (UICanvas.activeInHierarchy == true && mpm.selectedPiece == null)
        {
            //Canvas is active AND there is NO selectedPiece, options:
            // - InInventory (default)

            SetState(States.InInventory);
            
        }
        if (UICanvas.activeInHierarchy == true && mpm.selectedPiece != null)
        {
            //Canvas is active AND there is a piece selected, options:
            // - MovingPiece

            SetState(States.MovingPiece);
        }

        if (UICanvas.activeInHierarchy == false)
        {
            SetState(States.InWorld);
        }
    }

    void SetState(States stateToSet)
    {
        var keys = new List<States>(InputState.Keys);
        
        foreach (States key in keys)
        {
            if (key == stateToSet) //Correct key has been found:
            {
                //Set the state's value to true
                InputState[key] = true;
                currentState = key;
            }
            else //This is not the key to set to true,
            {
                //So set it to false
                InputState[key] = false;
            }
        }
    }
    
}
