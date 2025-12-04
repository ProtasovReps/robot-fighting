using YG;

namespace UI.Guide
{
    public class CustomizationGuide : TraderGuide
    {
        private const string GuidePassed = nameof(GuidePassed);
        
        private void OnDestroy()
        {
            if (YG2.saves.IsGuidePassed == false)
            {
                YG2.MetricaSend(GuidePassed);
                YG2.saves.IsGuidePassed = true;
                YG2.SaveProgress();
            }
        }
    }
}