using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public Quaternion setGlobalRotation;
    public static Quaternion globalRotation;
    [Space]
    [Space]
    public int x;

    // Start is called before the first frame update
    void Start()
    {
        globalRotation = setGlobalRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
