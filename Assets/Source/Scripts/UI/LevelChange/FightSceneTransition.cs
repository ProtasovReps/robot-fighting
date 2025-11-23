using UnityEngine.SceneManagement;
using YG;

namespace UI.LevelChange
{
    public class FightSceneTransition : SceneTransition
    {
        private const int FirstSceneIndex = 2;
        
        protected override void LoadScene()
        {
            int sceneIndex = YG2.saves.SceneIndex;
            
            if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                YG2.saves.SceneIndex = FirstSceneIndex;
                sceneIndex = FirstSceneIndex;
            }
            
            SceneManager.LoadScene(sceneIndex);
        }
    }
}