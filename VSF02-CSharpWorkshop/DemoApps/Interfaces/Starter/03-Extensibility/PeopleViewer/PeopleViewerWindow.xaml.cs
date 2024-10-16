using PersonReader.Interface;
using System.Windows;

namespace PeopleViewer;

public partial class PeopleViewerWindow : Window
{
    public PeopleViewerWindow()
    {
        InitializeComponent();
    }

    private async void ServiceButton_Click(object sender, RoutedEventArgs e)
    {
        await Task.Delay(1);
    }

    private async void CSVButton_Click(object sender, RoutedEventArgs e)
    {
        await Task.Delay(1);
    }

    private async void SQLButton_Click(object sender, RoutedEventArgs e)
    {
        await Task.Delay(1);
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        PersonListBox.Items.Clear();
        ClearReaderType();
    }

    private void ShowReaderType(IPersonReader reader)
    {
        ReaderTypeTextBlock.Text = reader.GetType().ToString();
    }

    private void ClearReaderType()
    {
        ReaderTypeTextBlock.Text = string.Empty;
    }
}
