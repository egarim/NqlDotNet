using DevExpress.DataAccess.Sql;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafSmartEditors.Razor.NqlDotNet;

namespace XafAsyncActions.Module.Controllers
{
    public class MyViewController : ViewController
    {
        SimpleAction AsyncAction;
        public MyViewController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            AsyncAction = new SimpleAction(this, "Async Action", "View");
            AsyncAction.Execute += AsyncAction_Execute;

        }
        public async Task<string> Task1()
        {

            await Task.Delay(1000);
            return "Result from Task 1:" + Guid.NewGuid().ToString();
        }

        public async Task<string> Task2()
        {
            await Task.Delay(2000);
            return "Result from Task 2:" + Guid.NewGuid().ToString();
        }
        protected virtual void ProcessingDone(Dictionary<int, object> results)
        {
        }
        protected virtual void AsyncAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {


            var tasks = new List<Func<Task<Object>>>
            {
                async () => { return await Task1(); },
                async () => { return await Task2(); },
                async () => { return await Task1(); },
                async () => { return await Task2(); },
                async () => { return await Task2(); },
                async () => { return await Task2(); },
            };




            Action<int, string, Object> onProgressChanged = (progress, status, result) =>
            {
                Debug.WriteLine($"{status} - Result: {result}");
            };

            var worker = new AsyncBackgroundWorker<Object>(
                tasks,
                onProgressChanged,
                result => ProcessingDone(result)
            );

            worker.Start();

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }
}
