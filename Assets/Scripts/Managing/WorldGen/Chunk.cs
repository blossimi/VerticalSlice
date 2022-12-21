using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public int x;
    public int z;

    public Chunk(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}
