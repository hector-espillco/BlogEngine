#pragma checksum "C:\Personal\BlogEngine\BlogEngine.UI\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "86097f78c4817bb26b355afdf9b5780b6bd8711b"
// <auto-generated/>
#pragma warning disable 1591
namespace BlogEngine.UI.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using System.Net;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using BlogEngine.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using BlogEngine.UI.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using BlogEngine.UI.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Personal\BlogEngine\BlogEngine.UI\_Imports.razor"
using BlogEngine.Shared.Enums;

#line default
#line hidden
#nullable disable
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "col-12 row background-size background-color-login");
            __builder.AddMarkupContent(2, "<div class=\"col-2\"></div>\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "col-8");
            __builder.AddContent(5, 
#nullable restore
#line 7 "C:\Personal\BlogEngine\BlogEngine.UI\Shared\MainLayout.razor"
         Body

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(6, "\r\n    <div class=\"col-2\"></div>");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
    }
}
#pragma warning restore 1591