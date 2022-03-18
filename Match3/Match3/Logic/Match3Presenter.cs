namespace Match3.Logic;

using global::Match3.Model;

public class Match3Presenter
{
    private readonly IMatch3View view;
    private readonly Random random;
    
    private  Dictionary<Cell, Icon?> deskCellIcons;
    private Cell? activeCell;
    private int timeoutInSec = 60;
    private int scorePerIcon = 10;

    public Match3Presenter(IMatch3View form1)
    {
        deskCellIcons = new Dictionary<Cell, Icon?>();
        random = new Random();

        this.view = form1;
    }
    
    /// заканчиваем игру
    public void GameOver()
    {
        this.view.ResetDesc();
        this.view.ResetScore();
        this.view.HideGameField();
        this.view.ShowStartMenu();
    }

    /// стартуем новую игру
    public async Task StartGame()
    {
        this.view.HideStartMenu();
        this.view.ShowGameField();
        this.view.ResetScore();
        await GenerateDeskCellIcons();
        
        this.view.SetTimeOut(timeoutInSec);
    }

    /// Нажатие на иконку
    public async Task ClickOnIcon(Cell cell)
    {
        try
        {
            if (activeCell == null)
            {
                activeCell = cell;
                await ActivateCellIcon(cell, true);
            }
            else
            if (activeCell.Equals(cell))
            {
                await ActivateCellIcon(cell, false);
                activeCell = null;
            }
            else
            {
                await ActivateCellIcon(cell, true);

                await SwitchCells(activeCell.Value, cell);

                activeCell = null;
            }
        }
        catch (Exception e)
        {
            //Do something
            throw;
        }
    }
    
