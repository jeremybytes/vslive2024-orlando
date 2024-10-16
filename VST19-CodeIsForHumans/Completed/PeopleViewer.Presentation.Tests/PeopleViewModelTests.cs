using Moq;
using PeopleViewer.Common;

namespace PeopleViewer.Presentation.Tests;

public partial class PeopleViewModelTests
{
    [SetUp]
    public void TestSetUp()
    {
        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
    }

    #region TodaysWinners Initialization

    [Test]
    public void PeopleViewModel_OnInitialization_TodaysWinnersIsPopulated()
    {
        // Arrange
        var viewModel = GetStandardViewModel();

        // Act
        viewModel.Initialize();

        // Assert
        Assert.That(viewModel.TodaysWinners, Is.Not.Null);
    }

    [Test]
    public void PeopleViewModel_OnInitializationAndTodaysOrdersMissing_ThrowsException()
    {
        // Arrange
        var dataReader = GetFakeDataReader();
        var viewModel = GetCustomViewModel(dataReader, null);

        // Act / Assert
        try
        {
            viewModel.Initialize();
            Assert.Fail("No Exception thrown when TodaysWinners is missing");
        }
        catch (MissingFieldException)
        {
            Assert.Pass();
        }
        catch (Exception)
        {
            Assert.Fail("Wrong exception thrown: expecting MissingFieldException");
        }
    }

    #endregion

    #region DataReader Initialization

    [Test]
    public void PeopleViewModel_OnInitialization_DataReaderIsPopulated()
    {
        var viewModel = GetStandardViewModel();
        viewModel.Initialize();
        Assert.That(viewModel.DataReader, Is.Not.Null);
    }

    [Test]
    public void PeopleViewModel_OnInitializationAndDataReaderMissing_ThrowsException()
    {
        var winners = GetFakeWinners();
        var viewModel = GetCustomViewModel(null, winners);
        try
        {
            viewModel.Initialize();
            Assert.Fail("No Exception thrown when DataReader is missing");
        }
        catch (MissingFieldException)
        {
            Assert.Pass();
        }
        catch (Exception)
        {
            Assert.Fail("Wrong exception thrown: expecting MissingFieldException");
        }
    }

    #endregion

    #region DataReader Caching

    [Test]
    public void DataReader_OnRefreshAndCacheExpired_DataReaderIsCalledTwice()
    {
        var mockReader = GetMockDataReader();
        var winners = GetFakeWinners();
        var viewModel = GetCustomViewModel(mockReader.Object, winners);
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        viewModel.LastRefreshTime = DateTime.Now.AddHours(-1);
        tracker.Reset();
        viewModel.RefreshPeople();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        mockReader.Verify(s => s.GetPeople(), Times.Exactly(2));
    }

    [Test]
    public void DataReader_OnRefreshAndCacheNotExpired_DataReaderIsCalledOnce()
    {
        var mockReader = GetMockDataReader();
        var winners = GetFakeWinners();
        var viewModel = GetCustomViewModel(mockReader.Object, winners);
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        tracker.Reset();
        viewModel.RefreshPeople();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        mockReader.Verify(s => s.GetPeople(), Times.Once);
    }

    #endregion

    #region DataReader Exceptions

    [Test]
    public void PeopleViewModel_OnInitializationWithDataReaderException_ThrowsExceptionOnCurrentThread()
    {
        var dataReader = GetExceptionDataReader();
        var winners = GetFakeWinners();
        var viewModel = GetCustomViewModel(dataReader, winners);

        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        Assert.That(viewModel.ViewModelException, Is.Not.Null);
    }

    [Test]
    public void PeopleViewModel_OnInitializationWithNoDataReaderException_NoExceptionThrown()
    {
        var dataReader = GetFakeDataReader();
        var winners = GetFakeWinners();
        var viewModel = GetCustomViewModel(dataReader, winners);

        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        Assert.That(viewModel.ViewModelException, Is.Null);
    }

    #endregion

    #region Filters

    [Test]
    public void People_FilterIncludes70s_70sRecordIsIncluded()
    {
        var viewModel = GetStandardViewModel();
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);
        Assert.That(viewModel.People.Contains(test70sPerson), Is.True,
            "Invalid Arrangement: Test Person is not in People");

        viewModel.Include70s = true;

