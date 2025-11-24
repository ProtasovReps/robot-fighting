using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    private const int CustomizationLevelIndex = 1;
    private const int FirstLevelIndex = 3;
    private const int FirstCustomizationsSceneIndex = 6;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        var awaiter = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SDKAwaiter>();

        awaiter.WaitSDKInitialization(InstallProject).Forget();
    }

    private void InstallProject()
    {
        InstallLevel();
    }

    private void InstallLocalization()
    {
        //
    }

    private void InstallLevel()
    {
        int sceneIndex;

        if (YG2.isFirstGameSession)
            sceneIndex = FirstLevelIndex;
        else if (YG2.saves.SceneIndex < FirstCustomizationsSceneIndex)
            sceneIndex = YG2.saves.SceneIndex;
        else
            sceneIndex = CustomizationLevelIndex;

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}