using System;
using Extensions;
using UnityEngine;
using YG.Saver;

namespace CharacterSystem
{
    public class FighterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Fighter[] _fighterSkins;
        
        public Fighter Spawn()
        {
            SkinSaver skinSaver = new SkinSaver(new Hasher<Fighter>());
            Fighter settedSkin = null;

            for (int i = 0; i < _fighterSkins.Length; i++)
            {
                if (skinSaver.IsSetted(_fighterSkins[i]) == false)
                {
                    continue;
                }

                settedSkin = _fighterSkins[i];
                break;
            }

            if (settedSkin == null)
            {
                throw new ArgumentException(nameof(settedSkin));
            }
            
            Fighter fighter = Instantiate(settedSkin, _spawnPoint.position, _spawnPoint.rotation);
            
            fighter.transform.SetParent(_spawnPoint);
            return fighter;

        }
    }
}