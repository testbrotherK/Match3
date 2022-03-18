using Xunit;

namespace Match3.Tests;

using global::Match3.Logic;
using global::Match3.Model;
using Moq;

public class StartGameTests
{
    private Mock<IMatch3View> view;
    private Match3Presenter presenter;
    public StartGameTests()
    {
        view = new Mock<IMatch3View>();
        presenter = new Match3Presenter(view.Object);
    }

    [Fact]
    public void When_StartClick_Draw_at_least_64_Icons()
    {
        presenter.StartGame();

        view.Verify(x=>x.SetCellIcon(It.IsAny<Cell>(), It.IsAny<Icon>()), Times.AtLeast(64));
    }

    [Fact]
    public void When_GameOver_Reset_All_Fields()
    {
        presenter.GameOver();

        view.Verify(x => x.ResetDesc(), Times.Once);
    }
}