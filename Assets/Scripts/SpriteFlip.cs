using UnityEngine;

public class SpriteFlip : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Go-Right")
        {
            gameObject.transform.localScale = new Vector3(-0.06f, 0.06f, 0.06f);

        }

        if (other.gameObject.tag == "Go-Left")
        {
            gameObject.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);

        }
    }
}