using PeopleViewer.Common;
using PeopleViewer.Library;
using System.Windows;

namespace PeopleViewer;

public partial class PeopleViewerWindow : Window
{
    PersonDataReader reader = new();

    public PeopleViewerWindow()
    {
        InitializeComponent();
    }

    private async void FetchConcreteButton_Click(object sender, RoutedEventArgs e)
    {
        List<Person> people = await reader.GetPeople();
        foreach (var person in people)
            PersonListBox.Items.Add(person);

        ShowReaderType();
    }

    private async void FetchAbstractButton_Click(object sender, RoutedEventArgs e)
    {
        IEnumerable<Person> people = await reader.GetPeople();
        foreach (var person in people)
            PersonListBox.Items.Add(person);

        ShowReaderType();
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        PersonListBox.Items.Clear();
        ClearReaderType();
    }

    private void ShowReaderType()
    {
        ReaderTypeTextBlock.Text = reader.GetType().ToString();
    }

    private void ClearReaderType()
    {
        ReaderTypeTextBlock.Text = string.Empty;
    }

}
