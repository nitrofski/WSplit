namespace WSplitTimer
{
    partial class BackgroundImageDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgroundImageDialog));
            this.groupBoxImageBg = new System.Windows.Forms.GroupBox();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.buttonAutoSelect = new System.Windows.Forms.Button();
            this.labelZoomDisplay = new System.Windows.Forms.Label();
            this.labelZoom = new System.Windows.Forms.Label();
            this.buttonZoomFit = new System.Windows.Forms.Button();
            this.buttonResetSelect = new System.Windows.Forms.Button();
            this.trackBarZoom = new System.Windows.Forms.TrackBar();
            this.picBoxImageSelectionModifier = new System.Windows.Forms.PictureBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxImagePath = new System.Windows.Forms.TextBox();
            this.checkBoxUseImageBg = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxTopLayer = new System.Windows.Forms.GroupBox();
            this.labelOpacityDisplay = new System.Windows.Forms.Label();
            this.radioButtonBlack = new System.Windows.Forms.RadioButton();
            this.radioButtonPlain = new System.Windows.Forms.RadioButton();
            this.radioButtonDefault = new System.Windows.Forms.RadioButton();
            this.labelOpacity = new System.Windows.Forms.Label();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelHideSettings = new System.Windows.Forms.Panel();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.groupBoxImageBg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImageSelectionModifier)).BeginInit();
            this.groupBoxTopLayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this.panelHideSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxImageBg
            // 
            this.groupBoxImageBg.Controls.Add(this.linkLabelHelp);
            this.groupBoxImageBg.Controls.Add(this.buttonAutoSelect);
            this.groupBoxImageBg.Controls.Add(this.labelZoomDisplay);
            this.groupBoxImageBg.Controls.Add(this.labelZoom);
            this.groupBoxImageBg.Controls.Add(this.buttonZoomFit);
            this.groupBoxImageBg.Controls.Add(this.buttonResetSelect);
            this.groupBoxImageBg.Controls.Add(this.trackBarZoom);
            this.groupBoxImageBg.Controls.Add(this.picBoxImageSelectionModifier);
            this.groupBoxImageBg.Controls.Add(this.buttonBrowse);
            this.groupBoxImageBg.Controls.Add(this.textBoxImagePath);
            this.groupBoxImageBg.Enabled = false;
            this.groupBoxImageBg.Location = new System.Drawing.Point(12, 12);
            this.groupBoxImageBg.Name = "groupBoxImageBg";
            this.groupBoxImageBg.Size = new System.Drawing.Size(466, 458);
            this.groupBoxImageBg.TabIndex = 0;
            this.groupBoxImageBg.TabStop = false;
            // 
            // linkLabelHelp
            // 
            this.linkLabelHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelHelp.Location = new System.Drawing.Point(6, 410);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(48, 13);
            this.linkLabelHelp.TabIndex = 11;
            this.linkLabelHelp.TabStop = true;
            this.linkLabelHelp.Text = "Help...";
            this.linkLabelHelp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabelHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHelp_LinkClicked);
            // 
            // buttonAutoSelect
            // 
            this.buttonAutoSelect.Enabled = false;
            this.buttonAutoSelect.Location = new System.Drawing.Point(6, 355);
            this.buttonAutoSelect.Name = "buttonAutoSelect";
            this.buttonAutoSelect.Size = new System.Drawing.Size(48, 23);
            this.buttonAutoSelect.TabIndex = 10;
            this.buttonAutoSelect.Text = "Auto";
            this.buttonAutoSelect.UseVisualStyleBackColor = true;
            this.buttonAutoSelect.Click += new System.EventHandler(this.buttonAutoSelect_Click);
            // 
            // labelZoomDisplay
            // 
            this.labelZoomDisplay.Enabled = false;
            this.labelZoomDisplay.Location = new System.Drawing.Point(8, 36);
            this.labelZoomDisplay.Name = "labelZoomDisplay";
            this.labelZoomDisplay.Size = new System.Drawing.Size(46, 13);
            this.labelZoomDisplay.TabIndex = 9;
            this.labelZoomDisplay.Text = "100%";
            this.labelZoomDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelZoom
            // 
            this.labelZoom.AutoSize = true;
            this.labelZoom.Enabled = false;
            this.labelZoom.Location = new System.Drawing.Point(6, 23);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(37, 13);
            this.labelZoom.TabIndex = 8;
            this.labelZoom.Text = "Zoom:";
            // 
            // buttonZoomFit
            // 
            this.buttonZoomFit.Enabled = false;
            this.buttonZoomFit.Location = new System.Drawing.Point(6, 326);
            this.buttonZoomFit.Name = "buttonZoomFit";
            this.buttonZoomFit.Size = new System.Drawing.Size(48, 23);
            this.buttonZoomFit.TabIndex = 7;
            this.buttonZoomFit.Text = "Fit";
            this.buttonZoomFit.UseVisualStyleBackColor = true;
            this.buttonZoomFit.Click += new System.EventHandler(this.buttonZoomFit_Click);
            // 
            // buttonResetSelect
            // 
            this.buttonResetSelect.Enabled = false;
            this.buttonResetSelect.Location = new System.Drawing.Point(6, 384);
            this.buttonResetSelect.Name = "buttonResetSelect";
            this.buttonResetSelect.Size = new System.Drawing.Size(48, 23);
            this.buttonResetSelect.TabIndex = 6;
            this.buttonResetSelect.Text = "Reset";
            this.buttonResetSelect.UseVisualStyleBackColor = true;
            this.buttonResetSelect.Click += new System.EventHandler(this.buttonResetSelect_Click);
            // 
            // trackBarZoom
            // 
            this.trackBarZoom.Enabled = false;
            this.trackBarZoom.LargeChange = 10;
            this.trackBarZoom.Location = new System.Drawing.Point(9, 52);
            this.trackBarZoom.Maximum = 200;
            this.trackBarZoom.Minimum = 5;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarZoom.Size = new System.Drawing.Size(45, 268);
            this.trackBarZoom.SmallChange = 5;
            this.trackBarZoom.TabIndex = 5;
            this.trackBarZoom.TickFrequency = 5;
            this.trackBarZoom.Value = 100;
            this.trackBarZoom.Scroll += new System.EventHandler(this.trackBarZoom_Scroll);
            this.trackBarZoom.ValueChanged += new System.EventHandler(this.trackBarZoom_ValueChanged);
            // 
            // picBoxImageSelectionModifier
            // 
            this.picBoxImageSelectionModifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxImageSelectionModifier.Enabled = false;
            this.picBoxImageSelectionModifier.InitialImage = null;
            this.picBoxImageSelectionModifier.Location = new System.Drawing.Point(60, 23);
            this.picBoxImageSelectionModifier.Name = "picBoxImageSelectionModifier";
            this.picBoxImageSelectionModifier.Size = new System.Drawing.Size(400, 400);
            this.picBoxImageSelectionModifier.TabIndex = 3;
            this.picBoxImageSelectionModifier.TabStop = false;
            this.picBoxImageSelectionModifier.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoxImageSelectionModifier_Paint);
            this.picBoxImageSelectionModifier.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBoxImageSelectionModifier_MouseDown);
            this.picBoxImageSelectionModifier.MouseEnter += new System.EventHandler(this.picBoxImageSelectionModifier_MouseEnter);
            this.picBoxImageSelectionModifier.MouseLeave += new System.EventHandler(this.picBoxImageSelectionModifier_MouseLeave);
            this.picBoxImageSelectionModifier.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBoxImageSelectionModifier_MouseMove);
            this.picBoxImageSelectionModifier.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoxImageSelectionModifier_MouseUp);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(385, 429);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxImagePath
            // 
            this.textBoxImagePath.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxImagePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxImagePath.Location = new System.Drawing.Point(6, 434);
            this.textBoxImagePath.Name = "textBoxImagePath";
            this.textBoxImagePath.ReadOnly = true;
            this.textBoxImagePath.Size = new System.Drawing.Size(373, 13);
            this.textBoxImagePath.TabIndex = 1;
            this.textBoxImagePath.Text = "No image selected";
            // 
            // checkBoxUseImageBg
            // 
            this.checkBoxUseImageBg.AutoSize = true;
            this.checkBoxUseImageBg.Enabled = false;
            this.checkBoxUseImageBg.Location = new System.Drawing.Point(18, 12);
            this.checkBoxUseImageBg.Name = "checkBoxUseImageBg";
            this.checkBoxUseImageBg.Size = new System.Drawing.Size(136, 17);
            this.checkBoxUseImageBg.TabIndex = 0;
            this.checkBoxUseImageBg.Text = "Use background image";
            this.checkBoxUseImageBg.UseVisualStyleBackColor = true;
            this.checkBoxUseImageBg.CheckedChanged += new System.EventHandler(this.checkBoxUseImageBg_CheckedChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image files (*.bmp, *.jpg, *.jpeg, *.png, *.gif, *.tif, *.tiff)|*.bmp;*.jpg;*.jpe" +
    "g;*.png;*.gif;*.tif;*.tiff";
            // 
            // groupBoxTopLayer
            // 
            this.groupBoxTopLayer.Controls.Add(this.labelOpacityDisplay);
            this.groupBoxTopLayer.Controls.Add(this.radioButtonBlack);
            this.groupBoxTopLayer.Controls.Add(this.radioButtonPlain);
            this.groupBoxTopLayer.Controls.Add(this.radioButtonDefault);
            this.groupBoxTopLayer.Controls.Add(this.labelOpacity);
            this.groupBoxTopLayer.Controls.Add(this.trackBarOpacity);
            this.groupBoxTopLayer.Location = new System.Drawing.Point(12, 476);
            this.groupBoxTopLayer.Name = "groupBoxTopLayer";
            this.groupBoxTopLayer.Size = new System.Drawing.Size(466, 95);
            this.groupBoxTopLayer.TabIndex = 4;
            this.groupBoxTopLayer.TabStop = false;
            this.groupBoxTopLayer.Text = "Top layer";
            // 
            // labelOpacityDisplay
            // 
            this.labelOpacityDisplay.Enabled = false;
            this.labelOpacityDisplay.Location = new System.Drawing.Point(55, 16);
            this.labelOpacityDisplay.Name = "labelOpacityDisplay";
            this.labelOpacityDisplay.Size = new System.Drawing.Size(405, 13);
            this.labelOpacityDisplay.TabIndex = 5;
            this.labelOpacityDisplay.Text = "100%";
            this.labelOpacityDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // radioButtonBlack
            // 
            this.radioButtonBlack.AutoSize = true;
            this.radioButtonBlack.Location = new System.Drawing.Point(348, 72);
            this.radioButtonBlack.Name = "radioButtonBlack";
            this.radioButtonBlack.Size = new System.Drawing.Size(112, 17);
            this.radioButtonBlack.TabIndex = 4;
            this.radioButtonBlack.TabStop = true;
            this.radioButtonBlack.Text = "Black background";
            this.radioButtonBlack.UseVisualStyleBackColor = true;
            // 
            // radioButtonPlain
            // 
            this.radioButtonPlain.AutoSize = true;
            this.radioButtonPlain.Location = new System.Drawing.Point(177, 72);
            this.radioButtonPlain.Name = "radioButtonPlain";
            this.radioButtonPlain.Size = new System.Drawing.Size(108, 17);
            this.radioButtonPlain.TabIndex = 3;
            this.radioButtonPlain.TabStop = true;
            this.radioButtonPlain.Text = "Plain background";
            this.radioButtonPlain.UseVisualStyleBackColor = true;
            // 
            // radioButtonDefault
            // 
            this.radioButtonDefault.AutoSize = true;
            this.radioButtonDefault.Location = new System.Drawing.Point(6, 72);
            this.radioButtonDefault.Name = "radioButtonDefault";
            this.radioButtonDefault.Size = new System.Drawing.Size(119, 17);
            this.radioButtonDefault.TabIndex = 2;
            this.radioButtonDefault.TabStop = true;
            this.radioButtonDefault.Text = "Default background";
            this.radioButtonDefault.UseVisualStyleBackColor = true;
            // 
            // labelOpacity
            // 
            this.labelOpacity.AutoSize = true;
            this.labelOpacity.Enabled = false;
            this.labelOpacity.Location = new System.Drawing.Point(6, 16);
            this.labelOpacity.Name = "labelOpacity";
            this.labelOpacity.Size = new System.Drawing.Size(46, 13);
            this.labelOpacity.TabIndex = 1;
            this.labelOpacity.Text = "Opacity:";
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.Enabled = false;
            this.trackBarOpacity.Location = new System.Drawing.Point(6, 32);
            this.trackBarOpacity.Maximum = 100;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(454, 45);
            this.trackBarOpacity.TabIndex = 0;
            this.trackBarOpacity.TickFrequency = 5;
            this.trackBarOpacity.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarOpacity.Value = 100;
            this.trackBarOpacity.ValueChanged += new System.EventHandler(this.trackBarOpacity_ValueChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(322, 577);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(403, 577);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelHideSettings
            // 
            this.panelHideSettings.Controls.Add(this.linkLabelGithub);
            this.panelHideSettings.Location = new System.Drawing.Point(12, 12);
            this.panelHideSettings.Name = "panelHideSettings";
            this.panelHideSettings.Size = new System.Drawing.Size(466, 458);
            this.panelHideSettings.TabIndex = 12;
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.Location = new System.Drawing.Point(7, 16);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(453, 439);
            this.linkLabelGithub.TabIndex = 0;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = resources.GetString("linkLabelGithub.Text");
            this.linkLabelGithub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGithub_LinkClicked);
            // 
            // BackgroundImageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 612);
            this.Controls.Add(this.panelHideSettings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxTopLayer);
            this.Controls.Add(this.checkBoxUseImageBg);
            this.Controls.Add(this.groupBoxImageBg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackgroundImageDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Background image";
            this.groupBoxImageBg.ResumeLayout(false);
            this.groupBoxImageBg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImageSelectionModifier)).EndInit();
            this.groupBoxTopLayer.ResumeLayout(false);
            this.groupBoxTopLayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.panelHideSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxImageBg;
        private System.Windows.Forms.CheckBox checkBoxUseImageBg;
        private System.Windows.Forms.TextBox textBoxImagePath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox picBoxImageSelectionModifier;
        private System.Windows.Forms.GroupBox groupBoxTopLayer;
        private System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.RadioButton radioButtonBlack;
        private System.Windows.Forms.RadioButton radioButtonPlain;
        private System.Windows.Forms.RadioButton radioButtonDefault;
        private System.Windows.Forms.Label labelOpacity;
        private System.Windows.Forms.Label labelOpacityDisplay;
        private System.Windows.Forms.Label labelZoomDisplay;
        private System.Windows.Forms.Label labelZoom;
        private System.Windows.Forms.Button buttonZoomFit;
        private System.Windows.Forms.Button buttonResetSelect;
        private System.Windows.Forms.TrackBar trackBarZoom;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAutoSelect;
        private System.Windows.Forms.LinkLabel linkLabelHelp;
        private System.Windows.Forms.Panel panelHideSettings;
        private System.Windows.Forms.LinkLabel linkLabelGithub;
    }
}