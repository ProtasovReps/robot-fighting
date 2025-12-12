using UnityEngine;

namespace YG
{
    public class LevelStartMetrica : MonoBehaviour
    {
        private const string Start = nameof(Start);
        
        [SerializeField, Min(1)] private int _levelNumber;

        private void Awake()
        {
            YG2.MetricaSend(_levelNumber + Start);
        }
    }
}