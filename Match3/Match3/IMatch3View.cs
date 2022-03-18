namespace Match3;

using global::Match3.Model;

public interface IMatch3View
{
    Task SetCellIcon(Cell cell, Icon icon);
    Task SetCellState(Cell cell, Icon icon, bool activ);
    Task RemoveCellIcon(Cell cell);
    Task ReplaceCellIconImage(Cell cell, Icon icon, string altImage);
    Task IncreaseScore(int scorePerIcon);
    Task WaitSomeTime(int milliseconds);
    
    void HideStartMenu();
    void ShowGameField();
    void SetTimeOut(int timeoutInSec);
    void HideGameField();
    void ShowStartMenu();
    void ResetScore();
    void ResetDesc();
}