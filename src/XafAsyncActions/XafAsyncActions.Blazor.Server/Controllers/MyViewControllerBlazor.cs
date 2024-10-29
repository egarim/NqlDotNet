using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Utils;
using Microsoft.JSInterop;
using XafAsyncActions.Module.Controllers;

namespace XafAsyncActions.Blazor.Server.Controllers
{
    //HACK https://supportcenter.devexpress.com/ticket/details/t1080389/notify-progress-of-action-execution-changing-the-loading-progress-icon
    public class MyViewControllerBlazor:MyViewController
    {

        IJSRuntime jsRuntime;
        IServiceProvider serviceProvider;
        ILoadingIndicatorProvider loading;
        protected override void OnActivated()
        {
            base.OnActivated();
            IServiceProvider serviceProvider = ((BlazorApplication)Application).ServiceProvider;
            jsRuntime = (IJSRuntime)serviceProvider.GetService(typeof(IJSRuntime));
            loading = serviceProvider.GetService(typeof(ILoadingIndicatorProvider)) as ILoadingIndicatorProvider;

          
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();

        }
        protected async override void AsyncAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {


            loading.Hold("Loading");
            base.AsyncAction_Execute(sender, e);
        

        }
        protected async override void ProcessingDone(Dictionary<int, object> results)
        {
            base.ProcessingDone(results);
            loading.Release("Loading");
           
        }
    }
}
