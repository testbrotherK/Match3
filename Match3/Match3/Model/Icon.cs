namespace Match3.Model;

public class Icon
{
    public string Image { get; set; }
    public Color Color { get; set; }
    public IconType Type { get; set; }
    public bool IsActive { get; set; }
}

public class Destroyer : Icon
{
    public bool Horizontal { get; set; }
}