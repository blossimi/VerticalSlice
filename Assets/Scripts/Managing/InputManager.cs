using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using NativeSerializableDictionary;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*public class KeyCodeSet : ScriptableObject
{
    public string type;
    public KeyCode input;
}*/

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
    public enum InputTypes
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back,
        OpenUI
    }

    [FormerlySerializedAs("UIInputs")] [Header("Input settings")] public SerializableDictionary<InputTypes, KeyCode> _UIInputs;
    public static SerializableDictionary<InputTypes, KeyCode> UIInputs;

    [Header("Settings")]
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private MapPieceMover mpm;
    [SerializeField] private CameraController cc;
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
        UIInputs = _UIInputs;
        
        UICanvas = GameObject.Find("Canvas");
        mpm = GameObject.Find("SelectionBorder").GetComponent<MapPieceMover>();
        cc = GameObject.Find("GameManager").GetComponent<CameraController>();
        
        UICanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(UIInputs.GetValue(InputTypes.OpenUI)))
        {
            if (UICanvas.activeInHierarchy) //If UI is ON:
            {
                //DISABLE UI
                UICanvas.SetActive(false);
                //cc.SwitchCamera(CameraController.CameraTypes.PlayerCamera);
            }
            else if (!UICanvas.activeInHierarchy) //If UI is OFF:
            {
                //ENABLE UI
                UICanvas.SetActive(true);
                //cc.SwitchCamera(CameraController.CameraTypes.UICamera);
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
