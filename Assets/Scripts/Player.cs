using System;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public GameObject bulletPrefab;
    public GameObject directionIndicatorPrefab; // Small circle sprite prefab
    public int indicatorCount = 5; // Number of indicator sprites
    public float indicatorSpacing = 1f; // Space between indicators
    public float maxIndicatorSize = 0.5f;  // Size of the first (closest) indicator
    public float minIndicatorSize = 0.1f;  // Size of the last (farthest) indicator
    public float sizeFalloffCurve = 1.5f;  // Controls how quickly size decreases (higher = fast
    
    public GameObject slowMotionSprite; // Reference to the slow motion sprite
    private bool isDragging;
    private float dragMagnitude;
    private GameObject[] directionIndicators;

    void Start()
    {
        // Create pool of indicator objects
        directionIndicators = new GameObject[indicatorCount];
        for (int i = 0; i < indicatorCount; i++)
        {
            directionIndicators[i] = Instantiate(directionIndicatorPrefab);
            directionIndicators[i].SetActive(false);
        }
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleShooting();
    }

    void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position); 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.unscaledDeltaTime * 10f);
    }

    void HandleMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += moveInput.normalized * moveSpeed * Time.deltaTime;
    }

    void HandleShooting()
    {
       
        if (Input.GetMouseButton(0))
        {  
            Time.timeScale = 0.1f;
            // Calculate drag magnitude
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePos.z = 0;
            
            dragMagnitude = Vector3.Distance(transform.position, currentMousePos);
            dragMagnitude = Mathf.Pow(dragMagnitude, 2.0f);
            
            // Update direction indicators
            UpdateDirectionIndicators(currentMousePos);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1f;

            Shoot(dragMagnitude);
            HideDirectionIndicators();
            isDragging = false;
        }

    }

     void UpdateDirectionIndicators(Vector3 mousePosition)
    {
        // Calculate direction from player to mouse
        Vector3 direction = (mousePosition - transform.position).normalized;
        float totalDistance = Vector3.Distance(transform.position, mousePosition);
        
        // Position indicators along this direction with decreasing size
        for (int i = 0; i < indicatorCount; i++)
        {
            // Calculate position (linear spacing)
            float distanceRatio = (i + 1f) / indicatorCount;
            float distance = distanceRatio * Mathf.Min(totalDistance, indicatorCount * indicatorSpacing);
            Vector3 position = transform.position + (direction * distance);
            
            // Calculate size (non-linear decrease)
            float sizeRatio = 1 - Mathf.Pow(distanceRatio, sizeFalloffCurve);
            float size = Mathf.Lerp(minIndicatorSize, maxIndicatorSize, sizeRatio);
            
            // Apply position and size
            directionIndicators[i].transform.position = position;
            directionIndicators[i].transform.localScale = new Vector3(size, size, 1);
            directionIndicators[i].SetActive(true);
            
            // Optional: Fade color based on distance
            Color color = directionIndicators[i].GetComponent<SpriteRenderer>().color;
            color.a = 1 - (distanceRatio * 0.7f);  // 30% transparency at max distance
            directionIndicators[i].GetComponent<SpriteRenderer>().color = color;
        }
    }

    void HideDirectionIndicators()
    {
        foreach (var indicator in directionIndicators)
        {
            indicator.SetActive(false);
        }
    }

    void Shoot(float magnitude)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.rotation = transform.rotation; // Set bullet rotation to player rotation
        bullet.GetComponent<Bullet>().SetDirection(-transform.up);
        bullet.GetComponent<Bullet>().SetMagnitude(magnitude);
    }
}