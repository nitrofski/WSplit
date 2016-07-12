namespace WSplitTimer
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("一般", 0);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("ホットキー", 1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("フォント", 2);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("画面表示", 3);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("開始・スプリット");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("一時停止・再開");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("停止");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("リセット");
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("前スプリット移動");
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("次スプリット移動");
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("比較方法変更");
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem("キー有効無効切り替え");
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listViewPanelSelector = new System.Windows.Forms.ListView();
            this.imageListPageIcons = new System.Windows.Forms.ImageList(this.components);
            this.panelGeneralOptions = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numwv = new System.Windows.Forms.NumericUpDown();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBarDoubleTap = new System.Windows.Forms.TrackBar();
            this.labelRefreshIntervalDisplay = new System.Windows.Forms.Label();
            this.labelDoubleTapDisplay = new System.Windows.Forms.Label();
            this.checkBoxReloadRun = new System.Windows.Forms.CheckBox();
            this.checkBoxWindowPos = new System.Windows.Forms.CheckBox();
            this.comboBoxFallback = new System.Windows.Forms.ComboBox();
            this.labelFallback = new System.Windows.Forms.Label();
            this.labelRefreshInterval = new System.Windows.Forms.Label();
            this.labelDoubleTap = new System.Windows.Forms.Label();
            this.trackBarRefreshInterval = new System.Windows.Forms.TrackBar();
            this.textBoxStartDelay = new System.Windows.Forms.TextBox();
            this.labelStartDelay = new System.Windows.Forms.Label();
            this.panelHotkeys = new System.Windows.Forms.Panel();
            this.buttonClearHotkey = new System.Windows.Forms.Button();
            this.buttonSetHotkey = new System.Windows.Forms.Button();
            this.textBoxHotkey = new System.Windows.Forms.TextBox();
            this.listViewHotkeys = new System.Windows.Forms.ListView();
            this.columnDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHotkey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListHotkey = new System.Windows.Forms.ImageList(this.components);
            this.checkBoxHotkeysEnabled = new System.Windows.Forms.CheckBox();
            this.panelDisplaySettings = new System.Windows.Forms.Panel();
            this.labelDetailedWidth = new System.Windows.Forms.Label();
            this.numericUpDownDetailedWidth = new System.Windows.Forms.NumericUpDown();
            this.buttonBackgroundSetup = new System.Windows.Forms.Button();
            this.groupBoxDisplayMode = new System.Windows.Forms.GroupBox();
            this.radioButtonDisplayDetailed = new System.Windows.Forms.RadioButton();
            this.radioButtonDisplayWide = new System.Windows.Forms.RadioButton();
            this.radioButtonDisplayCompact = new System.Windows.Forms.RadioButton();
            this.radioButtonDisplayTimer = new System.Windows.Forms.RadioButton();
            this.labelWideSegments = new System.Windows.Forms.Label();
            this.labelDetailedSegments = new System.Windows.Forms.Label();
            this.numericUpDownWideSegments = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDetailedSegments = new System.Windows.Forms.NumericUpDown();
            this.checkBoxWideShowLast = new System.Windows.Forms.CheckBox();
            this.labelIcons = new System.Windows.Forms.Label();
            this.checkBoxShowTitle = new System.Windows.Forms.CheckBox();
            this.checkBoxDetailedShowLast = new System.Windows.Forms.CheckBox();
            this.checkBoxWideBlanks = new System.Windows.Forms.CheckBox();
            this.checkBoxDetailedBlanks = new System.Windows.Forms.CheckBox();
            this.labelWideMode = new System.Windows.Forms.Label();
            this.labelDetailedMode = new System.Windows.Forms.Label();
            this.comboBoxIcons = new System.Windows.Forms.ComboBox();
            this.checkBoxShowAttemptCount = new System.Windows.Forms.CheckBox();
            this.labelOpacityDisplay = new System.Windows.Forms.Label();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.labelOpacity = new System.Windows.Forms.Label();
            this.panelFontSettings = new System.Windows.Forms.Panel();
            this.groupBoxDViewFont = new System.Windows.Forms.GroupBox();
            this.comboBoxDViewFont = new System.Windows.Forms.ComboBox();
            this.groupBoxPrimWndFont = new System.Windows.Forms.GroupBox();
            this.checkBoxClockDigitalFont = new System.Windows.Forms.CheckBox();
            this.comboBoxPrimWndFont = new System.Windows.Forms.ComboBox();
            this.numericUpDownPrimWndMult = new System.Windows.Forms.NumericUpDown();
            this.labelPrimWndMult = new System.Windows.Forms.Label();
            this.labelNote = new System.Windows.Forms.Label();
            this.panelGeneralOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numwv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDoubleTap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRefreshInterval)).BeginInit();
            this.panelHotkeys.SuspendLayout();
            this.panelDisplaySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDetailedWidth)).BeginInit();
            this.groupBoxDisplayMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWideSegments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDetailedSegments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this.panelFontSettings.SuspendLayout();
            this.groupBoxDViewFont.SuspendLayout();
            this.groupBoxPrimWndFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrimWndMult)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.Location = new System.Drawing.Point(12, 308);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(104, 21);
            this.buttonDefaults.TabIndex = 0;
            this.buttonDefaults.Text = "デフォルトに戻す";
            this.buttonDefaults.UseVisualStyleBackColor = true;
            this.buttonDefaults.Click += new System.EventHandler(this.buttonDefaults_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(235, 308);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 21);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "保存";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(316, 308);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // listViewPanelSelector
            // 
            this.listViewPanelSelector.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewPanelSelector.HideSelection = false;
            this.listViewPanelSelector.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16});
            this.listViewPanelSelector.LargeImageList = this.imageListPageIcons;
            this.listViewPanelSelector.Location = new System.Drawing.Point(12, 11);
            this.listViewPanelSelector.MultiSelect = false;
            this.listViewPanelSelector.Name = "listViewPanelSelector";
            this.listViewPanelSelector.Size = new System.Drawing.Size(104, 287);
            this.listViewPanelSelector.TabIndex = 0;
            this.listViewPanelSelector.UseCompatibleStateImageBehavior = false;
            this.listViewPanelSelector.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewPanelSelector_ItemSelectionChanged);
            this.listViewPanelSelector.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewPanelSelector_MouseUp);
            // 
            // imageListPageIcons
            // 
            this.imageListPageIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPageIcons.ImageStream")));
            this.imageListPageIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPageIcons.Images.SetKeyName(0, "OptionsIcon.png");
            this.imageListPageIcons.Images.SetKeyName(1, "HotkeyIcon.png");
            this.imageListPageIcons.Images.SetKeyName(2, "FontIcon.png");
            this.imageListPageIcons.Images.SetKeyName(3, "DisplayIcon.png");
            // 
            // panelGeneralOptions
            // 
            this.panelGeneralOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGeneralOptions.Controls.Add(this.label1);
            this.panelGeneralOptions.Controls.Add(this.numwv);
            this.panelGeneralOptions.Controls.Add(this.checkBox2);
            this.panelGeneralOptions.Controls.Add(this.checkBox1);
            this.panelGeneralOptions.Controls.Add(this.trackBarDoubleTap);
            this.panelGeneralOptions.Controls.Add(this.labelRefreshIntervalDisplay);
            this.panelGeneralOptions.Controls.Add(this.labelDoubleTapDisplay);
            this.panelGeneralOptions.Controls.Add(this.checkBoxReloadRun);
            this.panelGeneralOptions.Controls.Add(this.checkBoxWindowPos);
            this.panelGeneralOptions.Controls.Add(this.comboBoxFallback);
            this.panelGeneralOptions.Controls.Add(this.labelFallback);
            this.panelGeneralOptions.Controls.Add(this.labelRefreshInterval);
            this.panelGeneralOptions.Controls.Add(this.labelDoubleTap);
            this.panelGeneralOptions.Controls.Add(this.trackBarRefreshInterval);
            this.panelGeneralOptions.Controls.Add(this.textBoxStartDelay);
            this.panelGeneralOptions.Controls.Add(this.labelStartDelay);
            this.panelGeneralOptions.Location = new System.Drawing.Point(135, 11);
            this.panelGeneralOptions.Name = "panelGeneralOptions";
            this.panelGeneralOptions.Padding = new System.Windows.Forms.Padding(3);
            this.panelGeneralOptions.Size = new System.Drawing.Size(256, 293);
            this.panelGeneralOptions.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "横方向のスプリット高さ";
            // 
            // numwv
            // 
            this.numwv.Location = new System.Drawing.Point(118, 258);
            this.numwv.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numwv.Name = "numwv";
            this.numwv.Size = new System.Drawing.Size(124, 19);
            this.numwv.TabIndex = 10;
            this.numwv.Value = new decimal(new int[] {
            36,
            0,
            0,
            0});
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(118, 227);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(115, 16);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "スプリットタイム表示";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 227);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "虹色スプリット";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // trackBarDoubleTap
            // 
            this.trackBarDoubleTap.LargeChange = 2;
            this.trackBarDoubleTap.Location = new System.Drawing.Point(6, 41);
            this.trackBarDoubleTap.Maximum = 100;
            this.trackBarDoubleTap.Name = "trackBarDoubleTap";
            this.trackBarDoubleTap.Size = new System.Drawing.Size(242, 45);
            this.trackBarDoubleTap.TabIndex = 2;
            this.trackBarDoubleTap.TickFrequency = 4;
            this.trackBarDoubleTap.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarDoubleTap.ValueChanged += new System.EventHandler(this.trackBarDoubleTap_ValueChanged);
            // 
            // labelRefreshIntervalDisplay
            // 
            this.labelRefreshIntervalDisplay.Location = new System.Drawing.Point(128, 86);
            this.labelRefreshIntervalDisplay.Name = "labelRefreshIntervalDisplay";
            this.labelRefreshIntervalDisplay.Size = new System.Drawing.Size(120, 12);
            this.labelRefreshIntervalDisplay.TabIndex = 7;
            this.labelRefreshIntervalDisplay.Text = "ミリ秒";
            this.labelRefreshIntervalDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDoubleTapDisplay
            // 
            this.labelDoubleTapDisplay.Location = new System.Drawing.Point(127, 27);
            this.labelDoubleTapDisplay.Name = "labelDoubleTapDisplay";
            this.labelDoubleTapDisplay.Size = new System.Drawing.Size(121, 12);
            this.labelDoubleTapDisplay.TabIndex = 7;
            this.labelDoubleTapDisplay.Text = "ミリ秒";
            this.labelDoubleTapDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxReloadRun
            // 
            this.checkBoxReloadRun.AutoSize = true;
            this.checkBoxReloadRun.Location = new System.Drawing.Point(6, 206);
            this.checkBoxReloadRun.Name = "checkBoxReloadRun";
            this.checkBoxReloadRun.Size = new System.Drawing.Size(191, 16);
            this.checkBoxReloadRun.TabIndex = 6;
            this.checkBoxReloadRun.Text = "起動時最後に開いたスプリットを開く";
            this.checkBoxReloadRun.UseVisualStyleBackColor = true;
            // 
            // checkBoxWindowPos
            // 
            this.checkBoxWindowPos.AutoSize = true;
            this.checkBoxWindowPos.Location = new System.Drawing.Point(6, 185);
            this.checkBoxWindowPos.Name = "checkBoxWindowPos";
            this.checkBoxWindowPos.Size = new System.Drawing.Size(143, 16);
            this.checkBoxWindowPos.TabIndex = 5;
            this.checkBoxWindowPos.Text = "ウィンドウ位置を保存する";
            this.checkBoxWindowPos.UseVisualStyleBackColor = true;
            // 
            // comboBoxFallback
            // 
            this.comboBoxFallback.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFallback.FormattingEnabled = true;
            this.comboBoxFallback.Items.AddRange(new object[] {
            "未使用、不正確さ無視",
            "未使用、不正確さでたら知らせる",
            "大きくずれたら自動で利用",
            "常に使用"});
            this.comboBoxFallback.Location = new System.Drawing.Point(6, 160);
            this.comboBoxFallback.Name = "comboBoxFallback";
            this.comboBoxFallback.Size = new System.Drawing.Size(242, 20);
            this.comboBoxFallback.TabIndex = 4;
            // 
            // labelFallback
            // 
            this.labelFallback.AutoSize = true;
            this.labelFallback.Location = new System.Drawing.Point(6, 145);
            this.labelFallback.Name = "labelFallback";
            this.labelFallback.Size = new System.Drawing.Size(126, 12);
            this.labelFallback.TabIndex = 5;
            this.labelFallback.Text = "フォールバックタイマー利用";
            // 
            // labelRefreshInterval
            // 
            this.labelRefreshInterval.AutoSize = true;
            this.labelRefreshInterval.Location = new System.Drawing.Point(6, 86);
            this.labelRefreshInterval.Name = "labelRefreshInterval";
            this.labelRefreshInterval.Size = new System.Drawing.Size(65, 12);
            this.labelRefreshInterval.TabIndex = 4;
            this.labelRefreshInterval.Text = "再描画感覚";
            // 
            // labelDoubleTap
            // 
            this.labelDoubleTap.AutoSize = true;
            this.labelDoubleTap.Location = new System.Drawing.Point(6, 27);
            this.labelDoubleTap.Name = "labelDoubleTap";
            this.labelDoubleTap.Size = new System.Drawing.Size(92, 12);
            this.labelDoubleTap.TabIndex = 3;
            this.labelDoubleTap.Text = "2度押し間隔時間";
            // 
            // trackBarRefreshInterval
            // 
            this.trackBarRefreshInterval.Location = new System.Drawing.Point(6, 101);
            this.trackBarRefreshInterval.Maximum = 100;
            this.trackBarRefreshInterval.Minimum = 10;
            this.trackBarRefreshInterval.Name = "trackBarRefreshInterval";
            this.trackBarRefreshInterval.Size = new System.Drawing.Size(242, 45);
            this.trackBarRefreshInterval.TabIndex = 3;
            this.trackBarRefreshInterval.TickFrequency = 5;
            this.trackBarRefreshInterval.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarRefreshInterval.Value = 10;
            this.trackBarRefreshInterval.ValueChanged += new System.EventHandler(this.trackBarRefreshInterval_ValueChanged);
            // 
            // textBoxStartDelay
            // 
            this.textBoxStartDelay.Location = new System.Drawing.Point(72, 5);
            this.textBoxStartDelay.Name = "textBoxStartDelay";
            this.textBoxStartDelay.Size = new System.Drawing.Size(176, 19);
            this.textBoxStartDelay.TabIndex = 1;
            this.textBoxStartDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxStartDelay.TextChanged += new System.EventHandler(this.textBoxStartDelay_TextChanged);
            this.textBoxStartDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxStartDelay_KeyPress);
            // 
            // labelStartDelay
            // 
            this.labelStartDelay.AutoSize = true;
            this.labelStartDelay.Location = new System.Drawing.Point(6, 8);
            this.labelStartDelay.Name = "labelStartDelay";
            this.labelStartDelay.Size = new System.Drawing.Size(53, 12);
            this.labelStartDelay.TabIndex = 0;
            this.labelStartDelay.Text = "開始時間";
            // 
            // panelHotkeys
            // 
            this.panelHotkeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHotkeys.Controls.Add(this.buttonClearHotkey);
            this.panelHotkeys.Controls.Add(this.buttonSetHotkey);
            this.panelHotkeys.Controls.Add(this.textBoxHotkey);
            this.panelHotkeys.Controls.Add(this.listViewHotkeys);
            this.panelHotkeys.Controls.Add(this.checkBoxHotkeysEnabled);
            this.panelHotkeys.Location = new System.Drawing.Point(135, 11);
            this.panelHotkeys.Name = "panelHotkeys";
            this.panelHotkeys.Padding = new System.Windows.Forms.Padding(3);
            this.panelHotkeys.Size = new System.Drawing.Size(256, 293);
            this.panelHotkeys.TabIndex = 3;
            // 
            // buttonClearHotkey
            // 
            this.buttonClearHotkey.Location = new System.Drawing.Point(203, 265);
            this.buttonClearHotkey.Name = "buttonClearHotkey";
            this.buttonClearHotkey.Size = new System.Drawing.Size(45, 21);
            this.buttonClearHotkey.TabIndex = 5;
            this.buttonClearHotkey.Text = "消去";
            this.buttonClearHotkey.UseVisualStyleBackColor = true;
            this.buttonClearHotkey.Click += new System.EventHandler(this.buttonClearHotkey_Click);
            // 
            // buttonSetHotkey
            // 
            this.buttonSetHotkey.Location = new System.Drawing.Point(152, 265);
            this.buttonSetHotkey.Name = "buttonSetHotkey";
            this.buttonSetHotkey.Size = new System.Drawing.Size(45, 21);
            this.buttonSetHotkey.TabIndex = 4;
            this.buttonSetHotkey.Text = "セット";
            this.buttonSetHotkey.UseVisualStyleBackColor = true;
            this.buttonSetHotkey.Click += new System.EventHandler(this.buttonSetHotkey_Click);
            // 
            // textBoxHotkey
            // 
            this.textBoxHotkey.AcceptsTab = true;
            this.textBoxHotkey.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxHotkey.Location = new System.Drawing.Point(6, 266);
            this.textBoxHotkey.Multiline = true;
            this.textBoxHotkey.Name = "textBoxHotkey";
            this.textBoxHotkey.ReadOnly = true;
            this.textBoxHotkey.Size = new System.Drawing.Size(140, 19);
            this.textBoxHotkey.TabIndex = 3;
            this.textBoxHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkey_KeyDown);
            // 
            // listViewHotkeys
            // 
            this.listViewHotkeys.AllowDrop = true;
            this.listViewHotkeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDescription,
            this.columnHotkey});
            this.listViewHotkeys.FullRowSelect = true;
            this.listViewHotkeys.GridLines = true;
            this.listViewHotkeys.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewHotkeys.HideSelection = false;
            this.listViewHotkeys.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20});
            this.listViewHotkeys.Location = new System.Drawing.Point(6, 27);
            this.listViewHotkeys.MultiSelect = false;
            this.listViewHotkeys.Name = "listViewHotkeys";
            this.listViewHotkeys.Scrollable = false;
            this.listViewHotkeys.Size = new System.Drawing.Size(242, 233);
            this.listViewHotkeys.SmallImageList = this.imageListHotkey;
            this.listViewHotkeys.TabIndex = 2;
            this.listViewHotkeys.UseCompatibleStateImageBehavior = false;
            this.listViewHotkeys.View = System.Windows.Forms.View.Details;
            this.listViewHotkeys.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewHotkeys_ItemSelectionChanged);
            this.listViewHotkeys.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewHotkeys_MouseUp);
            // 
            // columnDescription
            // 
            this.columnDescription.Text = "概要";
            this.columnDescription.Width = 144;
            // 
            // columnHotkey
            // 
            this.columnHotkey.Text = "ホットキー";
            this.columnHotkey.Width = 121;
            // 
            // imageListHotkey
            // 
            this.imageListHotkey.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListHotkey.ImageSize = new System.Drawing.Size(1, 25);
            this.imageListHotkey.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // checkBoxHotkeysEnabled
            // 
            this.checkBoxHotkeysEnabled.AutoSize = true;
            this.checkBoxHotkeysEnabled.Location = new System.Drawing.Point(6, 6);
            this.checkBoxHotkeysEnabled.Name = "checkBoxHotkeysEnabled";
            this.checkBoxHotkeysEnabled.Size = new System.Drawing.Size(48, 16);
            this.checkBoxHotkeysEnabled.TabIndex = 1;
            this.checkBoxHotkeysEnabled.Text = "有効";
            this.checkBoxHotkeysEnabled.UseVisualStyleBackColor = true;
            // 
            // panelDisplaySettings
            // 
            this.panelDisplaySettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDisplaySettings.Controls.Add(this.labelDetailedWidth);
            this.panelDisplaySettings.Controls.Add(this.numericUpDownDetailedWidth);
            this.panelDisplaySettings.Controls.Add(this.buttonBackgroundSetup);
            this.panelDisplaySettings.Controls.Add(this.groupBoxDisplayMode);
            this.panelDisplaySettings.Controls.Add(this.labelWideSegments);
            this.panelDisplaySettings.Controls.Add(this.labelDetailedSegments);
            this.panelDisplaySettings.Controls.Add(this.numericUpDownWideSegments);
            this.panelDisplaySettings.Controls.Add(this.numericUpDownDetailedSegments);
            this.panelDisplaySettings.Controls.Add(this.checkBoxWideShowLast);
            this.panelDisplaySettings.Controls.Add(this.labelIcons);
            this.panelDisplaySettings.Controls.Add(this.checkBoxShowTitle);
            this.panelDisplaySettings.Controls.Add(this.checkBoxDetailedShowLast);
            this.panelDisplaySettings.Controls.Add(this.checkBoxWideBlanks);
            this.panelDisplaySettings.Controls.Add(this.checkBoxDetailedBlanks);
            this.panelDisplaySettings.Controls.Add(this.labelWideMode);
            this.panelDisplaySettings.Controls.Add(this.labelDetailedMode);
            this.panelDisplaySettings.Controls.Add(this.comboBoxIcons);
            this.panelDisplaySettings.Controls.Add(this.checkBoxShowAttemptCount);
            this.panelDisplaySettings.Controls.Add(this.labelOpacityDisplay);
            this.panelDisplaySettings.Controls.Add(this.trackBarOpacity);
            this.panelDisplaySettings.Controls.Add(this.labelOpacity);
            this.panelDisplaySettings.Location = new System.Drawing.Point(135, 11);
            this.panelDisplaySettings.Name = "panelDisplaySettings";
            this.panelDisplaySettings.Padding = new System.Windows.Forms.Padding(3);
            this.panelDisplaySettings.Size = new System.Drawing.Size(256, 293);
            this.panelDisplaySettings.TabIndex = 3;
            // 
            // labelDetailedWidth
            // 
            this.labelDetailedWidth.AutoSize = true;
            this.labelDetailedWidth.Location = new System.Drawing.Point(6, 91);
            this.labelDetailedWidth.Name = "labelDetailedWidth";
            this.labelDetailedWidth.Size = new System.Drawing.Size(87, 12);
            this.labelDetailedWidth.TabIndex = 14;
            this.labelDetailedWidth.Text = "詳細タイマーの幅";
            // 
            // numericUpDownDetailedWidth
            // 
            this.numericUpDownDetailedWidth.Location = new System.Drawing.Point(118, 90);
            this.numericUpDownDetailedWidth.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numericUpDownDetailedWidth.Minimum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownDetailedWidth.Name = "numericUpDownDetailedWidth";
            this.numericUpDownDetailedWidth.Size = new System.Drawing.Size(130, 19);
            this.numericUpDownDetailedWidth.TabIndex = 13;
            this.numericUpDownDetailedWidth.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // buttonBackgroundSetup
            // 
            this.buttonBackgroundSetup.Location = new System.Drawing.Point(6, 114);
            this.buttonBackgroundSetup.Name = "buttonBackgroundSetup";
            this.buttonBackgroundSetup.Size = new System.Drawing.Size(242, 21);
            this.buttonBackgroundSetup.TabIndex = 1;
            this.buttonBackgroundSetup.TabStop = false;
            this.buttonBackgroundSetup.Text = "背景設定";
            this.buttonBackgroundSetup.UseVisualStyleBackColor = true;
            this.buttonBackgroundSetup.Click += new System.EventHandler(this.buttonBackgroundImage_Click);
            // 
            // groupBoxDisplayMode
            // 
            this.groupBoxDisplayMode.Controls.Add(this.radioButtonDisplayDetailed);
            this.groupBoxDisplayMode.Controls.Add(this.radioButtonDisplayWide);
            this.groupBoxDisplayMode.Controls.Add(this.radioButtonDisplayCompact);
            this.groupBoxDisplayMode.Controls.Add(this.radioButtonDisplayTimer);
            this.groupBoxDisplayMode.Location = new System.Drawing.Point(6, 140);
            this.groupBoxDisplayMode.Name = "groupBoxDisplayMode";
            this.groupBoxDisplayMode.Size = new System.Drawing.Size(242, 60);
            this.groupBoxDisplayMode.TabIndex = 12;
            this.groupBoxDisplayMode.TabStop = false;
            this.groupBoxDisplayMode.Text = "表示形式";
            // 
            // radioButtonDisplayDetailed
            // 
            this.radioButtonDisplayDetailed.AutoSize = true;
            this.radioButtonDisplayDetailed.Location = new System.Drawing.Point(124, 39);
            this.radioButtonDisplayDetailed.Name = "radioButtonDisplayDetailed";
            this.radioButtonDisplayDetailed.Size = new System.Drawing.Size(47, 16);
            this.radioButtonDisplayDetailed.TabIndex = 3;
            this.radioButtonDisplayDetailed.TabStop = true;
            this.radioButtonDisplayDetailed.Text = "詳細";
            this.radioButtonDisplayDetailed.UseVisualStyleBackColor = true;
            // 
            // radioButtonDisplayWide
            // 
            this.radioButtonDisplayWide.AutoSize = true;
            this.radioButtonDisplayWide.Location = new System.Drawing.Point(124, 18);
            this.radioButtonDisplayWide.Name = "radioButtonDisplayWide";
            this.radioButtonDisplayWide.Size = new System.Drawing.Size(56, 16);
            this.radioButtonDisplayWide.TabIndex = 2;
            this.radioButtonDisplayWide.TabStop = true;
            this.radioButtonDisplayWide.Text = "横置き";
            this.radioButtonDisplayWide.UseVisualStyleBackColor = true;
            // 
            // radioButtonDisplayCompact
            // 
            this.radioButtonDisplayCompact.AutoSize = true;
            this.radioButtonDisplayCompact.Location = new System.Drawing.Point(6, 39);
            this.radioButtonDisplayCompact.Name = "radioButtonDisplayCompact";
            this.radioButtonDisplayCompact.Size = new System.Drawing.Size(47, 16);
            this.radioButtonDisplayCompact.TabIndex = 1;
            this.radioButtonDisplayCompact.TabStop = true;
            this.radioButtonDisplayCompact.Text = "縮小";
            this.radioButtonDisplayCompact.UseVisualStyleBackColor = true;
            // 
            // radioButtonDisplayTimer
            // 
            this.radioButtonDisplayTimer.AutoSize = true;
            this.radioButtonDisplayTimer.Location = new System.Drawing.Point(6, 18);
            this.radioButtonDisplayTimer.Name = "radioButtonDisplayTimer";
            this.radioButtonDisplayTimer.Size = new System.Drawing.Size(80, 16);
            this.radioButtonDisplayTimer.TabIndex = 0;
            this.radioButtonDisplayTimer.TabStop = true;
            this.radioButtonDisplayTimer.Text = "タイマーのみ";
            this.radioButtonDisplayTimer.UseVisualStyleBackColor = true;
            // 
            // labelWideSegments
            // 
            this.labelWideSegments.Location = new System.Drawing.Point(193, 240);
            this.labelWideSegments.Name = "labelWideSegments";
            this.labelWideSegments.Size = new System.Drawing.Size(55, 12);
            this.labelWideSegments.TabIndex = 11;
            this.labelWideSegments.Text = "区間数";
            // 
            // labelDetailedSegments
            // 
            this.labelDetailedSegments.Location = new System.Drawing.Point(193, 206);
            this.labelDetailedSegments.Name = "labelDetailedSegments";
            this.labelDetailedSegments.Size = new System.Drawing.Size(55, 12);
            this.labelDetailedSegments.TabIndex = 11;
            this.labelDetailedSegments.Text = "区間数";
            // 
            // numericUpDownWideSegments
            // 
            this.numericUpDownWideSegments.Location = new System.Drawing.Point(193, 253);
            this.numericUpDownWideSegments.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownWideSegments.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWideSegments.Name = "numericUpDownWideSegments";
            this.numericUpDownWideSegments.Size = new System.Drawing.Size(55, 19);
            this.numericUpDownWideSegments.TabIndex = 10;
            this.numericUpDownWideSegments.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownDetailedSegments
            // 
            this.numericUpDownDetailedSegments.Location = new System.Drawing.Point(193, 220);
            this.numericUpDownDetailedSegments.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericUpDownDetailedSegments.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDetailedSegments.Name = "numericUpDownDetailedSegments";
            this.numericUpDownDetailedSegments.Size = new System.Drawing.Size(55, 19);
            this.numericUpDownDetailedSegments.TabIndex = 10;
            this.numericUpDownDetailedSegments.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // checkBoxWideShowLast
            // 
            this.checkBoxWideShowLast.AutoSize = true;
            this.checkBoxWideShowLast.Location = new System.Drawing.Point(70, 254);
            this.checkBoxWideShowLast.Name = "checkBoxWideShowLast";
            this.checkBoxWideShowLast.Size = new System.Drawing.Size(117, 16);
            this.checkBoxWideShowLast.TabIndex = 9;
            this.checkBoxWideShowLast.Text = "常に最終区間表示";
            this.checkBoxWideShowLast.UseVisualStyleBackColor = true;
            // 
            // labelIcons
            // 
            this.labelIcons.Location = new System.Drawing.Point(180, 49);
            this.labelIcons.Name = "labelIcons";
            this.labelIcons.Size = new System.Drawing.Size(68, 15);
            this.labelIcons.TabIndex = 5;
            this.labelIcons.Text = "アイコン";
            // 
            // checkBoxShowTitle
            // 
            this.checkBoxShowTitle.AutoSize = true;
            this.checkBoxShowTitle.Location = new System.Drawing.Point(6, 48);
            this.checkBoxShowTitle.Name = "checkBoxShowTitle";
            this.checkBoxShowTitle.Size = new System.Drawing.Size(104, 16);
            this.checkBoxShowTitle.TabIndex = 3;
            this.checkBoxShowTitle.Text = "タイトル名を表示";
            this.checkBoxShowTitle.UseVisualStyleBackColor = true;
            // 
            // checkBoxDetailedShowLast
            // 
            this.checkBoxDetailedShowLast.AutoSize = true;
            this.checkBoxDetailedShowLast.Location = new System.Drawing.Point(70, 221);
            this.checkBoxDetailedShowLast.Name = "checkBoxDetailedShowLast";
            this.checkBoxDetailedShowLast.Size = new System.Drawing.Size(117, 16);
            this.checkBoxDetailedShowLast.TabIndex = 9;
            this.checkBoxDetailedShowLast.Text = "常に最終区間表示";
            this.checkBoxDetailedShowLast.UseVisualStyleBackColor = true;
            // 
            // checkBoxWideBlanks
            // 
            this.checkBoxWideBlanks.AutoSize = true;
            this.checkBoxWideBlanks.Location = new System.Drawing.Point(6, 254);
            this.checkBoxWideBlanks.Name = "checkBoxWideBlanks";
            this.checkBoxWideBlanks.Size = new System.Drawing.Size(48, 16);
            this.checkBoxWideBlanks.TabIndex = 8;
            this.checkBoxWideBlanks.Text = "空白";
            this.checkBoxWideBlanks.UseVisualStyleBackColor = true;
            // 
            // checkBoxDetailedBlanks
            // 
            this.checkBoxDetailedBlanks.AutoSize = true;
            this.checkBoxDetailedBlanks.Location = new System.Drawing.Point(6, 221);
            this.checkBoxDetailedBlanks.Name = "checkBoxDetailedBlanks";
            this.checkBoxDetailedBlanks.Size = new System.Drawing.Size(48, 16);
            this.checkBoxDetailedBlanks.TabIndex = 8;
            this.checkBoxDetailedBlanks.Text = "空白";
            this.checkBoxDetailedBlanks.UseVisualStyleBackColor = true;
            // 
            // labelWideMode
            // 
            this.labelWideMode.AutoSize = true;
            this.labelWideMode.Location = new System.Drawing.Point(6, 239);
            this.labelWideMode.Name = "labelWideMode";
            this.labelWideMode.Size = new System.Drawing.Size(38, 12);
            this.labelWideMode.TabIndex = 7;
            this.labelWideMode.Text = "横置き";
            // 
            // labelDetailedMode
            // 
            this.labelDetailedMode.AutoSize = true;
            this.labelDetailedMode.Location = new System.Drawing.Point(6, 206);
            this.labelDetailedMode.Name = "labelDetailedMode";
            this.labelDetailedMode.Size = new System.Drawing.Size(29, 12);
            this.labelDetailedMode.TabIndex = 7;
            this.labelDetailedMode.Text = "詳細";
            // 
            // comboBoxIcons
            // 
            this.comboBoxIcons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIcons.FormattingEnabled = true;
            this.comboBoxIcons.Items.AddRange(new object[] {
            "なし",
            "16x16",
            "24x24",
            "32x32"});
            this.comboBoxIcons.Location = new System.Drawing.Point(180, 65);
            this.comboBoxIcons.Name = "comboBoxIcons";
            this.comboBoxIcons.Size = new System.Drawing.Size(68, 20);
            this.comboBoxIcons.TabIndex = 6;
            // 
            // checkBoxShowAttemptCount
            // 
            this.checkBoxShowAttemptCount.AutoSize = true;
            this.checkBoxShowAttemptCount.Location = new System.Drawing.Point(6, 66);
            this.checkBoxShowAttemptCount.Name = "checkBoxShowAttemptCount";
            this.checkBoxShowAttemptCount.Size = new System.Drawing.Size(105, 16);
            this.checkBoxShowAttemptCount.TabIndex = 4;
            this.checkBoxShowAttemptCount.Text = "試行回数を表示";
            this.checkBoxShowAttemptCount.UseVisualStyleBackColor = true;
            // 
            // labelOpacityDisplay
            // 
            this.labelOpacityDisplay.Location = new System.Drawing.Point(130, 3);
            this.labelOpacityDisplay.Name = "labelOpacityDisplay";
            this.labelOpacityDisplay.Size = new System.Drawing.Size(118, 12);
            this.labelOpacityDisplay.TabIndex = 2;
            this.labelOpacityDisplay.Text = " %";
            this.labelOpacityDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.Location = new System.Drawing.Point(6, 18);
            this.trackBarOpacity.Maximum = 100;
            this.trackBarOpacity.Minimum = 5;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(242, 45);
            this.trackBarOpacity.TabIndex = 1;
            this.trackBarOpacity.TickFrequency = 5;
            this.trackBarOpacity.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarOpacity.Value = 5;
            this.trackBarOpacity.ValueChanged += new System.EventHandler(this.trackBarOpacity_ValueChanged);
            // 
            // labelOpacity
            // 
            this.labelOpacity.AutoSize = true;
            this.labelOpacity.Location = new System.Drawing.Point(6, 3);
            this.labelOpacity.Name = "labelOpacity";
            this.labelOpacity.Size = new System.Drawing.Size(89, 12);
            this.labelOpacity.TabIndex = 0;
            this.labelOpacity.Text = "タイマー不透明度";
            // 
            // panelFontSettings
            // 
            this.panelFontSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFontSettings.Controls.Add(this.groupBoxDViewFont);
            this.panelFontSettings.Controls.Add(this.groupBoxPrimWndFont);
            this.panelFontSettings.Controls.Add(this.labelNote);
            this.panelFontSettings.Location = new System.Drawing.Point(135, 11);
            this.panelFontSettings.Name = "panelFontSettings";
            this.panelFontSettings.Padding = new System.Windows.Forms.Padding(3);
            this.panelFontSettings.Size = new System.Drawing.Size(256, 293);
            this.panelFontSettings.TabIndex = 3;
            // 
            // groupBoxDViewFont
            // 
            this.groupBoxDViewFont.Controls.Add(this.comboBoxDViewFont);
            this.groupBoxDViewFont.Location = new System.Drawing.Point(6, 99);
            this.groupBoxDViewFont.Name = "groupBoxDViewFont";
            this.groupBoxDViewFont.Size = new System.Drawing.Size(242, 42);
            this.groupBoxDViewFont.TabIndex = 5;
            this.groupBoxDViewFont.TabStop = false;
            this.groupBoxDViewFont.Text = "詳細タイマーフォント";
            // 
            // comboBoxDViewFont
            // 
            this.comboBoxDViewFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDViewFont.FormattingEnabled = true;
            this.comboBoxDViewFont.Location = new System.Drawing.Point(6, 18);
            this.comboBoxDViewFont.Name = "comboBoxDViewFont";
            this.comboBoxDViewFont.Size = new System.Drawing.Size(230, 20);
            this.comboBoxDViewFont.TabIndex = 1;
            // 
            // groupBoxPrimWndFont
            // 
            this.groupBoxPrimWndFont.Controls.Add(this.checkBoxClockDigitalFont);
            this.groupBoxPrimWndFont.Controls.Add(this.comboBoxPrimWndFont);
            this.groupBoxPrimWndFont.Controls.Add(this.numericUpDownPrimWndMult);
            this.groupBoxPrimWndFont.Controls.Add(this.labelPrimWndMult);
            this.groupBoxPrimWndFont.Location = new System.Drawing.Point(6, 6);
            this.groupBoxPrimWndFont.Name = "groupBoxPrimWndFont";
            this.groupBoxPrimWndFont.Size = new System.Drawing.Size(242, 88);
            this.groupBoxPrimWndFont.TabIndex = 4;
            this.groupBoxPrimWndFont.TabStop = false;
            this.groupBoxPrimWndFont.Text = "優先フォント";
            // 
            // checkBoxClockDigitalFont
            // 
            this.checkBoxClockDigitalFont.AutoSize = true;
            this.checkBoxClockDigitalFont.Location = new System.Drawing.Point(6, 66);
            this.checkBoxClockDigitalFont.Name = "checkBoxClockDigitalFont";
            this.checkBoxClockDigitalFont.Size = new System.Drawing.Size(168, 16);
            this.checkBoxClockDigitalFont.TabIndex = 4;
            this.checkBoxClockDigitalFont.Text = "タイマーフォントをデジタルにする";
            this.checkBoxClockDigitalFont.UseVisualStyleBackColor = true;
            // 
            // comboBoxPrimWndFont
            // 
            this.comboBoxPrimWndFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrimWndFont.Location = new System.Drawing.Point(6, 18);
            this.comboBoxPrimWndFont.Name = "comboBoxPrimWndFont";
            this.comboBoxPrimWndFont.Size = new System.Drawing.Size(230, 20);
            this.comboBoxPrimWndFont.TabIndex = 1;
            // 
            // numericUpDownPrimWndMult
            // 
            this.numericUpDownPrimWndMult.DecimalPlaces = 2;
            this.numericUpDownPrimWndMult.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownPrimWndMult.Location = new System.Drawing.Point(85, 42);
            this.numericUpDownPrimWndMult.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownPrimWndMult.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownPrimWndMult.Name = "numericUpDownPrimWndMult";
            this.numericUpDownPrimWndMult.Size = new System.Drawing.Size(151, 19);
            this.numericUpDownPrimWndMult.TabIndex = 2;
            this.numericUpDownPrimWndMult.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // labelPrimWndMult
            // 
            this.labelPrimWndMult.AutoSize = true;
            this.labelPrimWndMult.Location = new System.Drawing.Point(6, 44);
            this.labelPrimWndMult.Name = "labelPrimWndMult";
            this.labelPrimWndMult.Size = new System.Drawing.Size(53, 12);
            this.labelPrimWndMult.TabIndex = 3;
            this.labelPrimWndMult.Text = "拡大倍率";
            // 
            // labelNote
            // 
            this.labelNote.Location = new System.Drawing.Point(6, 186);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(242, 89);
            this.labelNote.TabIndex = 1;
            this.labelNote.Text = "注意\r\n対応していないフォントがあるかもしれません";
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 337);
            this.Controls.Add(this.panelGeneralOptions);
            this.Controls.Add(this.listViewPanelSelector);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonDefaults);
            this.Controls.Add(this.panelDisplaySettings);
            this.Controls.Add(this.panelHotkeys);
            this.Controls.Add(this.panelFontSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.panelGeneralOptions.ResumeLayout(false);
            this.panelGeneralOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numwv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDoubleTap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRefreshInterval)).EndInit();
            this.panelHotkeys.ResumeLayout(false);
            this.panelHotkeys.PerformLayout();
            this.panelDisplaySettings.ResumeLayout(false);
            this.panelDisplaySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDetailedWidth)).EndInit();
            this.groupBoxDisplayMode.ResumeLayout(false);
            this.groupBoxDisplayMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWideSegments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDetailedSegments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.panelFontSettings.ResumeLayout(false);
            this.groupBoxDViewFont.ResumeLayout(false);
            this.groupBoxPrimWndFont.ResumeLayout(false);
            this.groupBoxPrimWndFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrimWndMult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDefaults;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listViewPanelSelector;
        private System.Windows.Forms.ImageList imageListPageIcons;
        private System.Windows.Forms.Panel panelGeneralOptions;
        private System.Windows.Forms.Label labelStartDelay;
        private System.Windows.Forms.Panel panelHotkeys;
        private System.Windows.Forms.Panel panelDisplaySettings;
        private System.Windows.Forms.TextBox textBoxStartDelay;
        private System.Windows.Forms.Label labelDoubleTap;
        private System.Windows.Forms.TrackBar trackBarDoubleTap;
        private System.Windows.Forms.Label labelRefreshInterval;
        private System.Windows.Forms.TrackBar trackBarRefreshInterval;
        private System.Windows.Forms.ComboBox comboBoxFallback;
        private System.Windows.Forms.Label labelFallback;
        private System.Windows.Forms.CheckBox checkBoxReloadRun;
        private System.Windows.Forms.CheckBox checkBoxWindowPos;
        private System.Windows.Forms.Label labelRefreshIntervalDisplay;
        private System.Windows.Forms.Label labelDoubleTapDisplay;
        private System.Windows.Forms.Button buttonClearHotkey;
        private System.Windows.Forms.Button buttonSetHotkey;
        private System.Windows.Forms.TextBox textBoxHotkey;
        private System.Windows.Forms.ListView listViewHotkeys;
        private System.Windows.Forms.ColumnHeader columnDescription;
        private System.Windows.Forms.ColumnHeader columnHotkey;
        private System.Windows.Forms.CheckBox checkBoxHotkeysEnabled;
        private System.Windows.Forms.Panel panelFontSettings;
        private System.Windows.Forms.ComboBox comboBoxPrimWndFont;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.GroupBox groupBoxPrimWndFont;
        private System.Windows.Forms.CheckBox checkBoxClockDigitalFont;
        private System.Windows.Forms.NumericUpDown numericUpDownPrimWndMult;
        private System.Windows.Forms.Label labelPrimWndMult;
        private System.Windows.Forms.GroupBox groupBoxDViewFont;
        private System.Windows.Forms.ComboBox comboBoxDViewFont;
        private System.Windows.Forms.Label labelOpacityDisplay;
        private System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.Label labelOpacity;
        private System.Windows.Forms.Label labelIcons;
        private System.Windows.Forms.CheckBox checkBoxShowAttemptCount;
        private System.Windows.Forms.CheckBox checkBoxShowTitle;
        private System.Windows.Forms.ComboBox comboBoxIcons;
        private System.Windows.Forms.GroupBox groupBoxDisplayMode;
        private System.Windows.Forms.RadioButton radioButtonDisplayTimer;
        private System.Windows.Forms.Label labelWideSegments;
        private System.Windows.Forms.Label labelDetailedSegments;
        private System.Windows.Forms.NumericUpDown numericUpDownWideSegments;
        private System.Windows.Forms.NumericUpDown numericUpDownDetailedSegments;
        private System.Windows.Forms.CheckBox checkBoxWideShowLast;
        private System.Windows.Forms.CheckBox checkBoxDetailedShowLast;
        private System.Windows.Forms.CheckBox checkBoxWideBlanks;
        private System.Windows.Forms.CheckBox checkBoxDetailedBlanks;
        private System.Windows.Forms.Label labelWideMode;
        private System.Windows.Forms.Label labelDetailedMode;
        private System.Windows.Forms.RadioButton radioButtonDisplayDetailed;
        private System.Windows.Forms.RadioButton radioButtonDisplayWide;
        private System.Windows.Forms.RadioButton radioButtonDisplayCompact;
        private System.Windows.Forms.Button buttonBackgroundSetup;
        private System.Windows.Forms.NumericUpDown numericUpDownDetailedWidth;
        private System.Windows.Forms.Label labelDetailedWidth;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numwv;
        private System.Windows.Forms.ImageList imageListHotkey;
    }
}