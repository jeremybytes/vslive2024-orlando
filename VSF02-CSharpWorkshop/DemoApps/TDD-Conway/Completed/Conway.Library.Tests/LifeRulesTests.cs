namespace Conway.Library.Tests;

//Any live cell with fewer than two live neighbours dies.
//Any live cell with two or three live neighbours lives.
//Any live cell with more than three live neighbours dies.    
//Any dead cell with exactly three live neighbours becomes a live cell.

public class LifeRulesTests
{
    //Any live cell with fewer than two live neighbours dies.
    [Test]
    public void LiveCell_FewerThan2LiveNeighbors_Dies(
        [Values(0, 1)] int liveNeighbors)
    {
        // Arrange
        var currentState = CellState.Alive;

        // Act
        CellState newState = LifeRules.GetNewState(currentState, liveNeighbors);

        // Assert
        Assert.That(newState, Is.EqualTo(CellState.Dead));
    }

    //Any live cell with two or three live neighbours lives.
    [Test]
    public void LiveCell_2or3LiveNeighbors_Lives(
        [Values(2, 3)] int liveNeighbors)
    {
        var currentState = CellState.Alive;

        var newState = LifeRules.GetNewState(currentState, liveNeighbors);

        Assert.That(newState, Is.EqualTo(CellState.Alive));
    }

    //Any live cell with more than three live neighbours dies.    
    [Test]
    public void LiveCell_MoreThan3LiveNeighbors_Dies(
        [Range(4, 8)] int liveNeighbors)
    {
        var currentState = CellState.Alive;

        var newState = LifeRules.GetNewState(currentState, liveNeighbors);

        Assert.That(newState, Is.EqualTo(CellState.Dead));
    }

    //Any dead cell with exactly three live neighbours becomes a live cell.
    [Test]
    public void DeadCell_Exactly3LiveNeighbors_Lives()
    {
        var currentState = CellState.Dead;
        var liveNeighbors = 3;

        var newState = LifeRules.GetNewState(currentState, liveNeighbors);

        Assert.That(newState, Is.EqualTo(CellState.Alive));
    }

    [Test]
    public void DeadCell_LessThan3LiveNeighbors_StaysDead(
        [Range(0, 2)] int liveNeighbors)
    {
        var currentState = CellState.Dead;

        var newState = LifeRules.GetNewState(currentState, liveNeighbors);

        Assert.That(newState, Is.EqualTo(CellState.Dead));
    }

    [Test]
    public void DeadCell_MoreThan3LiveNeighbors_StaysDead(
        [Range(4, 8)] int liveNeighbors)
    {
        var currentState = CellState.Dead;

        var newState = LifeRules.GetNewState(currentState, liveNeighbors);

        Assert.That(newState, Is.EqualTo(CellState.Dead));
    }

    [Test]
    public void InvalidCellState_ThrowsArgumentException()
    {
        var currentState = (CellState)2;
        var liveNeighbors = 0;

        try
        {
            LifeRules.GetNewState(currentState, liveNeighbors);
            Assert.Fail();
        }
        catch (ArgumentException ex)
        {
            if (ex.ParamName != "currentState")
                Assert.Fail();
            Assert.Pass();
        }
    }

    [Test]
    public void LiveNeighbors_LessThan0_ThrowsArgumentException()
    {
        var currentState = CellState.Alive;
        var liveNeighbors = -1;

        var ex = Assert.Catch<ArgumentException>(() =>
            LifeRules.GetNewState(currentState, liveNeighbors));

        if (ex.ParamName != "liveNeighbors")
            Assert.Fail();
    }

    [Test]
    public void LiveNeighbors_MoreThan8_ThrowsArgumentException()
    {
        var currentState = CellState.Alive;
        var liveNeighbors = 9;

        var ex = Assert.Catch<ArgumentException>(() =>
            LifeRules.GetNewState(currentState, liveNeighbors));

        if (ex.ParamName != "liveNeighbors")
            Assert.Fail();
    }
}
