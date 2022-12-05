using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Settings")]
    public string tagName = "BaseTile";

    public GameObject[] chunks;

    // Start is called before the first frame update
    void Start()
    {
        SetupTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupTiles()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag(tagName))
        {
            int newCordX = Mathf.RoundToInt(go.transform.position.x / 30);
            int newCordZ = Mathf.RoundToInt(go.transform.position.z / 30);

            go.name = newCordX + ", " + newCordZ;

            go.GetComponent<Chunk>().x = newCordX;
            go.GetComponent<Chunk>().z = newCordZ;
        }
    }

}
