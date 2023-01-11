using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [Header("Settings")]
    public Color colorFull;
    [Range(0.0f, 1.0f)] public float transparency;

    public bool switch_A_pickup;
    public bool switch_A_putdown;
    public bool switch_LR;
    public bool switch_B;
    public bool switch_Y;
    public bool switch_X;

    [Header("Buttons")]
    public GameObject A_pickup;
    public GameObject A_putdown;
    public GameObject LR;
    public GameObject B;
    public GameObject Y;
    public GameObject X;

    // Start is called before the first frame update
    void Start()
    {
        colorFull = Color.white;

    }

    // Update is called once per frame
    void Update()
    {

        switch (switch_A_pickup)
        {
            case false:
                RenderImage(A_pickup, false); break;
            default:
                RenderImage(A_pickup, true); break;
        }
        switch (switch_A_putdown)
        {
            case false:
                RenderImage(A_putdown, false); break;
            default:
                RenderImage(A_putdown, true); break;
        }
        switch (switch_LR)
        {
            case false:
                RenderImage(LR, false); break;
            default:
                RenderImage(LR, true); break;
        }
        switch (switch_B)
        {
            case false:
                RenderImage(B, false); break;
            default:
                RenderImage(B, true); break;
        }
        switch (switch_Y)
        {
            case false:
                RenderImage(Y, false); break;
            default:
                RenderImage(Y, true); break;
        }
        switch (switch_X)
        {
            case false:
                RenderImage(X, false); break;
            default:
                RenderImage(X, true); break;
        }
    }

    void RenderImage(GameObject button, bool render)
    {
        Color colorTrans = new Color(colorFull.r, colorFull.g, colorFull.b, transparency);

        if (render == true)
        {
            button.gameObject.GetComponent<Image>().color = colorFull;
        }
        if(render == false)
        {
            button.gameObject.GetComponent<Image>().color = colorTrans;
        }
    }
}
