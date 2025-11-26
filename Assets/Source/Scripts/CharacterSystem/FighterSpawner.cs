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
        
        //здесь получим сейвер, из него проврим хэш
        public Fighter Spawn() // мб сюда и получать скин сейвер
        {
            SkinSaver skinSaver = new SkinSaver(new Hasher<Fighter>()); // брать из сейва;
            Fighter settedSkin = null;

            for (int i = 0; i < _fighterSkins.Length; i++)
            {
                if (skinSaver.IsSetted(_fighterSkins[i]) == false)
                    continue;

                settedSkin = _fighterSkins[i];
                break;
            }

            if (settedSkin == null)
                throw new ArgumentException(nameof(settedSkin));
            
            Fighter fighter = Instantiate(settedSkin, _spawnPoint.position, _spawnPoint.rotation); // попробовать просто в setted skin записать
            
            fighter.transform.SetParent(_spawnPoint);
            return fighter;

        }
    }
}