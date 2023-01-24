using UnityEngine;

public class SpriteFlip : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        Vector3 scale = gameObject.transform.localScale;
        
        if (other.gameObject.tag == "Go-Right")
        {
            gameObject.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);

        }

        if (other.gameObject.tag == "Go-Left")
        {
            gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);

        }
    }
}