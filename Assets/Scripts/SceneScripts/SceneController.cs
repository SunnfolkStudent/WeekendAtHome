using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneScripts
{
    public class SceneController : MonoBehaviour
    {
        public void LoadSceneByName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void LoadSceneByIndex()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        public void QuitGame()
        {
            Application.Quit();
            print("Quit");
        }
    }
}