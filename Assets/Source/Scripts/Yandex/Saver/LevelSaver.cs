using Interface;

namespace YG.Saver
{
    public class LevelSaver : ISaver
    {
        public void Save()
        {
            YG2.saves.SceneIndex++;
        }
    }
}