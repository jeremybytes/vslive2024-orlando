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
        await FetchData("Service");
    }

    private async void CSVButton_Click(object sender, RoutedEventArgs e)
    {
        await FetchData("CSV");
    }

    private async void SQLButton_Click(object sender, RoutedEventArgs e)
    {
        await FetchData("SQL");
    }

    private async Task FetchData(string readerType)
    {
        IPersonReader reader = ReaderFactory.GetReader(readerType);

        var people = await reader.GetPeople();
        foreach (var person in people)
            PersonListBox.Items.Add(person);

        ShowReaderType(reader);
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
