using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WSplitTimer.Properties;

namespace WSplitTimer
{
    public partial class SettingsDialog : Form
    {
        private WSplit wsplit;

        private List<Panel> panelList = new List<Panel>();
        private int activePanel = -1;

        private List<Keys> hotkeyList = new List<Keys>();
        private int selectedHotkeyIndex;
        Keys newHotkey;

        private string[] fontNames = FontFamily.Families.Select(f => f.Name).ToArray();

        private BackgroundImageDialog backgroundImageDialog;
        private BackgroundImageDialog BackgroundImageDialog
        {
            get
            {
                if (this.backgroundImageDialog == null)
                    this.backgroundImageDialog = new BackgroundImageDialog();
                return this.backgroundImageDialog;
            }
        }

        protected override CreateParams CreateParams
        {
            // Overriding this property as done here gets rid of graphical artifacts
            // that occur when many controls are updated at once.
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public string StartDelay
        {
            get { return this.textBoxStartDelay.Text; }
            set { this.textBoxStartDelay.Text = value; }
        }

        public int DetailedWidth
        {
            get { return (int)this.numericUpDownDetailedWidth.Value; }
            set { this.numericUpDownDetailedWidth.Value = value; }
        }

        public bool BackgroundSettingsChanged
        {
            get;
            private set;
        }

        private int ActivePanel
        {
            get { return this.activePanel; }
            set
            {
                this.activePanel = value;
                this.panelList[this.activePanel].BringToFront();
            }
        }

        public SettingsDialog()
        {
            InitializeComponent();

            // Setting up list view and panels:
            this.panelList.Add(this.panelGeneralOptions);
            this.panelList.Add(this.panelHotkeys);
            this.panelList.Add(this.panelFontSettings);
            this.panelList.Add(this.panelDisplaySettings);

            // Setting up other controls:
            this.hotkeyList.AddRange(new Keys[8]);

            this.listViewHotkeys.BeginUpdate();
            for (int i = 0; i < hotkeyList.Count; ++i)
                this.listViewHotkeys.Items[i].SubItems.Add("");
            this.listViewHotkeys.EndUpdate();

            this.comboBoxPrimWndFont.BeginUpdate();
            this.comboBoxPrimWndFont.Items.AddRange(this.fontNames);
            this.comboBoxPrimWndFont.EndUpdate();

            this.comboBoxDViewFont.BeginUpdate();
            this.comboBoxDViewFont.Items.AddRange(this.fontNames);
            this.comboBoxDViewFont.EndUpdate();
        }

        // Custom ShowDialog method, that will populate the settings before calling the default method
        public DialogResult ShowDialog(WSplit wsplit, int startupPanel)
        {
            this.wsplit = wsplit;

            ListView_SetItemSpacing(listViewPanelSelector, (short)listViewPanelSelector.ClientSize.Width, 76);

            // Moves the wanted panel on top of the others
            this.listViewPanelSelector.Items[startupPanel].Selected = true;
            this.ActivePanel = startupPanel;

            // Tell that, so far, there were no change in the background settings:
            this.BackgroundSettingsChanged = false;

            // If for some reason, a value is not compatible with WSplit, the settings
            // will automatically be brought back to default.
            try
            {
                this.PopulateSettings();
            }
            catch (Exception)   // Any kind of exception
            {
                this.RestoreDefaults();
                MessageBoxEx.Show(this,
                    "An error has occurred and your settings were brought back to defaults.",
                    "Defaults Restored", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return base.ShowDialog(wsplit);
        }

        private void PopulateSettings()
        {
            //
            // Initializing controls with application global settings
            //
            // General options:
            this.trackBarDoubleTap.Value = Settings.Profile.DoubleTapGuard / 50;
            this.UpdateDoubleTapDelayDisplay();
            this.trackBarRefreshInterval.Value = Settings.Profile.RefreshRate;
            this.UpdateRefreshIntervalDisplay();
            this.comboBoxFallback.SelectedIndex = Settings.Profile.FallbackPreference;
            this.checkBoxWindowPos.Checked = Settings.Profile.SaveWindowPos;
            this.checkBoxReloadRun.Checked = Settings.Profile.LoadMostRecent;

            // Global hotkeys:
            this.checkBoxHotkeysEnabled.Checked = Settings.Profile.EnabledHotkeys;

            this.hotkeyList[0] = Settings.Profile.SplitKey;
            this.hotkeyList[1] = Settings.Profile.PauseKey;
            this.hotkeyList[2] = Settings.Profile.StopKey;
            this.hotkeyList[3] = Settings.Profile.ResetKey;
            this.hotkeyList[4] = Settings.Profile.PrevKey;
            this.hotkeyList[5] = Settings.Profile.NextKey;
            this.hotkeyList[6] = Settings.Profile.CompTypeKey;
            this.hotkeyList[7] = Settings.Profile.HotkeyToggleKey;

            this.listViewHotkeys.BeginUpdate();
            for (int i = 0; i < hotkeyList.Count; ++i)
                this.listViewHotkeys.Items[i].SubItems[1] = new ListViewItem.ListViewSubItem(this.listViewHotkeys.Items[i], FormatHotkey(hotkeyList[i]));
            this.listViewHotkeys.EndUpdate();

            this.selectedHotkeyIndex = 0;
            this.listViewHotkeys.Items[this.selectedHotkeyIndex].Selected = true;

            // Font settings:
            this.comboBoxPrimWndFont.SelectedItem =
                (fontNames.Any(f => f == Settings.Profile.FontFamilySegments)) ? Settings.Profile.FontFamilySegments : FontFamily.GenericSansSerif.Name;
            this.comboBoxDViewFont.SelectedItem =
                (fontNames.Any(f => f == Settings.Profile.FontFamilyDView)) ? Settings.Profile.FontFamilyDView : FontFamily.GenericSansSerif.Name;

            this.numericUpDownPrimWndMult.Value = (decimal)Settings.Profile.FontMultiplierSegments;
            this.checkBoxClockDigitalFont.Checked = Settings.Profile.DigitalClock;

            // Display settings:
            this.trackBarOpacity.Value = (int)(Settings.Profile.Opacity * 100);

            this.checkBoxShowTitle.Checked = Settings.Profile.ShowTitle;
            this.checkBoxShowAttemptCount.Checked = Settings.Profile.ShowAttempts;
            this.comboBoxIcons.SelectedIndex = Settings.Profile.SegmentIcons;

            switch ((WSplit.DisplayMode)Settings.Profile.DisplayMode)
            {
                case WSplit.DisplayMode.Timer: this.radioButtonDisplayTimer.Checked = true; break;
                case WSplit.DisplayMode.Compact: this.radioButtonDisplayCompact.Checked = true; break;
                case WSplit.DisplayMode.Wide: this.radioButtonDisplayWide.Checked = true; break;
                default: this.radioButtonDisplayDetailed.Checked = true; break;
            }

            this.checkBoxDetailedBlanks.Checked = Settings.Profile.DisplayBlankSegs;
            this.checkBoxDetailedShowLast.Checked = Settings.Profile.ShowLastDetailed;
            this.numericUpDownDetailedSegments.Value = Settings.Profile.DisplaySegs;

            this.checkBoxWideBlanks.Checked = Settings.Profile.WideSegBlanks;
            this.checkBoxWideShowLast.Checked = Settings.Profile.ShowLastWide;
            this.numericUpDownWideSegments.Value = Settings.Profile.WideSegs;
        }

        private void RestoreDefaults()
        {
            Settings.Profile.Reset();
            Settings.Profile.FirstRun = false;
            this.PopulateSettings();
        }

        public void ApplyChanges()
        {
            // Called manually after dialog is closed.
            // Saves all the control states in the Settings

            // General options:
            Settings.Profile.DoubleTapGuard = this.trackBarDoubleTap.Value * 50;
            Settings.Profile.RefreshRate = this.trackBarRefreshInterval.Value;
            Settings.Profile.FallbackPreference = this.comboBoxFallback.SelectedIndex;
            Settings.Profile.SaveWindowPos = this.checkBoxWindowPos.Checked;
            Settings.Profile.LoadMostRecent = this.checkBoxReloadRun.Checked;

            // Global hotkeys:
            Settings.Profile.EnabledHotkeys = this.checkBoxHotkeysEnabled.Checked;

            Settings.Profile.SplitKey = this.hotkeyList[0];
            Settings.Profile.PauseKey = this.hotkeyList[1];
            Settings.Profile.StopKey = this.hotkeyList[2];
            Settings.Profile.ResetKey = this.hotkeyList[3];
            Settings.Profile.PrevKey = this.hotkeyList[4];
            Settings.Profile.NextKey = this.hotkeyList[5];
            Settings.Profile.CompTypeKey = this.hotkeyList[6];
            Settings.Profile.HotkeyToggleKey = this.hotkeyList[7];

            // Font settings:
            Settings.Profile.FontFamilySegments = (string)this.comboBoxPrimWndFont.SelectedItem;
            Settings.Profile.FontMultiplierSegments = (float)this.numericUpDownPrimWndMult.Value;
            Settings.Profile.DigitalClock = this.checkBoxClockDigitalFont.Checked;

            Settings.Profile.FontFamilyDView = (string)this.comboBoxDViewFont.SelectedItem;

            // Display settings:
            Settings.Profile.Opacity = this.trackBarOpacity.Value / 100.0;

            Settings.Profile.ShowTitle = this.checkBoxShowTitle.Checked;
            Settings.Profile.ShowAttempts = this.checkBoxShowAttemptCount.Checked;
            Settings.Profile.SegmentIcons = this.comboBoxIcons.SelectedIndex;

            if (this.radioButtonDisplayTimer.Checked)
                Settings.Profile.DisplayMode = (int)WSplit.DisplayMode.Timer;
            else if (this.radioButtonDisplayCompact.Checked)
                Settings.Profile.DisplayMode = (int)WSplit.DisplayMode.Compact;
            else if (this.radioButtonDisplayWide.Checked)
                Settings.Profile.DisplayMode = (int)WSplit.DisplayMode.Wide;
            else
                Settings.Profile.DisplayMode = (int)WSplit.DisplayMode.Detailed;

            Settings.Profile.DisplayBlankSegs = this.checkBoxDetailedBlanks.Checked;
            Settings.Profile.ShowLastDetailed = this.checkBoxDetailedShowLast.Checked;
            Settings.Profile.DisplaySegs = (int)this.numericUpDownDetailedSegments.Value;

            Settings.Profile.WideSegBlanks = this.checkBoxWideBlanks.Checked;
            Settings.Profile.ShowLastWide = this.checkBoxWideShowLast.Checked;
            Settings.Profile.WideSegs = (int)this.numericUpDownWideSegments.Value;
        }

        private string FormatHotkey(Keys key)
        {
            string str = "";
            Keys keys = key & Keys.KeyCode;

            if ((keys != Keys.ControlKey) && ((key & Keys.Control) == Keys.Control))
                str += "Ctrl+";

            if ((keys != Keys.ShiftKey) && ((key & Keys.Shift) == Keys.Shift))
                str += "Shift+";

            if ((keys != Keys.Menu) && ((key & Keys.Alt) == Keys.Alt))
                str += "Alt+";

            return (str + keys);
        }

        private void UpdateDoubleTapDelayDisplay()
        {
            if (this.trackBarDoubleTap.Value == 0)
                this.labelDoubleTapDisplay.Text = "Off";
            else
                this.labelDoubleTapDisplay.Text = (this.trackBarDoubleTap.Value * 50) + " ms";
        }

        private void UpdateRefreshIntervalDisplay()
        {
            this.labelRefreshIntervalDisplay.Text = this.trackBarRefreshInterval.Value + " ms";
        }

        private void UpdateOpacityDisplay()
        {
            this.labelOpacityDisplay.Text = this.trackBarOpacity.Value + "%";
            this.wsplit.Opacity = trackBarOpacity.Value / 100.0;
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public void ListView_SetItemSpacing(ListView listView, short leftPadding, short topPadding)
        {
            SendMessage(listView.Handle, 0x1035, IntPtr.Zero, (IntPtr)(((ushort)leftPadding) | (uint)(topPadding << 16)));
        }

        private void listViewPanelSelector_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // If the item that sends the event is not the one that was already selected,
            // we proceed to the switch between the panels by bringing the selected one to the front
            if (this.ActivePanel != e.ItemIndex)
            {
                this.ActivePanel = e.ItemIndex;
            }
        }

        private void listViewPanelSelector_MouseUp(object sender, MouseEventArgs e)
        {
            // When the user is done clicking, the selected input is set to the last item
            // that got checked or unchecked. Therefore, if the user clicked on an item,
            // this item stays selected. If the user clicks the background, the last selected
            // item (which got unselected by clicking) is selected again.
            this.listViewPanelSelector.Items[this.activePanel].Selected = true;
        }

        private void trackBarDoubleTap_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateDoubleTapDelayDisplay();
        }

        private void trackBarRefreshInterval_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateRefreshIntervalDisplay();
        }

        private void listViewHotkeys_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // Same as page selector ListView
            if (e.Item.Selected)
            {
                this.selectedHotkeyIndex = e.ItemIndex;
                this.textBoxHotkey.Text = FormatHotkey(this.hotkeyList[this.selectedHotkeyIndex]);
            }
        }

