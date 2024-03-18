using Microsoft.AspNetCore.Components;

namespace RiverEngineeringResources
{
    public class SharedData
    {
        public bool EngineeringPageSelected { get; private set; }
        public bool Resources2PageSelected { get; private set; }
        public bool AssessmentPageSelected { get; private set; }

        public event Action OnChange;

        public void SetMainPageSelected()
        {
            EngineeringPageSelected = true;
            Resources2PageSelected = false;
            AssessmentPageSelected = false;
            NotifyStateChanged();
        }

        public void SetEngineeringPageSelected()
        {
            Resources2PageSelected = true;
                    EngineeringPageSelected = false;
            AssessmentPageSelected = false;
            NotifyStateChanged();
        }

        public void SetAssessmentPageSelected()
        {
            Resources2PageSelected = false;
            EngineeringPageSelected = false;
            AssessmentPageSelected = true;
            NotifyStateChanged();
        }

        public void SetHomePageSelected()
        {
            EngineeringPageSelected = false;
            Resources2PageSelected = false;
            AssessmentPageSelected = false;
            NotifyStateChanged();
        }

        //[Parameter]
        //public EventCallback<bool> OnGetStartedClicked { get; set; }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
