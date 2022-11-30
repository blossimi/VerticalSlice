using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectRandom : MonoBehaviour
{
    public GameObject objectToSpawn;
    [Range(0.0f, 30.0f)] public int minAmount = 0;
    [Range(0.0f, 30.0f)] public int maxAmount = 5;
    [Tooltip("Max inclusive")] [Range(0.0f, 30.0f)] public float maxDistance = 5f;
    [Tooltip("Whether or not y coordinate should also be randomized by max-distance")] public bool randomYDifference = false;
    [Tooltip("Y-coordinate spawning offset (so objects dont spawn *in* the ground)")] public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        int amountToSpawn = Random.Range(minAmount, maxAmount);

        for(int i = 0; i < amountToSpawn; i++)
        {
            SpawnObject(objectToSpawn);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnObject(GameObject obj)
    {
        float randomX = Random.Range(-maxDistance, maxDistance);
        float randomY = 0f;
        float randomZ = Random.Range(-maxDistance, maxDistance);

        if (randomYDifference)
        {
            randomY = Random.Range(-maxDistance, maxDistance);
        }

        Vector3 position = new Vector3(randomX, randomY + yOffset, randomZ);

        GameObject newObj = Instantiate(obj);

        newObj.transform.parent = transform;
        newObj.transform.localPosition = position;
        newObj.transform.rotation = GameSettings.globalRotation;
    }
}
