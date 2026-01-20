using Reflex.Core;
using UnityEngine;
using YG;
using YG.Saver;

namespace Reflex
{
    public class EndGameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ProgressSaver _progressSaver;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            YG2.saves.SceneIndex++;
            
            containerBuilder.AddSingleton(_progressSaver);
        }
    }
}