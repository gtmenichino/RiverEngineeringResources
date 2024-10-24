using Microsoft.AspNetCore.Components;
using RiverEngineeringResources.Shared;

namespace RiverEngineeringResources
{
    public class SharedData
    {
        public List<MyEngineeringResource>? MyEngineeringResourceList = new List<MyEngineeringResource>();
        public string csvDocumentsContent;
        public string csvDataToolsContent;
        public string csvEngineeringErrorMessage;

        public List<MyAssessmentResource>? MyAssessmentResourceList = new List<MyAssessmentResource>();
        public string csvAssessmentContent;
        public string assessmentErrorMessage;

        public bool EngineeringPageSelected { get; private set; }
        public bool Resources2PageSelected { get; private set; }
        public bool AssessmentPageSelected { get; private set; }
        public bool STAFPageSelected { get; private set; }

        public event Action OnChange;

        public void SetMainPageSelected()
        {
            EngineeringPageSelected = true;
            Resources2PageSelected = false;
            AssessmentPageSelected = false;
            STAFPageSelected = false;
            NotifyStateChanged();
        }

        public void SetEngineeringPageSelected()
        {
            Resources2PageSelected = true;
                    EngineeringPageSelected = false;
            AssessmentPageSelected = false;
            STAFPageSelected = false;
            NotifyStateChanged();
        }

        public void SetAssessmentPageSelected()
        {
            Resources2PageSelected = false;
            EngineeringPageSelected = false;
            AssessmentPageSelected = true;
            STAFPageSelected = false;
            NotifyStateChanged();
        }

        public void SetSTAFPageSelected()
        {
            Resources2PageSelected = false;
            EngineeringPageSelected = false;
            AssessmentPageSelected = false;
            STAFPageSelected = true;
            NotifyStateChanged();
        }

        public void SetHomePageSelected()
        {
            EngineeringPageSelected = false;
            Resources2PageSelected = false;
            AssessmentPageSelected = false;
            STAFPageSelected = false;
            NotifyStateChanged();
        }

        //[Parameter]
        //public EventCallback<bool> OnGetStartedClicked { get; set; }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
