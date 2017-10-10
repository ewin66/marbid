using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using System.Collections.Generic;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;

namespace Marbid.Win
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWinWinApplicationMembersTopicAll.aspx
    public partial class MarbidWindowsFormsApplication : WinApplication
    {
        public MarbidWindowsFormsApplication()
        {
            InitializeComponent();
            LinkNewObjectToParentImmediately = true;
        }
        protected override void OnLoggedOn(LogonEventArgs args)
        {
            base.OnLoggedOn(args);
            DevExpress.ExpressApp.ReportsV2.Win.ReportDesignerTypeDescriptionProvider.RemoveProvider();
        }
        //protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        //{
        //  //XPObjectSpaceProvider provider = new XPObjectSpaceProvider((SecurityStrategyComplex)Security, args.ConnectionString, args.Connection, false);
        //  ////provider.AllowICommandChannelDoWithSecurityContext = true;
        //  //args.ObjectSpaceProviders.Add(provider);
        //  //args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        //}
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {

            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection, true);
        }
        private void MarbidWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e)
        {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1)
            {
                e.Languages.Add(userLanguageName);
            }
        }
        private void MarbidWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if (System.Diagnostics.Debugger.IsAttached)
            {
                e.Updater.Update();
                e.Handled = true;
            }
            else
            {
                string message = "The application cannot connect to the specified database, " +
                  "because the database doesn't exist, its version is older " +
                  "than that of the application or its schema does not match " +
                  "the ORM data model structure. To avoid this error, use one " +
                  "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
                {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
    }
}
