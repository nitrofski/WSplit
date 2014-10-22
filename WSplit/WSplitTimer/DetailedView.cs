namespace WSplitTimer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Linq;
    using System.Windows.Forms;
    using Properties;

    public class DetailedView : Form
    {
        public const int HTCAPTION = 2;
        public const int HTCLIENT = 1;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int WM_NCHITTEST = 0x84;

        private IContainer components;

        private float plusPct = 0.5f;
        public int widthH = 1;
        public int widthHH = 1;
        public int widthHHH = 1;
        public int widthM = 1;
        public string clockText = "000:00:00.00";

        private WSplit parent;

        private ContextMenuStrip contextMenuStrip;

        private ToolStripMenuItem menuItemSelectColumns;
        private ToolStripMenuItem menuItemSetColors;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemShowSegs;
        private ToolStripMenuItem menuItemMarkSegments;
        private ToolStripMenuItem menuItemAlwaysOnTop;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItemClose;

        public Brush clockColor;
        public Font clockFont;

        public DataGridView segs;
        private DataGridViewTextBoxColumn SegName;
        private DataGridViewTextBoxColumn Old;
        private DataGridViewTextBoxColumn SumOfBests;
        private DataGridViewTextBoxColumn Best;
        private DataGridViewTextBoxColumn Live;
        private DataGridViewTextBoxColumn Delta;

        public DataGridView finalSeg;
        private DataGridViewTextBoxColumn finalSegName;
        private DataGridViewTextBoxColumn finalOld;
        private DataGridViewTextBoxColumn finalBest;
        private DataGridViewTextBoxColumn finalSumOfBests;
        private DataGridViewTextBoxColumn finalLive;
        private DataGridViewTextBoxColumn finalDelta;

        public List<PointF> deltaPoints = new List<PointF>();
        public List<double> Deltas = new List<double>();

        public Label displayTime;


        public DetailedView(Split useSplits, WSplit callingForm)
        {
            base.Paint += new PaintEventHandler(this.dviewPaint);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.InitializeComponent();
            this.parent = callingForm;
            this.menuItemShowSegs.Checked = Settings.Profile.DViewShowSegs;
            this.menuItemMarkSegments.Checked = Settings.Profile.DViewDeltaMarks;
            this.menuItemAlwaysOnTop.Checked = Settings.Profile.DViewOnTop;
            base.TopMost = Settings.Profile.DViewOnTop;
            this.updateColumns();
            this.clockFont = this.displayTime.Font;
        }

        public void InitializeFonts()
        {
            FontFamily family = FontFamily.Families.FirstOrDefault(f => f.Name == Settings.Profile.FontFamilySegments);

            if (family == null || !family.IsStyleAvailable(FontStyle.Bold))
                this.displayTime.Font = new Font(FontFamily.GenericSansSerif, 17.33333f, FontStyle.Bold, GraphicsUnit.Pixel);
            else
                this.displayTime.Font = new Font(family, 21f, FontStyle.Bold, GraphicsUnit.Pixel);

            family = FontFamily.Families.FirstOrDefault(f => f.Name == Settings.Profile.FontFamilyDView);
            Font font;
            if (family == null || !family.IsStyleAvailable(FontStyle.Regular))
                font = new Font(FontFamily.GenericSansSerif, 10.5f, FontStyle.Regular, GraphicsUnit.Pixel);
            else
                font = new Font(Settings.Profile.FontFamilyDView, 10.5f, FontStyle.Regular, GraphicsUnit.Pixel);

            foreach (DataGridViewColumn c in this.segs.Columns)
                c.DefaultCellStyle.Font = font;

            foreach (DataGridViewColumn c in this.finalSeg.Columns)
                c.DefaultCellStyle.Font = font;

            this.widthM = TextRenderer.MeasureText("00:00.00", this.displayTime.Font).Width;
            this.widthH = TextRenderer.MeasureText("0:00:00.00", this.displayTime.Font).Width;
            this.widthHH = TextRenderer.MeasureText("00:00:00.00", this.displayTime.Font).Width;
            this.widthHHH = TextRenderer.MeasureText("000:00:00.00", this.displayTime.Font).Width;
        }

        private void menuItemAlwaysOnTop_Click(object sender, EventArgs e)
        {
            Settings.Profile.DViewOnTop = !Settings.Profile.DViewOnTop;
            this.menuItemAlwaysOnTop.Checked = Settings.Profile.DViewOnTop;
            base.TopMost = Settings.Profile.DViewOnTop;
        }

        private void menuItemClose_click(object sender, EventArgs e)
        {
            base.Hide();
            this.parent.advancedDetailButton.Checked = false;
        }

        private void DetailedView_Resize(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dviewPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            Rectangle layoutRectangle = new Rectangle(this.displayTime.Left, this.displayTime.Top, this.displayTime.Width, this.displayTime.Height);
            if (this.clockText.Length == 8)
            {
                layoutRectangle.Width = this.widthM + 6;
            }
            else if (this.clockText.Length == 10)
            {
                layoutRectangle.Width = this.widthH + 6;
            }
            else if (this.clockText.Length == 11)
            {
                layoutRectangle.Width = this.widthHH + 6;
            }
            else if (this.clockText.Length == 12)
            {
                layoutRectangle.Width = this.widthHHH + 6;
            }
            e.Graphics.DrawString(this.clockText, this.displayTime.Font, this.clockColor, layoutRectangle, format);
            int right = layoutRectangle.Right;
            int y = layoutRectangle.Top + 4;
            int width = (base.Width - right) - 6;
            int height = (base.Height - y) - 6;
            if (width >= 30)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                Pen pen = new Pen(Brushes.White, 1f);
                Pen pen2 = new Pen(new SolidBrush(Color.FromArgb(0x40, 0, 0, 0)), 1f);
                Pen pen3 = new Pen(new SolidBrush(Color.FromArgb(0x80, 0xff, 0xff, 0xff)), 1f);
                float num5 = height - (height * this.plusPct);
                e.Graphics.FillRectangle(new SolidBrush(ColorSettings.Profile.GraphBehind), (float)right, (float)y, (float)width, num5);
                e.Graphics.FillRectangle(new SolidBrush(ColorSettings.Profile.GraphAhead), (float)right, y + num5, (float)width, height - num5);
                for (int i = 1; i <= (width / 7); i++)
                {
                    PointF tf = new PointF((float)(right + (7 * i)), (float)y);
                    PointF tf2 = new PointF(tf.X, (float)(y + height));
                    e.Graphics.DrawLine(pen2, tf, tf2);
                }
                for (int j = 1; j <= (height / 7); j++)
                {
                    PointF tf3 = new PointF((float)right, (float)(y + (7 * j)));
                    PointF tf4 = new PointF((float)(right + width), tf3.Y);
                    e.Graphics.DrawLine(pen2, tf3, tf4);
                }
                e.Graphics.DrawRectangle(pen3, new Rectangle(right, y, width, height));
                if (this.deltaPoints.Count >= 1)
                {
                    List<PointF> list = new List<PointF> {
                        new PointF((float) right, y + num5)
                    };
                    foreach (PointF tf5 in this.deltaPoints)
                    {
                        float x = (tf5.X * width) + right;
                        float num9 = (height - (tf5.Y * height)) + y;
                        if (Settings.Profile.DViewDeltaMarks)
                        {
                            e.Graphics.FillEllipse(Brushes.White, (float)(x - 2f), (float)(num9 - 2f), (float)4f, (float)4f);
                        }
                        list.Add(new PointF(x, num9));
                    }
                    e.Graphics.DrawLines(pen, list.ToArray());
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();

            this.contextMenuStrip = new ContextMenuStrip(this.components);

            this.menuItemSelectColumns = new ToolStripMenuItem();
            this.menuItemSetColors = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.menuItemShowSegs = new ToolStripMenuItem();
            this.menuItemMarkSegments = new ToolStripMenuItem();
            this.menuItemAlwaysOnTop = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.menuItemClose = new ToolStripMenuItem();

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            this.segs = new DataGridView();
            this.SegName = new DataGridViewTextBoxColumn();
            this.Old = new DataGridViewTextBoxColumn();
            this.Best = new DataGridViewTextBoxColumn();
            this.SumOfBests = new DataGridViewTextBoxColumn();
            this.Live = new DataGridViewTextBoxColumn();
            this.Delta = new DataGridViewTextBoxColumn();
            this.displayTime = new Label();
            this.finalSeg = new DataGridView();
            this.finalSegName = new DataGridViewTextBoxColumn();
            this.finalOld = new DataGridViewTextBoxColumn();
            this.finalBest = new DataGridViewTextBoxColumn();
            this.finalSumOfBests = new DataGridViewTextBoxColumn();
            this.finalLive = new DataGridViewTextBoxColumn();
            this.finalDelta = new DataGridViewTextBoxColumn();
            
            ((ISupportInitialize)this.segs).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((ISupportInitialize)this.finalSeg).BeginInit();
            base.SuspendLayout();
            //
            // contextMenuStrip
            //
            this.contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                this.menuItemSelectColumns,
                this.menuItemSetColors,
                this.toolStripSeparator1,
                this.menuItemShowSegs,
                this.menuItemMarkSegments,
                this.menuItemAlwaysOnTop,
                this.toolStripSeparator2,
                this.menuItemClose
            });
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new Size(280, 136);
            //
            // menuItemSelectColumns
            //
            this.menuItemSelectColumns.Name = "menuItemSelectColumns";
            this.menuItemSelectColumns.Size = new Size(280, 24);
            this.menuItemSelectColumns.Text = "Select columns...";
            this.menuItemSelectColumns.Click += this.menuItemSelectColumns_Click;
            //
            //
            //
            this.menuItemSetColors.Name = "menuItemSetColors";
            this.menuItemSetColors.Size = new Size(280, 24);
            this.menuItemSetColors.Text = "Set colors...";
            this.menuItemSetColors.Click += this.menuItemSetColors_Click;
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(276, 6);
            //
            // menuItemShowSegs
            //
            this.menuItemShowSegs.Name = "menuItemShowSegs";
            this.menuItemShowSegs.Size = new Size(280, 24);
            this.menuItemShowSegs.Text = "Show segment times";
            this.menuItemShowSegs.Click += this.menuItemShowSegs_Click;
            //
            // menuItemMarkSegments
            //
            this.menuItemMarkSegments.Name = "menuItemMarkSegments";
            this.menuItemMarkSegments.Size = new Size(280, 24);
            this.menuItemMarkSegments.Text = "Mark segments on delta graph";
            this.menuItemMarkSegments.Click += this.menuItemMarkSegments_Click;
            //
            // menuItemAlwaysOnTop
            //
            this.menuItemAlwaysOnTop.Name = "menuItemAlwaysOnTop";
            this.menuItemAlwaysOnTop.Size = new Size(280, 24);
            this.menuItemAlwaysOnTop.Text = "Always on top";
            this.menuItemAlwaysOnTop.Click += this.menuItemAlwaysOnTop_Click;
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(276, 6);
            //
            // menuItemClose
            //
            this.menuItemClose.Name = "menuItemClose";
            this.menuItemClose.Size = new Size(280, 24);
            this.menuItemClose.Text = "Close";
            this.menuItemClose.Click += this.menuItemClose_click;

            this.segs.AllowUserToAddRows = false;
            this.segs.AllowUserToDeleteRows = false;
            this.segs.AllowUserToResizeColumns = false;
            this.segs.AllowUserToResizeRows = false;
            style.BackColor = Color.Black;
            style.SelectionBackColor = Color.Black;
            this.segs.AlternatingRowsDefaultCellStyle = style;
            this.segs.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.segs.BackgroundColor = Color.Black;
            this.segs.BorderStyle = BorderStyle.None;
            this.segs.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.segs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.segs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.segs.ColumnHeadersVisible = false;
            this.segs.Columns.AddRange(new DataGridViewColumn[] { this.SegName, this.Old, this.Best, this.SumOfBests, this.Live, this.Delta });
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = Color.Black;
            style2.ForeColor = Color.WhiteSmoke;
            style2.SelectionBackColor = Color.Black;
            style2.SelectionForeColor = Color.WhiteSmoke;
            style2.WrapMode = DataGridViewTriState.False;
            this.segs.DefaultCellStyle = style2;
            this.segs.Enabled = false;
            this.segs.GridColor = Color.Black;
            this.segs.Location = new Point(0, 0);
            this.segs.Margin = new Padding(0);
            this.segs.MultiSelect = false;
            this.segs.Name = "segs";
            this.segs.ReadOnly = true;
            this.segs.RowHeadersVisible = false;
            this.segs.RowTemplate.Height = 0x10;
            this.segs.ScrollBars = ScrollBars.None;
            this.segs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.segs.Size = new Size(0xad, 12);
            this.segs.TabIndex = 0;
            this.SegName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.SegName.HeaderText = "Segment";
            this.SegName.Name = "SegName";
            this.SegName.ReadOnly = true;
            this.Old.HeaderText = "Old";
            this.Old.MinimumWidth = 2;
            this.Old.Name = "Old";
            this.Old.ReadOnly = true;
            this.Old.Width = 2;
            this.Best.HeaderText = "Best";
            this.Best.MinimumWidth = 2;
            this.Best.Name = "Best";
            this.Best.ReadOnly = true;
            this.Best.Width = 2;
            this.SumOfBests.HeaderText = "SoB";
            this.SumOfBests.MinimumWidth = 2;
            this.SumOfBests.Name = "SumOfBests";
            this.SumOfBests.ReadOnly = true;
            this.SumOfBests.Width = 2;
            this.Live.HeaderText = "Live";
            this.Live.MinimumWidth = 2;
            this.Live.Name = "Live";
            this.Live.ReadOnly = true;
            this.Live.Width = 2;
            style3.Alignment = DataGridViewContentAlignment.BottomRight;
            this.Delta.DefaultCellStyle = style3;
            this.Delta.HeaderText = "Delta";
            this.Delta.MinimumWidth = 2;
            this.Delta.Name = "Delta";
            this.Delta.ReadOnly = true;
            this.Delta.Width = 2;
            this.displayTime.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.displayTime.AutoSize = true;
            this.displayTime.BackColor = Color.Black;
            this.displayTime.ForeColor = Color.PaleGoldenrod;
            this.displayTime.Location = new Point(0, 0x2e);
            this.displayTime.Margin = new Padding(0);
            this.displayTime.MinimumSize = new Size(0, 0x22);
            this.displayTime.Name = "displayTime";
            this.displayTime.Size = new Size(0x81, 0x22);
            this.displayTime.TabIndex = 2;
            this.displayTime.Text = "000:00:00.00";
            this.displayTime.TextAlign = ContentAlignment.MiddleLeft;
            this.displayTime.Visible = false;
            this.finalSeg.AllowUserToAddRows = false;
            this.finalSeg.AllowUserToDeleteRows = false;
            this.finalSeg.AllowUserToResizeColumns = false;
            this.finalSeg.AllowUserToResizeRows = false;
            style4.BackColor = Color.Black;
            style4.SelectionBackColor = Color.Black;
            this.finalSeg.AlternatingRowsDefaultCellStyle = style4;
            this.finalSeg.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.finalSeg.BackgroundColor = Color.Black;
            this.finalSeg.BorderStyle = BorderStyle.None;
            this.finalSeg.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.finalSeg.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.finalSeg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.finalSeg.ColumnHeadersVisible = false;
            this.finalSeg.Columns.AddRange(new DataGridViewColumn[] { this.finalSegName, this.finalOld, this.finalBest, this.finalSumOfBests, this.finalLive, this.finalDelta });
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style5.BackColor = Color.Black;
            style5.ForeColor = Color.WhiteSmoke;
            style5.SelectionBackColor = Color.Black;
            style5.SelectionForeColor = Color.WhiteSmoke;
            style5.WrapMode = DataGridViewTriState.False;
            this.finalSeg.DefaultCellStyle = style5;
            this.finalSeg.Enabled = false;
            this.finalSeg.GridColor = Color.Black;
            this.finalSeg.Location = new Point(0, 12);
            this.finalSeg.Margin = new Padding(0);
            this.finalSeg.MultiSelect = false;
            this.finalSeg.Name = "finalSeg";
            this.finalSeg.ReadOnly = true;
            this.finalSeg.RowHeadersVisible = false;
            this.finalSeg.RowTemplate.Height = 0x10;
            this.finalSeg.ScrollBars = ScrollBars.None;
            this.finalSeg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.finalSeg.Size = new Size(0xad, 12);
            this.finalSeg.TabIndex = 3;
            this.finalSegName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.finalSegName.HeaderText = "Segment";
            this.finalSegName.Name = "finalSegName";
            this.finalSegName.ReadOnly = true;
            this.finalOld.HeaderText = "Old";
            this.finalOld.MinimumWidth = 2;
            this.finalOld.Name = "finalOld";
            this.finalOld.ReadOnly = true;
            this.finalOld.Width = 2;
            this.finalBest.HeaderText = "Best";
            this.finalBest.MinimumWidth = 2;
            this.finalBest.Name = "finalBest";
            this.finalBest.ReadOnly = true;
            this.finalBest.Width = 2;
            this.finalSumOfBests.HeaderText = "Sum of Bests";
            this.finalSumOfBests.MinimumWidth = 2;
            this.finalSumOfBests.Name = "finalSumOfBests";
            this.finalSumOfBests.ReadOnly = true;
            this.finalSumOfBests.Width = 2;
            this.finalLive.HeaderText = "Live";
            this.finalLive.MinimumWidth = 2;
            this.finalLive.Name = "finalLive";
            this.finalLive.ReadOnly = true;
            this.finalLive.Width = 2;
            style6.Alignment = DataGridViewContentAlignment.BottomRight;
            this.finalDelta.DefaultCellStyle = style6;
            this.finalDelta.HeaderText = "Delta";
            this.finalDelta.MinimumWidth = 2;
            this.finalDelta.Name = "finalDelta";
            this.finalDelta.ReadOnly = true;
            this.finalDelta.Width = 2;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Black;
            base.ClientSize = new Size(0xad, 80);
            this.ContextMenuStrip = this.contextMenuStrip;
            base.Controls.Add(this.segs);
            base.Controls.Add(this.finalSeg);
            base.Controls.Add(this.displayTime);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.AppIcon;
            base.Margin = new Padding(4, 4, 4, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x6b, 0);
            base.Name = "DetailedView";
            this.Text = "Detailed View";
            base.Resize += new EventHandler(this.DetailedView_Resize);
            ((ISupportInitialize)this.segs).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((ISupportInitialize)this.finalSeg).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void menuItemMarkSegments_Click(object sender, EventArgs e)
        {
            Settings.Profile.DViewDeltaMarks = !Settings.Profile.DViewDeltaMarks;
            this.menuItemMarkSegments.Checked = Settings.Profile.DViewDeltaMarks;
            base.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return this.parent.timerHotkey(keyData);
        }

        public void resetHeight()
        {
            this.finalSeg.Top = this.segs.Top + this.segs.Height;
            base.Height = this.finalSeg.Bottom + 0x22;
        }

        private void menuItemSelectColumns_Click(object sender, EventArgs e)
        {
            DViewSetColumnsDialog dialog = new DViewSetColumnsDialog();
            base.TopMost = false;
            this.parent.TopMost = false;
            this.parent.modalWindowOpened = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.parent.updateDetailed();
            }
            this.parent.TopMost = Settings.Profile.OnTop;
            base.TopMost = Settings.Profile.DViewOnTop;
            this.parent.modalWindowOpened = false;
        }

        private void menuItemSetColors_Click(object sender, EventArgs e)
        {
            CustomizeColors colorDialog = new CustomizeColors(true);
            this.parent.TopMost = false;
            base.TopMost = false;
            this.parent.modalWindowOpened = true;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
                this.parent.updateDetailed();

            this.parent.TopMost = Settings.Profile.OnTop;
            base.TopMost = Settings.Profile.DViewOnTop;
            this.parent.modalWindowOpened = false;
        }

        public void setDeltaPoints()
        {
            this.deltaPoints.Clear();
            double num = 0.0;
            double num2 = 0.0;
            foreach (double num3 in this.Deltas)
            {
                num = Math.Max(num3, num);
                num2 = Math.Min(num3, num2);
            }
            double num4 = this.parent.split.CompTime(this.parent.split.LastIndex);

            // "Temporary"? fix for the bug described below
            if (num4 != 0.0)
            {
                for (int i = 0; (i < this.Deltas.Count) && (i <= this.parent.split.LastIndex); i++)
                {
                    if ((this.parent.split.segments[i].LiveTime != 0.0) && (this.parent.split.CompTime(i) != 0.0))
                    {
                        // This next line causes a graphic crash if the last segment is empty and the segment i is not.
                        float x = (float)(this.parent.split.CompTime(i) / num4);
                        float y = 0.5f;
                        if ((num - num2) > 0.0)
                        {
                            y = (float)((this.Deltas[i] - num2) / (num - num2));
                        }
                        this.deltaPoints.Add(new PointF(x, y));
                    }
                }
            }

            if ((num - num2) > 0.0)
            {
                this.plusPct = (float)((0.0 - num2) / (num - num2));
            }
            else
            {
                this.plusPct = 0.5f;
            }
        }

        private void menuItemShowSegs_Click(object sender, EventArgs e)
        {
            Settings.Profile.DViewShowSegs = !Settings.Profile.DViewShowSegs;
            this.menuItemShowSegs.Checked = Settings.Profile.DViewShowSegs;
            this.parent.updateDetailed();
        }

        public void updateColumns()
        {
            int num = 0x2e;
            if ((this.segs.RowCount > 0) && (this.finalSeg.RowCount > 1))
            {
                if (Settings.Profile.DViewShowSegs)
                {
                    this.segs.Rows[0].Cells[2].Value = "Best [Seg]";
                    this.segs.Rows[0].Cells[4].Value = "Live [Seg]";
                }
                else
                {
                    this.segs.Rows[0].Cells[2].Value = "Best";
                    this.segs.Rows[0].Cells[4].Value = "Live";
                }
                
                // The detailed view used to only show a column if it had an ending time. I decided to change it, because
                // the user can still decide to show a column or not manually.
                if (Settings.Profile.DViewShowOld || (Settings.Profile.DViewShowComp && this.parent.split.ComparingType == Split.CompareType.Old))
                {
                    this.segs.Columns[1].Visible = true;
                    num += 0x22;
                }
                else
                    this.segs.Columns[1].Visible = false;

                if (Settings.Profile.DViewShowBest || (Settings.Profile.DViewShowComp && this.parent.split.ComparingType == Split.CompareType.Best))
                {
                    this.segs.Columns[2].Visible = true;
                    num += 0x22;
                    if (Settings.Profile.DViewShowSegs)
                    {
                        num += 0x24;
                    }
                }
                else
                    this.segs.Columns[2].Visible = false;

                if (Settings.Profile.DViewShowSumOfBests || (Settings.Profile.DViewShowComp && this.parent.split.ComparingType == Split.CompareType.SumOfBests))
                {
                    this.segs.Columns[3].Visible = true;
                    num += 0x22;
                }
                else
                    this.segs.Columns[3].Visible = false;

                this.segs.Columns[4].Visible = Settings.Profile.DViewShowLive;
                if (this.segs.Columns[4].Visible)
                {
                    num += 0x22;
                    if (Settings.Profile.DViewShowSegs)
                        num += 0x24;
                }

                // Again, the deltas used to only show if the comparison time had an ending time. It got changed.
                this.segs.Columns[5].Visible = Settings.Profile.DViewShowDeltas;
                if (this.segs.Columns[5].Visible)
                    num += 0x20;
            }
            num = Math.Max(num, this.displayTime.Width);
            if (base.Size.Width == this.MinimumSize.Width)
            {
                this.MinimumSize = new Size(num, 0);
                base.Width = this.MinimumSize.Width;
            }
            else
            {
                int num2 = base.Width - this.MinimumSize.Width;
                this.MinimumSize = new Size(num, 0);
                base.Width = this.MinimumSize.Width + num2;
            }
            foreach (DataGridViewColumn column in this.finalSeg.Columns)
            {
                column.Visible = this.segs.Columns[column.Index].Visible;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                base.WndProc(ref m);
                Point point = base.PointToClient(new Point(m.LParam.ToInt32()));
                if ((point.X <= 5) && (point.X >= 0))
                {
                    m.Result = (IntPtr)10;
                }
                else if ((point.X >= (base.ClientSize.Width - 5)) && (point.X <= base.ClientSize.Width))
                {
                    m.Result = (IntPtr)11;
                }
                else if ((Control.MouseButtons != MouseButtons.Right) && (((int)m.Result) == 1))
                {
                    m.Result = (IntPtr)2;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}

