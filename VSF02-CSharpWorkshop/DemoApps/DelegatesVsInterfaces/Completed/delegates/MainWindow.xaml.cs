using System.Windows;
using System.Windows.Controls;

namespace delegates;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        PersonListBox.ItemsSource = People.GetPeople();
    }

    private IPersonFormatter GetFormatter()
    {
        RadioButton? checkedValue = GetCheckedStringHandlingButton();

        return checkedValue?.Name switch
        {
            nameof(DefaultStringButton) => new DefaultFormatter(),
            nameof(FamilyNameStringButton) => new FamilyNameFormatter(),
            nameof(GivenNameStringButton) => new GivenNameFormatter(),
            nameof(FullNameStringButton) => new FullNameFormatter(),
            _ => new DefaultFormatter(),
        };
    }

    private RadioButton? GetCheckedStringHandlingButton()
    {
        return StringHandlingPanel.Children
                 .OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue
                                   && r.IsChecked.Value);
    }

    private Action<List<Person>> GetAction()
    {
        Action<List<Person>> action = p => { };

        if (BestCommanderCheckBox.IsChecked!.Value)
            action += p => MessageBox.Show(
                p.MaxBy(r => r.Rating)!.ToString());

        if (AverageRatingCheckBox.IsChecked!.Value)
            action += p => AddToList(
                p.Average(r => r.Rating).ToString("#.#"));

        if (EarliestStartDateCheckBox.IsChecked!.Value)
            action += p => AddToList(
                p.Min(s => s.StartDate).ToString("d"));

        if (FirstLettersCheckBox.IsChecked!.Value)
            action += p =>
            {
                string output = "";
                p.ForEach(c => output += c.FamilyName[0]);
                AddToList(output);
            };

        return action;
    }

    private void ProcessDataButton_Click(object sender, RoutedEventArgs e)
    {
        OutputList.Items.Clear();

        var people = People.GetPeople();

        if (StringExpander.IsExpanded)
        {
            IPersonFormatter formatter = GetFormatter();
            foreach (var person in people)
                AddToList(person.ToString(formatter));
        }
        if (ActionExpander.IsExpanded)
        {
            // DO NOT DO THIS
            // unless you really hate your co-workers
            GetAction()(people);
        }
    }

    private void AddToList(string item)
    {
        OutputList.Items.Add(item);
    }
}
