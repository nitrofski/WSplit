namespace WSplitTimer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class ColorPicker : Form
    {
        private Button button1;
        private Button button2;
        private NumericUpDown colorBbox;
        private PictureBox colorBox;
        private NumericUpDown colorGbox;
        private NumericUpDown colorRbox;
        private TextBox hexBox;
        public HSV hsvColor;
        private PictureBox hueArrow;
        private PictureBox hueArrow2;
        private Bitmap hues = new Bitmap(20, 0x100);
        private PictureBox hueSlider;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Bitmap leftArrow = new Bitmap(5, 10);
        private Bitmap palette = new Bitmap(0x100, 0x100);
        private PictureBox paletteBox;
        public Color rgbColor;
        private Bitmap rightArrow = new Bitmap(5, 10);

        public ColorPicker()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetColorRGB(0, 0, 0);
            using (Graphics graphics = Graphics.FromImage(this.hues))
            {
                for (int i = 0; i < (this.hues.Height - 2); i++)
                {
                    using (SolidBrush brush = new SolidBrush(HSVColor(0xff - i, 0xff, 0xff)))
                    {
                        graphics.FillRectangle(brush, 0, i, this.hues.Width, 1);
                    }
                }
            }
            using (Graphics graphics2 = Graphics.FromImage(this.leftArrow))
            {
                PointF[] points = new PointF[] { new Point(0, 0), new Point(5, 5), new Point(0, 10), new Point(0, 0) };
                GraphicsPath path = new GraphicsPath();
                path.AddLines(points);
                graphics2.FillPath(Brushes.Black, path);
            }
            using (Graphics graphics3 = Graphics.FromImage(this.rightArrow))
            {
                PointF[] tfArray2 = new PointF[] { new Point(5, 0), new Point(0, 5), new Point(5, 10), new Point(5, 0) };
                GraphicsPath path2 = new GraphicsPath();
                path2.AddLines(tfArray2);
                graphics3.FillPath(Brushes.Black, path2);
            }
            this.paletteBox.BackgroundImage = this.palette;
            this.hueSlider.BackgroundImage = this.hues;
            this.UpdatePalette();
        }

        private void colorBbox_ValueChanged(object sender, EventArgs e)
        {
            if (this.colorBbox.Value != this.rgbColor.B)
            {
                this.SetColorRGB(this.rgbColor.R, this.rgbColor.G, (int)this.colorBbox.Value);
            }
        }

        private void colorGbox_ValueChanged(object sender, EventArgs e)
        {
            if (this.colorGbox.Value != this.rgbColor.G)
            {
                this.SetColorRGB(this.rgbColor.R, (int)this.colorGbox.Value, this.rgbColor.B);
            }
        }

        private void colorRbox_ValueChanged(object sender, EventArgs e)
        {
            if (this.colorRbox.Value != this.rgbColor.R)
            {
                this.SetColorRGB((int)this.colorRbox.Value, this.rgbColor.G, this.rgbColor.B);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public static HSV GetHSV(Color C)
        {
            double num7;
            double num8;
            double num4 = ((double)C.R) / 255.0;
            double num5 = ((double)C.G) / 255.0;
            double num6 = ((double)C.B) / 255.0;
            double num = Math.Min(Math.Min(num4, num5), num6);
            double num2 = Math.Max(Math.Max(num4, num5), num6);
            double num9 = num2;
            double num3 = num2 - num;
            if ((num2 == 0.0) || (num3 == 0.0))
            {
                num8 = 0.0;
                num7 = 0.0;
            }
            else
            {
                num8 = num3 / num2;
                if (num4 == num2)
                {
                    num7 = (num5 - num6) / num3;
                }
                else if (num5 == num2)
                {
                    num7 = 2.0 + ((num6 - num4) / num3);
                }
                else
                {
                    num7 = 4.0 + ((num4 - num5) / num3);
                }
            }
            num7 *= 60.0;
            if (num7 < 0.0)
            {
                num7 += 360.0;
            }
            return new HSV { H = (int)((num7 / 360.0) * 255.0), S = (int)(num8 * 255.0), V = (int)(num9 * 255.0) };
        }

        private void hexBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Color c = ColorTranslator.FromHtml("#" + this.hexBox.Text);
                    if (this.rgbColor != c)
                    {
                        this.SetColor(c);
                    }
                    e.SuppressKeyPress = true;
                }
                catch
                {
                }
            }
        }

        private void hexBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), "[0-9A-Fa-f]"))
            {
                e.Handled = true;
            }
        }

        private void hexBox_Leave(object sender, EventArgs e)
        {
            try
            {
                Color c = ColorTranslator.FromHtml("#" + this.hexBox.Text);
                if (this.rgbColor != c)
                {
                    this.SetColor(c);
                }
            }
            catch
            {
            }
        }

        private void hexBox_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(this.hexBox.Text, "[^0-9A-Fa-f]"))
            {
                this.hexBox.Text = Regex.Replace(this.hexBox.Text, "[^0-9A-Fa-f]", "");
            }
        }

        public static Color HSVColor(int H, int S, int V)
        {
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num = ((H / 255.0) * 360.0) % 360.0;
            double num2 = S / 255.0;
            double num3 = V / 255.0;
            if (num2 == 0.0)
            {
                num4 = num3;
                num5 = num3;
                num6 = num3;
            }
            else
            {
                double d = num / 60.0;
                int num11 = (int)Math.Floor(d);
                double num10 = d - num11;
                double num7 = num3 * (1.0 - num2);
                double num8 = num3 * (1.0 - (num2 * num10));
                double num9 = num3 * (1.0 - (num2 * (1.0 - num10)));
                switch (num11)
                {
                    case 0:
                        num4 = num3;
                        num5 = num9;
                        num6 = num7;
                        break;

                    case 1:
                        num4 = num8;
                        num5 = num3;
                        num6 = num7;
                        break;

                    case 2:
                        num4 = num7;
                        num5 = num3;
                        num6 = num9;
                        break;

                    case 3:
                        num4 = num7;
                        num5 = num8;
                        num6 = num3;
                        break;

                    case 4:
                        num4 = num9;
                        num5 = num7;
                        num6 = num3;
                        break;

                    case 5:
                        num4 = num3;
                        num5 = num7;
                        num6 = num8;
                        break;
                }
            }
            return Color.FromArgb(255, (int)(num4 * 255.0), (int)(num5 * 255.0), (int)(num6 * 255.0));
        }

        private void hueArrow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.leftArrow, 0, 0xff - this.hsvColor.H);
        }

        private void hueArrow2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.rightArrow, 0, 0xff - this.hsvColor.H);
        }

        private void hueSlider_MouseDown(object sender, MouseEventArgs e)
        {
            int num = Math.Max(0, Math.Min(0xff - e.Y, 0xff));
            if (this.hsvColor.H != num)
            {
                this.hsvColor.H = num;
                this.SetColorHSV(this.hsvColor.H, this.hsvColor.S, this.hsvColor.V);
            }
        }

        private void hueSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                int num = Math.Max(0, Math.Min(0xff - e.Y, 0xff));
                if (this.hsvColor.H != num)
                {
                    this.hsvColor.H = num;
                    this.SetColorHSV(this.hsvColor.H, this.hsvColor.S, this.hsvColor.V);
                }
            }
        }

        private void InitializeComponent()
        {
            this.paletteBox = new PictureBox();
            this.hueSlider = new PictureBox();
            this.hueArrow = new PictureBox();
            this.hueArrow2 = new PictureBox();
            this.colorRbox = new NumericUpDown();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.colorGbox = new NumericUpDown();
            this.colorBbox = new NumericUpDown();
            this.colorBox = new PictureBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.hexBox = new TextBox();
            this.label4 = new Label();
            ((ISupportInitialize)this.paletteBox).BeginInit();
            ((ISupportInitialize)this.hueSlider).BeginInit();
            ((ISupportInitialize)this.hueArrow).BeginInit();
            ((ISupportInitialize)this.hueArrow2).BeginInit();
            this.colorRbox.BeginInit();
            this.colorGbox.BeginInit();
            this.colorBbox.BeginInit();
            ((ISupportInitialize)this.colorBox).BeginInit();
            base.SuspendLayout();
            this.paletteBox.BorderStyle = BorderStyle.FixedSingle;
            this.paletteBox.Location = new Point(12, 12);
            this.paletteBox.Name = "paletteBox";
            this.paletteBox.Size = new Size(0x100, 0x100);
            this.paletteBox.TabIndex = 0;
            this.paletteBox.TabStop = false;
            this.paletteBox.Paint += new PaintEventHandler(this.paletteBox_Paint);
            this.paletteBox.MouseDown += new MouseEventHandler(this.paletteBox_MouseDown);
            this.paletteBox.MouseMove += new MouseEventHandler(this.paletteBox_MouseMove);
            this.hueSlider.BorderStyle = BorderStyle.FixedSingle;
            this.hueSlider.Location = new Point(0x116, 12);
            this.hueSlider.Name = "hueSlider";
            this.hueSlider.Size = new Size(0x12, 0x100);
            this.hueSlider.TabIndex = 1;
            this.hueSlider.TabStop = false;
            this.hueSlider.MouseDown += new MouseEventHandler(this.hueSlider_MouseDown);
            this.hueSlider.MouseMove += new MouseEventHandler(this.hueSlider_MouseMove);
            this.hueArrow.Location = new Point(0x111, 7);
            this.hueArrow.Name = "hueArrow";
            this.hueArrow.Size = new Size(5, 0x10a);
            this.hueArrow.TabIndex = 3;
            this.hueArrow.TabStop = false;
            this.hueArrow.Paint += new PaintEventHandler(this.hueArrow_Paint);
            this.hueArrow2.Location = new Point(0x128, 7);
            this.hueArrow2.Name = "hueArrow2";
            this.hueArrow2.Size = new Size(5, 0x10a);
            this.hueArrow2.TabIndex = 4;
            this.hueArrow2.TabStop = false;
            this.hueArrow2.Paint += new PaintEventHandler(this.hueArrow2_Paint);
            this.colorRbox.Location = new Point(0x147, 0x30);
            int[] bits = new int[4];
            bits[0] = 0xff;
            this.colorRbox.Maximum = new decimal(bits);
            this.colorRbox.Name = "colorRbox";
            this.colorRbox.Size = new Size(40, 20);
            this.colorRbox.TabIndex = 5;
            int[] numArray2 = new int[4];
            numArray2[0] = 0xff;
            this.colorRbox.Value = new decimal(numArray2);
            this.colorRbox.ValueChanged += new EventHandler(this.colorRbox_ValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x132, 0x30);
            this.label1.MinimumSize = new Size(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(15, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "R";
            this.label1.TextAlign = ContentAlignment.MiddleRight;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x132, 0x4a);
            this.label2.MinimumSize = new Size(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(15, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "G";
            this.label2.TextAlign = ContentAlignment.MiddleRight;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x133, 100);
            this.label3.MinimumSize = new Size(0, 20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(14, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "B";
            this.label3.TextAlign = ContentAlignment.MiddleRight;
            this.colorGbox.Location = new Point(0x147, 0x4a);
            int[] numArray3 = new int[4];
            numArray3[0] = 0xff;
            this.colorGbox.Maximum = new decimal(numArray3);
            this.colorGbox.Name = "colorGbox";
            this.colorGbox.Size = new Size(40, 20);
            this.colorGbox.TabIndex = 9;
            int[] numArray4 = new int[4];
            numArray4[0] = 0xff;
            this.colorGbox.Value = new decimal(numArray4);
            this.colorGbox.ValueChanged += new EventHandler(this.colorGbox_ValueChanged);
            this.colorBbox.Location = new Point(0x147, 100);
            int[] numArray5 = new int[4];
            numArray5[0] = 0xff;
            this.colorBbox.Maximum = new decimal(numArray5);
            this.colorBbox.Name = "colorBbox";
            this.colorBbox.Size = new Size(40, 20);
            this.colorBbox.TabIndex = 10;
            int[] numArray6 = new int[4];
            numArray6[0] = 0xff;
            this.colorBbox.Value = new decimal(numArray6);
            this.colorBbox.ValueChanged += new EventHandler(this.colorBbox_ValueChanged);
            this.colorBox.BorderStyle = BorderStyle.FixedSingle;
            this.colorBox.Location = new Point(0x132, 12);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new Size(0x3d, 30);
            this.colorBox.TabIndex = 11;
            this.colorBox.TabStop = false;
            this.button1.DialogResult = DialogResult.OK;
            this.button1.Location = new Point(0x132, 0xd8);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x3d, 0x17);
            this.button1.TabIndex = 13;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x132, 0xf5);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x3d, 0x17);
            this.button2.TabIndex = 14;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.hexBox.Location = new Point(0x132, 0xa5);
            this.hexBox.MaxLength = 6;
            this.hexBox.Name = "hexBox";
            this.hexBox.Size = new Size(0x3d, 20);
            this.hexBox.TabIndex = 12;
            this.hexBox.Text = "FFFFFF";
            this.hexBox.TextChanged += new EventHandler(this.hexBox_TextChanged);
            this.hexBox.KeyDown += new KeyEventHandler(this.hexBox_KeyDown);
            this.hexBox.KeyPress += new KeyPressEventHandler(this.hexBox_KeyPress);
            this.hexBox.Leave += new EventHandler(this.hexBox_Leave);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x12f, 0x95);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1a, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Hex";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x17b, 280);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.hexBox);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.colorBox);
            base.Controls.Add(this.colorBbox);
            base.Controls.Add(this.colorGbox);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.colorRbox);
            base.Controls.Add(this.hueArrow2);
            base.Controls.Add(this.hueArrow);
            base.Controls.Add(this.hueSlider);
            base.Controls.Add(this.paletteBox);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ColorPicker";
            this.Text = "Choose color...";
            ((ISupportInitialize)this.paletteBox).EndInit();
            ((ISupportInitialize)this.hueSlider).EndInit();
            ((ISupportInitialize)this.hueArrow).EndInit();
            ((ISupportInitialize)this.hueArrow2).EndInit();
            this.colorRbox.EndInit();
            this.colorGbox.EndInit();
            this.colorBbox.EndInit();
            ((ISupportInitialize)this.colorBox).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void paletteBox_MouseDown(object sender, MouseEventArgs e)
        {
            int s = Math.Max(0, Math.Min(e.X, 0xff));
            int v = Math.Max(0, Math.Min(0xff - e.Y, 0xff));
            if ((s != this.hsvColor.S) || (v != this.hsvColor.V))
            {
                this.SetColorHSV(this.hsvColor.H, s, v);
            }
        }

        private void paletteBox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                int s = Math.Max(0, Math.Min(e.X, 0xff));
                int v = Math.Max(0, Math.Min(0xff - e.Y, 0xff));
                if ((s != this.hsvColor.S) || (v != this.hsvColor.V))
                {
                    this.SetColorHSV(this.hsvColor.H, s, v);
                }
            }
        }

        private void paletteBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(Pens.Black, this.hsvColor.S - 5, (0xff - this.hsvColor.V) - 5, 10, 10);
            e.Graphics.DrawEllipse(Pens.White, (float)(this.hsvColor.S - 4.5f), (float)((0xff - this.hsvColor.V) - 4.5f), (float)9f, (float)9f);
        }

        public void SetColor(Color c)
        {
            this.rgbColor = c;
            this.hsvColor = GetHSV(this.rgbColor);
            this.colorRbox.Value = this.rgbColor.R;
            this.colorGbox.Value = this.rgbColor.G;
            this.colorBbox.Value = this.rgbColor.B;
            this.colorBox.BackColor = this.rgbColor;
            this.UpdatePalette();
        }

        public void SetColorHSV(int H, int S, int V)
        {
            this.hsvColor.H = H;
            this.hsvColor.S = S;
            this.hsvColor.V = V;
            this.rgbColor = HSVColor(H, S, V);
            this.colorRbox.Value = this.rgbColor.R;
            this.colorGbox.Value = this.rgbColor.G;
            this.colorBbox.Value = this.rgbColor.B;
            this.colorBox.BackColor = this.rgbColor;
            this.UpdatePalette();
        }

        public void SetColorRGB(int R, int G, int B)
        {
            this.rgbColor = Color.FromArgb(R, G, B);
            this.hsvColor = GetHSV(this.rgbColor);
            this.colorRbox.Value = this.rgbColor.R;
            this.colorGbox.Value = this.rgbColor.G;
            this.colorBbox.Value = this.rgbColor.B;
            this.colorBox.BackColor = this.rgbColor;
            this.UpdatePalette();
        }

        private void UpdatePalette()
        {
            for (int i = 0; i < (this.palette.Width - 2); i++)
            {
                Color introduced3 = HSVColor(this.hsvColor.H, i, 0xff);
                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(i, 0, 1, 0x100), introduced3, HSVColor(this.hsvColor.H, i, 0), 90f))
                {
                    using (Graphics graphics = Graphics.FromImage(this.palette))
                    {
                        graphics.FillRectangle(brush, i, 0, 1, 0x100);
                    }
                }
            }
            this.hexBox.Text = string.Format("{0:X2}{1:X2}{2:X2}", this.rgbColor.R, this.rgbColor.G, this.rgbColor.B);
            this.paletteBox.Invalidate();
            this.hueArrow.Invalidate();
            this.hueArrow2.Invalidate();
            base.Update();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HSV
        {
            public int H;
            public int S;
            public int V;
        }
    }
}

