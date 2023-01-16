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

    [Header("Info")]
    public bool zoomedIn = true;
    public bool zoomedOut = false;

    private void Start()
    {
        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            ZoomOut();
        }
        if (Input.GetKeyDown(KeyCode.G) && zoomedOut)
        {
            ZoomIn();
        }
    }

    public void ZoomOut()
    {
        inPos = transform.position;

        zoomOutPath[0] = transform.position;

        zoomOutPath[1] = Vector3.Lerp(zoomOutPath[0], outPos, 0.33f);
        zoomOutPath[1] = new Vector3(zoomOutPath[1].x, zoomOutPath[1].y - offsetOne, zoomOutPath[1].z);

        zoomOutPath[2] = Vector3.Lerp(zoomOutPath[0], outPos, 0.66f);
        zoomOutPath[2] = new Vector3(zoomOutPath[2].x, zoomOutPath[2].y - offsetTwo, zoomOutPath[2].z);

        zoomOutPath[3] = outPos;

        iTween.MoveTo(gameObject, iTween.Hash("position", zoomOutPath[3], "time", speed, "path", zoomOutPath, "easetype", iTween.EaseType.easeInOutSine));
        Debug.Log("Test?");
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(90f, 0f, 0f), "time", speed, "easetype", iTween.EaseType.easeInOutSine));
    }

    public void ZoomIn()
    {

        zoomInPath[0] = outPos;

        zoomInPath[1] = Vector3.Lerp(zoomInPath[0], inPos, 0.33f);
        zoomInPath[1] = new Vector3(zoomInPath[1].x, zoomInPath[1].y - offsetOne, zoomInPath[1].z);

        zoomInPath[2] = Vector3.Lerp(zoomInPath[0], inPos, 0.66f);
        zoomInPath[2] = new Vector3(zoomInPath[2].x, zoomInPath[2].y - offsetTwo, zoomInPath[2].z);

        zoomInPath[3] = inPos;

        iTween.MoveTo(gameObject, iTween.Hash("position", zoomInPath[3], "time", speed, "path", zoomInPath, "easetype", iTween.EaseType.easeInOutSine));
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(60f, 0f, 0f), "time", speed, "easetype", iTween.EaseType.easeInOutSine));
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
