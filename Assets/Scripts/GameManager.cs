using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void SceneLoad(int index)
    {
        // Load the scene with the given name
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
