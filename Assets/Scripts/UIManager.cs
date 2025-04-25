using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject TextObject;
    public TextMeshProUGUI timerText; // Reference to your TMP text for the timer
    public GameObject winScreenPanel;
    
    private TextMeshPro text;
    private float timeRemaining = 60f; // 1 minute in seconds
    private bool timerIsRunning = false;

    void Start()
    {
        text = TextObject.GetComponent<TextMeshPro>();
        timerIsRunning = true; // Start the timer when the game begins
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                // Timer has reached zero
                timeRemaining = 0;
                timerIsRunning = false;
                winScreenPanel.SetActive(true); // Show win screen
                Time.timeScale = 0f;
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        // Convert seconds to minutes:seconds format
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        // Update the TMP text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void setText(string text)
    {
        this.text.text = text;
    }
}