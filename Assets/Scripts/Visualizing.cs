using UnityEngine;

public class Visualizing : MonoBehaviour
{
    public float maxDistance = 100f;
    public LayerMask hitLayers; // Assign in Inspector

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 origin = new Vector2(transform.position.x,transform.position.y);
        Vector2 direction = (new Vector2(mousePosition.x , mousePosition.y) - origin).normalized;

        // First raycast
        RaycastHit2D hit1 = Physics2D.Raycast(origin, direction, maxDistance, hitLayers);

        if (hit1.collider != null)
        {
            Debug.DrawLine(origin, hit1.point, Color.red);
            Debug.Log("First hit: " + hit1.collider.name + " at normal: " + hit1.normal);

            Vector2 reflectedDir = Vector2.Reflect(direction, hit1.normal);
            Vector2 secondOrigin = hit1.point + hit1.normal * 0.01f; // Slight offset to avoid hitting the same surface

            Debug.DrawLine(hit1.point, hit1.point + reflectedDir * 5f, Color.blue);

            // Second raycast from reflection
            RaycastHit2D hit2 = Physics2D.Raycast(secondOrigin, reflectedDir, maxDistance, hitLayers);

            if (hit2.collider != null)
            {
                Debug.DrawLine(secondOrigin, hit2.point, Color.cyan);
                Debug.Log("Second hit: " + hit2.collider.name + " at normal: " + hit2.normal);

                // Optional: Draw 2nd reflection
                Vector2 reflectedDir2 = Vector2.Reflect(reflectedDir, hit2.normal);
                Debug.DrawLine(hit2.point, hit2.point + reflectedDir2 * 5f, Color.yellow);
            }
            else
            {
                Debug.DrawLine(secondOrigin, secondOrigin + reflectedDir * maxDistance, Color.gray);
            }
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * maxDistance, Color.green);
        }
    }
}
