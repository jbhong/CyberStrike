using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadNextLevel()
    {
        Difficulty.IncreaseLevel();
        SceneManager.LoadSceneAsync(1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadAfterLobby()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadMainLevel()
    {
        if(Difficulty.difficulty < 0)
        {
            Difficulty.SetDifficulty(0);
        }
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadLobby()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
