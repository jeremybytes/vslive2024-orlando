namespace PeopleViewer.Presentation.Tests;

public class PeopleViewModelTests
{
    [Test]
    public async Task People_OnRefreshPeople_IsPopulated()
    {
        // Arrange
        var reader = new FakeReader();
        var viewModel = new PeopleViewModel(reader);

        // Act
        await viewModel.RefreshPeople();

        // Assert
        Assert.That(viewModel.People, Is.Not.Null);
        Assert.That(viewModel.People.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task People_OnClearPeople_IsEmpty()
    {
        // Arrange
        var reader = new FakeReader();
        var viewModel = new PeopleViewModel(reader);
        await viewModel.RefreshPeople();
        Assert.That(viewModel.People.Count(), Is.GreaterThan(0),
            "Invalid arrangement");

        // Act
        viewModel.ClearPeople();

        // Assert
        Assert.That(viewModel.People.Count(), Is.EqualTo(0));
    }
}