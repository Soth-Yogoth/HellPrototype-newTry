using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class WaveProjectail : Projectail
{
    protected static Random random;

    void Start()
    {
        random = random == null ? new Random() : random;
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Transform playerTransform = other.gameObject.GetComponent<Transform>();
            
            // float x = random.Next((int)GameManager.FirstScreenCorner.x, (int)GameManager.SecondScreenCorner.x);
            // float y = random.Next((int)GameManager.FirstScreenCorner.y, (int)GameManager.SecondScreenCorner.y);
            
            float x = random.Next(-5, 5);
            float y = random.Next(-5, 5);

            playerTransform.position = new Vector3(x, y, 0);
            
            //ObjectPool.Release(gameObject);
        }
    }
}
