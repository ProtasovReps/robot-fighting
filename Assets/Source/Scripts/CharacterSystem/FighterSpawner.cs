using UnityEngine;
using YG;

namespace CharacterSystem
{
    public class FighterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        
        public Fighter Spawn()
        {
            Fighter fighter = Instantiate(YG2.saves.SettedFighter, _spawnPoint.position, _spawnPoint.rotation);
            
            fighter.transform.SetParent(_spawnPoint);
            return fighter;

        }
    }
}