using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    public GameObject minX;
    public GameObject plusX;
    public GameObject minZ;
    public GameObject plusZ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckNeighbours();
    }

    void CheckNeighbours()
    {
        Ray plusXRay = new Ray(transform.position, new Vector3(1, 0, 0));
        Ray minXRay = new Ray(transform.position, new Vector3(-1, 0, 0));
        Ray plusZRay = new Ray(transform.position, new Vector3(0, 0, 1));
        Ray minZRay = new Ray(transform.position, new Vector3(0, 0, -1));

        RaycastBorderCollider(plusXRay, plusX);
        RaycastBorderCollider(minXRay, minX);
        RaycastBorderCollider(plusZRay, plusZ);
        RaycastBorderCollider(minZRay, minZ);
    }

    void RaycastBorderCollider(Ray ray, GameObject border)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 15f))
        {
            if (hit.collider.gameObject.tag == "BorderCollider")
            {
                RenderInOut(border, false);
            }
        }
        else
        {
            RenderInOut(border, true);
        }
    }

    void RenderInOut(GameObject border, bool render)
    {
        if(render == true)
        {
            border.gameObject.SetActive(true);
        }
        if(render == false)
        {
            border.gameObject.SetActive(false);
        }
    }
}
