using Ninject;
using PeopleViewer.Common;
using PersonDataReader.Service;
using System.Windows;
using System.Windows.Threading;

namespace PeopleViewer.Desktop.Ninject;

public partial class App : Application
{
    IKernel Container = new StandardKernel();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DispatcherUnhandledException += App_DispatcherUnhandledException;
        ConfigureContainer();
        Application.Current.MainWindow = Container.Get<PeopleViewerWindow>();
        Application.Current.MainWindow.Show();
    }

    private void ConfigureContainer()
    {
        Container.Bind<IPersonReader>().To<ServiceReader>()
            .InSingletonScope();

        Winners todaysWinners = new(DateTime.Today);
        Container.Bind<Winners>().ToConstant(todaysWinners)
            .Named("TodaysWinners");
    }

    void App_DispatcherUnhandledException(object sender,
    DispatcherUnhandledExceptionEventArgs e)
    {
        // This is where we catch any unhandled exceptions.
        // Log them to the system, then provide a generic message to the user.
        try
        {
            // Log.AddException(e.Exception.Message);
            e.Handled = true;
            MessageBox.Show("Something bad happened. Please contact the Help Desk for more information.");
            Application.Current.Shutdown();
        }
        catch
        {
            // If we get an exception in our unhandled exception handler, there's
            // not much we can do.
        }
    }
}
