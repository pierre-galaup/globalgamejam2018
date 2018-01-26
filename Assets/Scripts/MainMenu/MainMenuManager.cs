using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}