using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KutKindAnimation : MonoBehaviour
{
    private Quaternion rot;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rot = GetComponent<RectTransform>().rotation;

        GetComponent<RectTransform>().Rotate(new Vector3(rot.x, rot.y, rot.z + 2.5f));
    }
}
