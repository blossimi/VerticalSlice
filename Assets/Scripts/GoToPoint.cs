using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GoToPoint : MonoBehaviour
{
    public GameObject waypointsHolder;
    private Transform[] points;
    private int targetIndex = 0;
    public float speed = 1f;
    public GameObject Snotaap;
    
    
    
    //stap 1 geef de waypoints holder mee als refernce
    //stap 2 loop door de children van de holder en plaats elke transdorm in de points lijst
    //stap 3 bepaal eerte doelpositie
    //stap 4 beweeg naar doelpositie
    //stap 5 als doelpositiebereikt stel nieuwe doelpositie in terug naar stap 4
    //stap 6 als je laatse doelpositie hebt gehad weer eerste uit de lijst



    private void Awake()
    {
        

        points = new Transform[waypointsHolder.transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = waypointsHolder.transform.GetChild(i);
        }       

    }
    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = points[targetIndex].position;
        Vector3 direction = targetPosition - currentPosition;
        Vector3 normal = direction.normalized; //1 stap in de goede richting



        transform.position += normal * Time.deltaTime * speed;

        // check if the GameObject has reached the target waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            
            targetIndex = (targetIndex + 1) % points.Length;
        }




        /*Vector3 target = new Vector3((float)-7.22, (float)1.09,(float)8.07);

        Vector3 currenPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        //Check for plusX dir
        if (target.x > currentPosition.x)
        {
            //Walking right

            transform.rotation = new Quaternion(-60, transform.rotation.y, transform.rotation.z, 0);
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(currentRotation.x, 90, currentRotation.z), "time", 0.25f));
        }
        else
        {
            //Walking left

            transform.rotation = new Quaternion(60, currentRotation.y, currentRotation.z, 0);
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(currentRotation.x, 0, currentRotation.z), "time", 0.25f));

        }*/

         
      


    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    flip();
    //    //Debug.Log("hij collide");
    //    //if (other.gameObject.tag == ("kind"))
    //    //{
    //    //    Snotaap.transform.localScale = new Vector3(0, -0, 0);

    //    //    Debug.Log("hij collide");
    //    //}
    //}

    //void flip()
    //{
    //    Vector3 Currentscale = gameObject.transform.localscale;
    //    Currentscale.x *= -1;
    //    gameObject.transform.localScale = Currentscale;
    //    facingRight = !facingRight
    //}

}
