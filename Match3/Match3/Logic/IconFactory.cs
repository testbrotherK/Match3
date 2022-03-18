namespace Match3.Logic;

using global::Match3.Model;


public static class IconFactory
{
    public const string Bomb = "M";
    public const string Destroyer = "Q";

    public static Icon CreateByType(IconType iconType)
    {
        Icon icon;

        switch (iconType)
        {
            case IconType.Cancer :
                icon = Cancer();
                break;
            case IconType.Leo :
                icon = Leo();
                break;
            case IconType.Libra:
                icon = Libra();
                break;
            case IconType.Virgo:
                icon = Virgo();
                break;
            default:
                icon = Scorpio();
                break;
        }

        return icon;
    }

    public static Destroyer CreateDestroyer(Icon icon, bool horizontal)
    {
        return new Destroyer
        {
            Image = Destroyer,
            IsActive = icon.IsActive,
            Color = icon.Color,
            Type = icon.Type,
            Horizontal = horizontal
        };
    }
    
    public static Icon CreateBomb(Icon icon)
    {
        icon.Image = Bomb;
        return icon;
    }

    public static Icon Cancer()
    {
        return new Icon
        {
            Type = IconType.Cancer,
            Color = Color.LightSteelBlue,
            Image = "a"
        };
    }

    public static Icon Leo()
    {
        return new Icon
        {
            Type = IconType.Leo,
            Color = Color.DarkTurquoise,
            Image = "b"
        };
    }

    public static Icon Virgo()
    {
        return new Icon
        {
            Type = IconType.Virgo,
            Color = Color.DarkMagenta,
            Image = "c"
        };
    }

    public static Icon Libra()
    {
        return new Icon
        {
            Type = IconType.Libra,
            Color = Color.SeaGreen,
            Image = "d"
        };
    }

    public static Icon Scorpio()
    {
        return new Icon
        {
            Type = IconType.Scorpio,
            Color = Color.Goldenrod,
            Image = "e"
        };
    }

}