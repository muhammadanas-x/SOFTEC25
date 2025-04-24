using UnityEngine;

public class EnemyRotation : MonoBehaviour
{

    public GameObject leftwing;

    public GameObject rightwing;
    private GameObject CenterObject;  // Reference to the player GameObject
    public float RotationSpeed = 5f;  // Controls how quickly the enemy rotates

    void Start()
    {
        CenterObject = GameObject.Find("Center"); // Find the player GameObject by its tag
    }
    void Update()
    {
       float rotation = Mathf.Sin(Time.time * 2f) * 0.5f + 0.5f;
        float angle = Mathf.Lerp(0f, 30f, rotation);
        leftwing.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle); // Z axis for 2D rotation

        
        if (CenterObject != null)
        {
            


            Vector3 direction = CenterObject.transform.position - transform.position;
            
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle + 90f);


           


        }


    }
}