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

    private void Start()
    {
        
        im = GameObject.Find("GameManager").GetComponent<InputManager>();
    }

    private void Update()
    {
        //If camera is in outPos, set State to inInventory
        if (transform.position == outPos)
        {
            im.SetState(InputManager.States.InInventory);
        }
        else if (transform.position == inPos)
        {
            im.SetState((InputManager.States.InWorld));
        }
        


        if (Input.GetKeyDown(KeyCode.F))
        {
            ZoomOut();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ZoomIn();
        }
    }

    public IEnumerator ZoomOut()
    {
        
        //Determine position to return to when zooming in again
        inPos = transform.position;
        
        //Set path to zoom out along
        zoomOutPath[0] = inPos;

        zoomOutPath[1] = Vector3.Lerp(zoomOutPath[0], outPos, 0.33f);
        zoomOutPath[1] = new Vector3(zoomOutPath[1].x, zoomOutPath[1].y - offsetOne, zoomOutPath[1].z);

        zoomOutPath[2] = Vector3.Lerp(zoomOutPath[0], outPos, 0.66f);
        zoomOutPath[2] = new Vector3(zoomOutPath[2].x, zoomOutPath[2].y - offsetTwo, zoomOutPath[2].z);

        zoomOutPath[3] = outPos;
        
        //MoveTo
        iTween.MoveTo(gameObject, iTween.Hash("position", zoomOutPath[3], "time", speed, "path", zoomOutPath, "easetype", iTween.EaseType.linear));

        //RotateTo
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(90f, 0f, 0f), "time", speed, "easetype", iTween.EaseType.linear));
        
        //Wait for MoveTo & RotateTo to finish
        while (im.currentState != InputManager.States.InInventory) //While the state is not inInventory,
        {
            //Wait
            yield return null;
        }
        
        //Enable UI
        im.UICanvas.SetActive(true);

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
        im.UICanvas.SetActive(false);

        //MoveTo
        iTween.MoveTo(gameObject, iTween.Hash("position", zoomInPath[3], "time", speed, "path", zoomInPath, "easetype", iTween.EaseType.linear));

        //RotateTo
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(60f, 0f, 0f), "time", speed, "easetype", iTween.EaseType.linear));
        
        /*//Wait for MoveTo & RotateTo to finish
        while (im.currentState != InputManager.States.InWorld) //While the state is not inWorld,
        {
            //Wait
            yield return null;
        }*/
        
        

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
