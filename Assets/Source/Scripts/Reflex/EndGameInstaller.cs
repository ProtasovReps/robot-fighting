using Reflex.Core;
using UnityEngine;
using YG.Saver;

namespace Reflex
{
    public class EndGameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ProgressSaver _progressSaver;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_progressSaver);
        }
    }
}