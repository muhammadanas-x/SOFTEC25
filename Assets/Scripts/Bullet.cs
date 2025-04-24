using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 direction;

    public ParticleSystem particleSystem;
    private float bulletMagnitude;

    private float distanceToTarget;
    public float bulletSpeed;





    

    public void SetMagnitude(float mag)
    {
        bulletMagnitude = mag;
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += direction * Time.deltaTime * bulletMagnitude;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectDir = Vector3.Reflect(direction, normal);
            direction = reflectDir;



            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                VFXSpawner vFXSpawner = GameObject.Find("VFXSpawner").GetComponent<VFXSpawner>();   
                vFXSpawner.SpawnVFXAtPoint(collision.contacts[0].point);
                enemy.Attack(-transform.up);
                Instantiate(particleSystem.gameObject, collision.contacts[0].point, Quaternion.identity);

                enemy.TakeDamage(50);
            }

        }
    }


}

