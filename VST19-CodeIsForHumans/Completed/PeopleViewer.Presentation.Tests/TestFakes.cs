using Moq;
using Ninject;
using PeopleViewer.Common;

namespace PeopleViewer.Presentation.Tests;

public partial class PeopleViewModelTests
{
    static Person test00sPerson = new Person(1, "John", "Smith", new DateTime(2000, 10, 1), 7);
    static Person test70sPerson = new Person(2, "Mary", "Thomas", new DateTime(1971, 7, 23), 9);

    private class FakeReader : IPersonReader
    {
        private List<Person> testData = [test00sPerson, test70sPerson];

        public async Task<IReadOnlyCollection<Person>> GetPeople()
        {
            await Task.Delay(1);
            return testData;
        }

        public async Task<Person?> GetPerson(int id)
        {
            await Task.Delay(1);
            return testData.First(p => p.Id == id);
        }
    }

    private class ExceptionDataReader : IPersonReader
    {
        public async Task<IReadOnlyCollection<Person>> GetPeople()
        {
            await Task.Delay(1);
            throw new InvalidOperationException();
        }

        public async Task<Person?> GetPerson(int id)
        {
            await Task.Delay(1);
            throw new InvalidOperationException();
        }
    }

    private PeopleViewModel GetStandardViewModel()
    {
        var container = GetPopulatedContainer();
        var viewModel = new PeopleViewModel(container);
        return viewModel;
    }

    private PeopleViewModel GetCustomViewModel(IPersonReader? dataReader, Winners? todaysWinners)
    {
        var container = GetPopulatedContainer(dataReader, todaysWinners);
        var viewModel = new PeopleViewModel(container);
        return viewModel;
    }

    private IKernel GetPopulatedContainer()
    {
        IPersonReader dataReaderMock = GetFakeDataReader();
        Winners todaysWinners = GetFakeWinners();
        IKernel container = GetPopulatedContainer(dataReaderMock, todaysWinners);
        return container;
    }

    private IKernel GetPopulatedContainer(IPersonReader? dataReader, Winners? todaysWinners)
    {
        IKernel container = new StandardKernel();
        if (dataReader is not null)
            container.Bind<IPersonReader>().ToConstant(dataReader);
        if (todaysWinners is not null)
            container.Bind<Winners>().ToConstant(todaysWinners)
                .Named("TodaysWinners");
        return container;
    }

    private IPersonReader GetFakeDataReader()
    {
        return new FakeReader();
    }

    private IPersonReader GetExceptionDataReader()
    {
        return new ExceptionDataReader();
    }

    private Mock<IPersonReader> GetMockDataReader()
    {
        Mock<IPersonReader> mockReader = new();
        IPersonReader fakeReader = new FakeReader();

        mockReader.Setup(r => r.GetPeople()).Returns(fakeReader.GetPeople);
        return mockReader;
    }

    private Winners GetFakeWinners()
    {
        Winners todaysWinners = new(DateTime.Today)
        {
            SelectedPeople = [],
        };
        return todaysWinners;
    }
}
