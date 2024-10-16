using Ninject;
using PeopleViewer.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeopleViewer.Presentation;

public class PeopleViewModel : INotifyPropertyChanged
{
    private IKernel _container;
    private IPersonReader _dataReader;
    public IPersonReader DataReader => _dataReader;

    private Winners _todaysWinners;
    public Winners TodaysWinners
    {
        get => _todaysWinners;
        set { _todaysWinners = value; RaisePropertyChanged(); }
    }

    private DateTime _lastRefreshTime;
    public DateTime LastRefreshTime
    {
        get => _lastRefreshTime;
        set { _lastRefreshTime = value; RaisePropertyChanged(); }
    }

    #region Filters

    private bool _include70s;
    public bool Include70s
    {
        get => _include70s;
        set { _include70s = value; RefreshFilter(); }
    }

    private bool _include80s;
    public bool Include80s
    {
        get => _include80s;
        set { _include80s = value; RefreshFilter(); }
    }

    private bool _include90s;
    public bool Include90s
    {
        get => _include90s;
        set { _include90s = value; RefreshFilter(); }
    }

    private bool _include00s;
    public bool Include00s
    {
        get => _include00s;
        set { _include00s = value; RefreshFilter(); }
    }

    private bool _include10s;
    public bool Include10s
    {
        get => _include10s;
        set { _include10s = value; RefreshFilter(); }
    }

    #endregion

    private IEnumerable<Person> _fullPeopleList = [];
    private IEnumerable<Person> _people = [];
    public IEnumerable<Person> People
    {
        get => _people;
        set { _people = value; RaisePropertyChanged(); }
    }

    private Exception? _viewModelException;
    public Exception? ViewModelException
    {
        get => _viewModelException;
        set { _viewModelException = value; RaisePropertyChanged(); }
    }

    public PeopleViewModel(IKernel container)
    {
        _container = container;
    }

    public void Initialize()
    {
        _dataReader = GetDataReaderFromContainer();
        _todaysWinners = GetWinnersFromContainer();
        RefreshPeople();
    }

    public void RefreshPeople()
    {
        if (IsCacheValid)
        {
            ResetFiltersToDefaults();
        }
        else
        {
            RefreshPeopleFromDataReader();
        }
    }

    private Winners GetWinnersFromContainer()
    {
        if (!_container.CanResolve<Winners>("TodaysWinners"))
            throw new MissingFieldException(
                "TodaysWinners is not available from the DI Container");
        return _container.Get<Winners>("TodaysWinners");
    }

    private IPersonReader GetDataReaderFromContainer()
    {
        if (!_container.CanResolve<IPersonReader>())
            throw new MissingFieldException(
                "IPersonReader is not available from the DI Container");
        return _container.Get<IPersonReader>();
    }

    private void RefreshPeopleFromDataReader()
    {
        People = [];
        Task<IReadOnlyCollection<Person>> peopleTask = DataReader.GetPeople();
        peopleTask.ContinueWith(task =>
        {
            _fullPeopleList = task.Result;
            ResetFiltersToDefaults();
            LastRefreshTime = DateTime.Now;
        }, TaskContinuationOptions.NotOnFaulted);

        HandleDataReaderException(peopleTask);
    }

    private void HandleDataReaderException(Task<IReadOnlyCollection<Person>> peopleTask)
    {
        peopleTask.ContinueWith(task =>
        {
            ViewModelException =
                task.Exception!.Flatten().InnerExceptions.First();
        }, TaskContinuationOptions.OnlyOnFaulted);
    }

    private bool IsCacheValid => DateTime.Now - LastRefreshTime < TimeSpan.FromSeconds(10);

    private void ResetFiltersToDefaults()
    {
        _include70s = true;
        _include80s = true;
        _include90s = true;
        _include00s = true;
        _include10s = true;
        RefreshFilter();
    }

    private void RefreshFilter()
    {
        RaisePropertyChanged([nameof(Include70s), nameof(Include80s),
            nameof(Include90s), nameof(Include00s), nameof(Include10s)]);

        IEnumerable<Person> people = _fullPeopleList;
        if (!Include70s)
            people = people.Where(p => p.Decade != 1970);
        if (!Include80s)
            people = people.Where(p => p.Decade != 1980);
        if (!Include90s)
            people = people.Where(p => p.Decade != 1990);
        if (!Include00s)
            people = people.Where(p => p.Decade != 2000);
        if (!Include10s)
            people = people.Where(p => p.Decade != 2010);

        People = people.ToList();
    }

    public void AddToWinners(Person? person)
    {
        if (person is null) return;

        if (!TodaysWinners.SelectedPeople.Contains(person))
            TodaysWinners.SelectedPeople.Add(person);
    }

    public void RemoveFromWinners(Person? person)
    {
        if (person is null) return;
        TodaysWinners.SelectedPeople.Remove(person);
    }

    public void ClearWinners()
    {
        TodaysWinners.SelectedPeople.Clear();
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler? PropertyChanged;
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void RaisePropertyChanged(string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}