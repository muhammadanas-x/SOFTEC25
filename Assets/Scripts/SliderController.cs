using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderController : MonoBehaviour
{
    public Slider healthSlider;
    public float damageInterval = 5f; // Seconds between each hit
    private float damageTimer = 0f;

    public float damage = 2f; // Amount of health to reduce each hit
    public GameObject gameOverPanel;

    void Start()
    {
        if (healthSlider != null)
            healthSlider.value = 1f;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                ReduceHealth();
                damageTimer = 0f;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Reset timer when enemy leaves
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageTimer = 0f;
        }
    }

    public void ReduceHealth()
    {
        if (healthSlider != null)
        {
            if (healthSlider.value > 0)
            {
                healthSlider.value = Mathf.Clamp01(healthSlider.value - damage);
                Debug.Log(healthSlider.value);
            }
            else //gameover code here
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0.2f;
            }
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
