using System.Collections.Generic;
using UnityEngine;

namespace UI.Store
{
    public class GoodsRandomizer : MonoBehaviour
    {
        [SerializeField] private GoodView[] _goods;

        public Queue<GoodView> Get()
        {
            Randomize();
            return new Queue<GoodView>(_goods);
        }

        private void Randomize()
        {
            GoodView tempGood;
            
            for (int i = 0; i < _goods.Length; i++)
            {
                int randomIndex = Random.Range(0, _goods.Length);

                tempGood = _goods[randomIndex];
                _goods[randomIndex] = _goods[i];
                _goods[i] = tempGood;
            }
        }
    }
}