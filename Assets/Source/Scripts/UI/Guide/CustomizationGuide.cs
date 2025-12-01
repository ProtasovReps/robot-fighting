using YG;

namespace UI.Guide
{
    public class CustomizationGuide : TraderGuide
    {
        private void OnDestroy()
        {
            YG2.saves.IsGuidePassed = true;
            YG2.SaveProgress();
        }
    }
}