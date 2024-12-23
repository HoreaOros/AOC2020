using System.Text;

internal class Tile
{
    public Tile()
    {
    }

    public int Id { get; set; }
    public char[,] OrigImage { get; set; }

    internal Tile FlipHorizontally()
    {
        Tile t = new Tile();
        t.Id = Id;
        char[,] r = new char[OrigImage.GetLength(0), OrigImage.GetLength(1)];
        for (int i = 0; i < OrigImage.GetLength(0); i++)
            for (int j = 0; j < OrigImage.GetLength(1); j++)
                r[OrigImage.GetLength(0) - i - 1, j] = OrigImage[i, j];
        t.OrigImage = r;
        return t;
    }

    internal Tile RotateRight()
    {
        Tile t = new Tile();
        t.Id = Id;
        char[,] r = new char[OrigImage.GetLength(0), OrigImage.GetLength(1)];
        for(int i = 0; i < OrigImage.GetLength(0); i++)
            for(int j = 0; j < OrigImage.GetLength(1); j++)
                r[j, OrigImage.GetLength(0) - i- 1] = OrigImage[i, j];
        t.OrigImage = r;
        return t;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < OrigImage.GetLength(0); i++ )
        {
            for(int j = 0; j < OrigImage.GetLength (1); j++ )
                sb.Append(OrigImage[i, j]);
            sb.Append(Environment.NewLine);
        }
        sb.Append(Environment.NewLine);
        return sb.ToString();
    }

    internal void RemovBorders()
    {
        char[,] newImage = new char[OrigImage.GetLength(0) - 2, OrigImage.GetLength(1) - 2];
        for (int i = 1; i < OrigImage.GetLength(0) - 1; i++)
            for (int j = 1; j < OrigImage.GetLength(1) - 1; j++)
                newImage[i - 1, j - 1] = OrigImage[i, j];
        OrigImage = newImage;
    }

    internal string Left
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < OrigImage.GetLength(0); i++)
                sb.Append(OrigImage[i, 0]);
            return sb.ToString();
        }
    }
    internal string Right
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < OrigImage.GetLength(0); i++)
                sb.Append(OrigImage[i, OrigImage.GetLength(0) - 1]);
            return sb.ToString();
        }
    }
    internal string Up
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < OrigImage.GetLength(1); i++)
                sb.Append(OrigImage[0, i]);
            return sb.ToString();
        }
    }
    internal string Down
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < OrigImage.GetLength(1); i++)
                sb.Append(OrigImage[OrigImage.GetLength(1) - 1, i]);
            return sb.ToString();
        }
    }
}