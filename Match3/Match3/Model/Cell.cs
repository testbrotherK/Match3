namespace Match3.Model;

public struct Cell
{
    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row;
    public int Column;
}