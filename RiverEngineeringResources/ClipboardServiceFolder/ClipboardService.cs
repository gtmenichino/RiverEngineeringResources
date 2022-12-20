using Microsoft.JSInterop;

namespace RiverEngineeringResources.ClipboardServiceFolder
{
    public class ClipboardService
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly IJSRuntime _jsRuntime;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipboardService"/> class.
        /// </summary>
        /// <param name="jsRuntime">The js runtime.</param>
        public ClipboardService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public ValueTask WriteTextAsync(string text)
        {
            return _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}
