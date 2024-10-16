using System.Collections.ObjectModel;

namespace PeopleViewer.Common;

public class Winners
{
    public DateOnly Date {get;set;}
    public ObservableCollection<Person> SelectedPeople { get; set; }

    public Winners(DateTime date)
    {
        Date = DateOnly.FromDateTime(date);
        SelectedPeople = [];
    }
}
