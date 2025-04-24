using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject Center;
    public float moveSpeed = 10f;
    public float recoveryTime = 2f;

    private Rigidbody2D rb;
    private Vector2 dir;

    private bool isAttacked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
