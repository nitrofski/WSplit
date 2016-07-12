namespace WSplitTimer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Properties;

    public class CustomizeColors : Form
    {
        // Data
        private Font clockL = new Font("Arial", 20f, FontStyle.Bold, GraphicsUnit.Pixel);
        private Font clockM = new Font("Arial", 16f, FontStyle.Bold, GraphicsUnit.Pixel);
        private Font displayFont = new Font("Arial", 9.333333f, GraphicsUnit.Pixel);
        private Font timeFont = new Font("Arial", 10.66667f, FontStyle.Bold, GraphicsUnit.Pixel);
        private ClockType previewClockType;
        private List<SettingPair> ColorSettings = new List<SettingPair>();
        private ColorConverter converter = new ColorConverter();
        private int segHeight = 14;

        // Controls
        private PictureBox picBoxPreview;

        private Label labelPreview;
        private CheckBox checkBoxPlainBg;
        private Button buttonDefaultColors;
        private Button buttonLoad;
        private Button buttonSave;
        private Button buttonCancel;
        private Button buttonOk;

        private TabControl colorTabs;
        private TabPage tabPageClockColors;
        private TabPage tabPageSegColors;
        private TabPage tabPageDetailedViewColors;

        // Timer Tab
        private Label labelColumnFore;
        private Label labelColumnColors;
        private Label labelColumnPlain;

        private GroupBox groupBoxBackground;

        private Label labelAhead;
        private PictureBox picBoxAheadFore;
        private PictureBox picBoxAheadBack;
        private PictureBox picBoxAheadBack2;
        private PictureBox picBoxAheadBackPlain;

        private Label labelAheadLosing;
        private PictureBox picBoxAheadLosingFore;
        private PictureBox picBoxAheadLosingBack;
        private PictureBox picBoxAheadLosingBack2;
        private PictureBox picBoxAheadLosingBackPlain;

        private Label labelBehind;
        private PictureBox picBoxBehindFore;
        private PictureBox picBoxBehindBack;
        private PictureBox picBoxBehindBack2;
        private PictureBox picBoxBehindBackPlain;

        private Label labelBehindLosing;
        private PictureBox picBoxBehindLosingFore;
        private PictureBox picBoxBehindLosingBack;
        private PictureBox picBoxBehindLosingBack2;
        private PictureBox picBoxBehindLosingBackPlain;

        private Label labelNoLoaded;
        private PictureBox picBoxNoLoadedBack;
        private PictureBox picBoxNoLoadedBack2;
        private PictureBox picBoxNoLoadedBackPlain;
        private PictureBox picBoxNoLoadedFore;

        private Label labelFinished;
        private PictureBox picBoxFinishedFore;
        private PictureBox picBoxFinishedBack;
        private PictureBox picBoxFinishedBack2;
        private PictureBox picBoxFinishedBackPlain;

        private Label labelRecord;
        private PictureBox picBoxRecordFore;
        private PictureBox picBoxRecordBack;
        private PictureBox picBoxRecordBack2;
        private PictureBox picBoxRecordBackPlain;

        private Label labelDelay;
        private PictureBox picBoxDelayFore;
        private PictureBox picBoxDelayBack;
        private PictureBox picBoxDelayBack2;
        private PictureBox picBoxDelayBackPlain;

        private Label labelPaused;
        private PictureBox picBoxPaused;

        private Label labelFlash;
        private PictureBox picBoxFlash;

        private Label labelStatusBar;
        private PictureBox picBoxStatusBarFore;
        private PictureBox picBoxStatusBarBack;
        private PictureBox picBoxStatusBarBack2;
        private PictureBox picBoxStatusBarBackPlain;

        private Label labelRunTitle;
        private PictureBox picBoxRunTitleFore;
        private PictureBox picBoxRunTitleBack;
        private PictureBox picBoxRunTitleBack2;
        private PictureBox picBoxRunTitleBackPlain;
        //private PictureBox picturebox1;

        // Segment Tab
        private Label labelColumnSegColor;
        private Label labelColumnSegColor2;
        private Label labelColumnSegPlain;

        private Label labelSegBackground;
        private PictureBox picBoxSegBackground;
        private PictureBox picBoxSegBackground2;
        private PictureBox picBoxSegBackgroundPlain;

        private Label labelSegHighlight;
        private PictureBox picBoxSegHighlight;
        private PictureBox picBoxSegHighlight2;
        private PictureBox picBoxSegHighlightPlain;

        private Label labelSegHighlightBorder;
        private PictureBox picBoxSegHighlightBorder;

        private Label labelSegPastText;
        private PictureBox picBoxSegPastText;

        private Label labelSegLiveText;
        private PictureBox picBoxSegLiveText;

        private Label labelSegFutureText;
        private PictureBox picBoxSegFutureText;

        private Label labelSegFutureTime;
        private PictureBox picBoxSegFutureTime;

        private Label labelSegNewTime;
        private PictureBox picBoxSegNewTime;

        private Label labelSegMissing;
        private PictureBox picBoxSegMissing;

        private Label labelSegBestSegment;
        private PictureBox picBoxSegBestSegment;

        private Label labelSegAheadGain;
        private PictureBox picBoxSegAheadGain;

        private Label labelSegAheadLoss;
        private PictureBox picBoxSegAheadLoss;

        private Label labelSegBehindGain;
        private PictureBox picBoxSegBehindGain;

        private Label labelSegBehindLoss;
        private PictureBox picBoxSegBehindLoss;

        // Detailed view tab
        private CheckBox checkBoxDViewUsePrimary;

        private GroupBox groupBoxDViewClock;
        private GroupBox groupBoxDViewSegments;
        private GroupBox groupBoxGraph;

        private Label labelDViewAhead;
        private PictureBox picBoxDViewAhead;

        private Label labelDViewAheadLosing;
        private PictureBox picBoxDViewAheadLosing;

        private Label labelDViewBehind;
        private PictureBox picBoxDViewBehind;

        private Label labelDViewBehindLosing;
        private PictureBox picBoxDViewBehindLosing;

        private Label labelDViewFinished;
        private PictureBox picBoxDViewFinished;

        private Label labelDViewRecord;
        private PictureBox picBoxDViewRecord;

        private Label labelDViewDelay;
        private PictureBox picBoxDViewDelay;

        private Label labelDViewPaused;
        private PictureBox picBoxDViewPaused;

        private Label labelDViewFlash;
        private PictureBox picBoxDViewFlash;

        private Label labelDViewSegCurrentText;
        private PictureBox picBoxDViewSegCurrentText;

        private Label labelDViewSegDefaultText;
        private PictureBox picBoxDViewSegDefaultText;

        private Label labelDViewSegMissingTime;
        private PictureBox picBoxDViewSegMissingTime;

        private Label labelDViewSegBestSegment;
        private PictureBox picBoxDViewSegBestSegment;

        private Label labelDViewSegAheadGain;
        private PictureBox picBoxDViewSegAheadGain;

        private Label labelDViewSegAheadLoss;
        private PictureBox picBoxDViewSegAheadLoss;

        private Label labelDViewSegBehindGain;
        private PictureBox picBoxDViewSegBehindGain;

        private Label labelDViewSegBehindLoss;
        private PictureBox picBoxDViewSegBehindLoss;

        private Label labelDViewSegHighlight;
        private PictureBox picBoxDViewSegHighlight;

        private Label labelGraphAhead;
        private PictureBox picBoxGraphAhead;

        private Label labelGraphBehind;
        private PictureBox picBoxGraphBehind;

        private PictureBox picturebox1;


        public CustomizeColors(bool selectDViewTab = false)
        {
            this.InitializeComponent();
            this.PopulateSettings();
            this.PopulateColors();
            if (selectDViewTab)
                colorTabs.SelectedTab = tabPageDetailedViewColors;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Global Controls:
            this.picBoxPreview = new PictureBox();

            this.labelPreview = new Label();
            this.checkBoxPlainBg = new CheckBox();
            this.buttonDefaultColors = new Button();
            this.buttonLoad = new Button();
            this.buttonSave = new Button();
            this.buttonCancel = new Button();
            this.buttonOk = new Button();

            this.colorTabs = new TabControl();
            this.tabPageClockColors = new TabPage();
            this.tabPageSegColors = new TabPage();
            this.tabPageDetailedViewColors = new TabPage();

            // Timer tab:
            this.labelColumnFore = new Label();
            this.labelColumnColors = new Label();
            this.labelColumnPlain = new Label();

            this.groupBoxBackground = new GroupBox();

            this.labelAhead = new Label();
            this.picBoxAheadFore = new PictureBox();
            this.picBoxAheadBack = new PictureBox();
            this.picBoxAheadBack2 = new PictureBox();
            this.picBoxAheadBackPlain = new PictureBox();

            this.labelAheadLosing = new Label();
            this.picBoxAheadLosingFore = new PictureBox();
            this.picBoxAheadLosingBack = new PictureBox();
            this.picBoxAheadLosingBack2 = new PictureBox();
            this.picBoxAheadLosingBackPlain = new PictureBox();

            this.labelBehind = new Label();
            this.picBoxBehindFore = new PictureBox();
            this.picBoxBehindBack = new PictureBox();
            this.picBoxBehindBack2 = new PictureBox();
            this.picBoxBehindBackPlain = new PictureBox();

            this.labelBehindLosing = new Label();
            this.picBoxBehindLosingFore = new PictureBox();
            this.picBoxBehindLosingBack = new PictureBox();
            this.picBoxBehindLosingBack2 = new PictureBox();
            this.picBoxBehindLosingBackPlain = new PictureBox();

            this.labelNoLoaded = new Label();
            this.picBoxNoLoadedFore = new PictureBox();
            this.picBoxNoLoadedBack = new PictureBox();
            this.picBoxNoLoadedBack2 = new PictureBox();
            this.picBoxNoLoadedBackPlain = new PictureBox();

            this.labelFinished = new Label();
            this.picBoxFinishedFore = new PictureBox();
            this.picBoxFinishedBack = new PictureBox();
            this.picBoxFinishedBack2 = new PictureBox();
            this.picBoxFinishedBackPlain = new PictureBox();

            this.labelRecord = new Label();
            this.picBoxRecordFore = new PictureBox();
            this.picBoxRecordBack = new PictureBox();
            this.picBoxRecordBack2 = new PictureBox();
            this.picBoxRecordBackPlain = new PictureBox();

            this.labelDelay = new Label();
            this.picBoxDelayFore = new PictureBox();
            this.picBoxDelayBack = new PictureBox();
            this.picBoxDelayBack2 = new PictureBox();
            this.picBoxDelayBackPlain = new PictureBox();

            this.labelPaused = new Label();
            this.picBoxPaused = new PictureBox();

            this.labelFlash = new Label();
            this.picBoxFlash = new PictureBox();

            this.labelStatusBar = new Label();
            this.picBoxStatusBarFore = new PictureBox();
            this.picBoxStatusBarBack = new PictureBox();
            this.picBoxStatusBarBack2 = new PictureBox();
            this.picBoxStatusBarBackPlain = new PictureBox();

            this.labelRunTitle = new Label();
            this.picBoxRunTitleFore = new PictureBox();
            this.picBoxRunTitleBack = new PictureBox();
            this.picBoxRunTitleBack2 = new PictureBox();
            this.picBoxRunTitleBackPlain = new PictureBox();

            // Segment Tab:
            this.labelColumnSegColor = new Label();
            this.labelColumnSegColor2 = new Label();
            this.labelColumnSegPlain = new Label();

            this.labelSegBackground = new Label();
            this.picBoxSegBackground = new PictureBox();
            this.picBoxSegBackground2 = new PictureBox();
            this.picBoxSegBackgroundPlain = new PictureBox();

            this.labelSegHighlight = new Label();
            this.picBoxSegHighlight = new PictureBox();
            this.picBoxSegHighlight2 = new PictureBox();
            this.picBoxSegHighlightPlain = new PictureBox();

            this.labelSegHighlightBorder = new Label();
            this.picBoxSegHighlightBorder = new PictureBox();

            this.labelSegPastText = new Label();
            this.picBoxSegPastText = new PictureBox();

            this.labelSegLiveText = new Label();
            this.picBoxSegLiveText = new PictureBox();

            this.labelSegFutureText = new Label();
            this.picBoxSegFutureText = new PictureBox();

            this.labelSegFutureTime = new Label();
            this.picBoxSegFutureTime = new PictureBox();

            this.labelSegNewTime = new Label();
            this.picBoxSegNewTime = new PictureBox();

            this.labelSegMissing = new Label();
            this.picBoxSegMissing = new PictureBox();

            this.labelSegBestSegment = new Label();
            this.picBoxSegBestSegment = new PictureBox();

            this.labelSegAheadGain = new Label();
            this.picBoxSegAheadGain = new PictureBox();

            this.labelSegAheadLoss = new Label();
            this.picBoxSegAheadLoss = new PictureBox();

            this.labelSegBehindGain = new Label();
            this.picBoxSegBehindGain = new PictureBox();

            this.labelSegBehindLoss = new Label();
            this.picBoxSegBehindLoss = new PictureBox();

            this.labelGraphAhead = new Label();
            this.picBoxGraphAhead = new PictureBox();

            this.labelGraphBehind = new Label();
            this.picBoxGraphBehind = new PictureBox();

            this.picturebox1 = new PictureBox();

            // Detailed View tab:
            this.checkBoxDViewUsePrimary = new CheckBox();

            this.groupBoxDViewClock = new GroupBox();
            this.groupBoxDViewSegments = new GroupBox();
            this.groupBoxGraph = new GroupBox();

            this.labelDViewAhead = new Label();
            this.picBoxDViewAhead = new PictureBox();

            this.labelDViewAheadLosing = new Label();
            this.picBoxDViewAheadLosing = new PictureBox();

            this.labelDViewBehind = new Label();
            this.picBoxDViewBehind = new PictureBox();

            this.labelDViewBehindLosing = new Label();
            this.picBoxDViewBehindLosing = new PictureBox();

            this.labelDViewFinished = new Label();
            this.picBoxDViewFinished = new PictureBox();

            this.labelDViewRecord = new Label();
            this.picBoxDViewRecord = new PictureBox();

            this.labelDViewDelay = new Label();
            this.picBoxDViewDelay = new PictureBox();

            this.labelDViewPaused = new Label();
            this.picBoxDViewPaused = new PictureBox();

            this.labelDViewFlash = new Label();
            this.picBoxDViewFlash = new PictureBox();

            this.labelDViewSegCurrentText = new Label();
            this.picBoxDViewSegCurrentText = new PictureBox();

            this.labelDViewSegDefaultText = new Label();
            this.picBoxDViewSegDefaultText = new PictureBox();

            this.labelDViewSegMissingTime = new Label();
            this.picBoxDViewSegMissingTime = new PictureBox();

            this.labelDViewSegBestSegment = new Label();
            this.picBoxDViewSegBestSegment = new PictureBox();

            this.labelDViewSegAheadGain = new Label();
            this.picBoxDViewSegAheadGain = new PictureBox();

            this.labelDViewSegAheadLoss = new Label();
            this.picBoxDViewSegAheadLoss = new PictureBox();

            this.labelDViewSegBehindGain = new Label();
            this.picBoxDViewSegBehindGain = new PictureBox();

            this.labelDViewSegBehindLoss = new Label();
            this.picBoxDViewSegBehindLoss = new PictureBox();

            this.labelDViewSegHighlight = new Label();
            this.picBoxDViewSegHighlight = new PictureBox();

            // Starting the set up:
            this.colorTabs.SuspendLayout();
            this.tabPageClockColors.SuspendLayout();
            this.tabPageSegColors.SuspendLayout();
            this.tabPageDetailedViewColors.SuspendLayout();
            this.groupBoxBackground.SuspendLayout();
            this.groupBoxDViewClock.SuspendLayout();
            this.groupBoxDViewSegments.SuspendLayout();
            this.groupBoxGraph.SuspendLayout();
            ((ISupportInitialize)this.picBoxAheadBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxRunTitleBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxAheadLosingBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxBehindBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxBehindLosingBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxStatusBarBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxNoLoadedBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxFinishedBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxRecordBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxDelayBackPlain).BeginInit();
            ((ISupportInitialize)this.picBoxAheadBack2).BeginInit();
            ((ISupportInitialize)this.picBoxRunTitleBack2).BeginInit();
            ((ISupportInitialize)this.picBoxAheadLosingBack2).BeginInit();
            ((ISupportInitialize)this.picBoxBehindBack2).BeginInit();
            ((ISupportInitialize)this.picBoxBehindLosingBack2).BeginInit();
            ((ISupportInitialize)this.picBoxStatusBarBack2).BeginInit();
            ((ISupportInitialize)this.picBoxNoLoadedBack2).BeginInit();
            ((ISupportInitialize)this.picBoxFinishedBack2).BeginInit();
            ((ISupportInitialize)this.picBoxRecordBack2).BeginInit();
            ((ISupportInitialize)this.picBoxDelayBack2).BeginInit();
            ((ISupportInitialize)this.picBoxAheadBack).BeginInit();
            ((ISupportInitialize)this.picBoxRunTitleBack).BeginInit();
            ((ISupportInitialize)this.picBoxAheadLosingBack).BeginInit();
            ((ISupportInitialize)this.picBoxBehindBack).BeginInit();
            ((ISupportInitialize)this.picBoxBehindLosingBack).BeginInit();
            ((ISupportInitialize)this.picBoxStatusBarBack).BeginInit();
            ((ISupportInitialize)this.picBoxNoLoadedBack).BeginInit();
            ((ISupportInitialize)this.picBoxFinishedBack).BeginInit();
            ((ISupportInitialize)this.picBoxRecordBack).BeginInit();
            ((ISupportInitialize)this.picBoxDelayBack).BeginInit();
            ((ISupportInitialize)this.picBoxRunTitleFore).BeginInit();
            ((ISupportInitialize)this.picBoxStatusBarFore).BeginInit();
            ((ISupportInitialize)this.picBoxDelayFore).BeginInit();
            ((ISupportInitialize)this.picBoxRecordFore).BeginInit();
            ((ISupportInitialize)this.picBoxFinishedFore).BeginInit();
            ((ISupportInitialize)this.picBoxFlash).BeginInit();
            ((ISupportInitialize)this.picBoxPaused).BeginInit();
            ((ISupportInitialize)this.picBoxNoLoadedFore).BeginInit();
            ((ISupportInitialize)this.picBoxBehindLosingFore).BeginInit();
            ((ISupportInitialize)this.picBoxBehindFore).BeginInit();
            ((ISupportInitialize)this.picBoxAheadLosingFore).BeginInit();
            ((ISupportInitialize)this.picBoxAheadFore).BeginInit();
            ((ISupportInitialize)this.picBoxSegHighlightPlain).BeginInit();
            ((ISupportInitialize)this.picBoxSegBackgroundPlain).BeginInit();
            ((ISupportInitialize)this.picBoxSegHighlight2).BeginInit();
            ((ISupportInitialize)this.picBoxSegBackground2).BeginInit();
            ((ISupportInitialize)this.picBoxSegHighlightBorder).BeginInit();
            ((ISupportInitialize)this.picBoxSegBehindLoss).BeginInit();
            ((ISupportInitialize)this.picBoxSegBehindGain).BeginInit();
            ((ISupportInitialize)this.picBoxSegAheadLoss).BeginInit();
            ((ISupportInitialize)this.picBoxSegAheadGain).BeginInit();
            ((ISupportInitialize)this.picBoxSegMissing).BeginInit();
            ((ISupportInitialize)this.picBoxSegNewTime).BeginInit();
            ((ISupportInitialize)this.picBoxSegBestSegment).BeginInit();
            ((ISupportInitialize)this.picBoxSegFutureTime).BeginInit();
            ((ISupportInitialize)this.picBoxSegFutureText).BeginInit();
            ((ISupportInitialize)this.picBoxSegLiveText).BeginInit();
            ((ISupportInitialize)this.picBoxSegPastText).BeginInit();
            ((ISupportInitialize)this.picBoxSegHighlight).BeginInit();
            ((ISupportInitialize)this.picBoxSegBackground).BeginInit();
            ((ISupportInitialize)this.picBoxDViewAhead).BeginInit();
            ((ISupportInitialize)this.picBoxDViewAheadLosing).BeginInit();
            ((ISupportInitialize)this.picBoxDViewBehind).BeginInit();
            ((ISupportInitialize)this.picBoxDViewBehindLosing).BeginInit();
            ((ISupportInitialize)this.picBoxDViewFinished).BeginInit();
            ((ISupportInitialize)this.picBoxDViewRecord).BeginInit();
            ((ISupportInitialize)this.picBoxDViewDelay).BeginInit();
            ((ISupportInitialize)this.picBoxDViewPaused).BeginInit();
            ((ISupportInitialize)this.picBoxDViewFlash).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegCurrentText).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegDefaultText).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegMissingTime).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegBestSegment).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegAheadGain).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegAheadLoss).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegBehindGain).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegBehindLoss).BeginInit();
            ((ISupportInitialize)this.picBoxDViewSegHighlight).BeginInit();
            ((ISupportInitialize)this.picturebox1).BeginInit();
            ((ISupportInitialize)this.picBoxPreview).BeginInit();
            base.SuspendLayout();

            // ----------------------------------------
            // Setting up dialog: 
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x19b, 0x1b5);
            base.Controls.AddRange(new Control[]
            {
                this.colorTabs,
                this.labelPreview, this.picBoxPreview, this.checkBoxPlainBg,
                this.buttonDefaultColors, this.buttonSave, this.buttonLoad,
                this.buttonOk, this.buttonCancel
            });
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "CustomizeColors";
            this.Text = "色設定";

            // ----------------------------------------
            // Setting up globale controls:
            //
            // labelPreview
            //
            this.labelPreview.AutoSize = true;
            this.labelPreview.Location = new Point(0x110, 0x12);
            this.labelPreview.Name = "labelPreview";
            this.labelPreview.Size = new Size(0x2d, 13);
            this.labelPreview.TabIndex = 2;
            this.labelPreview.Text = "プレビュー";
            //
            // picBoxPreview
            //
            this.picBoxPreview.Location = new Point(0x113, 0x22);
            this.picBoxPreview.Name = "picBoxPreview";
            this.picBoxPreview.Size = new Size(0x7c, 0x119);
            this.picBoxPreview.TabIndex = 1;
            this.picBoxPreview.TabStop = false;
            this.picBoxPreview.Paint += this.previewBox_Paint;
            //
            // checkBoxPlainBg
            //
            this.checkBoxPlainBg.AutoSize = true;
            this.checkBoxPlainBg.Location = new Point(0x113, 0x141);
            this.checkBoxPlainBg.Name = "checkBoxPlainBg";
            this.checkBoxPlainBg.Size = new Size(0x6c, 0x11);
            this.checkBoxPlainBg.TabIndex = 6;
            this.checkBoxPlainBg.Text = "単色背景プレビュー";
            this.checkBoxPlainBg.UseVisualStyleBackColor = true;
            this.checkBoxPlainBg.CheckedChanged += this.plainBg_CheckedChanged;

            //
            // buttonDefaultColors
            //
            this.buttonDefaultColors.Location = new Point(0x113, 0x158);
            this.buttonDefaultColors.Name = "buttonDefaultColors";
            this.buttonDefaultColors.Size = new Size(0x7c, 0x17);
            this.buttonDefaultColors.TabIndex = 5;
            this.buttonDefaultColors.Text = "初期設定に戻す";
            this.buttonDefaultColors.UseVisualStyleBackColor = true;
            this.buttonDefaultColors.Click += this.buttonDefaultColors_Click;
            //
            // buttonSave
            //
            this.buttonSave.Location = new Point(0x113, 0x175);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new Size(0x3b, 0x17);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += this.buttonSave_Click;
            //
            // buttonLoad
            //
            this.buttonLoad.Location = new Point(340, 0x175);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new Size(0x3b, 0x17);
            this.buttonLoad.TabIndex = 8;
            this.buttonLoad.Text = "読込";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += this.buttonLoad_Click;
            //
            // buttonOk
            //
            this.buttonOk.Location = new Point(0x113, 0x192);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new Size(0x3b, 0x17);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += this.buttonOk_Click;
            //
            // buttonCancel
            //
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.buttonCancel.Location = new Point(340, 0x192);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size(0x3b, 0x17);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;

            // ----------------------------------------
            // Setting up Tabs:
            //
            // colorTabs
            //
            this.colorTabs.Controls.Add(this.tabPageClockColors);
            this.colorTabs.Controls.Add(this.tabPageSegColors);
            this.colorTabs.Controls.Add(this.tabPageDetailedViewColors);
            this.colorTabs.Location = new Point(12, 12);
            this.colorTabs.Name = "colorTabs";
            this.colorTabs.SelectedIndex = 0;
            this.colorTabs.Size = new Size(0x101, 0x19d);
            this.colorTabs.TabIndex = 0;
            //
            // tabPageClockColors
            //
            this.tabPageClockColors.Controls.AddRange(new Control[]
            {
                this.labelColumnFore, this.groupBoxBackground,
                this.labelAhead, this.picBoxAheadFore,
                this.labelAheadLosing, this.picBoxAheadLosingFore,
                this.labelBehind, this.picBoxBehindFore,
                this.labelBehindLosing, this.picBoxBehindLosingFore,
                this.labelNoLoaded, this.picBoxNoLoadedFore,
                this.labelFinished, this.picBoxFinishedFore,
                this.labelRecord, this.picBoxRecordFore,
                this.labelDelay, this.picBoxDelayFore,
                this.labelPaused, this.picBoxPaused,
                this.labelFlash, this.picBoxFlash,
                this.labelStatusBar, this.picBoxStatusBarFore,
                this.labelRunTitle, this.picBoxRunTitleFore,
            });
            this.tabPageClockColors.Name = "tabPageClockColors";
            this.tabPageClockColors.Padding = new Padding(3);
            this.tabPageClockColors.TabIndex = 0;
            this.tabPageClockColors.Text = "タイマー";
            this.tabPageClockColors.UseVisualStyleBackColor = true;
            //
            // tabPageSegColors
            //
            this.tabPageSegColors.Controls.AddRange(new Control[]
            {
                this.labelColumnSegColor, this.labelColumnSegColor2, this.labelColumnSegPlain,
                this.labelSegBackground, this.picBoxSegBackground, this.picBoxSegBackground2, this.picBoxSegBackgroundPlain,
                this.labelSegHighlight, this.picBoxSegHighlight, this.picBoxSegHighlight2, this.picBoxSegHighlightPlain,
                this.labelSegHighlightBorder, this.picBoxSegHighlightBorder,
                this.labelSegPastText, this.picBoxSegPastText,
                this.labelSegLiveText, this.picBoxSegLiveText,
                this.labelSegFutureText, this.picBoxSegFutureText,
                this.labelSegFutureTime, this.picBoxSegFutureTime,
                this.labelSegNewTime, this.picBoxSegNewTime,
                this.labelSegMissing, this.picBoxSegMissing,
                this.labelSegBestSegment, this.picBoxSegBestSegment,
                this.labelSegAheadGain, this.picBoxSegAheadGain,
                this.labelSegAheadLoss, this.picBoxSegAheadLoss,
                this.labelSegBehindGain, this.picBoxSegBehindGain,
                this.labelSegBehindLoss, this.picBoxSegBehindLoss, this.picturebox1
            });
            this.tabPageSegColors.Name = "segColorTab";
            this.tabPageSegColors.Padding = new Padding(3);
            this.tabPageSegColors.TabIndex = 1;
            this.tabPageSegColors.Text = "区間";
            this.tabPageSegColors.UseVisualStyleBackColor = true;
            //
            // tabPageDetailedView
            //
            this.tabPageDetailedViewColors.Controls.AddRange(new Control[]
            {
                this.checkBoxDViewUsePrimary,
                this.groupBoxDViewClock,
                this.groupBoxDViewSegments,
                this.groupBoxGraph
            });
            this.tabPageDetailedViewColors.Name = "tabPageDetailedView";
            this.tabPageDetailedViewColors.Padding = new Padding(3);
            this.tabPageDetailedViewColors.TabIndex = 2;
            this.tabPageDetailedViewColors.Text = "詳細";
            this.tabPageDetailedViewColors.UseVisualStyleBackColor = true;

            // ----------------------------------------
            // Clock tab:
            //
            // labelColumnFore
            //
            this.labelColumnFore.AutoSize = true;
            this.labelColumnFore.Location = new Point(0x61, 0x16);
            this.labelColumnFore.Name = "labelColumnFore";
            this.labelColumnFore.Size = new Size(0x1c, 13);
            this.labelColumnFore.Text = "テキスト";
            this.labelColumnColors.AutoSize = true;
            this.labelColumnColors.Location = new Point(0x0D, 0x10);
            this.labelColumnColors.Name = "labelColumnColors";
            this.labelColumnColors.Size = new Size(0x24, 13);
            this.labelColumnColors.Text = "色";
            this.labelColumnPlain.AutoSize = true;
            this.labelColumnPlain.Location = new Point(0x43, 0x10);
            this.labelColumnPlain.Name = "labelColumnPlain";
            this.labelColumnPlain.Size = new Size(30, 13);
            this.labelColumnPlain.Text = "単色";
            //
            // groupBoxBackground
            //
            this.groupBoxBackground.Controls.AddRange(new Control[]
            {
                this.labelColumnColors, this.labelColumnPlain,
                this.picBoxAheadBack, this.picBoxAheadBack2, this.picBoxAheadBackPlain,
                this.picBoxAheadLosingBack, this.picBoxAheadLosingBack2, this.picBoxAheadLosingBackPlain,
                this.picBoxBehindBack, this.picBoxBehindBack2, this.picBoxBehindBackPlain,
                this.picBoxBehindLosingBack, this.picBoxBehindLosingBack2, this.picBoxBehindLosingBackPlain,
                this.picBoxNoLoadedBack, this.picBoxNoLoadedBack2, this.picBoxNoLoadedBackPlain,
                this.picBoxFinishedBack, this.picBoxFinishedBack2, this.picBoxFinishedBackPlain,
                this.picBoxRecordBack, this.picBoxRecordBack2, this.picBoxRecordBackPlain,
                this.picBoxDelayBack, this.picBoxDelayBack2, this.picBoxDelayBackPlain,
                this.picBoxStatusBarBack, this.picBoxStatusBarBack2, this.picBoxStatusBarBackPlain,
                this.picBoxRunTitleBack, this.picBoxRunTitleBack2, this.picBoxRunTitleBackPlain
            });
            this.groupBoxBackground.Location = new Point(0x8b, 6);
            this.groupBoxBackground.Name = "groupBoxBackground";
            this.groupBoxBackground.Size = new Size(0x68, 0x15d);
            this.groupBoxBackground.TabStop = false;
            this.groupBoxBackground.Text = "背景";

            //
            // labelAhead
            //
            this.labelAhead.AutoSize = true;
            this.labelAhead.Cursor = Cursors.Hand;
            this.labelAhead.Location = new Point(7, 0x26);
            this.labelAhead.MinimumSize = new Size(100, 20);
            this.labelAhead.Name = "labelAhead";
            this.labelAhead.Text = "貯金";
            this.labelAhead.TextAlign = ContentAlignment.MiddleRight;
            this.labelAhead.Click += this.labelAhead_Click;
            //
            // picBoxAheadFore
            //
            this.picBoxAheadFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadFore.Cursor = Cursors.Hand;
            this.picBoxAheadFore.Location = new Point(0x71, 0x26);
            this.picBoxAheadFore.Name = "AheadFore";
            this.picBoxAheadFore.Size = new Size(20, 20);
            this.picBoxAheadFore.TabStop = false;
            this.picBoxAheadFore.Click += this.SetPictureBoxColor;
            //
            // picBoxAheadBack
            //
            this.picBoxAheadBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadBack.Cursor = Cursors.Hand;
            this.picBoxAheadBack.Location = new Point(12, 0x20);
            this.picBoxAheadBack.Name = "picBoxAheadBack";
            this.picBoxAheadBack.Size = new Size(20, 20);
            this.picBoxAheadBack.TabStop = false;
            this.picBoxAheadBack.Click += this.SetPictureBoxColor;
            //
            // picBoxAheadBack2
            //
            this.picBoxAheadBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadBack2.Cursor = Cursors.Hand;
            this.picBoxAheadBack2.Location = new Point(0x26, 0x20);
            this.picBoxAheadBack2.Name = "picBoxAheadBack2";
            this.picBoxAheadBack2.Size = new Size(20, 20);
            this.picBoxAheadBack2.TabStop = false;
            this.picBoxAheadBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxAheadBackPlain
            //
            this.picBoxAheadBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadBackPlain.Cursor = Cursors.Hand;
            this.picBoxAheadBackPlain.Location = new Point(0x48, 0x20);
            this.picBoxAheadBackPlain.Name = "picBoxAheadBackPlain";
            this.picBoxAheadBackPlain.Size = new Size(20, 20);
            this.picBoxAheadBackPlain.TabStop = false;
            this.picBoxAheadBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelAheadLosing
            //
            this.labelAheadLosing.AutoSize = true;
            this.labelAheadLosing.Cursor = Cursors.Hand;
            this.labelAheadLosing.Location = new Point(7, 0x40);
            this.labelAheadLosing.MinimumSize = new Size(100, 20);
            this.labelAheadLosing.Name = "labelAheadLosing";
            this.labelAheadLosing.Text = "貯金減";
            this.labelAheadLosing.TextAlign = ContentAlignment.MiddleRight;
            this.labelAheadLosing.Click += this.labelAheadLosing_Click;
            //
            // picBoxAheadLosingFore
            //
            this.picBoxAheadLosingFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadLosingFore.Cursor = Cursors.Hand;
            this.picBoxAheadLosingFore.Location = new Point(0x71, 0x40);
            this.picBoxAheadLosingFore.Name = "picBoxAheadLosingFore";
            this.picBoxAheadLosingFore.Size = new Size(20, 20);
            this.picBoxAheadLosingFore.TabStop = false;
            this.picBoxAheadLosingFore.Click += this.SetPictureBoxColor;
            //
            // picBoxAheadLosingBack
            //
            this.picBoxAheadLosingBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadLosingBack.Cursor = Cursors.Hand;
            this.picBoxAheadLosingBack.Location = new Point(12, 0x3a);
            this.picBoxAheadLosingBack.Name = "picBoxAheadLosingBack";
            this.picBoxAheadLosingBack.Size = new Size(20, 20);
            this.picBoxAheadLosingBack.TabStop = false;
            this.picBoxAheadLosingBack.Click += this.SetPictureBoxColor;
            //
            // picBoxAheadLosingBack2
            //
            this.picBoxAheadLosingBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadLosingBack2.Cursor = Cursors.Hand;
            this.picBoxAheadLosingBack2.Location = new Point(0x26, 0x3a);
            this.picBoxAheadLosingBack2.Name = "picBoxAheadLosingBack2";
            this.picBoxAheadLosingBack2.Size = new Size(20, 20);
            this.picBoxAheadLosingBack2.TabStop = false;
            this.picBoxAheadLosingBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxAheadLosingBackPlain
            //
            this.picBoxAheadLosingBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxAheadLosingBackPlain.Cursor = Cursors.Hand;
            this.picBoxAheadLosingBackPlain.Location = new Point(0x48, 0x3a);
            this.picBoxAheadLosingBackPlain.Name = "picBoxAheadLosingBackPlain";
            this.picBoxAheadLosingBackPlain.Size = new Size(20, 20);
            this.picBoxAheadLosingBackPlain.TabStop = false;
            this.picBoxAheadLosingBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelBehind
            //
            this.labelBehind.AutoSize = true;
            this.labelBehind.Cursor = Cursors.Hand;
            this.labelBehind.Location = new Point(7, 90);
            this.labelBehind.MinimumSize = new Size(100, 20);
            this.labelBehind.Name = "labelBehind";
            this.labelBehind.Text = "借金";
            this.labelBehind.TextAlign = ContentAlignment.MiddleRight;
            this.labelBehind.Click += this.labelBehind_Click;
            //
            // picBoxBehindFore
            //
            this.picBoxBehindFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindFore.Cursor = Cursors.Hand;
            this.picBoxBehindFore.Location = new Point(0x71, 90);
            this.picBoxBehindFore.Name = "picBoxBehindFore";
            this.picBoxBehindFore.Size = new Size(20, 20);
            this.picBoxBehindFore.TabStop = false;
            this.picBoxBehindFore.Click += this.SetPictureBoxColor;
            //
            // picBoxBehindBack
            //
            this.picBoxBehindBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindBack.Cursor = Cursors.Hand;
            this.picBoxBehindBack.Location = new Point(12, 0x54);
            this.picBoxBehindBack.Name = "picBoxBehindBack";
            this.picBoxBehindBack.Size = new Size(20, 20);
            this.picBoxBehindBack.TabStop = false;
            this.picBoxBehindBack.Click += this.SetPictureBoxColor;
            //
            // picBoxBehindBack2
            //
            this.picBoxBehindBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindBack2.Cursor = Cursors.Hand;
            this.picBoxBehindBack2.Location = new Point(0x26, 0x54);
            this.picBoxBehindBack2.Name = "picBoxBehindBack2";
            this.picBoxBehindBack2.Size = new Size(20, 20);
            this.picBoxBehindBack2.TabStop = false;
            this.picBoxBehindBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxBehindBackPlain
            //
            this.picBoxBehindBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindBackPlain.Cursor = Cursors.Hand;
            this.picBoxBehindBackPlain.Location = new Point(0x48, 0x54);
            this.picBoxBehindBackPlain.Name = "picBoxBehindBackPlain";
            this.picBoxBehindBackPlain.Size = new Size(20, 20);
            this.picBoxBehindBackPlain.TabStop = false;
            this.picBoxBehindBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelBehindLosing
            //
            this.labelBehindLosing.AutoSize = true;
            this.labelBehindLosing.Cursor = Cursors.Hand;
            this.labelBehindLosing.Location = new Point(7, 0x74);
            this.labelBehindLosing.MinimumSize = new Size(100, 20);
            this.labelBehindLosing.Name = "labelBehindLosing";
            this.labelBehindLosing.Text = "借金増";
            this.labelBehindLosing.TextAlign = ContentAlignment.MiddleRight;
            this.labelBehindLosing.Click += this.labelBehindLosing_Click;
            //
            // picBoxBehindLosingFore
            //
            this.picBoxBehindLosingFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindLosingFore.Cursor = Cursors.Hand;
            this.picBoxBehindLosingFore.Location = new Point(0x71, 0x74);
            this.picBoxBehindLosingFore.Name = "picBoxBehindLosingFore";
            this.picBoxBehindLosingFore.Size = new Size(20, 20);
            this.picBoxBehindLosingFore.TabStop = false;
            this.picBoxBehindLosingFore.Click += this.SetPictureBoxColor;
            //
            // picBoxBehindLosingBack
            //
            this.picBoxBehindLosingBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindLosingBack.Cursor = Cursors.Hand;
            this.picBoxBehindLosingBack.Location = new Point(12, 110);
            this.picBoxBehindLosingBack.Name = "picBoxBehindLosingBack";
            this.picBoxBehindLosingBack.Size = new Size(20, 20);
            this.picBoxBehindLosingBack.TabStop = false;
            this.picBoxBehindLosingBack.Click += this.SetPictureBoxColor;
            //
            // picBoxBehindLosingBack2
            //
            this.picBoxBehindLosingBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindLosingBack2.Cursor = Cursors.Hand;
            this.picBoxBehindLosingBack2.Location = new Point(0x26, 110);
            this.picBoxBehindLosingBack2.Name = "picBoxBehindLosingBack2";
            this.picBoxBehindLosingBack2.Size = new Size(20, 20);
            this.picBoxBehindLosingBack2.TabStop = false;
            this.picBoxBehindLosingBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxBehindLosingBackPlain
            //
            this.picBoxBehindLosingBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxBehindLosingBackPlain.Cursor = Cursors.Hand;
            this.picBoxBehindLosingBackPlain.Location = new Point(0x48, 110);
            this.picBoxBehindLosingBackPlain.Name = "picBoxBehindLosingBackPlain";
            this.picBoxBehindLosingBackPlain.Size = new Size(20, 20);
            this.picBoxBehindLosingBackPlain.TabStop = false;
            this.picBoxBehindLosingBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelNoLoaded
            //
            this.labelNoLoaded.AutoSize = true;
            this.labelNoLoaded.Cursor = Cursors.Hand;
            this.labelNoLoaded.Location = new Point(7, 0x8e);
            this.labelNoLoaded.MinimumSize = new Size(100, 20);
            this.labelNoLoaded.Name = "labelNoLoaded";
            this.labelNoLoaded.Text = "スプリット未使用";
            this.labelNoLoaded.TextAlign = ContentAlignment.MiddleRight;
            this.labelNoLoaded.Click += this.labelNoLoaded_Click;
            //
            // picBoxNoLoadedFore
            //
            this.picBoxNoLoadedFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxNoLoadedFore.Cursor = Cursors.Hand;
            this.picBoxNoLoadedFore.Location = new Point(0x71, 0x8e);
            this.picBoxNoLoadedFore.Name = "picBoxNoLoadedFore";
            this.picBoxNoLoadedFore.Size = new Size(20, 20);
            this.picBoxNoLoadedFore.TabStop = false;
            this.picBoxNoLoadedFore.Click += this.SetPictureBoxColor;
            //
            // picBoxNoLoadedBack
            //
            this.picBoxNoLoadedBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxNoLoadedBack.Cursor = Cursors.Hand;
            this.picBoxNoLoadedBack.Location = new Point(12, 0x88);
            this.picBoxNoLoadedBack.Name = "picBoxNoLoadedBack";
            this.picBoxNoLoadedBack.Size = new Size(20, 20);
            this.picBoxNoLoadedBack.TabStop = false;
            this.picBoxNoLoadedBack.Click += this.SetPictureBoxColor;
            //
            // picBoxNoLoadedBack2
            //
            this.picBoxNoLoadedBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxNoLoadedBack2.Cursor = Cursors.Hand;
            this.picBoxNoLoadedBack2.Location = new Point(0x26, 0x88);
            this.picBoxNoLoadedBack2.Name = "picBoxNoLoadedBack2";
            this.picBoxNoLoadedBack2.Size = new Size(20, 20);
            this.picBoxNoLoadedBack2.TabStop = false;
            this.picBoxNoLoadedBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxNoLoadedBackPlain
            //
            this.picBoxNoLoadedBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxNoLoadedBackPlain.Cursor = Cursors.Hand;
            this.picBoxNoLoadedBackPlain.Location = new Point(0x48, 0x88);
            this.picBoxNoLoadedBackPlain.Name = "picBoxNoLoadedBackPlain";
            this.picBoxNoLoadedBackPlain.Size = new Size(20, 20);
            this.picBoxNoLoadedBackPlain.TabStop = false;
            this.picBoxNoLoadedBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelFinished
            //
            this.labelFinished.AutoSize = true;
            this.labelFinished.Cursor = Cursors.Hand;
            this.labelFinished.Location = new Point(7, 0xa8);
            this.labelFinished.MinimumSize = new Size(100, 20);
            this.labelFinished.Name = "labelFinished";
            this.labelFinished.Text = "完走";
            this.labelFinished.TextAlign = ContentAlignment.MiddleRight;
            this.labelFinished.Click += this.labelFinished_Click;
            //
            // picBoxFinishedFore
            //
            this.picBoxFinishedFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxFinishedFore.Cursor = Cursors.Hand;
            this.picBoxFinishedFore.Location = new Point(0x71, 0xa8);
            this.picBoxFinishedFore.Name = "picBoxFinishedFore";
            this.picBoxFinishedFore.Size = new Size(20, 20);
            this.picBoxFinishedFore.TabStop = false;
            this.picBoxFinishedFore.Click += this.SetPictureBoxColor;
            //
            // picBoxFinishedBack
            //
            this.picBoxFinishedBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxFinishedBack.Cursor = Cursors.Hand;
            this.picBoxFinishedBack.Location = new Point(12, 0xa2);
            this.picBoxFinishedBack.Name = "picBoxFinishedBack";
            this.picBoxFinishedBack.Size = new Size(20, 20);
            this.picBoxFinishedBack.TabStop = false;
            this.picBoxFinishedBack.Click += this.SetPictureBoxColor;
            //
            // picBoxFinishedBack2
            //
            this.picBoxFinishedBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxFinishedBack2.Cursor = Cursors.Hand;
            this.picBoxFinishedBack2.Location = new Point(0x26, 0xa2);
            this.picBoxFinishedBack2.Name = "picBoxFinishedBack2";
            this.picBoxFinishedBack2.Size = new Size(20, 20);
            this.picBoxFinishedBack2.TabStop = false;
            this.picBoxFinishedBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxFinishedBackPlain
            //
            this.picBoxFinishedBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxFinishedBackPlain.Cursor = Cursors.Hand;
            this.picBoxFinishedBackPlain.Location = new Point(0x48, 0xa2);
            this.picBoxFinishedBackPlain.Name = "picBoxFinishedBackPlain";
            this.picBoxFinishedBackPlain.Size = new Size(20, 20);
            this.picBoxFinishedBackPlain.TabStop = false;
            this.picBoxFinishedBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelRecord
            //
            this.labelRecord.AutoSize = true;
            this.labelRecord.Cursor = Cursors.Hand;
            this.labelRecord.Location = new Point(7, 0xc2);
            this.labelRecord.MinimumSize = new Size(100, 20);
            this.labelRecord.Name = "labelRecord";
            this.labelRecord.Text = "新記録";
            this.labelRecord.TextAlign = ContentAlignment.MiddleRight;
            this.labelRecord.Click += this.labelRecord_Click;
            //
            // picBoxRecordFore
            //
            this.picBoxRecordFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxRecordFore.Cursor = Cursors.Hand;
            this.picBoxRecordFore.Location = new Point(0x71, 0xc2);
            this.picBoxRecordFore.Name = "picBoxRecordFore";
            this.picBoxRecordFore.Size = new Size(20, 20);
            this.picBoxRecordFore.TabStop = false;
            this.picBoxRecordFore.Click += this.SetPictureBoxColor;
            //
            // picBoxRecordBack
            //
            this.picBoxRecordBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxRecordBack.Cursor = Cursors.Hand;
            this.picBoxRecordBack.Location = new Point(12, 0xbc);
            this.picBoxRecordBack.Name = "picBoxRecordBack";
            this.picBoxRecordBack.Size = new Size(20, 20);
            this.picBoxRecordBack.TabStop = false;
            this.picBoxRecordBack.Click += this.SetPictureBoxColor;
            //
            // picBoxRecordBack2
            //
            this.picBoxRecordBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxRecordBack2.Cursor = Cursors.Hand;
            this.picBoxRecordBack2.Location = new Point(0x26, 0xbc);
            this.picBoxRecordBack2.Name = "picBoxRecordBack2";
            this.picBoxRecordBack2.Size = new Size(20, 20);
            this.picBoxRecordBack2.TabStop = false;
            this.picBoxRecordBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxRecordBackPlain
            //
            this.picBoxRecordBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxRecordBackPlain.Cursor = Cursors.Hand;
            this.picBoxRecordBackPlain.Location = new Point(0x48, 0xbc);
            this.picBoxRecordBackPlain.Name = "picBoxRecordBackPlain";
            this.picBoxRecordBackPlain.Size = new Size(20, 20);
            this.picBoxRecordBackPlain.TabStop = false;
            this.picBoxRecordBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelDelay
            //
            this.labelDelay.AutoSize = true;
            this.labelDelay.Cursor = Cursors.Hand;
            this.labelDelay.Location = new Point(7, 220);
            this.labelDelay.MinimumSize = new Size(100, 20);
            this.labelDelay.Name = "labelDelay";
            this.labelDelay.Text = "遅延開始";
            this.labelDelay.TextAlign = ContentAlignment.MiddleRight;
            this.labelDelay.Click += this.labelDelay_Click;
            //
            // picBoxDelayFore
            //
            this.picBoxDelayFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDelayFore.Cursor = Cursors.Hand;
            this.picBoxDelayFore.Location = new Point(0x71, 220);
            this.picBoxDelayFore.Name = "picBoxDelayFore";
            this.picBoxDelayFore.Size = new Size(20, 20);
            this.picBoxDelayFore.TabStop = false;
            this.picBoxDelayFore.Click += this.SetPictureBoxColor;
            //
            // picBoxDelayBack
            //
            this.picBoxDelayBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDelayBack.Cursor = Cursors.Hand;
            this.picBoxDelayBack.Location = new Point(12, 0xd6);
            this.picBoxDelayBack.Name = "picBoxDelayBack";
            this.picBoxDelayBack.Size = new Size(20, 20);
            this.picBoxDelayBack.TabStop = false;
            this.picBoxDelayBack.Click += this.SetPictureBoxColor;
            //
            // picBoxDelayBack2
            //
            this.picBoxDelayBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDelayBack2.Cursor = Cursors.Hand;
            this.picBoxDelayBack2.Location = new Point(0x26, 0xd6);
            this.picBoxDelayBack2.Name = "picBoxDelayBack2";
            this.picBoxDelayBack2.Size = new Size(20, 20);
            this.picBoxDelayBack2.TabStop = false;
            this.picBoxDelayBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxDelayBackPlain
            //
            this.picBoxDelayBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDelayBackPlain.Cursor = Cursors.Hand;
            this.picBoxDelayBackPlain.Location = new Point(0x48, 0xd6);
            this.picBoxDelayBackPlain.Name = "picBoxDelayBackPlain";
            this.picBoxDelayBackPlain.Size = new Size(20, 20);
            this.picBoxDelayBackPlain.TabStop = false;
            this.picBoxDelayBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelPaused
            //
            this.labelPaused.AutoSize = true;
            this.labelPaused.Cursor = Cursors.Hand;
            this.labelPaused.Location = new Point(7, 0xf6);
            this.labelPaused.MinimumSize = new Size(100, 20);
            this.labelPaused.Name = "labelPaused";
            this.labelPaused.Text = "一時停止";
            this.labelPaused.TextAlign = ContentAlignment.MiddleRight;
            this.labelPaused.Click += this.labelPaused_Click;
            //
            // picBoxPaused
            //
            this.picBoxPaused.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxPaused.Cursor = Cursors.Hand;
            this.picBoxPaused.Location = new Point(0x71, 0xf6);
            this.picBoxPaused.Name = "picBoxPaused";
            this.picBoxPaused.Size = new Size(20, 20);
            this.picBoxPaused.TabStop = false;
            this.picBoxPaused.Click += this.SetPictureBoxColor;

            //
            // labelFlash
            //
            this.labelFlash.AutoSize = true;
            this.labelFlash.Cursor = Cursors.Hand;
            this.labelFlash.Location = new Point(7, 0x110);
            this.labelFlash.MinimumSize = new Size(100, 20);
            this.labelFlash.Name = "labelFlash";
            this.labelFlash.Text = "スプリット時ハイライト";
            this.labelFlash.TextAlign = ContentAlignment.MiddleRight;
            this.labelFlash.Click += this.labelFlash_Click;
            //
            // picBoxFlash
            //
            this.picBoxFlash.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxFlash.Cursor = Cursors.Hand;
            this.picBoxFlash.Location = new Point(0x71, 0x110);
            this.picBoxFlash.Name = "picBoxFlash";
            this.picBoxFlash.Size = new Size(20, 20);
            this.picBoxFlash.TabStop = false;
            this.picBoxFlash.Click += this.SetPictureBoxColor;

            //
            // labelStatusBar
            //
            this.labelStatusBar.AutoSize = true;
            this.labelStatusBar.Location = new Point(7, 0x12a);
            this.labelStatusBar.MinimumSize = new Size(100, 20);
            this.labelStatusBar.Name = "labelStatusBar";
            this.labelStatusBar.Text = "区間ステータス";
            this.labelStatusBar.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxStatusBarFore
            //
            this.picBoxStatusBarFore.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxStatusBarFore.Cursor = Cursors.Hand;
            this.picBoxStatusBarFore.Location = new Point(0x71, 0x12a);
            this.picBoxStatusBarFore.Name = "picBoxStatusBarFore";
            this.picBoxStatusBarFore.Size = new Size(20, 20);
            this.picBoxStatusBarFore.TabStop = false;
            this.picBoxStatusBarFore.Click += this.SetPictureBoxColor;
            //
            // picBoxStatusBarBack
            //
            this.picBoxStatusBarBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxStatusBarBack.Cursor = Cursors.Hand;
            this.picBoxStatusBarBack.Location = new Point(12, 0x124);
            this.picBoxStatusBarBack.Name = "picBoxStatusBarBack";
            this.picBoxStatusBarBack.Size = new Size(20, 20);
            this.picBoxStatusBarBack.TabStop = false;
            this.picBoxStatusBarBack.Click += this.SetPictureBoxColor;
            //
            // picBoxStatusBarBack2
            //
            this.picBoxStatusBarBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxStatusBarBack2.Cursor = Cursors.Hand;
            this.picBoxStatusBarBack2.Location = new Point(0x26, 0x124);
            this.picBoxStatusBarBack2.Name = "picBoxStatusBarBack2";
            this.picBoxStatusBarBack2.Size = new Size(20, 20);
            this.picBoxStatusBarBack2.TabStop = false;
            this.picBoxStatusBarBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxStatusBarBackPlain
            //
            this.picBoxStatusBarBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxStatusBarBackPlain.Cursor = Cursors.Hand;
            this.picBoxStatusBarBackPlain.Location = new Point(0x48, 0x124);
            this.picBoxStatusBarBackPlain.Name = "picBoxStatusBarBackPlain";
            this.picBoxStatusBarBackPlain.Size = new Size(20, 20);
            this.picBoxStatusBarBackPlain.TabStop = false;
            this.picBoxStatusBarBackPlain.Click += this.SetPictureBoxColor;

            //
            // labelRunTitle
            //
            this.labelRunTitle.AutoSize = true;
            this.labelRunTitle.Location = new Point(7, 0x144);
            this.labelRunTitle.MinimumSize = new Size(100, 20);
            this.labelRunTitle.Name = "labelRunTitle";
            this.labelRunTitle.Text = "タイトル";
            this.labelRunTitle.TextAlign = ContentAlignment.MiddleRight;
            this.picBoxRunTitleFore.BorderStyle = BorderStyle.FixedSingle;
            //
            // picBoxRunTitleFore
            //
            this.picBoxRunTitleFore.Cursor = Cursors.Hand;
            this.picBoxRunTitleFore.Location = new Point(0x71, 0x144);
            this.picBoxRunTitleFore.Name = "picBoxRunTitleFore";
            this.picBoxRunTitleFore.Size = new Size(20, 20);
            this.picBoxRunTitleFore.TabStop = false;
            this.picBoxRunTitleFore.Click += this.SetPictureBoxColor;
            //
            // picBoxRunTitleBack
            //
            this.picBoxRunTitleBack.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxRunTitleBack.Cursor = Cursors.Hand;
            this.picBoxRunTitleBack.Location = new Point(12, 0x13e);
            this.picBoxRunTitleBack.Name = "picBoxRunTitleBack";
            this.picBoxRunTitleBack.Size = new Size(20, 20);
            this.picBoxRunTitleBack.TabStop = false;
            this.picBoxRunTitleBack.Click += this.SetPictureBoxColor;
            //
            // picBoxRunTitleBack2
            //
            this.picBoxRunTitleBack2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxRunTitleBack2.Cursor = Cursors.Hand;
            this.picBoxRunTitleBack2.Location = new Point(0x26, 0x13e);
            this.picBoxRunTitleBack2.Name = "picBoxRunTitleBack2";
            this.picBoxRunTitleBack2.Size = new Size(20, 20);
            this.picBoxRunTitleBack2.TabStop = false;
            this.picBoxRunTitleBack2.Click += this.SetPictureBoxColor;
            //
            // picBoxRunTitleBackPlain
            //
            this.picBoxRunTitleBackPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxRunTitleBackPlain.Cursor = Cursors.Hand;
            this.picBoxRunTitleBackPlain.Location = new Point(0x48, 0x13e);
            this.picBoxRunTitleBackPlain.Name = "picBoxRunTitleBackPlain";
            this.picBoxRunTitleBackPlain.Size = new Size(20, 20);
            this.picBoxRunTitleBackPlain.TabStop = false;
            this.picBoxRunTitleBackPlain.Click += this.SetPictureBoxColor;

            // ----------------------------------------
            // Segment Tab:
            //
            // labelColumnSegColor
            //
            this.labelColumnSegColor.AutoSize = true;
            this.labelColumnSegColor.Location = new Point(0x8d, 3);
            this.labelColumnSegColor.Name = "labelColumnSegColor";
            this.labelColumnSegColor.Size = new Size(0x1f, 13);
            this.labelColumnSegColor.Text = "色";
            //
            // labelColumnSegColor2
            //
            this.labelColumnSegColor2.AutoSize = true;
            this.labelColumnSegColor2.Location = new Point(0xB2, 3);
            this.labelColumnSegColor2.Name = "labelColumnSegColor2";
            this.labelColumnSegColor2.Size = new Size(0x25, 13);
            this.labelColumnSegColor2.Text = "色2";
            //
            // labelColumnSegPlain
            //
            this.labelColumnSegPlain.AutoSize = true;
            this.labelColumnSegPlain.Location = new Point(0xd1, 3);
            this.labelColumnSegPlain.Name = "labelColumnSegPlain";
            this.labelColumnSegPlain.Size = new Size(30, 13);
            this.labelColumnSegPlain.Text = "単色";

            //
            // labelSegBackground
            //
            this.labelSegBackground.AutoSize = true;
            this.labelSegBackground.Location = new Point(12, 0x13);
            this.labelSegBackground.MinimumSize = new Size(0x80, 20);
            this.labelSegBackground.Name = "labelSegBackground";
            this.labelSegBackground.Text = "背景";
            this.labelSegBackground.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegBackground
            //
            this.picBoxSegBackground.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegBackground.Cursor = Cursors.Hand;
            this.picBoxSegBackground.Location = new Point(0x92, 0x13);
            this.picBoxSegBackground.Name = "picBoxSegBackground";
            this.picBoxSegBackground.Size = new Size(20, 20);
            this.picBoxSegBackground.TabStop = false;
            this.picBoxSegBackground.Click += this.SetPictureBoxColor;
            //
            // picBoxSegBackground2
            //
            this.picBoxSegBackground2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegBackground2.Cursor = Cursors.Hand;
            this.picBoxSegBackground2.Location = new Point(180, 0x13);
            this.picBoxSegBackground2.Name = "picBoxSegBackground2";
            this.picBoxSegBackground2.Size = new Size(20, 20);
            this.picBoxSegBackground2.TabStop = false;
            this.picBoxSegBackground2.Click += this.SetPictureBoxColor;
            //
            // picBoxSegBackgroundPlain
            //
            this.picBoxSegBackgroundPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegBackgroundPlain.Cursor = Cursors.Hand;
            this.picBoxSegBackgroundPlain.Location = new Point(0xd6, 0x13);
            this.picBoxSegBackgroundPlain.Name = "picBoxSegBackgroundPlain";
            this.picBoxSegBackgroundPlain.Size = new Size(20, 20);
            this.picBoxSegBackgroundPlain.TabStop = false;
            this.picBoxSegBackgroundPlain.Click += this.SetPictureBoxColor;

            //
            // labelSegHighlight
            //
            this.labelSegHighlight.AutoSize = true;
            this.labelSegHighlight.Location = new Point(12, 0x2d);
            this.labelSegHighlight.MinimumSize = new Size(0x80, 20);
            this.labelSegHighlight.Name = "labelSegHighlight";
            this.labelSegHighlight.Text = "区間ハイライト";
            this.labelSegHighlight.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegHighlight
            //
            this.picBoxSegHighlight.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegHighlight.Cursor = Cursors.Hand;
            this.picBoxSegHighlight.Location = new Point(0x92, 0x2d);
            this.picBoxSegHighlight.Name = "picBoxSegHighlight";
            this.picBoxSegHighlight.Size = new Size(20, 20);
            this.picBoxSegHighlight.TabStop = false;
            this.picBoxSegHighlight.Click += this.SetPictureBoxColor;
            //
            // picBoxSegHighlight2
            //
            this.picBoxSegHighlight2.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegHighlight2.Cursor = Cursors.Hand;
            this.picBoxSegHighlight2.Location = new Point(180, 0x2d);
            this.picBoxSegHighlight2.Name = "picBoxSegHighlight2";
            this.picBoxSegHighlight2.Size = new Size(20, 20);
            this.picBoxSegHighlight2.TabStop = false;
            this.picBoxSegHighlight2.Click += this.SetPictureBoxColor;
            //
            // picBoxSegHighlightPlain
            //
            this.picBoxSegHighlightPlain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegHighlightPlain.Cursor = Cursors.Hand;
            this.picBoxSegHighlightPlain.Location = new Point(0xd6, 0x2d);
            this.picBoxSegHighlightPlain.Name = "picBoxSegHighlightPlain";
            this.picBoxSegHighlightPlain.Size = new Size(20, 20);
            this.picBoxSegHighlightPlain.TabStop = false;
            this.picBoxSegHighlightPlain.Click += this.SetPictureBoxColor;

            //
            // labelSegHighlightBorder
            //
            this.labelSegHighlightBorder.AutoSize = true;
            this.labelSegHighlightBorder.Location = new Point(12, 0x47);
            this.labelSegHighlightBorder.MinimumSize = new Size(0x80, 20);
            this.labelSegHighlightBorder.Name = "labelSegHighlightBorder";
            this.labelSegHighlightBorder.Text = "ハイライトの上下線";
            this.labelSegHighlightBorder.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegHighlightBorder
            //
            this.picBoxSegHighlightBorder.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegHighlightBorder.Cursor = Cursors.Hand;
            this.picBoxSegHighlightBorder.Location = new Point(0x92, 0x47);
            this.picBoxSegHighlightBorder.Name = "picBoxSegHighlightBorder";
            this.picBoxSegHighlightBorder.Size = new Size(20, 20);
            this.picBoxSegHighlightBorder.TabStop = false;
            this.picBoxSegHighlightBorder.Click += this.SetPictureBoxColor;

            //
            // labelSegPastText
            //
            this.labelSegPastText.AutoSize = true;
            this.labelSegPastText.Location = new Point(12, 0x61);
            this.labelSegPastText.MinimumSize = new Size(0x80, 20);
            this.labelSegPastText.Name = "labelSegPastText";
            this.labelSegPastText.Text = "過去区間";
            this.labelSegPastText.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegPastText
            //
            this.picBoxSegPastText.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegPastText.Cursor = Cursors.Hand;
            this.picBoxSegPastText.Location = new Point(0x92, 0x61);
            this.picBoxSegPastText.Name = "picBoxSegPastText";
            this.picBoxSegPastText.Size = new Size(20, 20);
            this.picBoxSegPastText.TabStop = false;
            this.picBoxSegPastText.Click += this.SetPictureBoxColor;

            //
            // labelSegLiveText
            //
            this.labelSegLiveText.AutoSize = true;
            this.labelSegLiveText.Location = new Point(12, 0x7b);
            this.labelSegLiveText.MinimumSize = new Size(0x80, 20);
            this.labelSegLiveText.Name = "labelSegLiveText";
            this.labelSegLiveText.Text = "現区間";
            this.labelSegLiveText.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegLiveText
            //
            this.picBoxSegLiveText.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegLiveText.Cursor = Cursors.Hand;
            this.picBoxSegLiveText.Location = new Point(0x92, 0x7b);
            this.picBoxSegLiveText.Name = "picBoxSegLiveText";
            this.picBoxSegLiveText.Size = new Size(20, 20);
            this.picBoxSegLiveText.TabStop = false;
            this.picBoxSegLiveText.Click += this.SetPictureBoxColor;

            //
            // labelSegFutureText
            //
            this.labelSegFutureText.AutoSize = true;
            this.labelSegFutureText.Location = new Point(12, 0x95);
            this.labelSegFutureText.MinimumSize = new Size(0x80, 20);
            this.labelSegFutureText.Name = "labelSegFutureText";
            this.labelSegFutureText.Text = "未来区間名";
            this.labelSegFutureText.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegFutureText
            //
            this.picBoxSegFutureText.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegFutureText.Cursor = Cursors.Hand;
            this.picBoxSegFutureText.Location = new Point(0x92, 0x95);
            this.picBoxSegFutureText.Name = "picBoxSegFutureText";
            this.picBoxSegFutureText.Size = new Size(20, 20);
            this.picBoxSegFutureText.TabStop = false;
            this.picBoxSegFutureText.Click += this.SetPictureBoxColor;

            //
            // labelSegFutureTime
            //
            this.labelSegFutureTime.AutoSize = true;
            this.labelSegFutureTime.Location = new Point(12, 0xaf);
            this.labelSegFutureTime.MinimumSize = new Size(0x80, 20);
            this.labelSegFutureTime.Name = "labelSegFutureTime";
            this.labelSegFutureTime.Text = "未来区間タイム";
            this.labelSegFutureTime.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegFutureTime
            //
            this.picBoxSegFutureTime.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegFutureTime.Cursor = Cursors.Hand;
            this.picBoxSegFutureTime.Location = new Point(0x92, 0xaf);
            this.picBoxSegFutureTime.Name = "picBoxSegFutureTime";
            this.picBoxSegFutureTime.Size = new Size(20, 20);
            this.picBoxSegFutureTime.TabStop = false;
            this.picBoxSegFutureTime.Click += this.SetPictureBoxColor;

            //
            // labelSegNewTime
            //
            this.labelSegNewTime.AutoSize = true;
            this.labelSegNewTime.Location = new Point(12, 0xc9);
            this.labelSegNewTime.MinimumSize = new Size(0x80, 20);
            this.labelSegNewTime.Name = "labelSegNewTime";
            this.labelSegNewTime.Text = "新規タイム";
            this.labelSegNewTime.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegNewTime
            //
            this.picBoxSegNewTime.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegNewTime.Cursor = Cursors.Hand;
            this.picBoxSegNewTime.Location = new Point(0x92, 0xc9);
            this.picBoxSegNewTime.Name = "picBoxSegNewTime";
            this.picBoxSegNewTime.Size = new Size(20, 20);
            this.picBoxSegNewTime.TabStop = false;
            this.picBoxSegNewTime.Click += this.SetPictureBoxColor;

            //
            // labelSegMissing
            //
            this.labelSegMissing.AutoSize = true;
            this.labelSegMissing.Location = new Point(12, 0xe3);
            this.labelSegMissing.MinimumSize = new Size(0x80, 20);
            this.labelSegMissing.Name = "labelSegMissing";
            this.labelSegMissing.Text = "区間タイムなし";
            this.labelSegMissing.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegMissing
            //
            this.picBoxSegMissing.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegMissing.Cursor = Cursors.Hand;
            this.picBoxSegMissing.Location = new Point(0x92, 0xe3);
            this.picBoxSegMissing.Name = "picBoxSegMissing";
            this.picBoxSegMissing.Size = new Size(20, 20);
            this.picBoxSegMissing.TabStop = false;
            this.picBoxSegMissing.Click += this.SetPictureBoxColor;

            //
            // labelSegBestSegment
            //
            this.labelSegBestSegment.AutoSize = true;
            this.labelSegBestSegment.Location = new Point(12, 0xfd);
            this.labelSegBestSegment.MinimumSize = new Size(0x80, 20);
            this.labelSegBestSegment.Name = "labelSegBestSegment";
            this.labelSegBestSegment.Text = "区間新記録";
            this.labelSegBestSegment.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegBestSegment
            //
            this.picBoxSegBestSegment.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegBestSegment.Cursor = Cursors.Hand;
            this.picBoxSegBestSegment.Location = new Point(0x92, 0xfd);
            this.picBoxSegBestSegment.Name = "SegBestSegment";
            this.picBoxSegBestSegment.Size = new Size(20, 20);
            this.picBoxSegBestSegment.TabStop = false;
            this.picBoxSegBestSegment.Click += this.SetPictureBoxColor;

            //
            // labelSegAheadGain
            //
            this.labelSegAheadGain.AutoSize = true;
            this.labelSegAheadGain.Location = new Point(12, 0x117);
            this.labelSegAheadGain.MinimumSize = new Size(0x80, 20);
            this.labelSegAheadGain.Name = "labelSegAheadGain";
            this.labelSegAheadGain.Text = "貯金増";
            this.labelSegAheadGain.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegAheadGain
            //
            this.picBoxSegAheadGain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegAheadGain.Cursor = Cursors.Hand;
            this.picBoxSegAheadGain.Location = new Point(0x92, 0x117);
            this.picBoxSegAheadGain.Name = "picBoxSegAheadGain";
            this.picBoxSegAheadGain.Size = new Size(20, 20);
            this.picBoxSegAheadGain.TabStop = false;
            this.picBoxSegAheadGain.Click += this.SetPictureBoxColor;

            //
            // labelSegAheadLoss
            //
            this.labelSegAheadLoss.AutoSize = true;
            this.labelSegAheadLoss.Location = new Point(12, 0x131);
            this.labelSegAheadLoss.MinimumSize = new Size(0x80, 20);
            this.labelSegAheadLoss.Name = "labelSegAheadLoss";
            this.labelSegAheadLoss.Text = "貯金減";
            this.labelSegAheadLoss.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegAheadLoss
            //
            this.picBoxSegAheadLoss.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegAheadLoss.Cursor = Cursors.Hand;
            this.picBoxSegAheadLoss.Location = new Point(0x92, 0x131);
            this.picBoxSegAheadLoss.Name = "picBoxSegAheadLoss";
            this.picBoxSegAheadLoss.Size = new Size(20, 20);
            this.picBoxSegAheadLoss.TabStop = false;
            this.picBoxSegAheadLoss.Click += this.SetPictureBoxColor;

            //
            // labelSegBehindGain
            //
            this.labelSegBehindGain.AutoSize = true;
            this.labelSegBehindGain.Location = new Point(12, 0x14b);
            this.labelSegBehindGain.MinimumSize = new Size(0x80, 20);
            this.labelSegBehindGain.Name = "labelSegBehindGain";
            this.labelSegBehindGain.Text = "借金減";
            this.labelSegBehindGain.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegBehindGain
            //
            this.picBoxSegBehindGain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegBehindGain.Cursor = Cursors.Hand;
            this.picBoxSegBehindGain.Location = new Point(0x92, 0x14b);
            this.picBoxSegBehindGain.Name = "picBoxSegBehindGain";
            this.picBoxSegBehindGain.Size = new Size(20, 20);
            this.picBoxSegBehindGain.TabStop = false;
            this.picBoxSegBehindGain.Click += this.SetPictureBoxColor;

            //
            // labelSegBehindLoss
            //
            this.labelSegBehindLoss.AutoSize = true;
            this.labelSegBehindLoss.Location = new Point(12, 0x165);
            this.labelSegBehindLoss.MinimumSize = new Size(0x80, 20);
            this.labelSegBehindLoss.Name = "labelSegBehindLoss";
            this.labelSegBehindLoss.Text = "借金増";
            this.labelSegBehindLoss.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxSegBehindLoss
            //
            this.picBoxSegBehindLoss.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxSegBehindLoss.Cursor = Cursors.Hand;
            this.picBoxSegBehindLoss.Location = new Point(0x92, 0x165);
            this.picBoxSegBehindLoss.Name = "picBoxSegBehindLoss";
            this.picBoxSegBehindLoss.Size = new Size(20, 20);
            this.picBoxSegBehindLoss.TabStop = false;
            this.picBoxSegBehindLoss.Click += this.SetPictureBoxColor;

            // ----------------------------------------
            // Detailed View tab:
            //
            // checkBoxDViewUsePrimary
            //
            this.checkBoxDViewUsePrimary.AutoSize = true;
            this.checkBoxDViewUsePrimary.Location = new Point(12, 10);
            this.checkBoxDViewUsePrimary.MinimumSize = new Size(128, 20);
            this.checkBoxDViewUsePrimary.Name = "checkBoxDViewUsePrimary";
            this.checkBoxDViewUsePrimary.Text = "本タイマーの色設定を使う";
            this.checkBoxDViewUsePrimary.TextAlign = ContentAlignment.BottomLeft;
            this.checkBoxDViewUsePrimary.CheckedChanged += checkBoxDViewUsePrimary_CheckedChanged;

            //
            // groupBoxDViewClock
            //
            this.groupBoxDViewClock.Controls.AddRange(new Control[]
            {
                this.labelDViewAhead, this.picBoxDViewAhead, this.labelDViewAheadLosing, this.picBoxDViewAheadLosing,
                this.labelDViewBehind, this.picBoxDViewBehind, this.labelDViewBehindLosing, this.picBoxDViewBehindLosing,
                this.labelDViewFinished, this.picBoxDViewFinished, this.labelDViewRecord, this.picBoxDViewRecord,
                this.labelDViewDelay, this.picBoxDViewDelay, this.labelDViewPaused, this.picBoxDViewPaused,
                this.labelDViewFlash, this.picBoxDViewFlash
            });
            this.groupBoxDViewClock.Location = new Point(5, 30);
            this.groupBoxDViewClock.Name = "groupBoxDViewClock";
            this.groupBoxDViewClock.Size = new Size(237, 148);
            this.groupBoxDViewClock.TabStop = false;
            this.groupBoxDViewClock.Text = "タイムの色";
            //
            // groupBoxDViewSegments
            //
            this.groupBoxDViewSegments.Controls.AddRange(new Control[]
            {
                this.labelDViewSegHighlight, this.picBoxDViewSegHighlight, this.labelDViewSegDefaultText, this.picBoxDViewSegDefaultText,
                this.labelDViewSegCurrentText, this.picBoxDViewSegCurrentText, this.labelDViewSegMissingTime, this.picBoxDViewSegMissingTime,
                this.labelDViewSegBestSegment, this.picBoxDViewSegBestSegment, this.labelDViewSegAheadGain, this.picBoxDViewSegAheadGain,
                this.labelDViewSegAheadLoss, this.picBoxDViewSegAheadLoss, this.labelDViewSegBehindGain, this.picBoxDViewSegBehindGain,
                this.labelDViewSegBehindLoss, this.picBoxDViewSegBehindLoss
            });
            this.groupBoxDViewSegments.Location = new Point(5, 183);
            this.groupBoxDViewSegments.Name = "groupBoxDViewSegments";
            this.groupBoxDViewSegments.Size = new Size(237, 148);
            this.groupBoxDViewSegments.TabStop = false;
            this.groupBoxDViewSegments.Text = "区間の色";
            //
            // groupBoxGraph
            //
            this.groupBoxGraph.Controls.AddRange(new Control[]
            {
                this.labelGraphAhead, this.picBoxGraphAhead, this.labelGraphBehind, this.picBoxGraphBehind
            });
            this.groupBoxGraph.Location = new Point(5, 336);
            this.groupBoxGraph.Name = "groupBoxGraph";
            this.groupBoxGraph.Size = new Size(237, 44);
            this.groupBoxGraph.TabStop = false;
            this.groupBoxGraph.Text = "グラフ";

            //
            // labelDViewAhead
            //
            this.labelDViewAhead.AutoSize = true;
            this.labelDViewAhead.Location = new Point(5, 15);
            this.labelDViewAhead.MinimumSize = new Size(84, 20);
            this.labelDViewAhead.Name = "labelDViewAhead";
            this.labelDViewAhead.Text = "貯金増";
            this.labelDViewAhead.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewAhead
            //
            this.picBoxDViewAhead.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewAhead.Cursor = Cursors.Hand;
            this.picBoxDViewAhead.Location = new Point(95, 15);
            this.picBoxDViewAhead.Name = "picBoxDViewAhead";
            this.picBoxDViewAhead.Size = new Size(20, 20);
            this.picBoxDViewAhead.TabStop = false;
            this.picBoxDViewAhead.Click += this.SetPictureBoxColor;

            //
            // labelDViewAheadLosing
            //
            this.labelDViewAheadLosing.AutoSize = true;
            this.labelDViewAheadLosing.Location = new Point(148, 15);
            this.labelDViewAheadLosing.MinimumSize = new Size(84, 20);
            this.labelDViewAheadLosing.Name = "labelDViewAheadLosing";
            this.labelDViewAheadLosing.Text = "貯金減";
            this.labelDViewAheadLosing.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewAheadLosing
            //
            this.picBoxDViewAheadLosing.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewAheadLosing.Cursor = Cursors.Hand;
            this.picBoxDViewAheadLosing.Location = new Point(122, 15);
            this.picBoxDViewAheadLosing.Name = "picBoxDViewAheadLosing";
            this.picBoxDViewAheadLosing.Size = new Size(20, 20);
            this.picBoxDViewAheadLosing.TabStop = false;
            this.picBoxDViewAheadLosing.Click += this.SetPictureBoxColor;

            //
            // labelDViewBehind
            //
            this.labelDViewBehind.AutoSize = true;
            this.labelDViewBehind.Location = new Point(5, 41);
            this.labelDViewBehind.MinimumSize = new Size(84, 20);
            this.labelDViewBehind.Name = "labelDViewBehind";
            this.labelDViewBehind.Text = "借金減";
            this.labelDViewBehind.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewBehind
            //
            this.picBoxDViewBehind.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewBehind.Cursor = Cursors.Hand;
            this.picBoxDViewBehind.Location = new Point(95, 41);
            this.picBoxDViewBehind.Name = "picBoxDViewBehind";
            this.picBoxDViewBehind.Size = new Size(20, 20);
            this.picBoxDViewBehind.TabStop = false;
            this.picBoxDViewBehind.Click += this.SetPictureBoxColor;

            //
            // labelDViewBehindLosing
            //
            this.labelDViewBehindLosing.AutoSize = true;
            this.labelDViewBehindLosing.Location = new Point(148, 41);
            this.labelDViewBehindLosing.MinimumSize = new Size(84, 20);
            this.labelDViewBehindLosing.Name = "labelDViewBehindLosing";
            this.labelDViewBehindLosing.Text = "借金増";
            this.labelDViewBehindLosing.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewBehindLosing
            //
            this.picBoxDViewBehindLosing.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewBehindLosing.Cursor = Cursors.Hand;
            this.picBoxDViewBehindLosing.Location = new Point(122, 41);
            this.picBoxDViewBehindLosing.Name = "picBoxDViewBehindLosing";
            this.picBoxDViewBehindLosing.Size = new Size(20, 20);
            this.picBoxDViewBehindLosing.TabStop = false;
            this.picBoxDViewBehindLosing.Click += this.SetPictureBoxColor;

            //
            // labelDViewFinished
            //
            this.labelDViewFinished.AutoSize = true;
            this.labelDViewFinished.Location = new Point(5, 67);
            this.labelDViewFinished.MinimumSize = new Size(84, 20);
            this.labelDViewFinished.Name = "labelDViewFinished";
            this.labelDViewFinished.Text = "完走";
            this.labelDViewFinished.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewFinished
            //
            this.picBoxDViewFinished.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewFinished.Cursor = Cursors.Hand;
            this.picBoxDViewFinished.Location = new Point(95, 67);
            this.picBoxDViewFinished.Name = "picBoxDViewFinished";
            this.picBoxDViewFinished.Size = new Size(20, 20);
            this.picBoxDViewFinished.TabStop = false;
            this.picBoxDViewFinished.Click += this.SetPictureBoxColor;

            //
            // labelDViewRecord
            //
            this.labelDViewRecord.AutoSize = true;
            this.labelDViewRecord.Location = new Point(148, 67);
            this.labelDViewRecord.MinimumSize = new Size(84, 20);
            this.labelDViewRecord.Name = "labelDViewRecord";
            this.labelDViewRecord.Text = "新記録";
            this.labelDViewRecord.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewRecord
            //
            this.picBoxDViewRecord.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewRecord.Cursor = Cursors.Hand;
            this.picBoxDViewRecord.Location = new Point(122, 67);
            this.picBoxDViewRecord.Name = "picBoxDViewRecord";
            this.picBoxDViewRecord.Size = new Size(20, 20);
            this.picBoxDViewRecord.TabStop = false;
            this.picBoxDViewRecord.Click += this.SetPictureBoxColor;

            //
            // labelDViewDelay
            //
            this.labelDViewDelay.AutoSize = true;
            this.labelDViewDelay.Location = new Point(5, 93);
            this.labelDViewDelay.MinimumSize = new Size(84, 20);
            this.labelDViewDelay.Name = "labelDViewDelay";
            this.labelDViewDelay.Text = "遅延開始";
            this.labelDViewDelay.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewDelay
            //
            this.picBoxDViewDelay.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewDelay.Cursor = Cursors.Hand;
            this.picBoxDViewDelay.Location = new Point(95, 93);
            this.picBoxDViewDelay.Name = "picBoxDViewDelay";
            this.picBoxDViewDelay.Size = new Size(20, 20);
            this.picBoxDViewDelay.TabStop = false;
            this.picBoxDViewDelay.Click += this.SetPictureBoxColor;

            //
            // labelDViewPaused
            //
            this.labelDViewPaused.AutoSize = true;
            this.labelDViewPaused.Location = new Point(148, 93);
            this.labelDViewPaused.MinimumSize = new Size(84, 20);
            this.labelDViewPaused.Name = "labelDViewPaused";
            this.labelDViewPaused.Text = "一時停止";
            this.labelDViewPaused.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewPaused
            //
            this.picBoxDViewPaused.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewPaused.Cursor = Cursors.Hand;
            this.picBoxDViewPaused.Location = new Point(122, 93);
            this.picBoxDViewPaused.Name = "picBoxDViewPaused";
            this.picBoxDViewPaused.Size = new Size(20, 20);
            this.picBoxDViewPaused.TabStop = false;
            this.picBoxDViewPaused.Click += this.SetPictureBoxColor;

            //
            // labelDViewFlash
            //
            this.labelDViewFlash.AutoSize = true;
            this.labelDViewFlash.Location = new Point(122, 119);
            this.labelDViewFlash.MinimumSize = new Size(84, 20);
            this.labelDViewFlash.Name = "labelDViewFlash";
            this.labelDViewFlash.Text = "スプリット時ハイライト";
            this.labelDViewFlash.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewFlash
            //
            this.picBoxDViewFlash.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewFlash.Cursor = Cursors.Hand;
            this.picBoxDViewFlash.Location = new Point(95, 119);
            this.picBoxDViewFlash.Name = "picBoxDViewFlash";
            this.picBoxDViewFlash.Size = new Size(20, 20);
            this.picBoxDViewFlash.TabStop = false;
            this.picBoxDViewFlash.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegCurrentText
            //
            this.labelDViewSegCurrentText.AutoSize = true;
            this.labelDViewSegCurrentText.Location = new Point(5, 15);
            this.labelDViewSegCurrentText.MinimumSize = new Size(84, 20);
            this.labelDViewSegCurrentText.Name = "labelDViewSegCurrentText";
            this.labelDViewSegCurrentText.Text = "比較対象";
            this.labelDViewSegCurrentText.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewSegCurrentText
            //
            this.picBoxDViewSegCurrentText.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegCurrentText.Cursor = Cursors.Hand;
            this.picBoxDViewSegCurrentText.Location = new Point(95, 15);
            this.picBoxDViewSegCurrentText.Name = "picBoxDViewSegCurrentText";
            this.picBoxDViewSegCurrentText.Size = new Size(20, 20);
            this.picBoxDViewSegCurrentText.TabStop = false;
            this.picBoxDViewSegCurrentText.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegDefaultText
            //
            this.labelDViewSegDefaultText.AutoSize = true;
            this.labelDViewSegDefaultText.Location = new Point(148, 15);
            this.labelDViewSegDefaultText.MinimumSize = new Size(84, 20);
            this.labelDViewSegDefaultText.Name = "labelDViewSegDefaultText";
            this.labelDViewSegDefaultText.Text = "デフォルト";
            this.labelDViewSegDefaultText.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewSegDefaultText
            //
            this.picBoxDViewSegDefaultText.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegDefaultText.Cursor = Cursors.Hand;
            this.picBoxDViewSegDefaultText.Location = new Point(122, 15);
            this.picBoxDViewSegDefaultText.Name = "picBoxDViewSegDefaultText";
            this.picBoxDViewSegDefaultText.Size = new Size(20, 20);
            this.picBoxDViewSegDefaultText.TabStop = false;
            this.picBoxDViewSegDefaultText.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegMissingTime
            //
            this.labelDViewSegMissingTime.AutoSize = true;
            this.labelDViewSegMissingTime.Location = new Point(5, 41);
            this.labelDViewSegMissingTime.MinimumSize = new Size(84, 20);
            this.labelDViewSegMissingTime.Name = "labelDViewSegMissingTime";
            this.labelDViewSegMissingTime.Text = "記録なし";
            this.labelDViewSegMissingTime.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewSegMissingTime
            //
            this.picBoxDViewSegMissingTime.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegMissingTime.Cursor = Cursors.Hand;
            this.picBoxDViewSegMissingTime.Location = new Point(95, 41);
            this.picBoxDViewSegMissingTime.Name = "picBoxDViewSegMissingTime";
            this.picBoxDViewSegMissingTime.Size = new Size(20, 20);
            this.picBoxDViewSegMissingTime.TabStop = false;
            this.picBoxDViewSegMissingTime.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegBestSegment
            //
            this.labelDViewSegBestSegment.AutoSize = true;
            this.labelDViewSegBestSegment.Location = new Point(148, 41);
            this.labelDViewSegBestSegment.MinimumSize = new Size(84, 20);
            this.labelDViewSegBestSegment.Name = "labelDViewSegBestSegment";
            this.labelDViewSegBestSegment.Text = "新記録";
            this.labelDViewSegBestSegment.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewSegBestSegment
            //
            this.picBoxDViewSegBestSegment.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegBestSegment.Cursor = Cursors.Hand;
            this.picBoxDViewSegBestSegment.Location = new Point(122, 41);
            this.picBoxDViewSegBestSegment.Name = "picBoxDViewSegBestSegment";
            this.picBoxDViewSegBestSegment.Size = new Size(20, 20);
            this.picBoxDViewSegBestSegment.TabStop = false;
            this.picBoxDViewSegBestSegment.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegAheadGain
            //
            this.labelDViewSegAheadGain.AutoSize = true;
            this.labelDViewSegAheadGain.Location = new Point(5, 67);
            this.labelDViewSegAheadGain.MinimumSize = new Size(84, 20);
            this.labelDViewSegAheadGain.Name = "labelDViewSegAheadGain";
            this.labelDViewSegAheadGain.Text = "貯金増";
            this.labelDViewSegAheadGain.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewSegAheadGain
            //
            this.picBoxDViewSegAheadGain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegAheadGain.Cursor = Cursors.Hand;
            this.picBoxDViewSegAheadGain.Location = new Point(95, 67);
            this.picBoxDViewSegAheadGain.Name = "picBoxDViewSegAheadGain";
            this.picBoxDViewSegAheadGain.Size = new Size(20, 20);
            this.picBoxDViewSegAheadGain.TabStop = false;
            this.picBoxDViewSegAheadGain.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegAheadLoss
            //
            this.labelDViewSegAheadLoss.AutoSize = true;
            this.labelDViewSegAheadLoss.Location = new Point(148, 67);
            this.labelDViewSegAheadLoss.MinimumSize = new Size(84, 20);
            this.labelDViewSegAheadLoss.Name = "labelDViewSegAheadLoss";
            this.labelDViewSegAheadLoss.Text = "貯金減";
            this.labelDViewSegAheadLoss.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewSegAheadLoss
            //
            this.picBoxDViewSegAheadLoss.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegAheadLoss.Cursor = Cursors.Hand;
            this.picBoxDViewSegAheadLoss.Location = new Point(122, 67);
            this.picBoxDViewSegAheadLoss.Name = "picBoxDViewSegAheadLoss";
            this.picBoxDViewSegAheadLoss.Size = new Size(20, 20);
            this.picBoxDViewSegAheadLoss.TabStop = false;
            this.picBoxDViewSegAheadLoss.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegBehindGain
            //
            this.labelDViewSegBehindGain.AutoSize = true;
            this.labelDViewSegBehindGain.Location = new Point(5, 93);
            this.labelDViewSegBehindGain.MinimumSize = new Size(84, 20);
            this.labelDViewSegBehindGain.Name = "labelDViewSegBehindGain";
            this.labelDViewSegBehindGain.Text = "借金減";
            this.labelDViewSegBehindGain.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewSegBehindGain
            //
            this.picBoxDViewSegBehindGain.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegBehindGain.Cursor = Cursors.Hand;
            this.picBoxDViewSegBehindGain.Location = new Point(95, 93);
            this.picBoxDViewSegBehindGain.Name = "picBoxDViewSegBehindGain";
            this.picBoxDViewSegBehindGain.Size = new Size(20, 20);
            this.picBoxDViewSegBehindGain.TabStop = false;
            this.picBoxDViewSegBehindGain.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegBehindLoss
            //
            this.labelDViewSegBehindLoss.AutoSize = true;
            this.labelDViewSegBehindLoss.Location = new Point(148, 93);
            this.labelDViewSegBehindLoss.MinimumSize = new Size(84, 20);
            this.labelDViewSegBehindLoss.Name = "labelDViewSegBehindLoss";
            this.labelDViewSegBehindLoss.Text = "借金増";
            this.labelDViewSegBehindLoss.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxDViewSegBehindLoss
            //
            this.picBoxDViewSegBehindLoss.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegBehindLoss.Cursor = Cursors.Hand;
            this.picBoxDViewSegBehindLoss.Location = new Point(122, 93);
            this.picBoxDViewSegBehindLoss.Name = "picBoxDViewSegBehindLoss";
            this.picBoxDViewSegBehindLoss.Size = new Size(20, 20);
            this.picBoxDViewSegBehindLoss.TabStop = false;
            this.picBoxDViewSegBehindLoss.Click += this.SetPictureBoxColor;

            //
            // labelDViewSegHighlight
            //
            this.labelDViewSegHighlight.AutoSize = true;
            this.labelDViewSegHighlight.Location = new Point(5, 119);
            this.labelDViewSegHighlight.MinimumSize = new Size(84, 20);
            this.labelDViewSegHighlight.Name = "labelDViewSegHighlight";
            this.labelDViewSegHighlight.Text = "現区間ハイライト";
            this.labelDViewSegHighlight.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxDViewSegHighlight
            //
            this.picBoxDViewSegHighlight.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxDViewSegHighlight.Cursor = Cursors.Hand;
            this.picBoxDViewSegHighlight.Location = new Point(95, 119);
            this.picBoxDViewSegHighlight.Name = "picBoxDViewSegHighlight";
            this.picBoxDViewSegHighlight.Size = new Size(20, 20);
            this.picBoxDViewSegHighlight.TabStop = false;
            this.picBoxDViewSegHighlight.Click += this.SetPictureBoxColor;

            //
            // labelGraphAhead
            //
            this.labelGraphAhead.AutoSize = true;
            this.labelGraphAhead.Location = new Point(5, 15);
            this.labelGraphAhead.MinimumSize = new Size(84, 20);
            this.labelGraphAhead.Name = "labelGraphAhead";
            this.labelGraphAhead.Text = "貯金";
            this.labelGraphAhead.TextAlign = ContentAlignment.MiddleRight;
            //
            // picBoxGraphAhead
            //
            this.picBoxGraphAhead.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxGraphAhead.Cursor = Cursors.Hand;
            this.picBoxGraphAhead.Location = new Point(95, 15);
            this.picBoxGraphAhead.Name = "picBoxGraphAhead";
            this.picBoxGraphAhead.Size = new Size(20, 20);
            this.picBoxGraphAhead.TabStop = false;
            this.picBoxGraphAhead.Click += this.SetPictureBoxColor;

            //
            // labelGraphBehind
            //
            this.labelGraphBehind.AutoSize = true;
            this.labelGraphBehind.Location = new Point(148, 15);
            this.labelGraphBehind.MinimumSize = new Size(84, 20);
            this.labelGraphBehind.Name = "labelGraphBehind";
            this.labelGraphBehind.Text = "借金";
            this.labelGraphBehind.TextAlign = ContentAlignment.MiddleLeft;
            //
            // picBoxGraphBehind
            //
            this.picBoxGraphBehind.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxGraphBehind.Cursor = Cursors.Hand;
            this.picBoxGraphBehind.Location = new Point(122, 15);
            this.picBoxGraphBehind.Name = "picBoxGraphBehind";
            this.picBoxGraphBehind.Size = new Size(20, 20);
            this.picBoxGraphBehind.TabStop = false;
            this.picBoxGraphBehind.Click += this.SetPictureBoxColor;
            // 
            // pictureBox1
            // 
            this.picturebox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturebox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picturebox1.Location = new System.Drawing.Point(180, 97);
            this.picturebox1.Name = "picturebox1";
            this.picturebox1.Size = new System.Drawing.Size(20, 20);
            this.picturebox1.TabIndex = 35;
            this.picturebox1.TabStop = false;
            this.picturebox1.Click += this.SetPictureBoxColor;

            this.colorTabs.ResumeLayout(false);
            this.tabPageClockColors.ResumeLayout(false);
            this.tabPageClockColors.PerformLayout();
            this.tabPageSegColors.ResumeLayout(false);
            this.tabPageSegColors.PerformLayout();
            this.tabPageDetailedViewColors.ResumeLayout(false);
            this.tabPageDetailedViewColors.PerformLayout();
            this.groupBoxBackground.ResumeLayout(false);
            this.groupBoxBackground.PerformLayout();
            this.groupBoxDViewClock.ResumeLayout(false);
            this.groupBoxDViewClock.PerformLayout();
            this.groupBoxDViewSegments.ResumeLayout(false);
            this.groupBoxDViewSegments.PerformLayout();
            this.groupBoxGraph.ResumeLayout(false);
            this.groupBoxGraph.PerformLayout();
            ((ISupportInitialize)this.picBoxAheadBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxRunTitleBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxAheadLosingBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxBehindBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxBehindLosingBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxStatusBarBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxNoLoadedBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxFinishedBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxRecordBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxDelayBackPlain).EndInit();
            ((ISupportInitialize)this.picBoxAheadBack2).EndInit();
            ((ISupportInitialize)this.picBoxRunTitleBack2).EndInit();
            ((ISupportInitialize)this.picBoxAheadLosingBack2).EndInit();
            ((ISupportInitialize)this.picBoxBehindBack2).EndInit();
            ((ISupportInitialize)this.picBoxBehindLosingBack2).EndInit();
            ((ISupportInitialize)this.picBoxStatusBarBack2).EndInit();
            ((ISupportInitialize)this.picBoxNoLoadedBack2).EndInit();
            ((ISupportInitialize)this.picBoxFinishedBack2).EndInit();
            ((ISupportInitialize)this.picBoxRecordBack2).EndInit();
            ((ISupportInitialize)this.picBoxDelayBack2).EndInit();
            ((ISupportInitialize)this.picBoxAheadBack).EndInit();
            ((ISupportInitialize)this.picBoxRunTitleBack).EndInit();
            ((ISupportInitialize)this.picBoxAheadLosingBack).EndInit();
            ((ISupportInitialize)this.picBoxBehindBack).EndInit();
            ((ISupportInitialize)this.picBoxBehindLosingBack).EndInit();
            ((ISupportInitialize)this.picBoxStatusBarBack).EndInit();
            ((ISupportInitialize)this.picBoxNoLoadedBack).EndInit();
            ((ISupportInitialize)this.picBoxFinishedBack).EndInit();
            ((ISupportInitialize)this.picBoxRecordBack).EndInit();
            ((ISupportInitialize)this.picBoxDelayBack).EndInit();
            ((ISupportInitialize)this.picBoxRunTitleFore).EndInit();
            ((ISupportInitialize)this.picBoxStatusBarFore).EndInit();
            ((ISupportInitialize)this.picBoxDelayFore).EndInit();
            ((ISupportInitialize)this.picBoxRecordFore).EndInit();
            ((ISupportInitialize)this.picBoxFinishedFore).EndInit();
            ((ISupportInitialize)this.picBoxFlash).EndInit();
            ((ISupportInitialize)this.picBoxPaused).EndInit();
            ((ISupportInitialize)this.picBoxNoLoadedFore).EndInit();
            ((ISupportInitialize)this.picBoxBehindLosingFore).EndInit();
            ((ISupportInitialize)this.picBoxBehindFore).EndInit();
            ((ISupportInitialize)this.picBoxAheadLosingFore).EndInit();
            ((ISupportInitialize)this.picBoxAheadFore).EndInit();
            ((ISupportInitialize)this.picBoxSegHighlightPlain).EndInit();
            ((ISupportInitialize)this.picBoxSegBackgroundPlain).EndInit();
            ((ISupportInitialize)this.picBoxSegHighlight2).EndInit();
            ((ISupportInitialize)this.picBoxSegBackground2).EndInit();
            ((ISupportInitialize)this.picBoxSegHighlightBorder).EndInit();
            ((ISupportInitialize)this.picBoxSegBehindLoss).EndInit();
            ((ISupportInitialize)this.picBoxSegBehindGain).EndInit();
            ((ISupportInitialize)this.picBoxSegAheadLoss).EndInit();
            ((ISupportInitialize)this.picBoxSegAheadGain).EndInit();
            ((ISupportInitialize)this.picBoxSegMissing).EndInit();
            ((ISupportInitialize)this.picBoxSegNewTime).EndInit();
            ((ISupportInitialize)this.picBoxSegBestSegment).EndInit();
            ((ISupportInitialize)this.picBoxSegFutureTime).EndInit();
            ((ISupportInitialize)this.picBoxSegFutureText).EndInit();
            ((ISupportInitialize)this.picBoxSegLiveText).EndInit();
            ((ISupportInitialize)this.picBoxSegPastText).EndInit();
            ((ISupportInitialize)this.picBoxSegHighlight).EndInit();
            ((ISupportInitialize)this.picBoxSegBackground).EndInit();
            ((ISupportInitialize)this.picBoxDViewAhead).EndInit();
            ((ISupportInitialize)this.picBoxDViewAheadLosing).EndInit();
            ((ISupportInitialize)this.picBoxDViewBehind).EndInit();
            ((ISupportInitialize)this.picBoxDViewBehindLosing).EndInit();
            ((ISupportInitialize)this.picBoxDViewFinished).EndInit();
            ((ISupportInitialize)this.picBoxDViewRecord).EndInit();
            ((ISupportInitialize)this.picBoxDViewDelay).EndInit();
            ((ISupportInitialize)this.picBoxDViewPaused).EndInit();
            ((ISupportInitialize)this.picBoxDViewFlash).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegCurrentText).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegDefaultText).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegMissingTime).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegBestSegment).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegAheadGain).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegAheadLoss).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegBehindGain).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegBehindLoss).EndInit();
            ((ISupportInitialize)this.picBoxDViewSegHighlight).EndInit();
            ((ISupportInitialize)this.picturebox1).EndInit();
            ((ISupportInitialize)this.picBoxPreview).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void PopulateColors()
        {
            foreach (SettingPair setting in this.ColorSettings)
            {
                setting.pb.BackColor = (Color)Properties.ColorSettings.Profile[setting.name];
            }
            this.checkBoxDViewUsePrimary.Checked = Properties.ColorSettings.Profile.DViewUsePrimary;
        }

        private void PopulateSettings()
        {
            this.ColorSettings.AddRange(new SettingPair[]
            {
                new SettingPair("AheadFore", this.picBoxAheadFore),
                new SettingPair("AheadBack", this.picBoxAheadBack),
                new SettingPair("AheadBack2", this.picBoxAheadBack2),
                new SettingPair("AheadBackPlain", this.picBoxAheadBackPlain),
                new SettingPair("AheadLosingFore", this.picBoxAheadLosingFore),
                new SettingPair("AheadLosingBack", this.picBoxAheadLosingBack),
                new SettingPair("AheadLosingBack2", this.picBoxAheadLosingBack2),
                new SettingPair("AheadLosingBackPlain", this.picBoxAheadLosingBackPlain),
                new SettingPair("BehindFore", this.picBoxBehindFore),
                new SettingPair("BehindBack", this.picBoxBehindBack),
                new SettingPair("BehindBack2", this.picBoxBehindBack2),
                new SettingPair("BehindBackPlain", this.picBoxBehindBackPlain),
                new SettingPair("BehindLosingFore", this.picBoxBehindLosingFore),
                new SettingPair("BehindLosingBack", this.picBoxBehindLosingBack),
                new SettingPair("BehindLosingBack2", this.picBoxBehindLosingBack2),
                new SettingPair("BehindLosingBackPlain", this.picBoxBehindLosingBackPlain),
                new SettingPair("WatchFore", this.picBoxNoLoadedFore),
                new SettingPair("SegPastTime", this.picturebox1),
                new SettingPair("WatchBack", this.picBoxNoLoadedBack),
                new SettingPair("WatchBack2", this.picBoxNoLoadedBack2),
                new SettingPair("WatchBackPlain", this.picBoxNoLoadedBackPlain),
                new SettingPair("Paused", this.picBoxPaused),
                new SettingPair("Flash", this.picBoxFlash),
                new SettingPair("FinishedFore", this.picBoxFinishedFore),
                new SettingPair("FinishedBack", this.picBoxFinishedBack),
                new SettingPair("FinishedBack2", this.picBoxFinishedBack2),
                new SettingPair("FinishedBackPlain", this.picBoxFinishedBackPlain),
                new SettingPair("RecordFore", this.picBoxRecordFore),
                new SettingPair("RecordBack", this.picBoxRecordBack),
                new SettingPair("RecordBack2", this.picBoxRecordBack2),
                new SettingPair("RecordBackPlain", this.picBoxRecordBackPlain),
                new SettingPair("DelayFore", this.picBoxDelayFore),
                new SettingPair("DelayBack", this.picBoxDelayBack),
                new SettingPair("DelayBack2", this.picBoxDelayBack2),
                new SettingPair("DelayBackPlain", this.picBoxDelayBackPlain),
                new SettingPair("StatusFore", this.picBoxStatusBarFore),
                new SettingPair("StatusBack", this.picBoxStatusBarBack),
                new SettingPair("StatusBack2", this.picBoxStatusBarBack2),
                new SettingPair("StatusBackPlain", this.picBoxStatusBarBackPlain),
                new SettingPair("TitleFore", this.picBoxRunTitleFore),
                new SettingPair("TitleBack", this.picBoxRunTitleBack),
                new SettingPair("TitleBack2", this.picBoxRunTitleBack2),
                new SettingPair("TitleBackPlain", this.picBoxRunTitleBackPlain),
                new SettingPair("SegBack", this.picBoxSegBackground),
                new SettingPair("SegBack2", this.picBoxSegBackground2),
                new SettingPair("SegBackPlain", this.picBoxSegBackgroundPlain),
                new SettingPair("SegHighlight", this.picBoxSegHighlight),
                new SettingPair("SegHighlight2", this.picBoxSegHighlight2),
                new SettingPair("SegHighlightPlain", this.picBoxSegHighlightPlain),
                new SettingPair("SegHighlightBorder", this.picBoxSegHighlightBorder),
                new SettingPair("PastSeg", this.picBoxSegPastText),
                new SettingPair("LiveSeg", this.picBoxSegLiveText),
                new SettingPair("FutureSegName", this.picBoxSegFutureText),
                new SettingPair("FutureSegTime", this.picBoxSegFutureTime),
                new SettingPair("SegNewTime", this.picBoxSegNewTime),
                new SettingPair("SegMissingTime", this.picBoxSegMissing),
                new SettingPair("SegAheadGain", this.picBoxSegAheadGain),
                new SettingPair("SegAheadLoss", this.picBoxSegAheadLoss),
                new SettingPair("SegBehindGain", this.picBoxSegBehindGain),
                new SettingPair("SegBehindLoss", this.picBoxSegBehindLoss),
                new SettingPair("SegBestSegment", this.picBoxSegBestSegment),
                new SettingPair("DViewAhead", this.picBoxDViewAhead),
                new SettingPair("DViewAheadLosing", this.picBoxDViewAheadLosing),
                new SettingPair("DViewBehind", this.picBoxDViewBehind),
                new SettingPair("DViewBehindLosing", this.picBoxDViewBehindLosing),
                new SettingPair("DViewFinished", this.picBoxDViewFinished),
                new SettingPair("DViewRecord", this.picBoxDViewRecord),
                new SettingPair("DViewDelay", this.picBoxDViewDelay),
                new SettingPair("DViewPaused", this.picBoxDViewPaused),
                new SettingPair("DViewFlash", this.picBoxDViewFlash),
                new SettingPair("DViewSegCurrentText", this.picBoxDViewSegCurrentText),
                new SettingPair("DViewSegDefaultText", this.picBoxDViewSegDefaultText),
                new SettingPair("DViewSegMissingTime", this.picBoxDViewSegMissingTime),
                new SettingPair("DViewSegBestSegment", this.picBoxDViewSegBestSegment),
                new SettingPair("DViewSegAheadGain", this.picBoxDViewSegAheadGain),
                new SettingPair("DViewSegAheadLoss", this.picBoxDViewSegAheadLoss),
                new SettingPair("DViewSegBehindGain", this.picBoxDViewSegBehindGain),
                new SettingPair("DViewSegBehindLoss", this.picBoxDViewSegBehindLoss),
                new SettingPair("DViewSegHighlight", this.picBoxDViewSegHighlight),
                new SettingPair("GraphAhead", this.picBoxGraphAhead),
                new SettingPair("GraphBehind", this.picBoxGraphBehind)
            });
        }

        public void LoadColors()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string str;
                    StreamReader reader = new StreamReader(dialog.FileName);
                    List<KeyValuePair<String, String>> list = new List<KeyValuePair<String, String>>();
                    while ((str = reader.ReadLine()) != null)
                    {
                        string[] strArray = str.Split('=');
                        if (strArray.Length == 2)
                        {
                            list.Add(new KeyValuePair<String, String>(strArray[0], strArray[1]));
                        }
                    }
                    /*using (List<KeyValuePair<String, String>>.Enumerator enumerator = list.GetEnumerator())
                    {
                        KeyValuePair<String, String> sp;
                        while (enumerator.MoveNext())
                        {
                            sp = enumerator.Current;
                            foreach (SettingPair setting in from cs in this.ColorSettings
                                                             where cs.name == sp.Key
                                                             select cs)
                            {
                                setting.pb.BackColor = ColorTranslator.FromHtml(sp.Value);
                            }
                        }
                    }*/

                    foreach (KeyValuePair<String, String> pair in list)
                    {
                        foreach (SettingPair sp in from cs in this.ColorSettings
                                                   where cs.name == pair.Key
                                                   select cs)
                        {
                            sp.pb.BackColor = ColorTranslator.FromHtml(pair.Value);
                        }
                    }
                }
                finally
                {
                    this.picBoxPreview.Invalidate();
                }
            }
        }

        public void SaveColors()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(dialog.FileName);
                    foreach (SettingPair setting in this.ColorSettings)
                    {
                        writer.WriteLine(setting.name + "=" + ColorTranslator.ToHtml(setting.pb.BackColor));
                    }
                    writer.Close();
                }
                catch
                {
                }
            }
        }

        private void labelAhead_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.Ahead;
            this.picBoxPreview.Invalidate();
        }

        private void labelAheadLosing_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.AheadLosing;
            this.picBoxPreview.Invalidate();
        }

        private void labelBehind_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.Behind;
            this.picBoxPreview.Invalidate();
        }

        private void labelBehindLosing_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.BehindLosing;
            this.picBoxPreview.Invalidate();
        }

        private void labelNoLoaded_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.NoRun;
            this.picBoxPreview.Invalidate();
        }

        private void labelFinished_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.Finished;
            this.picBoxPreview.Invalidate();
        }

        private void labelRecord_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.NewRecord;
            this.picBoxPreview.Invalidate();
        }

        private void labelDelay_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.Delay;
            this.picBoxPreview.Invalidate();
        }

        private void labelPaused_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.Paused;
            this.picBoxPreview.Invalidate();
        }

        private void labelFlash_Click(object sender, EventArgs e)
        {
            this.previewClockType = ClockType.Flash;
            this.picBoxPreview.Invalidate();
        }

        private void checkBoxDViewUsePrimary_CheckedChanged(object sender, EventArgs e)
        {
            bool state = !checkBoxDViewUsePrimary.Checked;
            int alpha = (state) ? 255 : 128;

            this.groupBoxDViewClock.Enabled = state;
            this.groupBoxDViewSegments.Enabled = state;

            this.picBoxDViewAhead.BackColor = Color.FromArgb(alpha, this.picBoxDViewAhead.BackColor);
            this.picBoxDViewAheadLosing.BackColor = Color.FromArgb(alpha, this.picBoxDViewAheadLosing.BackColor);
            this.picBoxDViewBehind.BackColor = Color.FromArgb(alpha, this.picBoxDViewBehind.BackColor);
            this.picBoxDViewBehindLosing.BackColor = Color.FromArgb(alpha, this.picBoxDViewBehindLosing.BackColor);
            this.picBoxDViewFinished.BackColor = Color.FromArgb(alpha, this.picBoxDViewFinished.BackColor);
            this.picBoxDViewRecord.BackColor = Color.FromArgb(alpha, this.picBoxDViewRecord.BackColor);
            this.picBoxDViewDelay.BackColor = Color.FromArgb(alpha, this.picBoxDViewDelay.BackColor);
            this.picBoxDViewPaused.BackColor = Color.FromArgb(alpha, this.picBoxDViewPaused.BackColor);
            this.picBoxDViewFlash.BackColor = Color.FromArgb(alpha, this.picBoxDViewFlash.BackColor);
            this.picBoxDViewSegCurrentText.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegCurrentText.BackColor);
            this.picBoxDViewSegDefaultText.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegDefaultText.BackColor);
            this.picBoxDViewSegMissingTime.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegMissingTime.BackColor);
            this.picBoxDViewSegBestSegment.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegBestSegment.BackColor);
            this.picBoxDViewSegAheadGain.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegAheadGain.BackColor);
            this.picBoxDViewSegAheadLoss.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegAheadLoss.BackColor);
            this.picBoxDViewSegBehindGain.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegBehindGain.BackColor);
            this.picBoxDViewSegBehindLoss.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegBehindLoss.BackColor);
            this.picBoxDViewSegHighlight.BackColor = Color.FromArgb(alpha, this.picBoxDViewSegHighlight.BackColor);
        }

        private void plainBg_CheckedChanged(object sender, EventArgs e)
        {
            this.picBoxPreview.Invalidate();
        }

        private void buttonDefaultColors_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this, "本当に初期化しますか？", "設定初期化", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool flag = false;
                foreach (SettingsPropertyValue v in Properties.ColorSettings.Profile.PropertyValues)
                {
                    foreach (SettingPair setting in from cs in ColorSettings
                                                    where cs.name == v.Name
                                                    select cs)
                    {
                        try
                        {
                            setting.pb.BackColor = (Color)this.converter.ConvertFrom(null, CultureInfo.GetCultureInfo(""), v.Property.DefaultValue);
                        }
                        catch
                        {
                            flag = true;
                        }
                    }
                }

                if (flag)
                    MessageBoxEx.Show(this, "設定初期化中にエラーが発生しました。");

                this.checkBoxDViewUsePrimary.Checked = Properties.ColorSettings.Profile.DViewUsePrimary;
                this.picBoxPreview.Invalidate();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.SaveColors();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            this.LoadColors();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.ColorSettings.Profile.DViewUsePrimary = this.checkBoxDViewUsePrimary.Checked;
            foreach (SettingPair setting in this.ColorSettings)
            {
                Properties.ColorSettings.Profile[setting.name] = Color.FromArgb(255, setting.pb.BackColor);
            }
            base.DialogResult = DialogResult.OK;
        }

        private void previewBox_Paint(object sender, PaintEventArgs e)
        {
            Brush brush;
            Color backColor;
            Color color2;
            Color color3;
            Graphics graphics = e.Graphics;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.Clear(Color.Black);
            Rectangle rect = new Rectangle(0, (this.picBoxPreview.Height - 0x12) - 0x1a, 0x7c, 0x1a);
            switch (this.previewClockType)
            {
                case ClockType.Ahead:
                    brush = new SolidBrush(this.picBoxAheadFore.BackColor);
                    backColor = this.picBoxAheadBack.BackColor;
                    color2 = this.picBoxAheadBack2.BackColor;
                    color3 = this.picBoxAheadBackPlain.BackColor;
                    break;

                case ClockType.AheadLosing:
                    brush = new SolidBrush(this.picBoxAheadLosingFore.BackColor);
                    backColor = this.picBoxAheadLosingBack.BackColor;
                    color2 = this.picBoxAheadLosingBack2.BackColor;
                    color3 = this.picBoxAheadLosingBackPlain.BackColor;
                    break;

                case ClockType.Behind:
                    brush = new SolidBrush(this.picBoxBehindFore.BackColor);
                    backColor = this.picBoxBehindBack.BackColor;
                    color2 = this.picBoxBehindBack2.BackColor;
                    color3 = this.picBoxBehindBackPlain.BackColor;
                    break;

                case ClockType.BehindLosing:
                    brush = new SolidBrush(this.picBoxBehindLosingFore.BackColor);
                    backColor = this.picBoxBehindLosingBack.BackColor;
                    color2 = this.picBoxBehindLosingBack2.BackColor;
                    color3 = this.picBoxBehindLosingBackPlain.BackColor;
                    break;

                case ClockType.Delay:
                    brush = new SolidBrush(this.picBoxDelayFore.BackColor);
                    backColor = this.picBoxDelayBack.BackColor;
                    color2 = this.picBoxDelayBack2.BackColor;
                    color3 = this.picBoxDelayBackPlain.BackColor;
                    break;

                case ClockType.NoRun:
                    brush = new SolidBrush(this.picBoxNoLoadedFore.BackColor);
                    backColor = this.picBoxNoLoadedBack.BackColor;
                    color2 = this.picBoxNoLoadedBack2.BackColor;
                    color3 = this.picBoxNoLoadedBackPlain.BackColor;
                    break;

                case ClockType.Paused:
                    brush = new SolidBrush(this.picBoxPaused.BackColor);
                    backColor = this.picBoxAheadBack.BackColor;
                    color2 = this.picBoxAheadBack2.BackColor;
                    color3 = this.picBoxAheadBackPlain.BackColor;
                    break;

                case ClockType.NewRecord:
                    brush = new SolidBrush(this.picBoxRecordFore.BackColor);
                    backColor = this.picBoxRecordBack.BackColor;
                    color2 = this.picBoxRecordBack2.BackColor;
                    color3 = this.picBoxRecordBackPlain.BackColor;
                    break;

                case ClockType.Finished:
                    brush = new SolidBrush(this.picBoxFinishedFore.BackColor);
                    backColor = this.picBoxFinishedBack.BackColor;
                    color2 = this.picBoxFinishedBack2.BackColor;
                    color3 = this.picBoxFinishedBackPlain.BackColor;
                    break;

                case ClockType.Flash:
                    brush = new SolidBrush(this.picBoxFlash.BackColor);
                    backColor = this.picBoxAheadBack.BackColor;
                    color2 = this.picBoxAheadBack2.BackColor;
                    color3 = this.picBoxAheadBackPlain.BackColor;
                    break;

                default:
                    brush = new SolidBrush(this.picBoxAheadFore.BackColor);
                    backColor = this.picBoxAheadBack.BackColor;
                    color2 = this.picBoxAheadBack2.BackColor;
                    color3 = this.picBoxAheadBackPlain.BackColor;
                    break;
            }
            if (this.checkBoxPlainBg.Checked)
            {
                graphics.FillRectangle(new SolidBrush(color3), rect);
            }
            else
            {
                graphics.FillRectangle(new LinearGradientBrush(rect, backColor, color2, 0f), rect);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(0x56, color2)), rect.X, rect.Y, rect.Width, rect.Height / 2);
            }
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            Rectangle layoutRectangle = new Rectangle(0, rect.Top + 1, 0x5f, 0x1a);
            Rectangle rectangle3 = new Rectangle(layoutRectangle.Right - 5, layoutRectangle.Y + 5, 30, 0x12);
            graphics.DrawString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, this.clockM, brush, rectangle3, format);
            rectangle3.X = layoutRectangle.Right;
            format.Alignment = StringAlignment.Far;
            graphics.DrawString("8:88", this.clockL, brush, layoutRectangle, format);
            format.Alignment = StringAlignment.Near;
            graphics.DrawString("88", this.clockM, brush, rectangle3, format);
            Rectangle rectangle4 = new Rectangle(0, rect.Bottom, this.picBoxPreview.Width, 0x12);
            Rectangle rectangle5 = new Rectangle(1, rect.Bottom + 2, this.picBoxPreview.Width - 1, 0x10);
            if (this.checkBoxPlainBg.Checked)
            {
                graphics.FillRectangle(new SolidBrush(this.picBoxStatusBarBackPlain.BackColor), rectangle4);
                graphics.FillRectangle(new SolidBrush(this.picBoxRunTitleBackPlain.BackColor), 0, 0, this.picBoxPreview.Width, 0x12);
            }
            else
            {
                graphics.FillRectangle(new LinearGradientBrush(rectangle4, this.picBoxStatusBarBack.BackColor, this.picBoxStatusBarBack2.BackColor, 0f), rectangle4);
                graphics.FillRectangle(new SolidBrush(this.picBoxRunTitleBack.BackColor), 0, 0, this.picBoxPreview.Width, 0x12);
                graphics.FillRectangle(new SolidBrush(this.picBoxRunTitleBack2.BackColor), 0, 0, this.picBoxPreview.Width, 9);
            }
            StringFormat format2 = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            StringFormat format3 = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                Alignment = StringAlignment.Far
            };
            StringFormat format4 = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.DrawString("区間差", this.displayFont, new SolidBrush(this.picBoxStatusBarFore.BackColor), rectangle5, format4);
            graphics.DrawString("タイトル", this.displayFont, new SolidBrush(this.picBoxRunTitleFore.BackColor), new Rectangle(0, 1, this.picBoxPreview.Width, 0x11), format2);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.DrawString("-8:88", this.timeFont, new SolidBrush(this.picBoxSegAheadGain.BackColor), rectangle5, format3);
            int y = 0x12;
            Rectangle rectangle6 = new Rectangle(0, y, this.picBoxPreview.Width, ((this.picBoxPreview.Height - rect.Height) - 0x13) - y);
            if (this.checkBoxPlainBg.Checked)
            {
                graphics.FillRectangle(new SolidBrush(this.picBoxSegBackgroundPlain.BackColor), rectangle6);
                graphics.FillRectangle(new SolidBrush(this.picBoxStatusBarBackPlain.BackColor), 0, rect.Y - 3, this.picBoxPreview.Width, 3);
            }
            else
            {
                graphics.FillRectangle(new LinearGradientBrush(rectangle6, this.picBoxSegBackground.BackColor, this.picBoxSegBackground2.BackColor, 0f), rectangle6);
                graphics.FillRectangle(new LinearGradientBrush(rectangle6, this.picBoxStatusBarBack.BackColor, this.picBoxStatusBarBack2.BackColor, 0f), 0, rect.Y - 3, this.picBoxPreview.Width, 3);
            }
            StringFormat format5 = new StringFormat
            {
                Trimming = StringTrimming.EllipsisCharacter,
                LineAlignment = StringAlignment.Center
            };
            StringFormat format6 = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Far
            };
            Rectangle rectangle7 = new Rectangle(0, 0, 0, 1);
            string[] strArray = new string[] { "過去区間", "区間新", "貯金増", "貯金減", "借金減", "借金減", "新タイム", "現区間", "タイムなし", "未来区間", "未来区間", "未来区間" };
            string[] strArray2 = new string[] { "17:17", "-88.8", "-88.8", "-88.8", "+88.8", "+88.8", "8:88", "-", "8:88.8", "8:88", "8:88", "8:88" };
            Color[] colorArray = new Color[] { this.picBoxSegPastText.BackColor, this.picBoxSegPastText.BackColor, this.picBoxSegPastText.BackColor, this.picBoxSegPastText.BackColor, this.picBoxSegPastText.BackColor, this.picBoxSegPastText.BackColor, this.picBoxSegPastText.BackColor, this.picBoxSegLiveText.BackColor, this.picBoxSegPastText.BackColor, this.picBoxSegFutureText.BackColor, this.picBoxSegFutureText.BackColor, this.picBoxSegFutureText.BackColor };
            Color[] colorArray2 = new Color[] { this.picturebox1.BackColor, this.picBoxSegBestSegment.BackColor, this.picBoxSegAheadGain.BackColor, this.picBoxSegAheadLoss.BackColor, this.picBoxSegBehindGain.BackColor, this.picBoxSegBehindLoss.BackColor, this.picBoxSegNewTime.BackColor, this.picBoxSegLiveText.BackColor, this.picBoxSegMissing.BackColor, this.picBoxSegFutureTime.BackColor, this.picBoxSegFutureTime.BackColor, this.picBoxSegFutureTime.BackColor };
            for (int i = 0; i < 12; i++)
            {
                Rectangle rectangle8 = new Rectangle(0, y, this.picBoxPreview.Width, this.segHeight);
                Rectangle rectangle9 = new Rectangle(0, y + 2, this.picBoxPreview.Width, this.segHeight);
                Rectangle rectangle10 = new Rectangle(0, y + 1, this.picBoxPreview.Width, this.segHeight);
                rectangle9.Y = (y + (this.segHeight / 2)) - 5;
                rectangle9.Height = 13;
                if (i == 7)
                {
                    rectangle7 = rectangle8;
                    if (this.checkBoxPlainBg.Checked)
                    {
                        graphics.FillRectangle(new SolidBrush(this.picBoxSegHighlightPlain.BackColor), rectangle8);
                    }
                    else
                    {
                        graphics.FillRectangle(new LinearGradientBrush(rectangle8, this.picBoxSegHighlight.BackColor, this.picBoxSegHighlight2.BackColor, 0f), rectangle8);
                    }
                }
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.DrawString(strArray[i], this.displayFont, new SolidBrush(colorArray[i]), rectangle9, format5);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.DrawString(strArray2[i], this.timeFont, new SolidBrush(colorArray2[i]), rectangle10, format6);
                y += this.segHeight;
            }
            Pen pen = new Pen(new SolidBrush(this.picBoxSegHighlightBorder.BackColor));
            graphics.DrawLine(pen, rectangle7.Left, rectangle7.Top, rectangle7.Right, rectangle7.Top);
            graphics.DrawLine(pen, rectangle7.Left, rectangle7.Bottom, rectangle7.Right, rectangle7.Bottom);
        }

        public void SetPictureBoxColor(object sender, EventArgs e)
        {
            ColorPicker picker = new ColorPicker();
            picker.SetColor(((PictureBox)sender).BackColor);
            if (picker.ShowDialog() == DialogResult.OK)
            {
                ((PictureBox)sender).BackColor = picker.rgbColor;
                this.picBoxPreview.Invalidate();
            }
        }

        private enum ClockType
        {
            Ahead,
            AheadLosing,
            Behind,
            BehindLosing,
            Delay,
            NoRun,
            Paused,
            NewRecord,
            Finished,
            Flash
        }

        private class SettingPair
        {
            public string name;
            public PictureBox pb;

            public SettingPair(string Name, PictureBox Box)
            {
                this.name = Name;
                this.pb = Box;
            }
        }
    }
}
