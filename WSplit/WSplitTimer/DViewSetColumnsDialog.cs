using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WSplitTimer.Properties;

namespace WSplitTimer
{
    public partial class DViewSetColumnsDialog : Form
    {
        public DViewSetColumnsDialog()
        {
            InitializeComponent();
            this.checkBoxOldTime.Checked = Settings.Profile.DViewShowOld;
            this.checkBoxBestTime.Checked = Settings.Profile.DViewShowBest;
            this.checkBoxSumOfBests.Checked = Settings.Profile.DViewShowSumOfBests;

            this.checkBoxAlwaysShowComp.Checked = Settings.Profile.DViewShowComp;
            this.checkBoxLiveTime.Checked = Settings.Profile.DViewShowLive;
            this.checkBoxLiveDelta.Checked = Settings.Profile.DViewShowDeltas;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Settings.Profile.DViewShowOld = this.checkBoxOldTime.Checked;
            Settings.Profile.DViewShowBest = this.checkBoxBestTime.Checked;
            Settings.Profile.DViewShowSumOfBests = this.checkBoxSumOfBests.Checked;

            Settings.Profile.DViewShowComp = this.checkBoxAlwaysShowComp.Checked;
            Settings.Profile.DViewShowLive = this.checkBoxLiveTime.Checked;
            Settings.Profile.DViewShowDeltas = this.checkBoxLiveDelta.Checked;
            base.DialogResult = DialogResult.OK;
        }
    }
}
