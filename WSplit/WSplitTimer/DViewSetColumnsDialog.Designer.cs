namespace WSplitTimer
{
    partial class DViewSetColumnsDialog
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
            this.checkBoxAlwaysShowComp = new System.Windows.Forms.CheckBox();
            this.checkBoxOldTime = new System.Windows.Forms.CheckBox();
            this.checkBoxBestTime = new System.Windows.Forms.CheckBox();
            this.checkBoxSumOfBests = new System.Windows.Forms.CheckBox();
            this.checkBoxLiveTime = new System.Windows.Forms.CheckBox();
            this.checkBoxLiveDelta = new System.Windows.Forms.CheckBox();
            this.groupBoxCompColumns = new System.Windows.Forms.GroupBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxCompColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxAlwaysShowComp
            // 
            this.checkBoxAlwaysShowComp.AutoSize = true;
            this.checkBoxAlwaysShowComp.Location = new System.Drawing.Point(12, 60);
            this.checkBoxAlwaysShowComp.Name = "checkBoxAlwaysShowComp";
            this.checkBoxAlwaysShowComp.Size = new System.Drawing.Size(201, 17);
            this.checkBoxAlwaysShowComp.TabIndex = 0;
            this.checkBoxAlwaysShowComp.Text = "Always show default comparison time";
            this.checkBoxAlwaysShowComp.UseVisualStyleBackColor = true;
            // 
            // checkBoxOldTime
            // 
            this.checkBoxOldTime.AutoSize = true;
            this.checkBoxOldTime.Location = new System.Drawing.Point(6, 19);
            this.checkBoxOldTime.Name = "checkBoxOldTime";
            this.checkBoxOldTime.Size = new System.Drawing.Size(64, 17);
            this.checkBoxOldTime.TabIndex = 1;
            this.checkBoxOldTime.Text = "Old time";
            this.checkBoxOldTime.UseVisualStyleBackColor = true;
            // 
            // checkBoxBestTime
            // 
            this.checkBoxBestTime.AutoSize = true;
            this.checkBoxBestTime.Location = new System.Drawing.Point(76, 19);
            this.checkBoxBestTime.Name = "checkBoxBestTime";
            this.checkBoxBestTime.Size = new System.Drawing.Size(69, 17);
            this.checkBoxBestTime.TabIndex = 2;
            this.checkBoxBestTime.Text = "Best time";
            this.checkBoxBestTime.UseVisualStyleBackColor = true;
            // 
            // checkBoxSumOfBests
            // 
            this.checkBoxSumOfBests.AutoSize = true;
            this.checkBoxSumOfBests.Location = new System.Drawing.Point(151, 19);
            this.checkBoxSumOfBests.Name = "checkBoxSumOfBests";
            this.checkBoxSumOfBests.Size = new System.Drawing.Size(87, 17);
            this.checkBoxSumOfBests.TabIndex = 3;
            this.checkBoxSumOfBests.Text = "Sum of bests";
            this.checkBoxSumOfBests.UseVisualStyleBackColor = true;
            // 
            // checkBoxLiveTime
            // 
            this.checkBoxLiveTime.AutoSize = true;
            this.checkBoxLiveTime.Location = new System.Drawing.Point(12, 83);
            this.checkBoxLiveTime.Name = "checkBoxLiveTime";
            this.checkBoxLiveTime.Size = new System.Drawing.Size(94, 17);
            this.checkBoxLiveTime.TabIndex = 4;
            this.checkBoxLiveTime.Text = "Show live time";
            this.checkBoxLiveTime.UseVisualStyleBackColor = true;
            // 
            // checkBoxLiveDelta
            // 
            this.checkBoxLiveDelta.AutoSize = true;
            this.checkBoxLiveDelta.Location = new System.Drawing.Point(12, 106);
            this.checkBoxLiveDelta.Name = "checkBoxLiveDelta";
            this.checkBoxLiveDelta.Size = new System.Drawing.Size(139, 17);
            this.checkBoxLiveDelta.TabIndex = 5;
            this.checkBoxLiveDelta.Text = "Show live run delta (+/-)";
            this.checkBoxLiveDelta.UseVisualStyleBackColor = true;
            // 
            // groupBoxCompColumns
            // 
            this.groupBoxCompColumns.Controls.Add(this.checkBoxOldTime);
            this.groupBoxCompColumns.Controls.Add(this.checkBoxBestTime);
            this.groupBoxCompColumns.Controls.Add(this.checkBoxSumOfBests);
            this.groupBoxCompColumns.Location = new System.Drawing.Point(12, 12);
            this.groupBoxCompColumns.Name = "groupBoxCompColumns";
            this.groupBoxCompColumns.Size = new System.Drawing.Size(244, 42);
            this.groupBoxCompColumns.TabIndex = 6;
            this.groupBoxCompColumns.TabStop = false;
            this.groupBoxCompColumns.Text = "Show comparison columns...";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(12, 129);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(119, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(137, 129);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(119, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // DViewSetColumnsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 164);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxCompColumns);
            this.Controls.Add(this.checkBoxLiveDelta);
            this.Controls.Add(this.checkBoxLiveTime);
            this.Controls.Add(this.checkBoxAlwaysShowComp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DViewSetColumnsDialog";
            this.Text = "Select columns...";
            this.groupBoxCompColumns.ResumeLayout(false);
            this.groupBoxCompColumns.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAlwaysShowComp;
        private System.Windows.Forms.CheckBox checkBoxOldTime;
        private System.Windows.Forms.CheckBox checkBoxBestTime;
        private System.Windows.Forms.CheckBox checkBoxSumOfBests;
        private System.Windows.Forms.CheckBox checkBoxLiveTime;
        private System.Windows.Forms.CheckBox checkBoxLiveDelta;
        private System.Windows.Forms.GroupBox groupBoxCompColumns;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}