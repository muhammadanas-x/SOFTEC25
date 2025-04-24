using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private GameObject Center;
    public float health = 100f;
    public float moveSpeed = 10f;
    public float recoveryTime = 2f;

    private Rigidbody2D rb;
    private Vector2 dir;

    private bool isAttacked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Center = GameObject.Find("Center"); // Find the player GameObject by its tag    
    }

    void Update()
    {
        dir = (Center.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (!isAttacked)
        {
            rb.linearVelocity = dir * moveSpeed;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject); // Destroy the enemy when health is zero
        }
    }
    public void Attack(Vector3 Direction)
    {
        isAttacked = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Direction * 10f, ForceMode2D.Impulse); // Impulse for smoother knockback
        StartCoroutine(RecoverAfterDelay());
    }

    private IEnumerator RecoverAfterDelay()
    {
        yield return new WaitForSeconds(recoveryTime);
        isAttacked = false;
    }


}
