using PersonReader.Interface;
using System.Windows;

namespace PeopleViewer;

public partial class PeopleViewerWindow : Window
{
    public PeopleViewerWindow()
    {
        InitializeComponent();
    }

    private async void FetchButton_Click(object sender, RoutedEventArgs e)
    {
        IPersonReader reader = ReaderFactory.GetReader();

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
