using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoBall : MonoBehaviour
{
    public Color gizmoColor;
    public Vector3 size = new Vector3(0.2f, 0.2f, 0.2f);

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, size);
        Gizmos.color = gizmoColor;
    }
}
