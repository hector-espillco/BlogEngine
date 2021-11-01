using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.UI.Pages
{
    public class BasePage : ComponentBase
    {
        [Inject]
        public ILogger<BasePage> Logger { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
    }
}
