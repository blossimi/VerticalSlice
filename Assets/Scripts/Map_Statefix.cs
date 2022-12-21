using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Statefix : MonoBehaviour
{
    public float changePerSecondZ;

    public GameObject image;
    public GameObject player;
    public GameObject Camera;
    public enum State
    {
        OPEN,
        CLOSED
    }

    public State currentState = State.CLOSED;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Set the camera's position to the player's position
        Camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.transform.position.z);

        Debug.Log("curr" + currentState);
        switch (currentState)
        {
            case State.OPEN:
                image.SetActive(true);
                // If the camera's Z position is less than -50, increase it by changePerSecondZ * Time.deltaTime
                if (Camera.transform.position.z >= -50)
                {
                    Camera.transform.position += new Vector3(0, 0, changePerSecondZ * Time.deltaTime);
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    currentState = State.CLOSED;
                }
                break;

            case State.CLOSED:
                image.SetActive(false);
                // If the camera's Z position is greater than -22.7, decrease it by changePerSecondZ * Time.deltaTime
                if (Camera.transform.position.z <= -22.7f)
                {
                    Camera.transform.position -= new Vector3(0, 0, changePerSecondZ * Time.deltaTime);
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    currentState = State.OPEN;
                }
                break;
        }
    }
}