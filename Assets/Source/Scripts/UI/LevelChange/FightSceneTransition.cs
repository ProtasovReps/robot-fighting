using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Random = UnityEngine.Random;

namespace UI.LevelChange
{
    public class FightSceneTransition : SceneTransition
    {
        private const int FirstSceneIndex = 2;

        protected override void LoadScene()
        {
            int sceneIndex = YG2.saves.SceneIndex;

            if (YG2.saves.IfBeatedGame)
            {
                sceneIndex = GetRandomScene(SceneManager.sceneCountInBuildSettings);
            }

            Debug.Log(sceneIndex);
            string sceneName = SceneManager.GetSceneByBuildIndex(sceneIndex).name;

            Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
        }

        private int GetRandomScene(int sceneCount)
        {
            return Random.Range(FirstSceneIndex, sceneCount);
        }
    }
}