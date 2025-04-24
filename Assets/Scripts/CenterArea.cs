using UnityEngine;

public class CenterArea : MonoBehaviour
{

    public SliderController sliderController;

    public float damageTimer = 0f;
    public float damageInterval = 5f;
    void OnCollisionStay2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Enemy"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                sliderController.ReduceHealth();
                damageTimer = 0f;
            }
        }
    }

}
