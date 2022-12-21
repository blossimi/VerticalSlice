using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode UI;
    public GameObject UICanvas;

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.Find("Canvas");
        
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
    }
}
