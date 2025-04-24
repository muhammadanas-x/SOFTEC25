using UnityEngine;
using UnityEngine.SceneManagement; // Needed for SceneManager

public class TransitionManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(currentSceneIndex);
            int nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
