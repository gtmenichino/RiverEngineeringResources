using Microsoft.AspNetCore.Components;

namespace RiverEngineeringResources
{
    public class SharedData
    {
        public bool ResourcesPageSelected { get; private set; }

        public event Action OnChange;

        public void SetResourcesPageSelected(bool val)
        {
            ResourcesPageSelected = val;
            NotifyStateChanged();
        }

        //[Parameter]
        //public EventCallback<bool> OnGetStartedClicked { get; set; }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
