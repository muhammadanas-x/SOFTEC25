using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject flowerPrefab;

    private float currentTimer = 0f;

    public float endTimer = 5f;

    public float maxSpawn = 3;
    public float currentCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.bulletCount > 2) return;

        
        if(currentTimer > endTimer)
        {   
           Vector3 screenPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.nearClipPlane);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0f; // Keep it 2D
            Instantiate(flowerPrefab, worldPosition, Quaternion.identity);
            currentTimer = 0f;
        }   
        else
        {
            currentTimer += Time.deltaTime;
        }


        
        
    }
}
