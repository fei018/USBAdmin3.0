#pragma checksum "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "535416101933008127189370ab76eca4a48a5182"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_USB_RequestDetail), @"mvc.1.0.view", @"/Views/USB/RequestDetail.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\_ViewImports.cshtml"
using USBAdminWebMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\_ViewImports.cshtml"
using USBModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"535416101933008127189370ab76eca4a48a5182", @"/Views/USB/RequestDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4594b8874090f6e413bd2d1f83140d4f6dde779b", @"/Views/_ViewImports.cshtml")]
    public class Views_USB_RequestDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UsbRequestVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("approveForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/USB/RequestToApprove"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("layui-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("rejectForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/USB/RequestToReject"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("deleteForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/USB/RequestToDelete"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/custom.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<style>
    div.usb-detail span{
        color:red;
    }
</style>

<div class=""layui-row layui-col-space15"">

    <div class=""layui-col-md6"">

        <div class=""layui-panel"">

            <div style=""padding: 30px;"" class=""usb-detail"">
                <div>USB Detial: ( current state: <span>");
#nullable restore
#line 16 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                                   Write(Model.RequestState);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> )</div>\r\n                <hr />\r\n                <div>Manufacturer: <span>");
#nullable restore
#line 18 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                    Write(Model.Manufacturer);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Product: <span>");
#nullable restore
#line 19 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                               Write(Model.Product);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Description: <span>");
#nullable restore
#line 20 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                   Write(Model.DeviceDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Vid: <span>");
#nullable restore
#line 21 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                           Write(Model.Vid);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Pid: <span>");
#nullable restore
#line 22 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                           Write(Model.Pid);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>SerialNumber: <span>");
#nullable restore
#line 23 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                    Write(Model.SerialNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Request State: <span>");
#nullable restore
#line 24 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                     Write(Model.RequestState);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>State Change Time: <span>");
#nullable restore
#line 25 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                         Write(Model.RequestStateChangeTimeString);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Request State Change By: <span>");
#nullable restore
#line 26 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                               Write(Model.RequestStateChangeBy);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Request Computer Name: <span>");
#nullable restore
#line 27 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                             Write(Model.ComputerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Request IP: <span>");
#nullable restore
#line 28 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                  Write(Model.IP);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Request User Email: <span>");
#nullable restore
#line 29 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                          Write(Model.RequestUserEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <hr />\r\n                <div>Request Time: <span>");
#nullable restore
#line 31 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                    Write(Model.RequestTimeString);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div><br />\r\n                <div>Request Reason:</div><br />\r\n                <div style=\"color:red\">");
#nullable restore
#line 33 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                  Write(Model.RequestReason);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div><br />\r\n\r\n");
#nullable restore
#line 35 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                 if (Model.RequestState == UsbRequestStateType.Reject)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <hr />\r\n                    <div>Reject Reason:</div>\r\n                    <textarea readonly class=\"layui-textarea\" style=\"width:400px;\">");
#nullable restore
#line 39 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                                                                              Write(Model.RejectReason);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea>\r\n");
#nullable restore
#line 40 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n    <hr>\r\n    <div>\r\n");
#nullable restore
#line 49 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
         if (Model.RequestState == UsbRequestStateType.UnderReview)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "535416101933008127189370ab76eca4a48a518213023", async() => {
                WriteLiteral("\r\n                <input type=\"hidden\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 2207, "\"", 2224, 1);
#nullable restore
#line 52 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
WriteAttributeValue("", 2215, Model.Id, 2215, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />

                <div class=""layui-form-item"">
                    <div class=""layui-input-block"">
                        <button type=""button"" class=""layui-btn layui-btn-primary"" onclick=""submitForm('#approveForm', 'Approve ?')"">Approve</button>
                    </div>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("            <br />\r\n            <hr />\r\n");
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "535416101933008127189370ab76eca4a48a518215864", async() => {
                WriteLiteral("\r\n                <input type=\"hidden\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 2745, "\"", 2762, 1);
#nullable restore
#line 65 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
WriteAttributeValue("", 2753, Model.Id, 2753, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />

                <div class=""layui-form-item layui-form-text"">
                    <label class=""layui-form-label"">Reject</label>
                    <div class=""layui-input-block"">
                        <textarea name=""RejectReason"" placeholder=""Reject reason"" class=""layui-textarea""></textarea>
                    </div>
                </div>

                <div class=""layui-form-item"">
                    <div class=""layui-input-block"">
                        <button type=""button"" class=""layui-btn layui-btn-primary"" onclick=""submitForm('#rejectForm', 'Reject ?')"">Reject</button>
                    </div>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("            <br />\r\n            <hr />\r\n");
#nullable restore
#line 83 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "535416101933008127189370ab76eca4a48a518219240", async() => {
                WriteLiteral("\r\n            <input type=\"hidden\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 3639, "\"", 3656, 1);
#nullable restore
#line 86 "C:\CodeRepos\USBAdmin2.0\Server\USBAdminWebMVC\Views\USB\RequestDetail.cshtml"
WriteAttributeValue("", 3647, Model.Id, 3647, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />

            <div class=""layui-form-item"">
                <div class=""layui-input-block"">
                    <button type=""button"" class=""layui-btn layui-btn-danger"" onclick=""submitForm('#deleteForm', 'Delete ?')"">Delete</button>
                </div>
            </div>
        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Script", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "535416101933008127189370ab76eca4a48a518222060", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <script>
        layui.use(['form', 'layer'], function () {
            var form = layui.form,
                layer = layui.layer;
        });

        function submitForm(id, msg) {

            layer.confirm(msg, { icon: 3, title: 'Confirm' }, function (index) {

                let url = $(id).attr('action');
                let data = $(id).serializeArray();

                $.post(url, data, function (text) {
                    //loadingToReloadPage(2);
                    document.body.innerHTML = text;
                });

                layer.close(index);
            });
        }
    </script>
");
            }
            );
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UsbRequestVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