        Assert.That(viewModel.People.Contains(test70sPerson), Is.True);
    }

    [Test]
    public void People_FilterDoesNotInclude70s_70sRecordIsNotIncluded()
    {
        var viewModel = GetStandardViewModel();
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);
        Assert.That(viewModel.People.Contains(test70sPerson), Is.True,
            "Invalid Arrangement: Test Person is not in People");

        viewModel.Include70s = false;

        Assert.That(viewModel.People.Contains(test70sPerson), Is.False);
    }

    [Test]
    public void People_FilterIncludes00s_00sRecordIsIncluded()
    {
        var viewModel = GetStandardViewModel();
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);
        Assert.That(viewModel.People.Contains(test00sPerson), Is.True,
            "Invalid Arrangement: Test Person is not in People");

        viewModel.Include00s = true;

        Assert.That(viewModel.People.Contains(test00sPerson), Is.True);
    }

    [Test]
    public void People_FilterDoesNotInclude00s_00sRecordIsNotIncluded()
    {
        var viewModel = GetStandardViewModel();
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);
        Assert.That(viewModel.People.Contains(test00sPerson), Is.True,
            "Invalid Arrangement: Test Person is not in People");

        viewModel.Include00s = false;

        Assert.That(viewModel.People.Contains(test00sPerson), Is.False);
    }

    #endregion

    #region Filter Reset

    [Test]
    public void Filters_OnRefreshAndCacheExpired_AreResetToDefaults()
    {
        var mockReader = GetMockDataReader();
        var winners = GetFakeWinners();
        var viewModel = GetCustomViewModel(mockReader.Object, winners);
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Include70s = false;
        viewModel.Include80s = false;
        viewModel.Include90s = false;
        viewModel.Include00s = false;
        viewModel.Include10s = false;
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        // Act
        viewModel.LastRefreshTime = DateTime.Now.AddHours(-1);
        tracker.Reset();
        viewModel.RefreshPeople();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);
        mockReader.Verify(s => s.GetPeople(), Times.Exactly(2), 
            "DataReader was not called twice");

        // Assert
        Assert.That(viewModel.Include70s, Is.True, "Include70s filter was not reset");
        Assert.That(viewModel.Include80s, Is.True, "Include80s filter was not reset");
        Assert.That(viewModel.Include90s, Is.True, "Include90s filter was not reset");
        Assert.That(viewModel.Include00s, Is.True, "Include00s filter was not reset");
        Assert.That(viewModel.Include10s, Is.True, "Include10s filter was not reset");
    }

    [Test]
    public void Filters_OnRefreshAndCacheNotExpired_AreResetToDefaults()
    {
        var mockReader = GetMockDataReader();
        var winners = GetFakeWinners();
        var viewModel = GetCustomViewModel(mockReader.Object, winners);
        var tracker = new PropertyChangeTracker(viewModel);
        viewModel.Include70s = false;
        viewModel.Include80s = false;
        viewModel.Include90s = false;
        viewModel.Include00s = false;
        viewModel.Include10s = false;
        viewModel.Initialize();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);

        // Act
        tracker.Reset();
        viewModel.RefreshPeople();
        tracker.WaitForChange(nameof(viewModel.LastRefreshTime), 1);
        mockReader.Verify(s => s.GetPeople(), Times.Once,
            "DataReader was not called once");

        // Assert
        Assert.That(viewModel.Include70s, Is.True, "Include70s filter was not reset");
        Assert.That(viewModel.Include80s, Is.True, "Include80s filter was not reset");
        Assert.That(viewModel.Include90s, Is.True, "Include90s filter was not reset");
        Assert.That(viewModel.Include00s, Is.True, "Include00s filter was not reset");
        Assert.That(viewModel.Include10s, Is.True, "Include10s filter was not reset");
    }

    #endregion

    #region Winners Item Selection

    [Test]
    public void WinnersSelectedPeople_AddToWinnersWithNewPerson_PersonAdded()
    {
        var viewModel = GetStandardViewModel();
        viewModel.Initialize();
        var newPerson = new Person(100, "Rogers", "Peter", 
            new DateTime(2013, 01, 01), 5);
        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.False, "Invalid Arrangement: Person already in list");

        viewModel.AddToWinners(newPerson);

        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.True, "New Person was not added to SelectedPeople");
    }

    [Test]
    public void WinnersSelectedPeople_AddToWinnersWithExistingPerson_PersonIsNotAdded()
    {
        var viewModel = GetStandardViewModel();
        viewModel.Initialize();
        var newPerson = new Person(100, "Rogers", "Peter",
            new DateTime(2013, 01, 01), 5);
        viewModel.AddToWinners(newPerson);
        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.True, "Invalid Arrangement: Could not add test person");
        var oldCount = viewModel.TodaysWinners.SelectedPeople.Count();

        viewModel.AddToWinners(newPerson);
        var newCount = viewModel.TodaysWinners.SelectedPeople.Count();

        Assert.That(newCount, Is.EqualTo(oldCount), "SelectedPeople count changed");
        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.True, "Expected Person is not in list");
    }

    [Test]
    public void WinnersSelectedPeople_RemoveFromWinnersWithExistingPerson_PersonIsRemoved()
    {
        var viewModel = GetStandardViewModel();
        viewModel.Initialize();
        var newPerson = new Person(100, "Rogers", "Peter",
            new DateTime(2013, 01, 01), 5);
        viewModel.AddToWinners(newPerson);
        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.True, "Invalid Arrangement: Could not add test person");
        var oldCount = viewModel.TodaysWinners.SelectedPeople.Count();

        viewModel.RemoveFromWinners(newPerson);
        var newCount = viewModel.TodaysWinners.SelectedPeople.Count();

        Assert.That(newCount, Is.LessThan(oldCount), "SelectedPeople count not changed");
        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.False, "Person was not removed");
    }

    [Test]
    public void WinnersSelectedPeople_RemoveFromWinnersWithNewPerson_SelectedPeopleIsUnchanged()
    {
        var viewModel = GetStandardViewModel();
        viewModel.Initialize();
        var newPerson = new Person(100, "Rogers", "Peter",
            new DateTime(2013, 01, 01), 5);
        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.False, "Invalid Arrangement: Test person is already in list");
        var oldCount = viewModel.TodaysWinners.SelectedPeople.Count();

        viewModel.RemoveFromWinners(newPerson);
        var newCount = viewModel.TodaysWinners.SelectedPeople.Count();

        Assert.That(newCount, Is.EqualTo(oldCount), "SelectedPeople count changed");
        Assert.That(viewModel.TodaysWinners.SelectedPeople.Contains(newPerson),
            Is.False, "Person found in SelectedPeople");
    }

    #endregion
}