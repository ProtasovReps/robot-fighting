using NewInputSystem;
using Reflex.Core;
using UnityEngine;

namespace Reflex
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private InputReader _inputReader;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            InstallInput(containerBuilder);
        }

        private void InstallInput(ContainerBuilder builder)
        {
            UserInput input = new();
            
            _inputReader.Initialize(input);
            builder.AddSingleton(_inputReader);
        }
    }
}