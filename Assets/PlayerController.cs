using UnityEngine;

public class PlayerController : MonoBehaviour
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
