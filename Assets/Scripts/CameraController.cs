using System.Collections;
using System.Collections.Generic;
using System.Net;
using NativeSerializableDictionary;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraTypes
    {
        PlayerCamera,
        UICamera
    }

    public GameObject currentCamera;
    
    [Space]
    
    [SerializeField]
    private SerializableDictionary<CameraTypes, GameObject> cameras;

    public void SwitchCamera(CameraTypes switchTo)
    {
        var keys = new List<CameraTypes>(cameras.Keys);
        
        foreach (CameraTypes key in keys)
        {
            if (key == switchTo) //Correct key has been found:
            {
                //Set the camera on
                cameras[key].Value.gameObject.SetActive(true);
                currentCamera = cameras[key].Value;
            }
            else //This is not the key,
            {
                //So set turn it off
                cameras[key].Value.gameObject.SetActive(false);
            }
        }
    }
}
