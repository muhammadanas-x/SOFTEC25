using UnityEngine;

public class GameManager : MonoBehaviour
{


    public void SceneLoad(string name)
    {
        // Load the scene with the given name
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
