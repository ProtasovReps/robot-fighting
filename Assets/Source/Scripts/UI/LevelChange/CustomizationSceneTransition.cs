using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.LevelChange
{
    public class CustomizationSceneTransition : SceneTransition
    {
        [SerializeField] private CustomizationSceneName _sceneName;

        protected override void LoadScene()
        {
            string sceneName = _sceneName.ToString();

            SceneManager.LoadScene(sceneName);
        }
    }
}