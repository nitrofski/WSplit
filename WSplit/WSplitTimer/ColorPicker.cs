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
            this.paletteBox = new System.Windows.Forms.PictureBox();
            this.hueSlider = new System.Windows.Forms.PictureBox();
            this.hueArrow = new System.Windows.Forms.PictureBox();
            this.hueArrow2 = new System.Windows.Forms.PictureBox();
            this.colorRbox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorGbox = new System.Windows.Forms.NumericUpDown();
            this.colorBbox = new System.Windows.Forms.NumericUpDown();
            this.colorBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.hexBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueArrow2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // paletteBox
            // 
            this.paletteBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteBox.Location = new System.Drawing.Point(12, 11);
            this.paletteBox.Name = "paletteBox";
            this.paletteBox.Size = new System.Drawing.Size(256, 236);
            this.paletteBox.TabIndex = 0;
            this.paletteBox.TabStop = false;
            this.paletteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.paletteBox_Paint);
            this.paletteBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseDown);
            this.paletteBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseMove);
            // 
            // hueSlider
            // 
            this.hueSlider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hueSlider.Location = new System.Drawing.Point(278, 11);
            this.hueSlider.Name = "hueSlider";
            this.hueSlider.Size = new System.Drawing.Size(18, 236);
            this.hueSlider.TabIndex = 1;
            this.hueSlider.TabStop = false;
            this.hueSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hueSlider_MouseDown);
            this.hueSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hueSlider_MouseMove);
            // 
            // hueArrow
            // 
            this.hueArrow.Location = new System.Drawing.Point(273, 6);
            this.hueArrow.Name = "hueArrow";
            this.hueArrow.Size = new System.Drawing.Size(5, 246);
            this.hueArrow.TabIndex = 3;
            this.hueArrow.TabStop = false;
            this.hueArrow.Paint += new System.Windows.Forms.PaintEventHandler(this.hueArrow_Paint);
            // 
            // hueArrow2
            // 
            this.hueArrow2.Location = new System.Drawing.Point(296, 6);
            this.hueArrow2.Name = "hueArrow2";
            this.hueArrow2.Size = new System.Drawing.Size(5, 246);
            this.hueArrow2.TabIndex = 4;
            this.hueArrow2.TabStop = false;
            this.hueArrow2.Paint += new System.Windows.Forms.PaintEventHandler(this.hueArrow2_Paint);
            // 
            // colorRbox
            // 
            this.colorRbox.Location = new System.Drawing.Point(327, 44);
            this.colorRbox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorRbox.Name = "colorRbox";
            this.colorRbox.Size = new System.Drawing.Size(40, 19);
            this.colorRbox.TabIndex = 5;
            this.colorRbox.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorRbox.ValueChanged += new System.EventHandler(this.colorRbox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 44);
            this.label1.MinimumSize = new System.Drawing.Size(0, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "R";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 68);
            this.label2.MinimumSize = new System.Drawing.Size(0, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "G";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 92);
            this.label3.MinimumSize = new System.Drawing.Size(0, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "B";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // colorGbox
            // 
            this.colorGbox.Location = new System.Drawing.Point(327, 68);
            this.colorGbox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorGbox.Name = "colorGbox";
            this.colorGbox.Size = new System.Drawing.Size(40, 19);
            this.colorGbox.TabIndex = 9;
            this.colorGbox.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorGbox.ValueChanged += new System.EventHandler(this.colorGbox_ValueChanged);
            // 
            // colorBbox
            // 
            this.colorBbox.Location = new System.Drawing.Point(327, 92);
            this.colorBbox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorBbox.Name = "colorBbox";
            this.colorBbox.Size = new System.Drawing.Size(40, 19);
            this.colorBbox.TabIndex = 10;
            this.colorBbox.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorBbox.ValueChanged += new System.EventHandler(this.colorBbox_ValueChanged);
            // 
            // colorBox
            // 
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorBox.Location = new System.Drawing.Point(306, 11);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(61, 28);
            this.colorBox.TabIndex = 11;
            this.colorBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(306, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 21);
            this.button1.TabIndex = 13;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(306, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 21);
            this.button2.TabIndex = 14;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // hexBox
            // 
            this.hexBox.Location = new System.Drawing.Point(306, 152);
            this.hexBox.MaxLength = 6;
            this.hexBox.Name = "hexBox";
            this.hexBox.Size = new System.Drawing.Size(61, 19);
            this.hexBox.TabIndex = 12;
            this.hexBox.Text = "FFFFFF";
            this.hexBox.TextChanged += new System.EventHandler(this.hexBox_TextChanged);
            this.hexBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hexBox_KeyDown);
            this.hexBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hexBox_KeyPress);
            this.hexBox.Leave += new System.EventHandler(this.hexBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(303, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "16進数";
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 258);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hexBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.colorBbox);
            this.Controls.Add(this.colorGbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorRbox);
            this.Controls.Add(this.hueArrow2);
            this.Controls.Add(this.hueArrow);
            this.Controls.Add(this.hueSlider);
            this.Controls.Add(this.paletteBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorPicker";
            this.Text = "色選択";
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hueArrow2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

