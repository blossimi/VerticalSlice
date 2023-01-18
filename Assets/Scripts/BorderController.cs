using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    [Header("Info")] 
    public bool tilePlusX;
    public bool tileMinX;
    public bool tilePlusZ;
    public bool tileMinZ;
    
    [Header("Borders")]
    public GameObject minX;
    public GameObject plusX;
    public GameObject minZ;
    public GameObject plusZ;

    [Header("Corners")] 
    public GameObject c_minX_plusZ;
    public GameObject c_plusX_plusZ;
    public GameObject c_minX_minZ;
    public GameObject c_plusX_minZ;
    
    //Private variables
    private Ray plusXRay;
    private Ray minXRay;
    private Ray plusZRay;
    private Ray minZRay;

    // Start is called before the first frame update
    void Start()
    {
        plusXRay = new Ray(transform.position, new Vector3(1, 0, 0));
        minXRay = new Ray(transform.position, new Vector3(-1, 0, 0));
        plusZRay = new Ray(transform.position, new Vector3(0, 0, 1));
        minZRay = new Ray(transform.position, new Vector3(0, 0, -1));
    }

    // Update is called once per frame
    void Update()
    {
        CheckNeighbours();
        CheckCorners();
    }

    void CheckNeighbours()
    {
        GameObject[] plusXc = {plusX, c_plusX_plusZ, c_plusX_minZ};
        GameObject[] minXc = {minX, c_minX_plusZ, c_minX_minZ};
        GameObject[] plusZc = {plusZ, c_plusX_plusZ, c_minX_plusZ};
        GameObject[] minZc = {minZ, c_minX_minZ, c_plusX_minZ};

        RaycastBorderCollider(plusXRay, plusX, plusXc);
        RaycastBorderCollider(minXRay, minX, minXc);
        RaycastBorderCollider(plusZRay, plusZ, plusZc);
        RaycastBorderCollider(minZRay, minZ, minZc);
    }

    void RaycastBorderCollider(Ray ray, GameObject border, GameObject[] corners)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 15f))
        {
            if (hit.collider.gameObject.tag == "BorderCollider")
            {
                //There is a tile next to subject tile in this direction, so
                //The border needs to render out
                RenderInOut(border, false);
                
                //And boolean needs to be set
                if (ray.direction == plusXRay.direction)
                {//Set plusX
                    tilePlusX = true;
                }else if (ray.direction == minXRay.direction)
                {//Set minX
                    tileMinX = true;
                }else if (ray.direction == plusZRay.direction)
                {//Set plusZ
                    tilePlusZ = true;
                }else if (ray.direction == minZRay.direction)
                {//Set minZ
                    tileMinZ = true;
                }
            }
        }
        else //There is no hit in this direction, so
        {
            //The border needs to render in
            RenderInOut(border, true);
        }
    }

    void CheckCorners()
    {
        if (!tilePlusX && !tileMinZ) //If there is no tile both in plusX and minZ, ONLY THEN c_plusX_minZ can be rendered
        {
            RenderInOut(c_plusX_minZ, true);
        } else { RenderInOut(c_plusX_minZ, false); }
        
        if (!tilePlusX && !tilePlusZ) //If there is no tile both in plusX and plusZ, ONLY THEN c_plusX_plusZ can be rendered
        {
            RenderInOut(c_plusX_plusZ, true);
        } else { RenderInOut(c_plusX_plusZ, false); }
        
        if (!tileMinX && !tilePlusZ) //If there is no tile both in minX and plusZ, ONLY THEN c_minX_plusZ can be rendered
        {
            RenderInOut(c_minX_plusZ, true);
        } else { RenderInOut(c_minX_plusZ, false); }
        
        if (!tileMinX && !tileMinZ) //If there is no tile both in minX and minZ, ONLY THEN c_minX_minZ can be rendered
        {
            RenderInOut(c_minX_minZ, true);
        } else { RenderInOut(c_minX_minZ, false); }
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
