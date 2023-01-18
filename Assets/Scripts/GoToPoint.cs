using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GoToPoint : MonoBehaviour
{
    public GameObject waypointsHolder;
    private Transform[] points;
    private int targetIndex = 0;
    public float speed = 1f;
    
    
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
        


    }

   
}
