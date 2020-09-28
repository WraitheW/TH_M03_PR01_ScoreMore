using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadGame : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.onClicked += loadMenu;
    }

    public void onDisable()
    {
        EventManager.onClicked -= loadMenu;
    }

    public void restartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void loadMenu()
    {
        EventManager.onClicked -= loadMenu;
        SceneManager.LoadScene("menuScene");
    }
}
