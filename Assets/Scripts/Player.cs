using UnityEngine;

public class Player : MonoBehaviour
{


    public float moveSpeed;
    private Vector3 mousePosition;
    private Vector3 dir;

    public GameObject bulletPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position); 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        

        if(Input.GetMouseButtonDown(0))
        {
            dir = mousePosition - transform.position;
            dir.Normalize();


            Shoot();
        }



        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(dir);

    }
}
