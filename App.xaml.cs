using Microsoft.Maui.Controls;
using System;
using blossom.Utilities;

namespace blossom
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                FileLogger.Log("App constructor started");
                InitializeComponent();

                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

                MainPage = new NavigationPage(new StartupPage());
                FileLogger.Log("App constructor completed");
            }
            catch (Exception ex)
            {
                FileLogger.LogException("App Constructor", ex);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            FileLogger.LogException("Unhandled Exception", exception);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            FileLogger.LogException("Unobserved Task Exception", e.Exception);
            e.SetObserved();
        }

        protected override void OnStart()
        {
            FileLogger.Log("Application started");
        }

        protected override void OnSleep()
        {
            FileLogger.Log("Application went to sleep");
            FileLogger.FlushLogs();
        }

        protected override void OnResume()
        {
            FileLogger.Log("Application resumed");
        }
    }
}