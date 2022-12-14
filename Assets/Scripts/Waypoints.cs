using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public float speed = 1;
    private Transform target;
    private int waypointindex = 0;
    void Start()
    {
        target = GoToPoint.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint()
    {
        if (waypointindex == GoToPoint.points.Length - 1)
        {

            Destroy(gameObject);
            return;
        }
        waypointindex++;
        target = GoToPoint.points[waypointindex];
    }

}
