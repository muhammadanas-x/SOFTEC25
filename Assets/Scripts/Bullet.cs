using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 direction;
    private float distanceToTarget;
    public float bulletSpeed;



    

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hi");
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectDir = Vector3.Reflect(direction, normal);
            direction = reflectDir;

        }
    }


}

