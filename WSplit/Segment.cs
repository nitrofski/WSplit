using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Segment
{
    public double BackupBest;
    public double BackupBestSegment;
    public double BestSegment;
    public double BestTime;
    public Image GrayIcon;
    public Bitmap GrayIcon16;
    public Bitmap GrayIcon24;
    public Bitmap GrayIcon32;
    private Image icon;
    public Bitmap Icon16;
    public Bitmap Icon24;
    public Bitmap Icon32;
    public string IconPath;
    public double LiveTime;
    public string Name;
    public double OldTime;
    public Color TimeColor;
    public string TimeString;
    public int TimeWidth;

    public Segment(string name)
    {
        this.IconPath = "";
        this.TimeString = "";
        this.TimeColor = Color.White;
        this.Name = name;
    }

    public Segment(string name, double oldtime, double besttime, double bestseg)
    {
        this.IconPath = "";
        this.TimeString = "";
        this.TimeColor = Color.White;
        this.Name = name;
        this.OldTime = oldtime;
        this.BestTime = besttime;
        this.BackupBest = besttime;
        this.BestSegment = bestseg;
        this.BackupBestSegment = bestseg;
    }

    private void prerenderSizes()
    {
        Bitmap image = new Bitmap(16, 16);
        Bitmap bitmap2 = new Bitmap(16, 16);
        Bitmap bitmap3 = new Bitmap(24, 24);
        Bitmap bitmap4 = new Bitmap(24, 24);
        Bitmap bitmap5 = new Bitmap(32, 32);
        Bitmap bitmap6 = new Bitmap(32, 32);

        Graphics graphics = Graphics.FromImage(image);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(this.icon, 0, 0, 16, 16);
        this.Icon16 = image;

        graphics = Graphics.FromImage(bitmap3);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(this.icon, 0, 0, 24, 24);
        this.Icon24 = bitmap3;

        graphics = Graphics.FromImage(bitmap5);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(this.icon, 0, 0, 32, 32);
        this.Icon32 = bitmap5;

        graphics = Graphics.FromImage(bitmap2);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(this.GrayIcon, 0, 0, 16, 16);
        this.GrayIcon16 = bitmap2;

        graphics = Graphics.FromImage(bitmap4);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(this.GrayIcon, 0, 0, 24, 24);
        this.GrayIcon24 = bitmap4;

        graphics = Graphics.FromImage(bitmap6);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(this.GrayIcon, 0, 0, 32, 32);
        this.GrayIcon32 = bitmap6;
    }

    public Image Icon
    {
        get
        {
            return this.icon;
        }
        set
        {
            this.icon = value;
            Bitmap icon = (Bitmap) this.icon;
            Bitmap bitmap2 = new Bitmap(this.icon.Width, this.icon.Height);
            for (int i = 0; i < this.icon.Width; i++)
            {
                for (int j = 0; j < this.icon.Height; j++)
                {
                    Color pixel = icon.GetPixel(i, j);
                    int num3 = (int) (((pixel.R * 0.3) + (pixel.G * 0.59)) + (pixel.B * 0.11));
                    bitmap2.SetPixel(i, j, Color.FromArgb(pixel.A, ((num3 * 4) + pixel.R) / 5, ((num3 * 4) + pixel.G) / 5, ((num3 * 4) + pixel.B) / 5));
                }
            }
            this.GrayIcon = bitmap2;
            this.prerenderSizes();
        }
    }
}