    /// заполняем dictionary случайными иконками
    private async Task GenerateDeskCellIcons()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Icon icon = GetRandomIcon();
                Cell cell = new(i, j);
                if (!deskCellIcons.ContainsKey(cell))
                {
                    deskCellIcons.Add(cell, icon);
                }
                else
                {
                    deskCellIcons[cell] = icon;
                }
            }
        }
        await DrawIconsToDeskCells();
    }

    /// создаем случайную иконку из 5 вариантов
    private Icon GetRandomIcon()
    {
        int randomNumber = random.Next(5);
        IconType iconType = (IconType)randomNumber;
        Icon icon = IconFactory.CreateByType(iconType);

        return icon;
    }

    /// заполняем доску иконками из dictionary
    private async Task DrawIconsToDeskCells()
    {
        foreach (var rowCells in deskCellIcons.OrderByDescending(c => c.Key.Row).GroupBy(c => c.Key.Row))
        {
            foreach (KeyValuePair<Cell, Icon?> cell in rowCells)
            {
                await DrawCellIcon(cell.Key);
            }
        }

        //Производим первую проверку на соответствия
        await CleanUpDesk();
    }

    /// передаем иконку view
    private async Task DrawCellIcon(Cell cell)
    {
        deskCellIcons.TryGetValue(cell, out Icon? icon);
        if (icon != null)
        {
            deskCellIcons[cell]!.IsActive = false;

            await view.SetCellIcon(cell, icon);
        }
    }

    /// удаляем иконку и проверяем удаляемые иконки
    private async Task RemoveCellIcon(Cell cell)
    {
        deskCellIcons.TryGetValue(cell, out Icon? icon);
        if (icon != null)
        {
            await RemoveCell(cell);

            await TriggerDestroyerAsync(cell, icon);
            await TriggerBomb(cell, icon);
        }
    }

    /// удаляем иконку в view
    private async Task RemoveCell(Cell cell)
    {
        deskCellIcons[cell] = null;
        await view.RemoveCellIcon(cell);
    }

    /// удаляем поле для разрушителя или бомбы, если оно не является  источником разрушения
    private async Task RemoveIfNotEqual(Cell originCell, Cell otherCell)
    {
        if (!originCell.Equals(otherCell))
        {
            await RemoveCellIcon(otherCell);
        }
    }

    /// взрываем бомбу
    private async Task TriggerBomb(Cell cell, Icon icon)
    {
        if (icon is { Image: IconFactory.Bomb })
        {
            await view.WaitSomeTime(250);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cell.Column - i >= 0)
                    {
                        await RemoveIfNotEqual(cell, new Cell(cell.Row, cell.Column - i));

                        if (cell.Row - j >= 0)
                        {
                            await RemoveIfNotEqual(cell, new Cell(cell.Row - j, cell.Column - i));
                        }
                        if (cell.Row + j < 8)
                        {
                            await RemoveIfNotEqual(cell, new Cell(cell.Row + j, cell.Column - i));
                        }
                    }
                    if (cell.Column + i < 8)
                    {
                        await RemoveIfNotEqual(cell, new Cell(cell.Row, cell.Column + i));
                        if (cell.Row - j >= 0)
                        {
                            await RemoveIfNotEqual(cell, new Cell(cell.Row - j, cell.Column + i));
                        }
                        if (cell.Row + i < 8)
                        {
                            await RemoveIfNotEqual(cell, new Cell(cell.Row + j, cell.Column + i));
                        }
                    }
                }
            }
        }
    }

    /// запускаем разрушителя
    private async Task TriggerDestroyerAsync(Cell cell, Icon icon)
    {
        if (icon.Image == IconFactory.Destroyer)
        {
            Destroyer destroyer = (Destroyer)icon;
            
            for (int i = 0; i < 7; i++)
            {
                if (destroyer.Horizontal)
                {
                    var replCellMinus = new Cell(cell.Row, cell.Column - i);
                    var replCellPlus = new Cell(cell.Row, cell.Column + i);

                    if (!cell.Equals(replCellMinus) && replCellMinus.Column >= 0)
                    {
                        await view.ReplaceCellIconImage(replCellMinus, icon, destroyer.Image);
                    
                    }
                    if (!cell.Equals(replCellPlus) && replCellPlus.Column < 8)
                    {
                        await view.ReplaceCellIconImage(replCellPlus, icon, destroyer.Image);
                    }

                    //await view.WaitSomeTime(50);
                    await RemoveIfNotEqual(cell, replCellMinus);
                    await RemoveIfNotEqual(cell, replCellPlus);
                }
                else
                {
                    var replRowCellMinus = new Cell(cell.Row - i, cell.Column);
                    var replRowCellPlus = new Cell(cell.Row + i, cell.Column);
                    if (!cell.Equals(replRowCellMinus) && replRowCellMinus.Row >= 0)
                    {
                        await view.ReplaceCellIconImage(replRowCellMinus, icon, destroyer.Image);
                    }
                    if (!cell.Equals(replRowCellPlus) && replRowCellPlus.Row < 8)
                    {
                        await view.ReplaceCellIconImage(replRowCellPlus, icon, destroyer.Image);
                    }

                    //await view.WaitSomeTime(60);
                    await RemoveIfNotEqual(cell,replRowCellMinus);
                    await RemoveIfNotEqual(cell, replRowCellPlus);
                }
            }
        }
    }
    
    /// активируем либо деактивируем иконку в view
    private async Task ActivateCellIcon(Cell cell, bool isActive)
    {
        deskCellIcons.TryGetValue(cell, out Icon? icon);
        if (icon != null)
        {
            //await view.WaitSomeTime(20);

            deskCellIcons[cell]!.IsActive = isActive;
            await view.SetCellState(cell, icon, isActive);
        }
    }

    /// заменяем иконку на специальную
    private async Task DrawDestroyerCellIcon(Cell cell)
    {
        deskCellIcons.TryGetValue(cell, out Icon? icon);
        if (icon != null)
        {
            Destroyer destroyer = IconFactory.CreateDestroyer(deskCellIcons[cell]!, random.Next(100) <= 20);
            deskCellIcons[cell] = destroyer;
            //await view.WaitSomeTime(20);

            await view.ReplaceCellIconImage(cell, icon, destroyer.Image);
        }
    }

    /// заменяем иконку на специальную
    private async Task DrawBombCellIcon(Cell cell)
    {
        deskCellIcons.TryGetValue(cell, out Icon? icon);
        if (icon != null)
        {
            Icon bomb = IconFactory.CreateBomb(deskCellIcons[cell]!);
            deskCellIcons[cell] = bomb;
            //await view.WaitSomeTime(20);

            await view.ReplaceCellIconImage(cell, icon, bomb.Image);
        }
    }
    
    /// Производим проверку на соответствия
    private async Task CleanUpDesk()
    {
        Dictionary<IconType, List<Cell>?> iconsCells = deskCellIcons.GroupByIcons();
        Dictionary<Guid, List<Cell>> cellsToRemove = new Dictionary<Guid, List<Cell>>();

        foreach (KeyValuePair<IconType, List<Cell>?> keyValuePair in iconsCells)
        {
            var iconCells = keyValuePair.Value;
            if (iconCells != null)
            {
                IdentifyRowsCellsToRemove(iconCells, cellsToRemove);
                IdentifyColumnsCellsToRemove(iconCells, cellsToRemove);
            }
        }

        await RemoveMath3Cells(cellsToRemove);
    }
    
    /// удаляем разрушенные иконки
    private async Task RemoveMath3Cells(Dictionary<Guid, List<Cell>> cellsToRemove)
    {
        if (cellsToRemove.Any())
        {
            List<Cell> crossCells = GetCrossCells(cellsToRemove);
            foreach (KeyValuePair<Guid, List<Cell>> cellsGroup in cellsToRemove)
            {
                Cell? destroyer = null;
                Cell? bomb = null;

                if (cellsGroup.Value.Count > 4 ||
                         cellsGroup.Value.Intersect(crossCells).Any())
                {
                    bool bombSet = false;
                    foreach (Cell cell in cellsGroup.Value)
                    {
                        if (deskCellIcons[cell] != null && deskCellIcons[cell]!.IsActive ||
                            crossCells.Contains(cell) || 
                            !bombSet && cell.Equals(cellsGroup.Value.Last()))
                        {
                            await DrawBombCellIcon(cell);
                            bombSet = true;
                        }
                        else
                        {
                            await RemoveCellIcon(cell);
                        }
                        await view.IncreaseScore(scorePerIcon);
                    }
                }
                else if (cellsGroup.Value.Count == 4)
                {
                    bool destroyerSet = false;
                    foreach (Cell cell in cellsGroup.Value)
                    {
                        if (deskCellIcons[cell] != null && deskCellIcons[cell]!.IsActive ||
                            !destroyerSet && cell.Equals(cellsGroup.Value.Last()))
                        {
                            if (deskCellIcons[cell]?.Image != IconFactory.Bomb)
                            {
                                await DrawDestroyerCellIcon(cell);
                                destroyerSet = true;
                            }
                        }
                        else
                        {
                            await RemoveCellIcon(cell);
                        }
                        await view.IncreaseScore(scorePerIcon);
                    }
                }
                else
                {
                    foreach (Cell cell in cellsGroup.Value)
                    {
                        if (!destroyer.Equals(cell) && !bomb.Equals(cell))
                        {
                            await RemoveCellIcon(cell);
                        }
                        await view.IncreaseScore(scorePerIcon);
                    }
                }
            }

            while (deskCellIcons.Any(x => x.Value == null))
            {
                await FillEmptyCells(); 
            }
            ////обновляем игровое поле
            await CleanUpDesk();
        }
    }

    /// Находим пересекающиеся поля
    private List<Cell> GetCrossCells(Dictionary<Guid, List<Cell>> cellsToRemove)
    {
        List<Cell> globalCellList = new List<Cell>();
        List<Cell> crossCells = new List<Cell>();
        foreach (List<Cell> value in cellsToRemove.Values)
        {
            foreach (Cell cell in value)
            {
                if (globalCellList.Contains(cell) && !crossCells.Contains(cell))
                {
                    deskCellIcons[cell]!.IsActive = true;
                    crossCells.Add(cell);
                }
                globalCellList.Add(cell);
            }
        }

        return crossCells;
    }
    
    /// заполняем пустые места в dictionary
    private async Task FillEmptyCells()
    {
        var emptyCells = deskCellIcons
            .Where(c => c.Value == null)
            .OrderBy(c => c.Key.Column)
            .ThenByDescending(c => c.Key.Row); 

        foreach (KeyValuePair<Cell, Icon?> emptyCell in emptyCells)
        {
            await ReplaceWithFirstUnemptyIconOrNew(emptyCell.Key);
        }
    }

    /// заполняем пустые места в dictionary
    private async Task ReplaceWithFirstUnemptyIconOrNew(Cell emptyCell)
    {
        if (emptyCell.Row == 0)
        {
            deskCellIcons[emptyCell] = GetRandomIcon();
            await DrawCellIcon(emptyCell);
        }
        else
        {
            int rowCount = emptyCell.Row;
            for (int i = rowCount; i >= 0 ; i--)
            {
                var replaceCell = new Cell(i, emptyCell.Column);
                Icon? icon = deskCellIcons[replaceCell];
                if (icon != null)
                {
                    await RemoveCell(replaceCell);

                    deskCellIcons[emptyCell] = icon;
                    await DrawCellIcon(emptyCell);

                    emptyCell = replaceCell;
                    if (i == 0)
                    {
                        deskCellIcons[new Cell(i, emptyCell.Column)] = GetRandomIcon();
                        await DrawCellIcon(emptyCell);
                    }
                }
            }
        }
    }

    /// заменяем иконки друг с другом
    private async Task SwitchCells(Cell previousCell, Cell currentCell)
    {

        //when both cells are neighbours
        if (previousCell.Column == currentCell.Column && Math.Abs(previousCell.Row - currentCell.Row) == 1 || 
            previousCell.Row == currentCell.Row && Math.Abs(previousCell.Column - currentCell.Column) == 1) 
        {
            (deskCellIcons[previousCell], deskCellIcons[currentCell]) = (deskCellIcons[currentCell], deskCellIcons[previousCell]);
            await DrawCellIcon(previousCell);
            await DrawCellIcon(currentCell);

            Dictionary<Guid, List<Cell>> cellsToRemove = new Dictionary<Guid, List<Cell>>();

            var iconsCells = deskCellIcons.GroupByIcons();

            IdentifyRowCellsForMovedCell(iconsCells, previousCell, cellsToRemove);
            IdentifyRowCellsForMovedCell(iconsCells, currentCell, cellsToRemove);
            IdentifyColumnCellsForMovedCell(iconsCells, previousCell, cellsToRemove);
            IdentifyColumnCellsForMovedCell(iconsCells, currentCell, cellsToRemove);

            if (cellsToRemove.Any())
            {
                foreach (KeyValuePair<Guid, List<Cell>> keyValuePair in cellsToRemove)
                {
                    foreach (Cell cell in keyValuePair.Value)
                    {
                        if (cell.Equals(currentCell))
                            await ActivateCellIcon(currentCell, true);
                        if (cell.Equals(previousCell))
                            await ActivateCellIcon(previousCell, true);
                    }
                }
                await RemoveMath3Cells(cellsToRemove);
            }
            else
            {
                (deskCellIcons[currentCell], deskCellIcons[previousCell]) = (deskCellIcons[previousCell], deskCellIcons[currentCell]);
                await ActivateCellIcon(previousCell, false);
                await ActivateCellIcon(currentCell, false);
            }
        }
        else
        {
            await ActivateCellIcon(previousCell, false);
            await ActivateCellIcon(currentCell, false);
        }
    }

    #region вычисляем группы иконок для удаления
    private static void IdentifyRowsCellsToRemove(List<Cell>? iconCells, Dictionary<Guid, List<Cell>> cellsToRemove)
    {
        if (iconCells != null)
        {
            var rows = iconCells.OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToArray();
            foreach (IGrouping<int, Cell> groupedCells in rows)
            {
                var rowCellsToRemove = IdentifyRowCellsToRemove(groupedCells.ToList());
                if (rowCellsToRemove.Any())
                {
                    foreach (var columnCell in rowCellsToRemove)
                    {
                        cellsToRemove.Add(columnCell.Key, columnCell.Value);
                    }
                }
            }
        }
    }

    private static void IdentifyColumnsCellsToRemove(List<Cell>? iconCells, Dictionary<Guid, List<Cell>> cellsToRemove)
    {
        if (iconCells != null)
        {
            var columns = iconCells.OrderBy(x => x.Column).ThenBy(x => x.Row).GroupBy(x => x.Column).ToArray();
            foreach (IGrouping<int, Cell> groupedCells in columns)
            {
                var columCellsToRemove = IdentifyColumCellsToRemove(groupedCells.ToList());
                if (columCellsToRemove.Any())
                {
                    foreach (var columnCell in columCellsToRemove)
                    {
                        cellsToRemove.Add(columnCell.Key, columnCell.Value);
                    }
                }
            }
        }
    }

    private static Dictionary<Guid, List<Cell>> IdentifyColumCellsToRemove(List<Cell> groupedCells)
    {
        Guid counter = Guid.NewGuid();
        Dictionary<Guid, List<Cell>> cellsToRemove = new Dictionary<Guid, List<Cell>>();
        List<Cell> candidate = new List<Cell>();

        foreach (Cell groupedCell in groupedCells)
        {
            if (!candidate.Any())
            {
                candidate.Add(groupedCell);
            }
            else
            {
                if (candidate.Last().Row == groupedCell.Row - 1)
                {
                    candidate.Add(groupedCell);
                    if (candidate.Count > 2)
                    {
                        List<Cell> dictList = new List<Cell>();
                        dictList.AddRange(candidate);

                        if (cellsToRemove.ContainsKey(counter))
                        {
                            cellsToRemove[counter] = dictList;
                        }
                        else
                        {
                            cellsToRemove.Add(counter, dictList);
                        }
                    }
                }
                else
                {
                    candidate.Clear();
                    candidate.Add(groupedCell);
                    counter = Guid.NewGuid();
                }
            }
        }

        return cellsToRemove;
    }

    private static Dictionary<Guid, List<Cell>> IdentifyRowCellsToRemove(List<Cell> groupedCells)
    {
        Guid counter = Guid.NewGuid();
        List<Cell> candidate = new List<Cell>();
        Dictionary<Guid, List<Cell>> cellsToRemove = new Dictionary<Guid, List<Cell>>();

        foreach (Cell groupedCell in groupedCells)
        {
            if (!candidate.Any())
            {
                candidate.Add(groupedCell);
            }
            else
            {
                if (candidate.Last().Column == groupedCell.Column - 1)
                {
                    candidate.Add(groupedCell);
                    if (candidate.Count > 2)
                    {
                        List<Cell> dictList = new();
                        dictList.AddRange(candidate);

                        if (cellsToRemove.ContainsKey(counter))
                        {
                            cellsToRemove[counter] = dictList;
                        }
                        else
                        {
                            cellsToRemove.Add(counter, dictList);
                        }
                    }
                }
                else
                {
                    candidate.Clear();
                    candidate.Add(groupedCell);
                    counter = Guid.NewGuid();
                }
            }
        }

        return cellsToRemove;
    }

    private void IdentifyRowCellsForMovedCell(Dictionary<IconType, List<Cell>?> iconsCells, Cell cell, Dictionary<Guid, List<Cell>> cellsToRemove)
    {
        var iconCells = iconsCells.First(x => x.Value != null && x.Value.Contains(cell));
        List<Cell>? cells = iconCells.Value;
        IdentifyRowsCellsToRemove(cells, cellsToRemove);
    }

    private void IdentifyColumnCellsForMovedCell(Dictionary<IconType, List<Cell>?> iconsCells, Cell cell, Dictionary<Guid, List<Cell>> cellsToRemove)
    {
        var iconCells = iconsCells.First(x => x.Value != null && x.Value.Contains(cell));
        List<Cell>? cells = iconCells.Value;
        IdentifyColumnsCellsToRemove(cells, cellsToRemove);
    }
    #endregion
}