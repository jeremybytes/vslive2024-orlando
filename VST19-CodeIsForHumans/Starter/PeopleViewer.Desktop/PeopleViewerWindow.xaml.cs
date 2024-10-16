using PeopleViewer.Common;
using PeopleViewer.Presentation;
using System.Windows;

namespace PeopleViewer;

public partial class PeopleViewerWindow : Window
{
    PeopleViewModel viewModel { get; }

    public PeopleViewerWindow(PeopleViewModel peopleViewModel)
    {
        InitializeComponent();
        viewModel = peopleViewModel;
        ConfigureExceptionHandling();
        Loaded += (o, e) =>
        {
            viewModel.Initialize();
            this.DataContext = viewModel;
        };
    }

    private void ConfigureExceptionHandling()
    {
        viewModel.PropertyChanged += (o, e) =>
        {
            if (e.PropertyName == nameof(viewModel.ViewModelException))
            {
                Action<Exception> rtxDel = (ex) => { throw ex; };
                Dispatcher.Invoke(rtxDel, viewModel.ViewModelException);
            }
        };
    }

    private void RefreshPeopleButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.RefreshPeople();
    }

    private void SelectedPersonListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        viewModel.RemoveFromWinners(SelectedPersonListBox.SelectedItem as Person);
    }

    private void ClearSelectionButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.ClearWinners();
    }

    private void PersonListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        viewModel.AddToWinners(PersonListBox.SelectedItem as Person);
    }

    private void DoneButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Saved");
    }
}