        private void listViewHotkeys_MouseUp(object sender, MouseEventArgs e)
        {
            // Same as page selector ListView
            this.listViewHotkeys.Items[this.selectedHotkeyIndex].Selected = true;
            this.textBoxHotkey.Focus();
            this.textBoxHotkey.Select(0, 0);
        }

        private void textBoxHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            this.newHotkey = e.KeyData;
            this.textBoxHotkey.Text = FormatHotkey(this.newHotkey);
        }

        private void buttonSetHotkey_Click(object sender, EventArgs e)
        {
            this.hotkeyList[this.listViewHotkeys.SelectedIndices[0]] = this.newHotkey;
            this.listViewHotkeys.Items[this.listViewHotkeys.SelectedIndices[0]].SubItems[1].Text = this.FormatHotkey(this.newHotkey);
        }

        private void buttonClearHotkey_Click(object sender, EventArgs e)
        {
            this.newHotkey = Keys.None;
            this.hotkeyList[this.listViewHotkeys.SelectedIndices[0]] = this.newHotkey;
            this.listViewHotkeys.Items[this.listViewHotkeys.SelectedIndices[0]].SubItems[1].Text = this.FormatHotkey(this.newHotkey);
            this.textBoxHotkey.Text = this.FormatHotkey(this.newHotkey);
        }

        private void trackBarOpacity_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateOpacityDisplay();
        }

        private void buttonDefaults_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this, "Are you sure?", "Restore Defaults", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.RestoreDefaults();
            }
        }

        private void textBoxStartDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ':' && e.KeyChar != '.' && e.KeyChar != ',')
                e.Handled = true;
        }

        private void textBoxStartDelay_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(this.textBoxStartDelay.Text, "[^0-9:.,]"))
                this.textBoxStartDelay.Text = Regex.Replace(this.textBoxStartDelay.Text, "[^0-9:.,]", "");
        }

        private void buttonBackgroundImage_Click(object sender, EventArgs e)
        {
            // Shows the backgroundImageDialog and, if OK is clicked, apply the settings
            if (this.BackgroundImageDialog.ShowDialog(this, wsplit) == DialogResult.OK)
            {
                this.BackgroundImageDialog.ApplyChanges();
                this.BackgroundSettingsChanged = true;
            }
        }
    }
}
