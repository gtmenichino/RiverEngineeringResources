using Microsoft.AspNetCore.Components;

namespace RiverEngineeringResources
{
    public class SharedData
    {
        public bool ResourcesPageSelected { get; private set; }
        public bool Resources2PageSelected { get; private set; }
        public bool SubmitPageSelected { get; private set; }

        public event Action OnChange;

        public void SetResourcesPageSelected()
        {
            ResourcesPageSelected = true;
            Resources2PageSelected = false;
            SubmitPageSelected = false;
            NotifyStateChanged();
        }

        public void SetResources2PageSelected()
        {
            Resources2PageSelected = true;
                    ResourcesPageSelected = false;
            SubmitPageSelected = false;
            NotifyStateChanged();
        }

        public void SetSubmitPageSelected()
        {
            Resources2PageSelected = false;
            ResourcesPageSelected = false;
            SubmitPageSelected = true;
            NotifyStateChanged();
        }

        public void SetHomePageSelected()
        {
            ResourcesPageSelected = false;
            Resources2PageSelected = false;
            SubmitPageSelected = false;
            NotifyStateChanged();
        }

        //[Parameter]
        //public EventCallback<bool> OnGetStartedClicked { get; set; }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
