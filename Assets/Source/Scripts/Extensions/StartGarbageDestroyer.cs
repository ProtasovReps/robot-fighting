using UnityEngine;

namespace Extensions
{
    public class StartGarbageDestroyer : MonoBehaviour
    {
        [SerializeField] private Transform[] _deletables;

        private void Start()
        {
            for (int i = 0; i < _deletables.Length; i++)
            {
                Destroy(_deletables[i].gameObject);                
            }
            
            Destroy(gameObject);
        }
    }
}