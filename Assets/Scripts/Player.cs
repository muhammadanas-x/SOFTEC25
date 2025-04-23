using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public GameObject bulletPrefab;
    
    private Vector3 dragStartPosition;
    private bool isDragging;
    private Vector3 shootDirection;

    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleShooting();
    }

    void HandleRotation()
    {
        // Make player always face mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position); 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

    }

    void HandleMovement()
    {
        // Handle WASD or arrow key movement
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += moveInput.normalized * moveSpeed * Time.deltaTime;
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Start of drag
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStartPosition.z = 0;
            isDragging = true;
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            // End of drag - shoot
            Vector3 dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragEndPosition.z = 0;
            
            shootDirection = (dragStartPosition - dragEndPosition);
            Shoot(shootDirection , shootDirection.magnitude);
            
            isDragging = false;
        }
    }

   void Shoot(Vector3 direction , float magnitude)
{
    // Spawn bullet slightly in front of player to avoid immediate collision
    Vector3 spawnPosition = transform.position + (direction * 0.5f); // 0.5 units forward
    
    GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
    
    // Align bullet rotation with shooting direction
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    
    bullet.GetComponent<Bullet>().SetDirection(-transform.up);
    bullet.GetComponent<Bullet>().SetMagnitude(magnitude);
}
}