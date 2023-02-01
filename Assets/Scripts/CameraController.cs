using System.Collections;
using System.Collections.Generic;
using System.Net;
using NativeSerializableDictionary;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Settings")]
    [Tooltip("0: normal position - This path is set automatically with code, do not edit")] public Vector3[] zoomOutPath;
    public Vector3[] zoomInPath;
    public Vector3 inPos;
    public Vector3 outPos;
    public float speed = 1f;
    public float offsetOne = 2f;
    public float offsetTwo = 1f;
    
    //Private variables
    private InputManager im;
    private Movement playerMovement;

    private void Start()
    {
        
        im = GameObject.Find("GameManager").GetComponent<InputManager>();
        playerMovement = transform.parent.gameObject.GetComponent<Movement>();
    }

    private void Update()
    {
        im = GameObject.Find("GameManager").GetComponent<InputManager>();
        
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            //im.UICanvas.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //ZoomIn();
        }*/
    }

    public IEnumerator ZoomOut()
    {
        //Lock player movement
        playerMovement.movementLocked = true;
        
        //Determine position to return to when zooming in again
        inPos = transform.position;
        
        //Set path to zoom out along
        zoomOutPath[0] = inPos;

        zoomOutPath[1] = Vector3.Lerp(zoomOutPath[0], outPos, 0.33f);
        zoomOutPath[1] = new Vector3(zoomOutPath[1].x, zoomOutPath[1].y - offsetOne, zoomOutPath[1].z);

        zoomOutPath[2] = Vector3.Lerp(zoomOutPath[0], outPos, 0.66f);
        zoomOutPath[2] = new Vector3(zoomOutPath[2].x, zoomOutPath[2].y - offsetTwo, zoomOutPath[2].z);

        zoomOutPath[3] = outPos;
        
        //Start fading UI
        StartCoroutine(im.FadeUIInOut(false)); //Fade UI from 0.0f to 1.0f alpha
        
        //MoveTo
        iTween.MoveTo(gameObject, iTween.Hash("position", zoomOutPath[3], "time", speed, "path", zoomOutPath, "easetype", iTween.EaseType.linear));

        //RotateTo
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(90f, 0f, 0f), "time", speed, "easetype", iTween.EaseType.linear));
        
        //Wait for MoveTo & RotateTo to finish
        while (Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, outPos.y, 0)) < 2) //While the camera is not yet zoomed out
        {
            //Wait
            yield return null;
        }
        
        //Set correct state
        im.SetState(InputManager.States.InInventory);
        
        
        
        yield return null;
    }

    public IEnumerator ZoomIn()
    {

        //Set path to zoom out along
        zoomInPath[0] = outPos;

        zoomInPath[1] = Vector3.Lerp(zoomInPath[0], inPos, 0.33f);
        zoomInPath[1] = new Vector3(zoomInPath[1].x, zoomInPath[1].y - offsetOne, zoomInPath[1].z);

        zoomInPath[2] = Vector3.Lerp(zoomInPath[0], inPos, 0.66f);
        zoomInPath[2] = new Vector3(zoomInPath[2].x, zoomInPath[2].y - offsetTwo, zoomInPath[2].z);

        zoomInPath[3] = inPos;
        
        //Disable UI
        StartCoroutine(im.FadeUIInOut(true));

        //MoveTo
        iTween.MoveTo(gameObject, iTween.Hash("position", zoomInPath[3], "time", speed, "path", zoomInPath, "easetype", iTween.EaseType.linear));

        //RotateTo
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(60f, 0f, 0f), "time", speed, "easetype", iTween.EaseType.linear));
        
        //Wait for MoveTo & RotateTo to finish
        while (Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, inPos.y, 0)) < 2) //While the camera is not yet zoomed out
        {
            //Wait
            yield return null;
        }
        
        //Set correct state
        im.SetState(InputManager.States.InWorld);
        
        //Unlock playerMovement
        playerMovement.movementLocked = false;
        
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(zoomOutPath[0], 0.25f);
        Gizmos.DrawWireSphere(zoomOutPath[1], 0.25f);
        Gizmos.DrawWireSphere(zoomOutPath[2], 0.25f);
        Gizmos.DrawWireSphere(zoomOutPath[3], 0.25f);

        Gizmos.DrawSphere(outPos, 0.5f);
    }

}
