using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using NativeSerializableDictionary;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public enum States
    {
        InWorld,
        InInventory,
        MovingPiece
    }

    public enum UIMovementDirections
    {
        Up,
        Down,
        Left,
        Right
    }
    /*public enum InputTypes
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back,
        OpenUI,
        NewPiece,
        Cancel,
        Confirm
    }*/

    /*[FormerlySerializedAs("UIInputs")] [Header("Input settings")] public SerializableDictionary<InputTypes, KeyCode> _UIInputs;
    public static SerializableDictionary<InputTypes, KeyCode> UIInputs;*/

    [Header("Settings")]
    public GameObject UICanvas;
    [Range(0.0f, 2.0f)] public float canvasFadeTime;
    [Range(0.0f, 0.1f)] public float canvasFadeAmount;
    [Range(0.0f, 0.5f)] public float canvasFadeDelay;
    [SerializeField] private MapPieceMover mpm;
    [SerializeField] private CameraController cc;
    public States currentState;
    public KeyCode reset;
    

    

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
        /*UIInputs = _UIInputs;*/
        
        UICanvas = GameObject.Find("Canvas");
        mpm = GameObject.Find("SelectionBorder").GetComponent<MapPieceMover>();
        cc = GameObject.Find("Camera").GetComponent<CameraController>();
        
        UICanvas.SetActive(false);

        canvasFadeTime = (cc.speed) - canvasFadeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (UICanvas.activeInHierarchy) //If UI is ON:
            {
                //DISABLE UI
                
                Debug.Log("Starting zoom-in coroutine:");

                StartCoroutine(cc.ZoomIn());

            }
            else if (!UICanvas.activeInHierarchy) //If UI is OFF:
            {
                //ENABLE UI
                
                Debug.Log("Starting zoom-out coroutine:");
                
                StartCoroutine(cc.ZoomOut());
            }
        }

        if (Input.GetKeyDown(reset))
        {
            SceneManager.LoadScene("Start");
        }
        
    }

    public IEnumerator FadeUIInOut(bool transparent)
    {
        CanvasGroup cg = UICanvas.GetComponent<CanvasGroup>();

        yield return new WaitForSeconds(canvasFadeDelay);

        if (transparent) //Fade to transparent
        {
            
        }
        else if(!transparent) //Fade to visible
        {
            UICanvas.SetActive(true);
            cg.alpha = 0.0f;

            while (cg.alpha != 1.0f)
            {
                cg.alpha = cg.alpha + canvasFadeAmount;
                yield return new WaitForSeconds(canvasFadeTime / (1 / canvasFadeAmount));
            }
        }
        
        yield return null;
    }

    /*void GetInputState()
    {
        if (UICanvas.activeInHierarchy == true && mpm.selectedPiece == null)
        {
            //Canvas is active AND there is NO selectedPiece, options:
            // - InInventory (default)

            SetState(States.InInventory);
            
        }
        /*if (UICanvas.activeInHierarchy == true && mpm.selectedPiece != null)
        {
            //Canvas is active AND there is a piece selected, options:
            // - MovingPiece

            SetState(States.MovingPiece);
        }#1#

        if (UICanvas.activeInHierarchy == false)
        {
            SetState(States.InWorld);
        }
    }*/

    public void SetState(States stateToSet)
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
