using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager> 
    {
        public void RestartCurrentLevel()
        {
            var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeSceneIndex);
        }
    }
}