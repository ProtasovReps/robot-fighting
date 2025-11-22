using Interface;
using UnityEngine;

namespace YG.Saver
{
    public class LevelSaver : ISaver
    {
        public void Save()
        {
            Debug.LogError("Saved");
            ++YG2.saves.SceneIndex;
        }
    }
}