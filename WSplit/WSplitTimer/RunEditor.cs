namespace WSplitTimer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Properties;
    using System.Text;
    using System.Globalization;

    public class RunEditorDialog : Form
    {
        public TextBox attemptsBox;
        private DataGridViewTextBoxColumn best;
        private DataGridViewTextBoxColumn bseg;
        private int cellHeight;
        private Button discardButton;
        private Control eCtl;
        public List<Segment> editList = new List<Segment>();
        private DataGridViewImageColumn icon;
        private DataGridViewTextBoxColumn iconPath;
        private Button insertButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button offsetButton;
        private DataGridViewTextBoxColumn old;
        private TextBox oldOffset;
        private OpenFileDialog openIconDialog;
        private Button resetButton;
        private DataGridView runView;
        private Button saveButton;
        private DataGridViewTextBoxColumn segment;
        public TextBox titleBox;

        private Button buttonAutoFillBests;
        private Button buttonImport;
        private ContextMenuStrip contextMenuImport;
        private ToolStripMenuItem menuItemImportLlanfair;
        private ToolStripMenuItem menuItemImportSplitterZ;

        private OpenFileDialog openFileDialog;
        private IContainer components;
        public TextBox txtGoal;
        private Label lblGoal;

        private int windowHeight;

        public RunEditorDialog(Split splits)
        {
            this.InitializeComponent();
            this.cellHeight = this.runView.RowTemplate.Height;
            this.windowHeight = (base.Height - (this.runView.Height - this.cellHeight)) - 2;
            this.MaximumSize = new Size(500, (15 * this.cellHeight) + this.windowHeight);

            foreach (Segment segment in splits.segments)
                this.editList.Add(segment);

            this.populateList(this.editList);
            this.runView.EditingControlShowing += this.runView_EditingControlShowing;
        }

        private void attemptsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void attemptsBox_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(this.attemptsBox.Text, "[^0-9]"))
                this.attemptsBox.Text = Regex.Replace(this.attemptsBox.Text, "[^0-9]", "");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void eCtl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.runView.CurrentCell.ColumnIndex == 0)
            {
                if (e.KeyChar == ',')
                    e.Handled = true;
            }
            else if (((!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) && ((e.KeyChar != ':') && (e.KeyChar != '.'))) && (e.KeyChar != ','))
                e.Handled = true;
        }

        private void eCtl_TextChanged(object sender, EventArgs e)
        {
            if (this.runView.CurrentCell.ColumnIndex == 0)
            {
                if (Regex.IsMatch(this.eCtl.Text, ","))
                    this.eCtl.Text = Regex.Replace(this.eCtl.Text, ",", "");
            }
            else if (Regex.IsMatch(this.eCtl.Text, "[^0-9:.,]"))
                this.eCtl.Text = Regex.Replace(this.eCtl.Text, "[^0-9:.,]", "");
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.runView = new System.Windows.Forms.DataGridView();
            this.segment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.old = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.best = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bseg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iconPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.saveButton = new System.Windows.Forms.Button();
            this.discardButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.oldOffset = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.offsetButton = new System.Windows.Forms.Button();
            this.titleBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.insertButton = new System.Windows.Forms.Button();
            this.openIconDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.attemptsBox = new System.Windows.Forms.TextBox();
            this.buttonAutoFillBests = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.contextMenuImport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemImportLlanfair = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemImportSplitterZ = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtGoal = new System.Windows.Forms.TextBox();
            this.lblGoal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.runView)).BeginInit();
            this.contextMenuImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // runView
            // 
            this.runView.AllowUserToResizeRows = false;
            this.runView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.runView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.runView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.runView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.runView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.runView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.segment,
            this.old,
            this.best,
            this.bseg,
            this.iconPath,
            this.icon});
            this.runView.Location = new System.Drawing.Point(12, 58);
            this.runView.Name = "runView";
            this.runView.RowHeadersVisible = false;
            this.runView.Size = new System.Drawing.Size(359, 42);
            this.runView.TabIndex = 0;
            this.runView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.runView_CellDoubleClick);
            this.runView.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.runView_UserAddedRow);
            this.runView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.runView_UserDeletedRow);
            this.runView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.runView_KeyDown);
            // 
            // segment
            // 
            this.segment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.segment.HeaderText = "Segment";
            this.segment.Name = "segment";
            this.segment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // old
            // 
            this.old.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.old.HeaderText = "Old Time";
            this.old.Name = "old";
            this.old.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.old.Width = 55;
            // 
            // best
            // 
            this.best.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.best.HeaderText = "Best Time";
            this.best.Name = "best";
            this.best.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.best.Width = 60;
            // 
            // bseg
            // 
            this.bseg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.bseg.HeaderText = "Best Seg.";
            this.bseg.Name = "bseg";
            this.bseg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bseg.Width = 59;
            // 
            // iconPath
            // 
            this.iconPath.HeaderText = "Icon Path";
            this.iconPath.Name = "iconPath";
            this.iconPath.Visible = false;
            // 
            // icon
            // 
            this.icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.icon.HeaderText = "Icon";
            this.icon.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.icon.MinimumWidth = 40;
            this.icon.Name = "icon";
            this.icon.ReadOnly = true;
            this.icon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.icon.Width = 40;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(266, 136);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(50, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // discardButton
            // 
            this.discardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.discardButton.DialogResult = DialogResult.Cancel;
            this.discardButton.Location = new System.Drawing.Point(322, 136);
            this.discardButton.Name = "discardButton";
            this.discardButton.Size = new System.Drawing.Size(50, 23);
            this.discardButton.TabIndex = 2;
            this.discardButton.Text = "Cancel";
            this.discardButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.Location = new System.Drawing.Point(266, 106);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(106, 23);
            this.resetButton.TabIndex = 3;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // oldOffset
            // 
            this.oldOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.oldOffset.Location = new System.Drawing.Point(230, 31);
            this.oldOffset.Name = "oldOffset";
            this.oldOffset.Size = new System.Drawing.Size(86, 20);
            this.oldOffset.TabIndex = 5;
            this.oldOffset.TextChanged += new System.EventHandler(this.oldOffset_TextChanged);
            this.oldOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oldOffset_KeyDown);
            this.oldOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.oldOffset_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Old time offset:";
            // 
            // offsetButton
            // 
            this.offsetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetButton.Location = new System.Drawing.Point(322, 29);
            this.offsetButton.Name = "offsetButton";
            this.offsetButton.Size = new System.Drawing.Size(50, 23);
            this.offsetButton.TabIndex = 7;
            this.offsetButton.Text = "Apply";
            this.offsetButton.UseVisualStyleBackColor = true;
            this.offsetButton.Click += new System.EventHandler(this.offsetButton_Click);
            // 
            // titleBox
            // 
            this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBox.Location = new System.Drawing.Point(12, 31);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(106, 20);
            this.titleBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Run title:";
            // 
            // insertButton
            // 
            this.insertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertButton.Location = new System.Drawing.Point(12, 136);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(50, 23);
            this.insertButton.TabIndex = 10;
            this.insertButton.Text = "Insert";
            this.insertButton.UseVisualStyleBackColor = true;
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // openIconDialog
            // 
            this.openIconDialog.Filter = "Image files (*.bmp; *.gif; *.jpg; *.png; *.tiff)|*.bmp;*.gif;*.jpg;*.png;*.tiff";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Attempts:";
            // 
            // attemptsBox
            // 
            this.attemptsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.attemptsBox.Location = new System.Drawing.Point(125, 138);
            this.attemptsBox.Name = "attemptsBox";
            this.attemptsBox.Size = new System.Drawing.Size(40, 20);
            this.attemptsBox.TabIndex = 13;
            this.attemptsBox.Text = "0";
            this.attemptsBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attemptsBox.TextChanged += new System.EventHandler(this.attemptsBox_TextChanged);
            this.attemptsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.attemptsBox_KeyPress);
            // 
            // buttonAutoFillBests
            // 
            this.buttonAutoFillBests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAutoFillBests.Location = new System.Drawing.Point(88, 106);
            this.buttonAutoFillBests.Name = "buttonAutoFillBests";
            this.buttonAutoFillBests.Size = new System.Drawing.Size(120, 23);
            this.buttonAutoFillBests.TabIndex = 4;
            this.buttonAutoFillBests.Text = "Auto-fill best segments";
            this.buttonAutoFillBests.UseVisualStyleBackColor = true;
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImport.Location = new System.Drawing.Point(12, 106);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(70, 23);
            this.buttonImport.TabIndex = 5;
            this.buttonImport.Text = "Import... ▼";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // contextMenuImport
            // 
            this.contextMenuImport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemImportLlanfair,
            this.menuItemImportSplitterZ});
            this.contextMenuImport.Name = "contextMenuImport";
            this.contextMenuImport.Size = new System.Drawing.Size(182, 26);
            // 
            // menuItemImportLlanfair
            // 
            this.menuItemImportLlanfair.Name = "menuItemImportLlanfair";
            this.menuItemImportLlanfair.Size = new System.Drawing.Size(181, 22);
            this.menuItemImportLlanfair.Text = "Import from Llanfair";
            // 
            // menuItemImportSplitterZ
            // 
            this.menuItemImportSplitterZ.Name = "menuItemImportSplitterZ";
            this.menuItemImportSplitterZ.Size = new System.Drawing.Size(32, 19);
            this.menuItemImportSplitterZ.Text = "Import from SplitterZ";
            // 
            // txtGoal
            // 
            this.txtGoal.Location = new System.Drawing.Point(124, 31);
            this.txtGoal.Name = "txtGoal";
            this.txtGoal.Size = new System.Drawing.Size(100, 20);
            this.txtGoal.TabIndex = 14;
            // 
            // lblGoal
            // 
            this.lblGoal.AutoSize = true;
            this.lblGoal.Location = new System.Drawing.Point(125, 15);
            this.lblGoal.Name = "lblGoal";
            this.lblGoal.Size = new System.Drawing.Size(35, 13);
            this.lblGoal.TabIndex = 15;
            this.lblGoal.Text = "Goal :";

            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(384, 171);
            base.Controls.Add(this.lblGoal);
            base.Controls.Add(this.txtGoal);
            base.Controls.Add(this.attemptsBox);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.insertButton);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.titleBox);
            base.Controls.Add(this.offsetButton);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.oldOffset);
            base.Controls.Add(this.resetButton);
            base.Controls.Add(this.discardButton);
            base.Controls.Add(this.saveButton);
            base.Controls.Add(this.runView);
            base.Controls.Add(this.buttonAutoFillBests);
            base.Controls.Add(this.buttonImport);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            this.MinimumSize = new Size(390, 120);
            base.Name = "RunEditorDialog";
            this.Text = "Run Editor";
            base.Shown += new EventHandler(this.RunEditor_Shown);
            ((ISupportInitialize)this.runView).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();

        }

        private void menuItemImportSplitterZ_Click(object sender, EventArgs e)
        {
            // Imports a file from a SplitterZ run file
            if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using (FileStream stream = File.OpenRead(this.openFileDialog.FileName))
                    {
                        var reader = new StreamReader(stream);

                        var newLine = reader.ReadLine();
                        var title = newLine.Split(',');
                        titleBox.Text = title[0].Replace(@"‡", @",");

                        List<Segment> segmentList = new List<Segment>();

                        while ((newLine = reader.ReadLine()) != null)
                        {
                            var segmentInfo = newLine.Split(',');
                            var name = segmentInfo[0].Replace(@"‡", @",");
                            double splitTime = timeParse(segmentInfo[1]);
                            double bestSegment = timeParse(segmentInfo[2]);

                            var newSegment = new Segment(name, 0.0, splitTime, bestSegment);
                            if (segmentInfo.Length > 3)
                            {
                                newSegment.IconPath = segmentInfo[3].Replace(@"‡", @",");
                                newSegment.Icon = Image.FromFile(newSegment.IconPath);
                            }
                            segmentList.Add(newSegment);
                        }
                        populateList(segmentList);
                    }
                }
                catch (Exception)
                {
                    // An error has occured...
                }
            }
        }

        private void menuItemImportLlanfair_Click(object sender, EventArgs e)
        {
            // Imports a file from a Llanfair run file
            if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using (FileStream stream = File.OpenRead(this.openFileDialog.FileName))
                    {
                        Int16 strLength;
                        byte[] buffer = new byte[128];

                        // Finds the goal string in the file
                        stream.Seek(0xC5, SeekOrigin.Current);  // Skips to the length of the goal string

                        stream.Read(buffer, 0, 2);
                        strLength = BitConverter.ToInt16(this.toConverterEndianness(buffer, 0, 2), 0);
                        stream.Read(buffer, 0, strLength);

                        string strGoal = Encoding.UTF8.GetString(buffer, 0, strLength);

                        // Finds the title string in the file
                        stream.Seek(0x1, SeekOrigin.Current);   // Skips to the length of the title string

                        stream.Read(buffer, 0, 2);
                        strLength = BitConverter.ToInt16(this.toConverterEndianness(buffer, 0, 2), 0);
                        stream.Read(buffer, 0, strLength);

                        string strTitle = Encoding.UTF8.GetString(buffer, 0, strLength);

                        // Finds the number of elements in the segment list
                        stream.Seek(0x6, SeekOrigin.Current);

                        stream.Read(buffer, 0, 4);

                        Int32 segmentListCount = BitConverter.ToInt32(this.toConverterEndianness(buffer, 0, 4), 0);
                        
                        // The object header changes if there is no instance of one of the object used by the Run class.
                        // The 2 objects that can be affected are the Time object and the ImageIcon object.
                        // The next step of the import algorythm is to check for their presence.
                        bool timeObjectDeclarationEncountered = false;
                        bool iconObjectDeclarationEncountered = false;

                        List<Segment> segmentList = new List<Segment>();

                        // Seeks to the first byte of the first segment
                        stream.Seek(0x8F, SeekOrigin.Current);
                        for (int i = 0; i < segmentListCount; ++i)
                        {
                            Int64 bestSegmentMillis = 0;
                            stream.Read(buffer, 0, 1);
                            if (buffer[0] != 0x70)
                            {
                                if (!timeObjectDeclarationEncountered)
                                {
                                    timeObjectDeclarationEncountered = true;

                                    // Seek past the object declaration
                                    stream.Seek(0x36, SeekOrigin.Current);
                                }
                                else
                                    stream.Seek(0x5, SeekOrigin.Current);

                                // Read the remaining 7 bytes of data in the buffer:
                                stream.Read(buffer, 0, 8);
                                bestSegmentMillis = BitConverter.ToInt64(this.toConverterEndianness(buffer, 0, 8), 0);
                            }

                            stream.Read(buffer, 0, 1);
                            if (buffer[0] != 0x70)
                            {
                                long seekOffsetBase;
                                if (!iconObjectDeclarationEncountered)
                                {
                                    iconObjectDeclarationEncountered = true;

                                    stream.Seek(0xBC, SeekOrigin.Current);
                                    seekOffsetBase = 0x25;
                                }
                                else
                                {
                                    stream.Seek(0x5, SeekOrigin.Current);
                                    seekOffsetBase = 0x18;
                                }

                                stream.Read(buffer, 0, 8);
                                Int32 iconHeight = BitConverter.ToInt32(this.toConverterEndianness(buffer, 0, 4), 0);
                                Int32 iconWidth = BitConverter.ToInt32(this.toConverterEndianness(buffer, 4, 4), 4);

                                // Seek past the image:
                                stream.Seek(seekOffsetBase + (iconHeight * iconWidth * 4), SeekOrigin.Current);
                            }

                            // Finds the name of the segment (can't be empty)
                            stream.Seek(0x1, SeekOrigin.Current);   // Skip to the length of the name string
                            stream.Read(buffer, 0, 2);
                            strLength = BitConverter.ToInt16(this.toConverterEndianness(buffer, 0, 2), 0);
                            stream.Read(buffer, 0, strLength);

                            string name = Encoding.UTF8.GetString(buffer, 0, strLength);

                            // Finds the best time of the segment
                            Int64 bestTimeMillis = 0;
                            stream.Read(buffer, 0, 1);
                            if (buffer[0] == 0x71)
                            {
                                stream.Seek(0x4, SeekOrigin.Current);
                                bestTimeMillis = bestSegmentMillis;
                            }
                            else if (buffer[0] != 0x70)
                            {
                                // Since there is always a best segment when there is a best time in Llanfair,
                                // I assume that there will never be another Time object declaration before this data.
                                stream.Seek(0x5, SeekOrigin.Current);
                                stream.Read(buffer, 0, 8);
                                bestTimeMillis = BitConverter.ToInt64(this.toConverterEndianness(buffer, 0, 8), 0);
                            }

                            double bestTime = bestTimeMillis / 1000.0;

                            if (bestTimeMillis != 0)
                            {
                                for (int j = i - 1; j >= 0; --j)
                                {
                                    if (segmentList[j].BestTime != 0.0)
                                    {
                                        bestTime += segmentList[j].BestTime;
                                        break;
                                    }
                                }
                            }

                            segmentList.Add(new Segment(name, 0.0, bestTime, bestSegmentMillis / 1000.0));

                            // Seek to the beginning of the next segment name
                            stream.Seek(0x6, SeekOrigin.Current);
                        }

                        // The only remaining thing in the file should be the window height and width for Llanfair usage.
                        // We don't need to extract it.

                        if (strGoal == "")
                            ;
                        
                        this.populateList(segmentList);
                        this.titleBox.Text = strTitle;
                    }
                }
                catch (Exception)
                {
                    // An error has occured...
                }
            }
        }

        private byte[] toConverterEndianness(byte[] array, int offset, int length)
        {
            if (BitConverter.IsLittleEndian)
            {
                byte[] newArray = (byte[])array.Clone();
                Array.Reverse(newArray, offset, length);
                return newArray;
            }

            return array;
        }

        private void buttonImport_MouseUp(object sender, MouseEventArgs e)
        {
            this.contextMenuImport.Show(this, new Point(this.buttonImport.Location.X, this.buttonImport.Location.Y + (this.buttonImport.ClientRectangle.Height - 1)));
        }

        private void buttonAutoFillBests_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this, "Are you sure?", "Auto-fill best segments", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<Segment> splitList = new List<Segment>();
                this.FillSplitList(ref splitList);

                for (int i = 0; i < splitList.Count; ++i)
                {
                    double segmentTime = 0.0;

                    if (i == 0)
                        segmentTime = splitList[i].BestTime;
                    else if (splitList[i].BestTime != 0.0 && splitList[i - 1].BestTime != 0.0)
                        segmentTime = splitList[i].BestTime - splitList[i - 1].BestTime;

                    if (splitList[i].BestSegment == 0.0 || (segmentTime != 0.0 && segmentTime < splitList[i].BestSegment))
                        splitList[i].BestSegment = segmentTime;
                }

                this.populateList(splitList);
            }
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            if (this.runView.SelectedCells.Count > 0)
            {
                new DataGridViewRow();
                this.runView.Rows.Insert(this.runView.SelectedCells[0].RowIndex, new object[0]);
                base.Height = (Math.Min(15, this.runView.Rows.Count) * this.cellHeight) + this.windowHeight;
                this.runView.CurrentCell = this.runView.Rows[this.runView.SelectedCells[0].RowIndex - 1].Cells[0];
            }
        }

        private void offsetButton_Click(object sender, EventArgs e)
        {
            if (this.oldOffset.Text.Length != 0)
            {
                foreach (DataGridViewRow row in (IEnumerable)this.runView.Rows)
                {
                    if (row.Cells[1].Value != null)
                        row.Cells[1].Value = this.timeFormat(Math.Max((double)0.0, (double)(this.timeParse(row.Cells[1].Value.ToString()) - this.timeParse(this.oldOffset.Text))));
                }
                this.oldOffset.Text = "";
            }
        }

        private void oldOffset_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (this.oldOffset.Text.Length != 0))
            {
                foreach (DataGridViewRow row in (IEnumerable)this.runView.Rows)
                {
                    if (row.Cells[1].Value != null)
                        row.Cells[1].Value = this.timeFormat(Math.Max((double)0.0, (double)(this.timeParse(row.Cells[1].Value.ToString()) - this.timeParse(this.oldOffset.Text))));
                }
                this.oldOffset.Text = "";
                e.SuppressKeyPress = true;
            }
        }

        private void oldOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) && ((e.KeyChar != ':') && (e.KeyChar != '.'))) && (e.KeyChar != ','))
                e.Handled = true;
        }

        private void oldOffset_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(this.oldOffset.Text, "[^0-9:.,]"))
                this.oldOffset.Text = Regex.Replace(this.oldOffset.Text, "[^0-9:.,]", "");
        }

        private void populateList(List<Segment> splitList)
        {
            this.runView.Rows.Clear();
            this.runView.Rows[0].Cells[5].Value = Resources.MissingIcon;
            foreach (Segment segment in splitList)
            {
                if (segment.Name != null)
                {
                    string name = segment.Name;
                    string str2 = "";
                    string str3 = "";
                    string str4 = "";
                    string iconPath = "";
                    Image missingIcon = Resources.MissingIcon;

                    if (segment.OldTime != 0.0)
                        str2 = this.timeFormat(segment.OldTime);

                    if (segment.BestTime != 0.0)
                        str3 = this.timeFormat(segment.BestTime);

                    if (segment.BestSegment != 0.0)
                        str4 = this.timeFormat(segment.BestSegment);

                    if ((segment.IconPath != null) && (segment.IconPath.Length > 1))
                    {
                        iconPath = segment.IconPath;
                        try
                        {
                            missingIcon = Image.FromFile(segment.IconPath);
                        }
                        catch
                        { }
                    }
                    this.runView.Rows.Add(new object[] { name, str2, str3, str4, iconPath, missingIcon });
                }
            }

            base.Height = (Math.Min(15, this.runView.Rows.Count) * this.cellHeight) + this.windowHeight;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.populateList(this.editList);
        }

        private void RunEditor_Shown(object sender, EventArgs e)
        {
            base.BringToFront();
        }

        private void runView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 5) && (e.RowIndex >= 0))
            {
                if (this.runView.Rows[e.RowIndex].Cells[0].Value != null)
                    this.openIconDialog.Title = "Set Icon for " + this.runView.Rows[e.RowIndex].Cells[0].Value.ToString() + "...";
                else
                    this.openIconDialog.Title = "Set Icon...";

                if (this.openIconDialog.ShowDialog() == DialogResult.OK)
                {
                    this.runView.Rows[e.RowIndex].Cells[4].Value = this.openIconDialog.FileName;
                    Image missingIcon = Resources.MissingIcon;
                    try
                    {
                        missingIcon = Image.FromFile(this.openIconDialog.FileName);
                    }
                    catch
                    { }

                    this.runView.Rows[e.RowIndex].Cells[5].Value = missingIcon;
                }
            }
        }

        private void runView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            this.eCtl = e.Control;
            this.eCtl.TextChanged -= new EventHandler(this.eCtl_TextChanged);
            this.eCtl.KeyPress -= new KeyPressEventHandler(this.eCtl_KeyPress);
            this.eCtl.TextChanged += new EventHandler(this.eCtl_TextChanged);
            this.eCtl.KeyPress += new KeyPressEventHandler(this.eCtl_KeyPress);
        }

        private void runView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell cell in this.runView.SelectedCells)
                {
                    if (((cell.RowIndex >= 0) && (cell.ColumnIndex >= 0)) && !this.runView.Rows[cell.RowIndex].IsNewRow)
                    {
                        if (cell.ColumnIndex == 0)
                            this.runView.Rows.RemoveAt(cell.RowIndex);
                        else if (cell.ColumnIndex == 5)
                        {
                            cell.Value = Resources.MissingIcon;
                            this.runView.Rows[cell.RowIndex].Cells[4].Value = "";
                        }
                        else
                            cell.Value = null;
                    }
                }
                base.Height = (Math.Min(15, this.runView.Rows.Count) * this.cellHeight) + this.windowHeight;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                for (int i = this.runView.SelectedCells.Count - 1; i >= 0; i--)
                {
                    DataGridViewCell cell2 = this.runView.SelectedCells[i];
                    if ((cell2.RowIndex >= 0) && (cell2.ColumnIndex == 5))
                    {
                        if (this.runView.Rows[cell2.RowIndex].Cells[0].Value != null)
                            this.openIconDialog.Title = "Set Icon for " + this.runView.Rows[cell2.RowIndex].Cells[0].Value.ToString() + "...";
                        else
                            this.openIconDialog.Title = "Set Icon...";

                        if (this.openIconDialog.ShowDialog() != DialogResult.OK)
                            break;

                        this.runView.Rows[cell2.RowIndex].Cells[4].Value = this.openIconDialog.FileName;
                        Image missingIcon = Resources.MissingIcon;

                        try
                        {
                            missingIcon = Image.FromFile(this.openIconDialog.FileName);
                        }
                        catch
                        { }
                        cell2.Value = missingIcon;
                    }
                }
            }
        }

        private void runView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            base.Height = (Math.Min(15, this.runView.Rows.Count) * this.cellHeight) + this.windowHeight;
            e.Row.Cells[5].Value = Resources.MissingIcon;
        }

        private void runView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            base.Height = (Math.Min(15, this.runView.Rows.Count) * this.cellHeight) + this.windowHeight;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.editList.Clear();
            FillSplitList(ref this.editList);
        }

        private void FillSplitList(ref List<Segment> splitList)
        {
            foreach (DataGridViewRow row in (IEnumerable)this.runView.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    Bitmap missingIcon = Resources.MissingIcon;
                    Segment item = new Segment(row.Cells[0].Value.ToString());
                    if (row.Cells[1].Value != null)
                        item.OldTime = this.timeParse(row.Cells[1].Value.ToString());

                    if (row.Cells[2].Value != null)
                        item.BestTime = this.timeParse(row.Cells[2].Value.ToString());

                    if (row.Cells[3].Value != null)
                        item.BestSegment = this.timeParse(row.Cells[3].Value.ToString());

                    if (row.Cells[4].Value != null)
                    {
                        item.IconPath = row.Cells[4].Value.ToString();
                        try
                        {
                            item.Icon = Image.FromFile(item.IconPath);
                        }
                        catch
                        {
                            item.Icon = Resources.MissingIcon;
                        }
                    }
                    else
                        item.Icon = Resources.MissingIcon;

                    splitList.Add(item);
                }
            }
        }

        private string timeFormat(double secs)
        {
            TimeSpan span = TimeSpan.FromSeconds(Math.Truncate(secs * 100) / 100);
            //TimeSpan span = TimeSpan.FromSeconds(Math.Round(secs, 2));
            if (span.TotalHours >= 1.0)
                return string.Format("{0}:{1:00}:{2:00.00}", Math.Floor(span.TotalHours), span.Minutes, span.Seconds + (((double)span.Milliseconds) / 1000.0));

            if (span.TotalMinutes >= 1.0)
                return string.Format("{0}:{1:00.00}", Math.Floor(span.TotalMinutes), span.Seconds + (((double)span.Milliseconds) / 1000.0));

            return string.Format("{0:0.00}", span.TotalSeconds);
        }

        private double timeParse(string timeString)
        {
            double num = 0.0;
            foreach (string str in timeString.Split(new char[] { ':' }))
            {
                double num2;
                if (double.TryParse(str, out num2))
                    num = (num * 60.0) + num2;
            }
            return num;
        }
    }
}

