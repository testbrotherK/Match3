namespace Match3.Logic;

using global::Match3.Model;

public static class Extensions
{
    public static Dictionary<IconType, List<Cell>?> GroupByIcons(this Dictionary<Cell, Icon?> deskCellIcons)
    {
        Dictionary<IconType, List<Cell>?> iconsCells = new();
        foreach ((Cell cell, Icon icon) in deskCellIcons
                     .OrderBy(x => x.Key.Row)
                     .ThenBy(x => x.Key.Column))
        {
            if(icon != null)
            {
                if (iconsCells.TryGetValue(icon.Type, out List<Cell>? cells))
                {
                    if (cells != null) cells.Add(cell);
                }
                else
                {
                    iconsCells.Add(icon.Type, new List<Cell> { cell });
                }
            }
        }

        return iconsCells;
    }
}