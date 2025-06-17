using HealthSystem;
using UnityEngine;

namespace CharacterSystem
{ //Возвращать в случае чего файтера, сама фабрика должна быть Fighter, а враг или игрок - наследники переопределят
    public class FighterFactory : MonoBehaviour // пока что по сути мок
    {
        [SerializeField] private Fighter _fighter; // А если скины менять, т.е. модельки? ScriptableObject?
        [SerializeField] private HealthView _healthView;
        [SerializeField] [Min(1)] private float _startHealthValue;
        
        private void Awake() // точно не тут, скорее билдер уровня должен этим заниматься
        {
            Produce();
        }

        private Fighter Produce()
        {
            Health health = new(_startHealthValue);
            _fighter.Initialize(health);
            _healthView.Initialize(health);
            return _fighter;
        }
    }
}