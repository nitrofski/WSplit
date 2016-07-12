using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WSplitTimer.Properties;
using System.Diagnostics;

namespace WSplitTimer
{
    public class WSplit : Form
    {
        private ContextMenuStrip timerMenu;

        private ToolStripMenuItem newButton;
        private ToolStripMenuItem openButton;
        private ToolStripMenuItem openRecent;
        private ToolStripMenuItem saveButton;
        private ToolStripMenuItem saveAsButton;
        private ToolStripMenuItem reloadButton;
        private ToolStripMenuItem closeButton;

        private ToolStripSeparator toolStripSeparator1;

        private ToolStripMenuItem menuItemStartAt;
        private ToolStripMenuItem resetButton;
        private ToolStripMenuItem stopButton;
        private ToolStripMenuItem newOldButton;

        private ToolStripSeparator toolStripSeparator2;

        private ToolStripMenuItem menuItemSettings;

        private ToolStripMenuItem displaySettingsMenu;
        private ToolStripMenuItem alwaysOnTop;
        private ToolStripMenuItem showRunTitleButton;
        private ToolStripMenuItem showAttemptCount;
        private ToolStripMenuItem showRunGoalMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem displayTimerOnlyButton;
        private ToolStripMenuItem displayCompactButton;
        private ToolStripMenuItem displayDetailedButton;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem displayWideButton;
        private ToolStripMenuItem clockAppearanceToolStripMenuItem;
        private ToolStripMenuItem showDecimalSeparator;
        private ToolStripMenuItem digitalClockButton;
        private ToolStripMenuItem clockAccent;
        private ToolStripMenuItem plainBg;
        private ToolStripMenuItem blackBg;
        private ToolStripMenuItem menuItemAdvancedDisplay;
        private ToolStripMenuItem setColorsButton;
        private ToolStripSeparator toolStripSeparator5;
        public ToolStripMenuItem advancedDetailButton;

        private ToolStripMenuItem compareMenu;
        private ToolStripMenuItem compareOldButton;
        private ToolStripMenuItem compareBestButton;
        private ToolStripMenuItem compareFastestButton;
        private ToolStripMenuItem compareSumBestButton;

        private ToolStripMenuItem trackBestMenu;
        private ToolStripMenuItem bestAsOverallButton;
        private ToolStripMenuItem bestAsSplitsButton;

        private ToolStripMenuItem layoutMenu;
        private ToolStripMenuItem prevsegButton;
        private ToolStripMenuItem timesaveButton;
        private ToolStripMenuItem sobButton;
        private ToolStripMenuItem predpbButton;
        private ToolStripMenuItem predbestButton;

        private ToolStripMenuItem gradientMenu;
        private ToolStripMenuItem horiButton;
        private ToolStripMenuItem vertButton;

        private ToolStripSeparator toolStripSeparator6;

        private ToolStripMenuItem aboutButton;
        private ToolStripMenuItem exitButton;

        public Font digitLarge;
        public Font digitMed;
        public Font clockLarge;
        public Font clockMed;
        public Font displayFont;
        public Font timeFont;
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
        private SettingsDialog settingsDialog;

        //private int attemptCount;
        private Size clockMinimumSize = new Size(120, 25);
        private Size clockMinimumSizeAbsolute = new Size(120, 25);
        private Rectangle clockRect;
        private bool clockResize;
        private IContainer components;
        private DisplayMode currentDispMode = DisplayMode.Null;
        private string decimalChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private Size detailPreferredSize = new Size(120, 0x19);
        private bool detailResizing;
        private int detailResizingY;
        private Timer doubleTapDelay;
        private DetailedView dview;
        private Timer flashDelay;
        //private int offsetStart;
        private DateTime offsetStartTime = new DateTime();
        //private string runFile;
        //private string runGoal;
        //private string runTitle = "";
        private int segHeight = 14;
        public Split split = new Split();
        private Timer startDelay;
        private Timer stopwatch;
        public DualStopwatch timer = new DualStopwatch(false);
        private bool wideResizing;
        private int wideResizingX;
        private int wideSegResizeWidth;
        private bool wideSegResizing;
        private int wideSegWidth = 100;
        private int wideSegWidthBase = 100;
        private int wideSegX;

        //public bool unsavedSplits;
        public bool modalWindowOpened;

        // Apparently unused variables...
        public const int HOTKEY_ID = 0x9d82;
        public KeyModifiers hotkeyMod;

        // The painter object is a sub object that has for only purpose to separate
        // the drawing code from the logic code, including the variables used for drawing.
        // The fact that all the drawing code is in a different object makes it possible to
        // have a modular drawing code without messing up this object more than it already was.
        //
        // NOTE: Currently, the painter object need access to the WSplit object it paints into
        // to be able to paint it correctly. Eventually, it would be a good thing to remove that
        // dependancy and have the WSplit object parameter the Painter object rather than have the
        // Painter object take values from the WSplit members.
        private Painter painter;

        //test
        private int r = 255;
        private int g = 127;
        private int b = 255;
        private bool rg = false;
        private bool gg = false;
        private bool gb = false;
        private bool bb = false;
        private bool br = false;
        private bool rr = true;



        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, uint vk);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // If the Settings Dialog has not been created yet, this property will take care of creating it:
        // It has for a purpose to try and speed up the startup by not loading the window yet,
        // but to also speed up the opening of the settings window after the first time.
        // It is not known yet if it fulfills its purpose or if it is completely useless.
        private SettingsDialog SettingsDialog
        {
            get
            {
                if (this.settingsDialog == null)
                    this.settingsDialog = new SettingsDialog();
                return this.settingsDialog;
            }
        }

        public WSplit()
        {
            this.InitializeComponent();
            base.Paint += new PaintEventHandler(this.clockPaint);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.ResizeRedraw = true;

            // Eventually, the painter should not be dependant of the WSplit object...
            this.painter = new Painter(this);

            this.dview = new DetailedView(this.split, this);
            this.Initialize();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            string str = Assembly.GetExecutingAssembly().GetName().Name + " v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            DateTime buildDateTime = GetBuildDateTime(Assembly.GetExecutingAssembly());
            double driftMilliseconds = this.timer.driftMilliseconds;
            string str2 = "Current fallback timer: " + string.Format("{0:+0.000;-0.000}", driftMilliseconds / 1000.0) + "s";
            string str3 = "";
            if (this.timer.useFallback)
                str3 = " [Using Fallback]";

            MessageBoxEx.Show(this, str + Environment.NewLine +
                            "by Wodanaz@SDA until 1.4.4" + Environment.NewLine +
                            "currently maintained by Nitrofski (twitch.tv/Nitrofski)" + Environment.NewLine +
                            "Japanese Translation by 0xwas" + Environment.NewLine +
                            Environment.NewLine +
                            "Github repository location: https://github.com/Nitrofski/WSplit" + Environment.NewLine +
                            "Compiled: " + buildDateTime.ToString() +
                            Environment.NewLine +
                            Environment.NewLine +
                            str2 + str3 + Environment.NewLine +
                            "(difference between fallback and standard timing methods)",
                            "About", MessageBoxButtons.OK);
        }

        private void advancedDetailButton_Click(object sender, EventArgs e)
        {
            if (this.advancedDetailButton.Checked)
            {
                if (this.split.LiveRun)
                    this.dview.Show();
            }
            else

                this.dview.Hide();
        }

        private void alwaysOnTop_Click(object sender, EventArgs e)
        {
            Settings.Profile.OnTop = !Settings.Profile.OnTop;
            this.alwaysOnTop.Checked = Settings.Profile.OnTop;
            base.TopMost = Settings.Profile.OnTop;
        }

        private void bestAsOverallButton_Click(object sender, EventArgs e)
        {
            this.bestAsOverallButton.Checked = true;
            this.bestAsSplitsButton.Checked = false;
            Settings.Profile.BestAsOverall = true;
        }

        private void bestAsSplitsButton_Click(object sender, EventArgs e)
        {
            this.bestAsOverallButton.Checked = false;
            this.bestAsSplitsButton.Checked = true;
            Settings.Profile.BestAsOverall = false;
        }

        private void blackBg_Click(object sender, EventArgs e)
        {
            this.plainBg.Checked = false;
            Settings.Profile.BackgroundPlain = false;
            Settings.Profile.BackgroundBlack = !Settings.Profile.BackgroundBlack;
            this.blackBg.Checked = Settings.Profile.BackgroundBlack;
            this.painter.RequestBackgroundRedraw();
            base.Invalidate();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            Settings.Profile.RecentFiles.Clear();
            this.populateRecentFiles();
        }

        private void clearHotkeys()
        {
            for (int i = 0; i < 7; i++)
                UnregisterHotKey(base.Handle, 0x9d82 + i);
        }

        private void clockAccent_Click(object sender, EventArgs e)
        {
            Settings.Profile.ClockAccent = !Settings.Profile.ClockAccent;
            this.clockAccent.Checked = Settings.Profile.ClockAccent;
            this.painter.RequestBackgroundRedraw();
            base.Invalidate();
        }

        private void clockPaint(object sender, PaintEventArgs e)
        {
            // The painter object will take care of drawing the clock
            this.painter.PaintAll(e.Graphics);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (this.promptForSave())
            {
                this.closeFile();
            }
        }

        private void closeFile()
        {
            this.split.Clear();
            this.split.RunTitle = "";
            this.split.RunGoal = "";
            this.split.AttemptsCount = 0;
            this.split.StartDelay = 0;
            this.detailPreferredSize = this.clockMinimumSize;
            this.InitializeDisplay();
        }

        private void compareOldButton_Click(object sender, EventArgs e)
        {
            this.SetCompareOld();
        }

        private void compareBestButton_Click(object sender, EventArgs e)
        {
            this.SetCompareBest();
        }

        private void compareFastestButton_Click(object sender, EventArgs e)
        {
            this.SetCompareFastest();
        }

        private void compareSumBestButton_Click(object sender, EventArgs e)
        {
            this.SetCompareSoB();
        }

        private void SetCompareOld()
        {
            Settings.Profile.CompareAgainst = 1;
            this.compareOldButton.Checked = true;
            this.compareBestButton.Checked = false;
            this.compareFastestButton.Checked = false;
            this.compareSumBestButton.Checked = false;
            this.updateDisplay();
        }

        private void SetCompareBest()
        {
            Settings.Profile.CompareAgainst = 2;
            this.compareOldButton.Checked = false;
            this.compareBestButton.Checked = true;
            this.compareFastestButton.Checked = false;
            this.compareSumBestButton.Checked = false;
            this.updateDisplay();
        }

        private void SetCompareFastest()
        {
            Settings.Profile.CompareAgainst = 0;
            this.compareOldButton.Checked = false;
            this.compareBestButton.Checked = false;
            this.compareFastestButton.Checked = true;
            this.compareSumBestButton.Checked = false;
            this.updateDisplay();
        }

        private void SetCompareSoB()
        {
            Settings.Profile.CompareAgainst = 3;
            this.compareOldButton.Checked = false;
            this.compareBestButton.Checked = false;
            this.compareFastestButton.Checked = false;
            this.compareSumBestButton.Checked = true;
            this.updateDisplay();
        }

        private void SwitchComparisonType()
        {
            switch (Settings.Profile.CompareAgainst)
            {
                case 1: // Old
                    SetCompareBest();
                    break;
                case 3: // Sum of Bests
                    SetCompareOld();
                    break;
                default: // Fastest & Best
                    SetCompareSoB();
                    break;
            }
        }

        private void configure(int startingPage)
        {
            // Prepares the Main and DView Windows:
            this.clearHotkeys();

            base.TopMost = false;
            this.dview.TopMost = false;
            this.modalWindowOpened = true;

            // A few settings are necessary before calling the custom ShowDialog method
            this.SettingsDialog.StartDelay = this.timeFormatter(((double)this.split.StartDelay) / 1000.0, TimeFormat.Seconds);
            this.SettingsDialog.DetailedWidth = this.clockRect.Width;

            // Costum ShowDialog method...
            if (this.SettingsDialog.ShowDialog(this, startingPage) == DialogResult.OK)
            {
                this.SettingsDialog.ApplyChanges();

                this.split.StartDelay = Convert.ToInt32((double)(this.timeParse(this.SettingsDialog.StartDelay) * 1000.0));
                this.clockRect.Width = this.SettingsDialog.DetailedWidth;
                this.updateDetailed();
                this.InitializeSettings();
                this.InitializeFonts();
            }

            if (this.SettingsDialog.BackgroundSettingsChanged)
                this.InitializeBackground();

            // Some changes need to be applied live:
            base.Opacity = Settings.Profile.Opacity;
            base.TopMost = Settings.Profile.OnTop;
            this.dview.TopMost = Settings.Profile.DViewOnTop;

            this.modalWindowOpened = false;
            this.setHotkeys();
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            this.configure(0);
        }

        private Color DarkenColor(Color original, double lightness)
        {
            lightness = Math.Max(Math.Min(lightness, 255.0), 0.0);
            return Color.FromArgb((int)(original.R * lightness), (int)(original.G * lightness), (int)(original.B * lightness));
        }

        private int detailSegCount()
        {
            int displaySegs = Settings.Profile.DisplaySegs;
            if (!Settings.Profile.DisplayBlankSegs)
            {
                displaySegs = Math.Min(this.split.Count, displaySegs);
            }
            return displaySegs;
        }

        private void digitalClockButton_Click(object sender, EventArgs e)
        {
            Settings.Profile.DigitalClock = !Settings.Profile.DigitalClock;
            this.digitalClockButton.Checked = Settings.Profile.DigitalClock;
            this.painter.RequestBackgroundRedraw();
            base.Invalidate();
        }

        private void displayCompact()
        {
            this.clockRect.Location = new Point(0, 15);
            this.clockMinimumSize = this.clockMinimumSizeAbsolute;
            if (Settings.Profile.SegmentIcons > 1)
            {
                this.clockMinimumSize.Width = (this.clockMinimumSizeAbsolute.Width + ((Settings.Profile.SegmentIcons + 1) * 8)) + 6;
            }
            if (this.currentDispMode != DisplayMode.Compact)
            {
                this.clockRect.Size = Settings.Profile.ClockSize;
                this.clockRect.Width += this.clockMinimumSize.Width - this.clockMinimumSizeAbsolute.Width;
            }
            if ((this.clockRect.Height < this.clockMinimumSize.Height) || (this.clockRect.Width < this.clockMinimumSize.Width))
            {
                this.clockRect.Size = this.clockMinimumSize;
            }
            this.currentDispMode = DisplayMode.Compact;
            base.Size = new Size(this.clockRect.Width, this.clockRect.Height + 0x1f);
        }

        private void displayCompactButton_Click(object sender, EventArgs e)
        {
            this.setDisplay(DisplayMode.Compact);
        }

        private void displayDetail()
        {
            int hh = 0;
            this.segHeight = Math.Max(14, (Settings.Profile.SegmentIcons + 1) * 8);
            this.clockMinimumSize = this.clockMinimumSizeAbsolute;
            if (this.currentDispMode != DisplayMode.Detailed)
            {
                this.clockRect.Size = this.detailPreferredSize;
            }
            if ((this.clockRect.Height < this.clockMinimumSize.Height) || (this.clockRect.Width < this.clockMinimumSize.Width))
            {
                this.clockRect.Size = this.clockMinimumSize;
            }
            int height = (this.clockRect.Height + (this.detailSegCount() * this.segHeight)) + 0x15;
            if (((this.split.RunTitle != "") && Settings.Profile.ShowTitle) && ((this.split.RunGoal != "") && Settings.Profile.ShowGoal))
            {
                height += 0x20;
            }
            else if ((this.split.RunTitle != "") && Settings.Profile.ShowTitle)
            {
                height += 0x12;
            }
            else if ((this.split.RunGoal != "") && Settings.Profile.ShowGoal)
            {
                height += 0x12;
            }
            /* component stuff here */
            if (Settings.Profile.ShowPrevSeg) { hh += 18; };
            if (Settings.Profile.ShowTimeSave) { hh += 18; };
            if (Settings.Profile.ShowSoB) { hh += 18; };
            if (Settings.Profile.PredPB) { hh += 18; };
            if (Settings.Profile.PredBest) { hh += 18; };
            height += hh - 18;
            this.clockRect.Location = new Point(0, (height - this.clockRect.Height) - hh);
            this.currentDispMode = DisplayMode.Detailed;
            base.Size = new Size(this.clockRect.Width, height);
        }

        private void displayDetailedButton_Click(object sender, EventArgs e)
        {
            this.setDisplay(DisplayMode.Detailed);
        }

        private int displaySegsWide()
        {
            int wideSegs = Settings.Profile.WideSegs;
            if (!Settings.Profile.WideSegBlanks)
            {
                wideSegs = Math.Min(this.split.Count, wideSegs);
            }
            return wideSegs;
        }

        private void displayTimer()
        {
            this.clockRect.Location = new Point(0, 0);
            this.clockMinimumSize = this.clockMinimumSizeAbsolute;
            if (this.currentDispMode != DisplayMode.Timer)
            {
                this.clockRect.Size = Settings.Profile.ClockSize;
            }
            if ((this.clockRect.Height < this.clockMinimumSize.Height) || (this.clockRect.Width < this.clockMinimumSize.Width))
            {
                this.clockRect.Size = this.clockMinimumSize;
            }
            this.currentDispMode = DisplayMode.Timer;
            base.Size = this.clockRect.Size;
        }

        private void displayTimerOnlyButton_Click(object sender, EventArgs e)
        {
            this.setDisplay(DisplayMode.Timer);
        }

        private void displayWide()
        {
            if (Settings.Profile.SegmentIcons >= 1)
            {
                this.wideSegWidth = this.wideSegWidthBase + ((Settings.Profile.SegmentIcons + 1) * 8);
            }
            else
            {
                this.wideSegWidth = this.wideSegWidthBase;
            }
            this.clockMinimumSize.Width = this.clockMinimumSizeAbsolute.Width;
            if (Settings.Profile.SegmentIcons == 3)
            {
                this.clockMinimumSize.Height = 0x20;
            }
            else
            {
                this.clockMinimumSize.Height = Settings.Profile.WideHeight; // make this changeable //
            }
            this.clockRect.Location = new Point(0, 0);
            if (((this.currentDispMode != DisplayMode.Wide) || (this.clockRect.Height != this.clockMinimumSize.Height)) || (this.clockRect.Width < this.clockMinimumSize.Width))
            {
                this.clockRect.Size = this.clockMinimumSize;
            }
            this.currentDispMode = DisplayMode.Wide;
            base.Size = new Size((this.clockRect.Width + 124) + (this.displaySegsWide() * this.wideSegWidth), this.clockRect.Height);
        }

        private void displayWideButton_Click(object sender, EventArgs e)
        {
            this.setDisplay(DisplayMode.Wide);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void doSplit()
        {
            double time = Math.Truncate(this.timer.Elapsed.TotalSeconds * 100) / 100;
            this.split.DoSplit(time);
            if (!this.split.Done)
            {
                this.flashClock();
            }
            else
            {
                this.stopwatch.Enabled = false;
                this.newOldButton.Enabled = true;
            }
            this.updateDisplay();
        }

        private void doubleTapDelay_Tick(object sender, EventArgs e)
        {
            this.doubleTapDelay.Dispose();
            this.doubleTapDelay = null;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (this.promptForSave())
            {
                base.Close();
            }
        }

        private void flashClock()
        {
            /* if (this.flashDelay == null)
            {
                this.flashDelay = new Timer();
                this.flashDelay.Tick += new EventHandler(this.unflashClock);
                this.flashDelay.Interval = 750;
                this.flashDelay.Enabled = true;
                base.Invalidate();
            } */
        }

        private static DateTime GetBuildDateTime(Assembly assembly)
        {
            if (File.Exists(assembly.Location))
            {
                byte[] buffer = new byte[Math.Max(Marshal.SizeOf(typeof(_IMAGE_FILE_HEADER)), 4)];
                using (FileStream stream = new FileStream(assembly.Location, FileMode.Open, FileAccess.Read))
                {
                    stream.Position = 60L;
                    stream.Read(buffer, 0, 4);
                    stream.Position = BitConverter.ToUInt32(buffer, 0);
                    stream.Read(buffer, 0, 4);
                    stream.Read(buffer, 0, buffer.Length);
                }
                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    _IMAGE_FILE_HEADER _image_file_header = (_IMAGE_FILE_HEADER)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(_IMAGE_FILE_HEADER));
                    DateTime time2 = new DateTime(0x7b2, 1, 1);
                    return TimeZone.CurrentTimeZone.ToLocalTime(time2.AddSeconds((double)_image_file_header.TimeDateStamp));
                }
                finally
                {
                    handle.Free();
                }
            }
            return new DateTime();
        }

        private Color getDViewDeltaColor(double newDelta, double oldDelta)
        {
            if (newDelta > 0.0)
            {
                if (newDelta > oldDelta)
                {
                    return ColorSettings.Profile.UsedDViewSegBehindLoss;
                }
                return ColorSettings.Profile.UsedDViewSegBehindGain;
            }
            if (newDelta > oldDelta)
            {
                return ColorSettings.Profile.UsedDViewSegAheadLoss;
            }
            return ColorSettings.Profile.UsedDViewSegAheadGain;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.saveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadButton = new System.Windows.Forms.ToolStripMenuItem();
            this.closeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemStartAt = new System.Windows.Forms.ToolStripMenuItem();
            this.resetButton = new System.Windows.Forms.ToolStripMenuItem();
            this.stopButton = new System.Windows.Forms.ToolStripMenuItem();
            this.newOldButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.displaySettingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.showRunTitleButton = new System.Windows.Forms.ToolStripMenuItem();
            this.showAttemptCount = new System.Windows.Forms.ToolStripMenuItem();
            this.showRunGoalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.displayTimerOnlyButton = new System.Windows.Forms.ToolStripMenuItem();
            this.displayCompactButton = new System.Windows.Forms.ToolStripMenuItem();
            this.displayWideButton = new System.Windows.Forms.ToolStripMenuItem();
            this.displayDetailedButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.clockAppearanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDecimalSeparator = new System.Windows.Forms.ToolStripMenuItem();
            this.digitalClockButton = new System.Windows.Forms.ToolStripMenuItem();
            this.clockAccent = new System.Windows.Forms.ToolStripMenuItem();
            this.plainBg = new System.Windows.Forms.ToolStripMenuItem();
            this.blackBg = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdvancedDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.setColorsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.advancedDetailButton = new System.Windows.Forms.ToolStripMenuItem();
            this.compareMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.compareOldButton = new System.Windows.Forms.ToolStripMenuItem();
            this.compareBestButton = new System.Windows.Forms.ToolStripMenuItem();
            this.compareFastestButton = new System.Windows.Forms.ToolStripMenuItem();
            this.compareSumBestButton = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBestMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.bestAsOverallButton = new System.Windows.Forms.ToolStripMenuItem();
            this.bestAsSplitsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.prevsegButton = new System.Windows.Forms.ToolStripMenuItem();
            this.timesaveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.sobButton = new System.Windows.Forms.ToolStripMenuItem();
            this.predpbButton = new System.Windows.Forms.ToolStripMenuItem();
            this.predbestButton = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.horiButton = new System.Windows.Forms.ToolStripMenuItem();
            this.vertButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.stopwatch = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.timerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerMenu
            // 
            this.timerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.openButton,
            this.openRecent,
            this.saveButton,
            this.saveAsButton,
            this.reloadButton,
            this.closeButton,
            this.toolStripSeparator2,
            this.menuItemStartAt,
            this.resetButton,
            this.stopButton,
            this.newOldButton,
            this.toolStripSeparator1,
            this.menuItemSettings,
            this.displaySettingsMenu,
            this.compareMenu,
            this.trackBestMenu,
            this.layoutMenu,
            this.gradientMenu,
            this.toolStripSeparator4,
            this.aboutButton,
            this.exitButton});
            this.timerMenu.Name = "timerMenu";
            this.timerMenu.Size = new System.Drawing.Size(213, 462);
            // 
            // newButton
            // 
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(214, 22);
            this.newButton.Text = "スプリット作成・編集";
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // openButton
            // 
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(214, 22);
            this.openButton.Text = "開く";
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // openRecent
            // 
            this.openRecent.Name = "openRecent";
            this.openRecent.Size = new System.Drawing.Size(214, 22);
            this.openRecent.Text = "履歴から開く";
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(214, 22);
            this.saveButton.Text = "上書き保存";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Enabled = false;
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(214, 22);
            this.saveAsButton.Text = "別名で保存";
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // reloadButton
            // 
            this.reloadButton.Enabled = false;
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(214, 22);
            this.reloadButton.Text = "再読込";
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Enabled = false;
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(214, 22);
            this.closeButton.Text = "閉じる";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
            // 
            // menuItemStartAt
            // 
            this.menuItemStartAt.Name = "menuItemStartAt";
            this.menuItemStartAt.Size = new System.Drawing.Size(214, 22);
            this.menuItemStartAt.Text = "スタート位置変更";
            this.menuItemStartAt.Click += new System.EventHandler(this.menuItemStartAt_Click);
            // 
            // resetButton
            // 
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(214, 22);
            this.resetButton.Text = "リセット";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(214, 22);
            this.stopButton.Text = "停止";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // newOldButton
            // 
            this.newOldButton.Enabled = false;
            this.newOldButton.Name = "newOldButton";
            this.newOldButton.Size = new System.Drawing.Size(214, 22);
            this.newOldButton.Text = "このスプリットを旧記録にする";
            this.newOldButton.Click += new System.EventHandler(this.newOldButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Name = "menuItemSettings";
            this.menuItemSettings.Size = new System.Drawing.Size(214, 22);
            this.menuItemSettings.Text = "設定";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // displaySettingsMenu
            // 
            this.displaySettingsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTop,
            this.showRunTitleButton,
            this.showAttemptCount,
            this.showRunGoalMenuItem,
            this.toolStripSeparator3,
            this.displayTimerOnlyButton,
            this.displayCompactButton,
            this.displayWideButton,
            this.displayDetailedButton,
            this.toolStripSeparator5,
            this.clockAppearanceToolStripMenuItem,
            this.plainBg,
            this.blackBg,
            this.menuItemAdvancedDisplay,
            this.setColorsButton,
            this.toolStripSeparator6,
            this.advancedDetailButton});
            this.displaySettingsMenu.Name = "displaySettingsMenu";
            this.displaySettingsMenu.Size = new System.Drawing.Size(214, 22);
            this.displaySettingsMenu.Text = "表示設定";
            // 
            // alwaysOnTop
            // 
            this.alwaysOnTop.Name = "alwaysOnTop";
            this.alwaysOnTop.Size = new System.Drawing.Size(214, 22);
            this.alwaysOnTop.Text = "常に最前面表示";
            this.alwaysOnTop.Click += new System.EventHandler(this.alwaysOnTop_Click);
            // 
            // showRunTitleButton
            // 
            this.showRunTitleButton.Name = "showRunTitleButton";
            this.showRunTitleButton.Size = new System.Drawing.Size(214, 22);
            this.showRunTitleButton.Text = "タイトルを表示";
            this.showRunTitleButton.Click += new System.EventHandler(this.showRunTitleButton_Click);
            // 
            // showAttemptCount
            // 
            this.showAttemptCount.Name = "showAttemptCount";
            this.showAttemptCount.Size = new System.Drawing.Size(214, 22);
            this.showAttemptCount.Text = "試行回数を表示";
            this.showAttemptCount.Click += new System.EventHandler(this.showAttemptCount_Click);
            // 
            // showRunGoalMenuItem
            // 
            this.showRunGoalMenuItem.Name = "showRunGoalMenuItem";
            this.showRunGoalMenuItem.Size = new System.Drawing.Size(214, 22);
            this.showRunGoalMenuItem.Text = "ゴールを表示";
            this.showRunGoalMenuItem.Click += new System.EventHandler(this.showRunGoal_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
            // 
            // displayTimerOnlyButton
            // 
            this.displayTimerOnlyButton.Name = "displayTimerOnlyButton";
            this.displayTimerOnlyButton.Size = new System.Drawing.Size(214, 22);
            this.displayTimerOnlyButton.Text = "タイマーのみ";
            this.displayTimerOnlyButton.Click += new System.EventHandler(this.displayTimerOnlyButton_Click);
            // 
            // displayCompactButton
            // 
            this.displayCompactButton.Name = "displayCompactButton";
            this.displayCompactButton.Size = new System.Drawing.Size(214, 22);
            this.displayCompactButton.Text = "縮小";
            this.displayCompactButton.Click += new System.EventHandler(this.displayCompactButton_Click);
            // 
            // displayWideButton
            // 
            this.displayWideButton.Name = "displayWideButton";
            this.displayWideButton.Size = new System.Drawing.Size(214, 22);
            this.displayWideButton.Text = "横置き";
            this.displayWideButton.Click += new System.EventHandler(this.displayWideButton_Click);
            // 
            // displayDetailedButton
            // 
            this.displayDetailedButton.Name = "displayDetailedButton";
            this.displayDetailedButton.Size = new System.Drawing.Size(214, 22);
            this.displayDetailedButton.Text = "詳細";
            this.displayDetailedButton.Click += new System.EventHandler(this.displayDetailedButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(211, 6);
            // 
            // clockAppearanceToolStripMenuItem
            // 
            this.clockAppearanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDecimalSeparator,
            this.digitalClockButton,
            this.clockAccent});
            this.clockAppearanceToolStripMenuItem.Name = "clockAppearanceToolStripMenuItem";
            this.clockAppearanceToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.clockAppearanceToolStripMenuItem.Text = "時計のスタイル";
            // 
            // showDecimalSeparator
            // 
            this.showDecimalSeparator.Name = "showDecimalSeparator";
            this.showDecimalSeparator.Size = new System.Drawing.Size(221, 22);
            this.showDecimalSeparator.Text = "小数点を表示";
            this.showDecimalSeparator.Click += new System.EventHandler(this.showDecimalSeparator_Click);
            // 
            // digitalClockButton
            // 
            this.digitalClockButton.Name = "digitalClockButton";
            this.digitalClockButton.Size = new System.Drawing.Size(221, 22);
            this.digitalClockButton.Text = "デジタルフォント使用";
            this.digitalClockButton.Click += new System.EventHandler(this.digitalClockButton_Click);
            // 
            // clockAccent
            // 
            this.clockAccent.Name = "clockAccent";
            this.clockAccent.Size = new System.Drawing.Size(221, 22);
            this.clockAccent.Text = "タイマー窓の上下で色の強弱をつける";
            this.clockAccent.Click += new System.EventHandler(this.clockAccent_Click);
            // 
            // plainBg
            // 
            this.plainBg.Name = "plainBg";
            this.plainBg.Size = new System.Drawing.Size(214, 22);
            this.plainBg.Text = "単色背景";
            this.plainBg.Click += new System.EventHandler(this.plainBg_Click);
            // 
            // blackBg
            // 
            this.blackBg.Name = "blackBg";
            this.blackBg.Size = new System.Drawing.Size(214, 22);
            this.blackBg.Text = "黒背景";
            this.blackBg.Click += new System.EventHandler(this.blackBg_Click);
            // 
            // menuItemAdvancedDisplay
            // 
            this.menuItemAdvancedDisplay.Name = "menuItemAdvancedDisplay";
            this.menuItemAdvancedDisplay.Size = new System.Drawing.Size(214, 22);
            this.menuItemAdvancedDisplay.Text = "詳細設定";
            this.menuItemAdvancedDisplay.Click += new System.EventHandler(this.menuItemAdvancedDisplay_Click);
            // 
            // setColorsButton
            // 
            this.setColorsButton.Name = "setColorsButton";
            this.setColorsButton.Size = new System.Drawing.Size(214, 22);
            this.setColorsButton.Text = "色設定";
            this.setColorsButton.Click += new System.EventHandler(this.setColorsButton_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(211, 6);
            // 
            // advancedDetailButton
            // 
            this.advancedDetailButton.CheckOnClick = true;
            this.advancedDetailButton.Name = "advancedDetailButton";
            this.advancedDetailButton.Size = new System.Drawing.Size(214, 22);
            this.advancedDetailButton.Text = "別窓詳細タイマー";
            this.advancedDetailButton.Click += new System.EventHandler(this.advancedDetailButton_Click);
            // 
            // compareMenu
            // 
            this.compareMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compareOldButton,
            this.compareBestButton,
            this.compareFastestButton,
            this.compareSumBestButton});
            this.compareMenu.Name = "compareMenu";
            this.compareMenu.Size = new System.Drawing.Size(212, 22);
            this.compareMenu.Text = "比較対象";
            // 
            // compareOldButton
            // 
            this.compareOldButton.Name = "compareOldButton";
            this.compareOldButton.Size = new System.Drawing.Size(206, 22);
            this.compareOldButton.Text = "旧記録";
            this.compareOldButton.Click += new System.EventHandler(this.compareOldButton_Click);
            // 
            // compareBestButton
            // 
            this.compareBestButton.Name = "compareBestButton";
            this.compareBestButton.Size = new System.Drawing.Size(206, 22);
            this.compareBestButton.Text = "自己ベスト";
            this.compareBestButton.Click += new System.EventHandler(this.compareBestButton_Click);
            // 
            // compareFastestButton
            // 
            this.compareFastestButton.Name = "compareFastestButton";
            this.compareFastestButton.Size = new System.Drawing.Size(152, 22);
            this.compareFastestButton.Text = "最速";
            this.compareFastestButton.Click += new System.EventHandler(this.compareFastestButton_Click);
            // 
            // compareSumBestButton
            // 
            this.compareSumBestButton.Name = "compareSumBestButton";
            this.compareSumBestButton.Size = new System.Drawing.Size(152, 22);
            this.compareSumBestButton.Text = "区間最速合計";
            this.compareSumBestButton.Click += new System.EventHandler(this.compareSumBestButton_Click);
            // 
            // trackBestMenu
            // 
            this.trackBestMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bestAsOverallButton,
            this.bestAsSplitsButton});
            this.trackBestMenu.Name = "trackBestMenu";
            this.trackBestMenu.Size = new System.Drawing.Size(214, 22);
            this.trackBestMenu.Text = "自己ベスト保存方法";
            // 
            // bestAsOverallButton
            // 
            this.bestAsOverallButton.Name = "bestAsOverallButton";
            this.bestAsOverallButton.Size = new System.Drawing.Size(192, 22);
            this.bestAsOverallButton.Text = "通しでの最速";
            this.bestAsOverallButton.Click += new System.EventHandler(this.bestAsOverallButton_Click);
            // 
            // bestAsSplitsButton
            // 
            this.bestAsSplitsButton.Name = "bestAsSplitsButton";
            this.bestAsSplitsButton.Size = new System.Drawing.Size(192, 22);
            this.bestAsSplitsButton.Text = "区間毎";
            this.bestAsSplitsButton.Click += new System.EventHandler(this.bestAsSplitsButton_Click);
            // 
            // layoutMenu
            // 
            this.layoutMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prevsegButton,
            this.timesaveButton,
            this.sobButton,
            this.predpbButton,
            this.predbestButton});
            this.layoutMenu.Name = "layoutMenu";
            this.layoutMenu.Size = new System.Drawing.Size(212, 22);
            this.layoutMenu.Text = "レイアウト(詳細モード時のみ)";
            // 
            // prevsegButton
            // 
            this.prevsegButton.Name = "prevsegButton";
            this.prevsegButton.Size = new System.Drawing.Size(211, 22);
            this.prevsegButton.Text = "前区間";
            this.prevsegButton.Click += new System.EventHandler(this.prevsegButton_Click);
            // 
            // timesaveButton
            // 
            this.timesaveButton.Name = "timesaveButton";
            this.timesaveButton.Size = new System.Drawing.Size(211, 22);
            this.timesaveButton.Text = "更新可能時間";
            this.timesaveButton.Click += new System.EventHandler(this.timesaveButton_Click);
            // 
            // sobButton
            // 
            this.sobButton.Name = "sobButton";
            this.sobButton.Size = new System.Drawing.Size(211, 22);
            this.sobButton.Text = "最速区間タイム合計";
            this.sobButton.Click += new System.EventHandler(this.sobButton_Click);
            // 
            // predpbButton
            // 
            this.predpbButton.Name = "predpbButton";
            this.predpbButton.Size = new System.Drawing.Size(211, 22);
            this.predpbButton.Text = "予想完走時間 (自己ベスト)";
            this.predpbButton.Click += new System.EventHandler(this.predpbButton_Click);
            // 
            // predbestButton
            // 
            this.predbestButton.Name = "predbestButton";
            this.predbestButton.Size = new System.Drawing.Size(211, 22);
            this.predbestButton.Text = "予想完走時間(区間最速)";
            this.predbestButton.Click += new System.EventHandler(this.predbestButton_Click);
            // 
            // gradientMenu
            // 
            this.gradientMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horiButton,
            this.vertButton});
            this.gradientMenu.Name = "gradientMenu";
            this.gradientMenu.Size = new System.Drawing.Size(212, 22);
            this.gradientMenu.Text = "グラデーション";
            // 
            // horiButton
            // 
            this.horiButton.Name = "horiButton";
            this.horiButton.Size = new System.Drawing.Size(152, 22);
            this.horiButton.Text = "横方向";
            this.horiButton.Click += new System.EventHandler(this.horiButton_Click);
            // 
            // vertButton
            // 
            this.vertButton.Name = "vertButton";
            this.vertButton.Size = new System.Drawing.Size(152, 22);
            this.vertButton.Text = "縦方向";
            this.vertButton.Click += new System.EventHandler(this.vertButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(211, 6);
            // 
            // aboutButton
            // 
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(214, 22);
            this.aboutButton.Text = "タイマーについて";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(212, 22);
            this.exitButton.Text = "閉じる";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // stopwatch
            // 
            this.stopwatch.Interval = 15;
            this.stopwatch.Tick += new System.EventHandler(this.stopwatch_Tick);
            // 
            // WSplit
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(124, 26);
            this.ContextMenuStrip = this.timerMenu;
            this.ControlBox = false;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::WSplitTimer.Properties.Resources.AppIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(124, 26);
            this.Name = "WSplit";
            this.Text = "WSplit";
            this.timerMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void vertButton_Click(object sender, EventArgs e)
        {
            if (!this.vertButton.Checked)
            {
                this.vertButton.Checked = true;
                Settings.Profile.HGrad = false;
                this.horiButton.Checked = false;
                this.updateDetailed();
            };
        }

        void horiButton_Click(object sender, EventArgs e)
        {
            if (!this.horiButton.Checked)
            {
                this.horiButton.Checked = true;
                Settings.Profile.HGrad = true;
                this.vertButton.Checked = false;
                this.updateDetailed();
            }
        }

        private void Initialize()
        {
            if (Settings.Profile.FirstRun)
            {
                Settings.Profile.Upgrade();
                ColorSettings.Profile.Upgrade();
                Settings.Profile.FirstRun = false;
            }

            this.InitializeSettings();
            this.InitializeBackground();
            this.InitializeFonts();

            this.clockRect.Location = new Point(0, 0);
            this.clockRect.Size = base.Size;

            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int i = 1; (i < commandLineArgs.Length) && !this.split.LiveRun; i++)
            {
                this.split.RunFile = commandLineArgs[i];
                this.loadFile();
            }

            if ((this.split.RunFile == null) && Settings.Profile.LoadMostRecent)
            {
                this.split.RunFile = Settings.Profile.LastFile;
                this.loadFile();
            }

            if (Settings.Profile.SaveWindowPos)
            {
                bool flag = false;
                Rectangle rectangle = new Rectangle(Settings.Profile.WindowPosition, base.Size);
                foreach (Screen screen in Screen.AllScreens)
                {
                    if (rectangle.IntersectsWith(screen.WorkingArea))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    base.StartPosition = FormStartPosition.Manual;
                    base.Location = Settings.Profile.WindowPosition;
                }
            }

            this.modalWindowOpened = false;
        }

        private void InitializeSettings()
        {
            // Initialize every item to the correct setting value
            this.stopwatch.Interval = Settings.Profile.RefreshRate;

            base.TopMost = Settings.Profile.OnTop;
            this.alwaysOnTop.Checked = Settings.Profile.OnTop;

            this.showRunTitleButton.Checked = Settings.Profile.ShowTitle;
            this.digitalClockButton.Checked = Settings.Profile.DigitalClock;
            this.showAttemptCount.Checked = Settings.Profile.ShowAttempts;
            this.showRunGoalMenuItem.Checked = Settings.Profile.ShowGoal;

            this.prevsegButton.Checked = Settings.Profile.ShowPrevSeg;
            this.timesaveButton.Checked = Settings.Profile.ShowTimeSave;
            this.sobButton.Checked = Settings.Profile.ShowSoB;
            this.predpbButton.Checked = Settings.Profile.PredPB;
            this.predbestButton.Checked = Settings.Profile.PredBest;

            this.horiButton.Checked = Settings.Profile.HGrad;
            this.vertButton.Checked = !Settings.Profile.HGrad;

            if (Settings.Profile.BestAsOverall)
                this.bestAsOverallButton.Checked = true;
            else
                this.bestAsSplitsButton.Checked = true;

            if (Settings.Profile.CompareAgainst == 0)
                this.compareFastestButton.Checked = true;
            else if (Settings.Profile.CompareAgainst == 1)
                this.compareOldButton.Checked = true;
            else if (Settings.Profile.CompareAgainst == 2)
                this.compareBestButton.Checked = true;
            else
                this.compareSumBestButton.Checked = true;

            this.populateRecentFiles();

            this.timer.useFallback = Settings.Profile.FallbackPreference == 3;
            this.showDecimalSeparator.Checked = Settings.Profile.ShowDecimalSeparator;
            this.clockAccent.Checked = Settings.Profile.ClockAccent;
            base.Opacity = Math.Min(Math.Abs(Settings.Profile.Opacity), 1.0);

            this.setHotkeys();
            this.setDisplay((DisplayMode)Settings.Profile.DisplayMode);
        }

        private void InitializeBackground()
        {
            this.plainBg.Checked = Settings.Profile.BackgroundPlain;
            this.blackBg.Checked = Settings.Profile.BackgroundBlack;
            this.painter.PrepareBackground();
            this.Invalidate();
        }

        private void InitializeFonts()
        {
            // Initialize fonts according to settings

            // Loads clockFont from file:
            if (this.digitLarge == null || this.digitMed == null)
            {
                uint num;   // Necessary, as AddFontMemResourceEx needs a uint as a out parameter

                byte[] clockFont = Resources.ClockFont;
                IntPtr destination = Marshal.AllocCoTaskMem(clockFont.Length);
                AddFontMemResourceEx(clockFont, clockFont.Length, IntPtr.Zero, out num);
                Marshal.Copy(clockFont, 0, destination, clockFont.Length);
                privateFontCollection.AddMemoryFont(destination, clockFont.Length);
                Marshal.FreeCoTaskMem(destination);

                // Once the digital font is loaded in memory, we instanciate the Font objects:
                this.digitLarge = new Font(privateFontCollection.Families[0], 24f, GraphicsUnit.Pixel);
                this.digitMed = new Font(privateFontCollection.Families[0], 17.33333f, GraphicsUnit.Pixel);
            }

            FontFamily family = FontFamily.Families.FirstOrDefault(f => f.Name == Settings.Profile.FontFamilySegments);

            if (family == null || !family.IsStyleAvailable(FontStyle.Bold) || !family.IsStyleAvailable(FontStyle.Regular))
                family = FontFamily.GenericSansSerif;

            this.displayFont = new Font(family, 10.66667f * Settings.Profile.FontMultiplierSegments, GraphicsUnit.Pixel);
            this.timeFont = new Font(family, 12f * Settings.Profile.FontMultiplierSegments, FontStyle.Bold, GraphicsUnit.Pixel);
            this.clockLarge = new Font(family, 22.66667f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.clockMed = new Font(family, 18.66667f, FontStyle.Bold, GraphicsUnit.Pixel);

            this.dview.InitializeFonts();
        }

        public void InitializeDisplay()
        {
            if (this.startDelay != null)
            {
                this.startDelay.Dispose();
                this.startDelay = null;
            }
            this.split.Reset();

            this.newOldButton.Enabled = false;
            this.menuItemStartAt.Enabled = true;
            this.stopButton.Enabled = false;
            this.resetButton.Enabled = false;

            if (this.split.LastIndex < 0)
            {
                this.dview.Hide();
                this.split.RunFile = null;
                this.closeButton.Enabled = false;
                this.saveButton.Enabled = false;
                this.saveAsButton.Enabled = false;
                this.reloadButton.Enabled = false;
            }
            else
            {
                if (this.advancedDetailButton.Checked)
                {
                    this.dview.Show();
                }
                this.closeButton.Enabled = true;
                if (split.RunFile != null)
                {
                    this.saveButton.Enabled = true;
                    this.reloadButton.Enabled = true;
                }
                else
                {
                    this.saveButton.Enabled = false;
                    this.reloadButton.Enabled = false;
                }
                this.saveAsButton.Enabled = true;
            }
            this.stopwatch.Enabled = false;
            this.timer.Reset();
            this.populateDetailed();
            this.updateDisplay();
            this.setDisplay((DisplayMode)Settings.Profile.DisplayMode);
        }

        public static bool IsTextFile(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                byte[] buffer = new byte[0x400];
                char[] chArray = new char[0x400];
                bool flag = true;
                int num = stream.Read(buffer, 0, buffer.Length);
                stream.Seek(0L, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream))
                {
                    reader.Read(chArray, 0, chArray.Length);
                }
                using (MemoryStream stream2 = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(stream2))
                    {
                        writer.Write(chArray);
                        writer.Flush();
                        byte[] buffer2 = stream2.GetBuffer();
                        for (int i = 0; (i < num) && flag; i++)
                        {
                            flag = buffer[i] == buffer2[i];
                        }
                    }
                }
                return flag;
            }
        }

        private KeyModifiers keyMods(Keys key)
        {
            KeyModifiers none = KeyModifiers.None;
            if ((key & Keys.Alt) == Keys.Alt)
            {
                none |= KeyModifiers.Alt;
            }
            if ((key & Keys.Shift) == Keys.Shift)
            {
                none |= KeyModifiers.Shift;
            }
            if ((key & Keys.Control) == Keys.Control)
            {
                none |= KeyModifiers.Control;
            }
            return none;
        }

        private void loadFile()
        {
            if ((File.Exists(this.split.RunFile) && (new FileInfo(this.split.RunFile).Length < (10.0 * Math.Pow(1024.0, 2.0)))) && IsTextFile(this.split.RunFile))
            {
                this.split.Clear();

                this.split.RunGoal = "";
                this.split.StartDelay = 0;
                this.split.RunTitle = "";
                this.split.AttemptsCount = 0;

                this.detailPreferredSize = this.clockMinimumSize;
                using (StreamReader reader = new StreamReader(this.split.RunFile))
                {
                    string str;
                    List<string> list = new List<string>();
                    while ((str = reader.ReadLine()) != null)
                    {
                        if (str.StartsWith("Title="))
                        {
                            this.split.RunTitle = str.Substring(6);
                        }
                        else
                        {
                            if (str.StartsWith("Goal="))
                            {
                                this.split.RunGoal = str.Substring(5);
                            }
                            if (str.StartsWith("Attempts="))
                            {
                                int attemptsCount = 0;
                                int.TryParse(str.Substring(9), out attemptsCount);
                                this.split.AttemptsCount = attemptsCount;
                                continue;
                            }
                            if (str.StartsWith("Offset="))
                            {
                                int offsetStart = 0;
                                int.TryParse(str.Substring(7), out offsetStart);
                                this.split.StartDelay = offsetStart;
                                continue;
                            }
                            if (str.StartsWith("Width="))
                            {
                                int result = 0;
                                int.TryParse(str.Substring(6), out result);
                                this.detailPreferredSize = this.clockMinimumSize;
                                this.detailPreferredSize.Width = Math.Max(this.clockMinimumSize.Width, result);
                                continue;
                            }
                            if (str.StartsWith("Size=") && (str.Split(new char[] { ',' }).Length == 2))
                            {
                                int num2 = 0;
                                int num3 = 0;
                                int.TryParse(str.Split(new char[] { ',' })[0].Substring(5), out num2);
                                int.TryParse(str.Split(new char[] { ',' })[1], out num3);
                                int width = Math.Max(this.clockMinimumSize.Width, num2);
                                this.detailPreferredSize = new Size(width, Math.Max(this.clockMinimumSize.Height, num3));
                                continue;
                            }
                            if (str.StartsWith("Icons="))
                            {
                                foreach (string str2 in Regex.Split(str.Substring(6), "\","))
                                {
                                    list.Add(str2.Replace("\"", ""));
                                }
                                continue;
                            }
                            if (str.Split(new char[] { ',' }).Length == 4)
                            {
                                string[] strArray2 = str.Split(new char[] { ',' });
                                string name = strArray2[0];
                                double num4 = 0.0;
                                double num5 = 0.0;
                                double num6 = 0.0;
                                double.TryParse(strArray2[1], NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture(""), out num4);
                                double.TryParse(strArray2[2], NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture(""), out num5);
                                double.TryParse(strArray2[3], NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture(""), out num6);
                                this.split.Add(new Segment(name, num4, num5, num6));
                            }
                        }
                    }
                    for (int i = 0; i < this.split.Count; i++)
                    {
                        if (i < list.Count)
                        {
                            this.split.segments[i].IconPath = list[i];
                            try
                            {
                                this.split.segments[i].Icon = Image.FromFile(list[i]);
                            }
                            catch
                            {
                                this.split.segments[i].Icon = Resources.MissingIcon;
                            }
                        }
                        else
                        {
                            this.split.segments[i].Icon = Resources.MissingIcon;
                            this.split.segments[i].IconPath = "";
                        }
                    }
                    this.currentDispMode = DisplayMode.Null;
                    this.InitializeDisplay();
                    if (this.split.RunFile != null)
                    {
                        if (Settings.Profile.RecentFiles != null)
                        {
                            if (Settings.Profile.RecentFiles.Contains(this.split.RunFile))
                            {
                                Settings.Profile.RecentFiles.Remove(this.split.RunFile);
                            }
                            else if (Settings.Profile.RecentFiles.Count > 9)
                            {
                                Settings.Profile.RecentFiles.RemoveAt(Settings.Profile.RecentFiles.Count - 1);
                            }
                            Settings.Profile.RecentFiles.Insert(0, this.split.RunFile);
                        }
                        this.populateRecentFiles();
                    }

                    this.split.UnsavedSplit = false;
                    return;
                }
            }
            this.closeFile();
        }

        public static int MeasureDisplayStringWidth(string text, Font font)
        {
            if (text.Length < 1)
                return 0;

            Bitmap image = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(image);
            StringFormat stringFormat = new StringFormat();
            RectangleF layoutRect = new RectangleF(0f, 0f, 1000f, 1000f);
            CharacterRange[] ranges = new CharacterRange[] { new CharacterRange(0, text.Length) };
            Region[] regionArray = new Region[1];
            stringFormat.SetMeasurableCharacterRanges(ranges);
            layoutRect = g.MeasureCharacterRanges(text, font, layoutRect, stringFormat)[0].GetBounds(g);
            g.Dispose();
            image.Dispose();
            return (int)(layoutRect.Right + 1f);
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            this.split.UpdateBest(Settings.Profile.BestAsOverall);
            RunEditorDialog editor = new RunEditorDialog(this.split)
            {
                titleBox = { Text = this.split.RunTitle },
                attemptsBox = { Text = this.split.AttemptsCount.ToString() },
                txtGoal = { Text = this.split.RunGoal }
            };
            base.TopMost = false;
            this.dview.TopMost = false;
            this.modalWindowOpened = true;
            if (editor.ShowDialog() == DialogResult.OK)
            {
                this.split.Clear();
                foreach (Segment segment in editor.editList)
                {
                    this.split.Add(segment);
                }
                int attemptsCount = 0;
                this.split.RunTitle = editor.titleBox.Text;
                this.split.RunGoal = editor.txtGoal.Text;
                int.TryParse(editor.attemptsBox.Text, out attemptsCount);
                this.split.AttemptsCount = attemptsCount;
                this.InitializeDisplay();
                this.split.UnsavedSplit = true;
                this.split.StartDelay = editor.startDelay;
            }
            else
            {
                this.split.RestoreBest();
            }
            base.TopMost = Settings.Profile.OnTop;
            this.dview.TopMost = Settings.Profile.DViewOnTop;
            this.modalWindowOpened = false;
        }

        private void newOldButton_Click(object sender, EventArgs e)
        {
            this.split.LiveToOld();
        }

        public void nextStage()
        {
            this.split.Next();
            this.updateDisplay();
        }

        private void menuItemAdvancedDisplay_Click(object sender, EventArgs e)
        {
            // Used to be opacity button
            /*ChangeOpacity opacity = new ChangeOpacity(this);
            base.TopMost = false;
            this.dview.TopMost = false;
            this.modalWindowOpened = true;
            if (opacity.ShowDialog() == DialogResult.OK)
            {
                Settings.Profile.Opacity = base.Opacity;
            }
            else
            {
                base.Opacity = Math.Min(Math.Abs(Settings.Profile.Opacity), 1.0);
            }
            base.TopMost = Settings.Profile.OnTop;
            this.dview.TopMost = Settings.Profile.DViewOnTop;
            this.modalWindowOpened = false;*/

            this.configure(3);
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (this.promptForSave())
            {
                this.modalWindowOpened = true;
                if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.split.RunFile = this.openFileDialog.FileName;
                    this.loadFile();
                }
                this.modalWindowOpened = false;
            }
        }

        public void pauseResume()
        {
            // If the refreshing stopwatch is running
            if (this.stopwatch.Enabled)
            {
                this.stopwatch.Enabled = false;
                if (this.startDelay != null)
                {
                    /*this.startDelay.Dispose();
                    this.startDelay = null;

                    this.menuItemStartAt.Enabled = true;
                    this.resetButton.Enabled = false;
                    this.stopButton.Enabled = false;*/

                    InitializeDisplay();
                }
                this.timer.Stop();
                base.Invalidate();
            }

            // If the split aren't done, so if the timer isn't running
            else if (!this.split.Done && (this.startDelay == null))
            {
                // If the timer had not been started yet
                if (this.timer.ElapsedTicks == 0L)
                {
                    // If it wasn't running
                    if (!this.timer.IsRunning)
                        this.InitializeDisplay();

                    this.startTimer();

                    this.menuItemStartAt.Enabled = false;
                    this.resetButton.Enabled = true;
                    this.stopButton.Enabled = true;
                }

                // If the timer was paused
                else
                    this.timer.Start();

                this.stopwatch.Enabled = true;
            }
        }

        private void plainBg_Click(object sender, EventArgs e)
        {
            this.blackBg.Checked = false;
            Settings.Profile.BackgroundBlack = false;
            Settings.Profile.BackgroundPlain = !Settings.Profile.BackgroundPlain;
            this.plainBg.Checked = Settings.Profile.BackgroundPlain;
            this.painter.RequestBackgroundRedraw();
            base.Invalidate();
        }

        private void prevsegButton_Click(object sender, EventArgs e)
        {
            this.prevsegButton.Checked = !Settings.Profile.ShowPrevSeg;
            Settings.Profile.ShowPrevSeg = !Settings.Profile.ShowPrevSeg;
            if (this.currentDispMode == DisplayMode.Detailed) { this.displayDetail(); };
        }

        private void timesaveButton_Click(object sender, EventArgs e)
        {
            this.timesaveButton.Checked = !Settings.Profile.ShowTimeSave;
            Settings.Profile.ShowTimeSave = !Settings.Profile.ShowTimeSave;
            if (this.currentDispMode == DisplayMode.Detailed) { this.displayDetail(); };
        }

        private void sobButton_Click(object sender, EventArgs e)
        {
            this.sobButton.Checked = !Settings.Profile.ShowSoB;
            Settings.Profile.ShowSoB = !Settings.Profile.ShowSoB;
            if (this.currentDispMode == DisplayMode.Detailed) { this.displayDetail(); };
        }

        private void predpbButton_Click(object sender, EventArgs e)
        {
            this.predpbButton.Checked = !Settings.Profile.PredPB;
            Settings.Profile.PredPB = !Settings.Profile.PredPB;
            if (this.currentDispMode == DisplayMode.Detailed) { this.displayDetail(); };
        }

        private void predbestButton_Click(object sender, EventArgs e)
        {
            this.predbestButton.Checked = !Settings.Profile.PredBest;
            Settings.Profile.PredBest = !Settings.Profile.PredBest;
            if (this.currentDispMode == DisplayMode.Detailed) { this.displayDetail(); };
        }

        public void populateDetailed()
        {
            this.dview.segs.Rows.Clear();
            this.dview.segs.Rows.Add(new object[] { "Segment", "Old", "Best", "SoB", "Live", "+/-" });
            this.dview.segs.Rows[0].Frozen = true;
            this.dview.finalSeg.Rows.Clear();
            this.dview.finalSeg.Rows.Add();

            foreach (Segment segment in this.split.segments)
            {
                this.dview.segs.Rows.Add(new object[] { segment.Name });
                if (this.dview.finalSeg.RowCount > 1)
                    this.dview.finalSeg.Rows.RemoveAt(1);

                this.dview.finalSeg.Rows.Add(new object[] { segment.Name });
            }

            if (this.dview.segs.RowCount >= 2)
                this.dview.segs.Rows.RemoveAt(this.dview.segs.RowCount - 1);

            this.dview.finalSeg.Rows[0].Height = 0;
            this.dview.finalSeg.Height = (this.dview.finalSeg.RowCount - 1) * this.dview.finalSeg.RowTemplate.Height;
        }

        private void populateRecentFiles()
        {
            this.openRecent.DropDownItems.Clear();
            if ((Settings.Profile.RecentFiles != null) && (Settings.Profile.RecentFiles.Count > 0))
            {
                foreach (string str in Settings.Profile.RecentFiles)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem
                    {
                        Name = str,
                        Text = Path.GetFileName(str),
                        ToolTipText = str
                    };
                    item.Click += new EventHandler(this.recent_Click);
                    this.openRecent.DropDownItems.Add(item);
                }
                this.openRecent.DropDownItems.Add(new ToolStripSeparator());
                ToolStripMenuItem item2 = new ToolStripMenuItem
                {
                    Text = "履歴を消去"
                };
                item2.Click += new EventHandler(this.clear_Click);
                this.openRecent.DropDownItems.Add(item2);
            }
            else
            {
                ToolStripMenuItem item3 = new ToolStripMenuItem
                {
                    Text = "履歴なし",
                    Enabled = false
                };
                this.openRecent.DropDownItems.Add(item3);
            }
        }

        public void prevStage()
        {
            if (this.split.LiveIndex >= 1)
            {
                if (this.split.LiveIndex > this.split.LastIndex)
                {
                    this.stopwatch.Enabled = true;
                }
                this.split.Previous();
                this.updateDisplay();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return this.timerHotkey(keyData);
        }

        private void recent_Click(object sender, EventArgs e)
        {
            if (this.promptForSave())
            {
                this.split.RunFile = ((ToolStripMenuItem)sender).Name;
                this.loadFile();
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.loadFile();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.ResetSplits();
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            this.saveAs();
        }

        private void saveAs()
        {
            this.modalWindowOpened = true;
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.split.RunFile = this.saveFileDialog.FileName;
                this.saveFile();
            }
            this.modalWindowOpened = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.saveFile();
        }

        private void saveFile()
        {
            if (this.split.RunFile == null)
            {
                this.saveButton.Enabled = false;
            }
            else
            {
                this.split.UpdateBest(Settings.Profile.BestAsOverall);
                new FileInfo(this.split.RunFile);
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(this.split.RunFile);
                    writer.WriteLine("Title=" + this.split.RunTitle);
                    writer.WriteLine("Goal=" + this.split.RunGoal);
                    writer.WriteLine("Attempts=" + this.split.AttemptsCount);
                    writer.WriteLine("Offset=" + this.split.StartDelay);
                    writer.WriteLine(string.Concat(new object[] { "Size=", this.detailPreferredSize.Width, ",", this.detailPreferredSize.Height }));
                    List<string> list = new List<string>();
                    foreach (Segment segment in this.split.segments)
                    {
                        if (segment.Name != null)
                        {
                            list.Add("\"" + segment.IconPath + "\"");
                            string[] strArray = new string[] { segment.Name, segment.OldTime.ToString(CultureInfo.GetCultureInfo("").NumberFormat), segment.BestTime.ToString(CultureInfo.GetCultureInfo("").NumberFormat), segment.BestSegment.ToString(CultureInfo.GetCultureInfo("").NumberFormat) };
                            string str = string.Join(",", strArray);
                            writer.WriteLine(str);
                        }
                    }
                    writer.WriteLine("Icons=" + string.Join(",", list.ToArray()));
                    writer.Close();
                    this.split.UnsavedSplit = false;
                }
                catch (Exception exception)
                {
                    MessageBoxEx.Show(this, exception.Message, "Save error");
                }
                finally
                {
                    if (writer != null)
                    {
                        if (Settings.Profile.RecentFiles != null)
                        {
                            if (Settings.Profile.RecentFiles.Contains(this.split.RunFile))
                            {
                                Settings.Profile.RecentFiles.Remove(this.split.RunFile);
                            }
                            else if (Settings.Profile.RecentFiles.Count > 9)
                            {
                                Settings.Profile.RecentFiles.RemoveAt(Settings.Profile.RecentFiles.Count - 1);
                            }
                            Settings.Profile.RecentFiles.Insert(0, this.split.RunFile);
                        }
                        this.populateRecentFiles();
                    }
                }
            }
        }

        private void setColorsButton_Click(object sender, EventArgs e)
        {
            CustomizeColors colors = new CustomizeColors();
            base.TopMost = false;
            this.dview.TopMost = false;
            this.modalWindowOpened = true;
            if (colors.ShowDialog(this) == DialogResult.OK)
                this.updateDetailed();

            base.TopMost = Settings.Profile.OnTop;
            this.dview.TopMost = Settings.Profile.DViewOnTop;
            this.modalWindowOpened = false;
        }

        private void setDisplay(DisplayMode mode)
        {
            Settings.Profile.DisplayMode = (int)mode;
            this.clockResize = false;
            this.MinimumSize = new Size(0, 0);
            this.displayTimerOnlyButton.Checked = false;
            this.displayCompactButton.Checked = false;
            this.displayWideButton.Checked = false;
            this.displayDetailedButton.Checked = false;
            if (mode == DisplayMode.Compact)
            {
                this.displayCompactButton.Checked = true;
            }
            else if (mode == DisplayMode.Wide)
            {
                this.displayWideButton.Checked = true;
            }
            else if (mode == DisplayMode.Detailed)
            {
                this.displayDetailedButton.Checked = true;
            }
            else
            {
                this.displayTimerOnlyButton.Checked = true;
            }
            if (this.split.LiveRun)
            {
                if (mode == DisplayMode.Compact)
                {
                    this.displayCompact();
                }
                else if (mode == DisplayMode.Wide)
                {
                    this.displayWide();
                }
                else if (mode == DisplayMode.Detailed)
                {
                    this.displayDetail();
                }
                else
                {
                    this.displayTimer();
                }
            }
            else
            {
                this.displayTimer();
            }

            this.painter.RequestBackgroundRedraw();
            base.Invalidate();
        }

        private void setHotkeys()
        {
            this.clearHotkeys();
            if (Settings.Profile.EnabledHotkeys)
            {
                RegisterHotKey(base.Handle, 0x9d82, this.keyMods(Settings.Profile.SplitKey), this.stripkeyMods(Settings.Profile.SplitKey));
                RegisterHotKey(base.Handle, 0x9d83, this.keyMods(Settings.Profile.PauseKey), this.stripkeyMods(Settings.Profile.PauseKey));
                RegisterHotKey(base.Handle, 0x9d84, this.keyMods(Settings.Profile.StopKey), this.stripkeyMods(Settings.Profile.StopKey));
                RegisterHotKey(base.Handle, 0x9d85, this.keyMods(Settings.Profile.ResetKey), this.stripkeyMods(Settings.Profile.ResetKey));
                RegisterHotKey(base.Handle, 0x9d86, this.keyMods(Settings.Profile.PrevKey), this.stripkeyMods(Settings.Profile.PrevKey));
                RegisterHotKey(base.Handle, 0x9d87, this.keyMods(Settings.Profile.NextKey), this.stripkeyMods(Settings.Profile.NextKey));
                RegisterHotKey(base.Handle, 0x9d88, this.keyMods(Settings.Profile.CompTypeKey), this.stripkeyMods(Settings.Profile.CompTypeKey));
            }
            RegisterHotKey(base.Handle, 0x9d89, this.keyMods(Settings.Profile.HotkeyToggleKey), this.stripkeyMods(Settings.Profile.HotkeyToggleKey));
        }

        private void showAttemptCount_Click(object sender, EventArgs e)
        {
            Settings.Profile.ShowAttempts = !Settings.Profile.ShowAttempts;
            this.showAttemptCount.Checked = Settings.Profile.ShowAttempts;
            this.painter.RequestBackgroundRedraw();
            base.Invalidate();
        }

        private void showRunGoal_Click(object sender, EventArgs e)
        {
            Settings.Profile.ShowGoal = !Settings.Profile.ShowGoal;
            this.showRunGoalMenuItem.Checked = Settings.Profile.ShowGoal;
            this.setDisplay((DisplayMode)Settings.Profile.DisplayMode);
        }

        private void showDecimalSeparator_Click(object sender, EventArgs e)
        {
            Settings.Profile.ShowDecimalSeparator = !Settings.Profile.ShowDecimalSeparator;
            this.showDecimalSeparator.Checked = Settings.Profile.ShowDecimalSeparator;
            this.painter.RequestBackgroundRedraw();
            base.Invalidate();
        }

        private void showRunTitleButton_Click(object sender, EventArgs e)
        {
            Settings.Profile.ShowTitle = !Settings.Profile.ShowTitle;
            this.showRunTitleButton.Checked = Settings.Profile.ShowTitle;
            this.setDisplay((DisplayMode)Settings.Profile.DisplayMode);
        }

        private void showSegsDec()
        {
            if (Settings.Profile.DisplaySegs > 2)
            {
                Settings.Profile.DisplaySegs--;
            }
            this.setDisplay(this.currentDispMode);
            this.updateDetailed();
        }

        private void showSegsInc()
        {
            if (Settings.Profile.DisplaySegs < 40)
            {
                Settings.Profile.DisplaySegs++;
            }
            this.setDisplay(this.currentDispMode);
            this.updateDetailed();
        }

        private void startDelay_Tick(object sender, EventArgs e, long startingTicks = 0)
        {
            this.startDelay.Dispose();
            this.startDelay = null;
            this.split.AttemptsCount++;
            this.timer.StartAt(new TimeSpan(startingTicks));
        }

        private void startTimer(long startingTicks = 0, bool useDelay = true)
        {
            if (this.split.Count > 0)
            {
                Settings.Profile.Width = this.split.segments[this.split.LastIndex].TimeWidth;
                Settings.Profile.last = this.split.segments[this.split.LastIndex].TimeString;
            };
            if (this.startDelay == null)
            {
                if (useDelay && this.split.StartDelay > 0)
                {
                    this.offsetStartTime = DateTime.UtcNow;
                    this.startDelay = new Timer();
                    this.startDelay.Interval = this.split.StartDelay;
                    this.startDelay.Tick += (sender, e) => startDelay_Tick(sender, e, startingTicks);
                    this.startDelay.Enabled = true;
                    base.Invalidate();
                }
                else
                {
                    if (this.split.LiveRun)
                    {
                        this.split.AttemptsCount++;
                    }
                    this.timer.StartAt(new TimeSpan(startingTicks));
                    this.painter.RequestBackgroundRedraw();
                    base.Invalidate();
                }
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopSplits();
        }

        private void stopwatch_Tick(object sender, EventArgs e)
        {
            if (this.flashDelay == null)
            {
                base.Invalidate();
            }
            if (Settings.Profile.RainbowSplits)
            {
                ColorSettings.Profile.SegRainbow = Color.FromArgb(255, r, g, b);
                //colour stuff
                if (rr) { b -= 4; g += 4; };
                if (gg) { r -= 4; b += 4; };
                if (bb) { g -= 4; r += 4; };
                if ((rr) && (b == 127)) { gg = true; rr = false; };
                if ((gg) && (r == 127)) { bb = true; gg = false; };
                if ((bb) && (g == 127)) { rr = true; bb = false; };
                // I don't like this at all
                this.updateDetailed();
            };
        }

        private uint stripkeyMods(Keys key)
        {
            return (uint)(key & Keys.KeyCode);
        }

        private string timeFormatter(double secs, TimeFormat format)
        {
            TimeSpan span = TimeSpan.FromSeconds(Math.Abs(Math.Truncate(secs * 10) / 10));

            string str = "";
            if (((format == TimeFormat.Delta) || (format == TimeFormat.DeltaShort)) && (secs >= 0.0))
                str = str + "+";
            else if (secs < 0.0)
                str = str + "-";

            if (format == TimeFormat.Seconds)
                return (Math.Truncate(secs * 100) / 100).ToString();

            if (format == TimeFormat.Long)
            {
                if (span.TotalHours >= 1.0)
                    return (str + string.Format("{0}:{1:00}:{2:00.0}", Math.Floor(span.TotalHours), span.Minutes, span.Seconds + (((double)span.Milliseconds) / 1000.0)));
                return (str + string.Format("{0}:{1:00.0}", span.Minutes, span.Seconds + (((double)span.Milliseconds) / 1000.0)));
            }

            if ((span.TotalMinutes >= 1.0) || (format == TimeFormat.Short))
                span = TimeSpan.FromSeconds(Math.Abs(Math.Truncate(secs)));

            if ((span.TotalMinutes >= 100.0) && (format == TimeFormat.DeltaShort))
                span = new TimeSpan(0, 0x63, 0x3b);

            if (span.TotalHours >= 100.0)
                span = TimeSpan.FromMinutes(Math.Truncate(span.TotalMinutes));

            if ((span.TotalHours >= 100.0) && (format != TimeFormat.DeltaShort))
                return (str + string.Format("{0}h{1:00}", Math.Floor(span.TotalHours), span.Minutes));

            if ((span.TotalHours >= 1.0) && (format != TimeFormat.DeltaShort))
                return (str + string.Format("{0}:{1:00}:{2:00}", Math.Floor(span.TotalHours), span.Minutes, span.Seconds));

            if ((span.TotalMinutes >= 1.0) || (format == TimeFormat.Short))
                return (str + string.Format("{0}:{1:00}", Math.Floor(span.TotalMinutes), span.Seconds));

            return (str + string.Format("{0:0.0}", span.TotalSeconds));
        }

        private double timeParse(string timeString)
        {
            double num = 0.0;
            foreach (string str in timeString.Split(new char[] { ':' }))
            {
                double num2;
                if (double.TryParse(str, out num2))
                {
                    num = (num * 60.0) + num2;
                }
            }
            return num;
        }

        public void timerControl()
        {
            if (this.doubleTapDelay == null)
            {
                if (Settings.Profile.DoubleTapGuard > 0)
                {
                    this.doubleTapDelay = new Timer();
                    this.doubleTapDelay.Tick += new EventHandler(this.doubleTapDelay_Tick);
                    this.doubleTapDelay.Interval = Math.Min(0x1388, Settings.Profile.DoubleTapGuard);
                    this.doubleTapDelay.Enabled = true;
                }
                if (this.split.LiveRun)
                {
                    if (this.stopwatch.Enabled && (this.startDelay == null))
                    {
                        this.doSplit();
                    }
                    else if (this.split.Done)
                    {
                        this.StopSplits();
                    }
                    else
                    {
                        this.pauseResume();
                    }
                }
                else
                {
                    this.pauseResume();
                }
            }
        }

        // Checks for local hotkeys
        public bool timerHotkey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Space:
                case Keys.Enter:    // Split
                    this.timerControl();
                    return true;

                case Keys.Left:     // Previous
                    this.prevStage();
                    return true;

                case Keys.Up:       // Expand splits
                    this.showSegsInc();
                    return true;

                case Keys.Right:    // Next
                    this.nextStage();
                    return true;

                case Keys.Down:     // Unexpand splits
                    this.showSegsDec();
                    return true;

                case Keys.D0:       // Switch to timer display
                    this.setDisplay(DisplayMode.Timer);
                    return true;

                case Keys.D1:       // Switch to compact display
                    this.setDisplay(DisplayMode.Compact);
                    return true;

                case Keys.D2:       // Switch to wide display
                    this.setDisplay(DisplayMode.Wide);
                    return true;

                case Keys.D3:       // Switch to detailed display
                    this.setDisplay(DisplayMode.Detailed);
                    return true;

                // Got removed for being more annoying than useful
                /*case Keys.C:        // Configure
                    this.configure(0);
                    return true;*/

                case Keys.P:        // Pause
                    this.pauseResume();
                    return true;

                case Keys.R:        // Reset
                    this.ResetSplits();
                    return true;

                case Keys.S:        // Stop
                    this.StopSplits();
                    return true;

                case Keys.Tab:      // Switch comparison type
                    this.SwitchComparisonType();
                    return true;
            }
            return false;
        }

        private void unflashClock(object sender, EventArgs e)
        {
            this.flashDelay.Dispose();
            this.flashDelay = null;
        }

        // Detailed Update (Updates some stuff, not necessarely in the detailed window or detailed view.
        public void updateDetailed()
        {
            ColorSettings colors = ColorSettings.Profile;
            // For every split, checks conditions set the time string, the color and the width of the split time
            for (int i = 0; i <= this.split.LastIndex; i++)
            {
                if ((i < this.split.LiveIndex) && (this.timer.ElapsedTicks > 0L))
                {
                    double num2 = this.split.SegDelta(this.split.segments[i].LiveTime, i);
                    double num3 = this.split.RunDelta(this.split.segments[i].LiveTime, i);

                    // If there is a Delta to write...
                    if ((this.split.segments[i].LiveTime > 0.0) && (this.split.CompTime(i) > 0.0))
                    {
                        this.split.segments[i].TimeString = this.timeFormatter(this.split.RunDeltaAt(i), TimeFormat.Delta);
                        if (this.split.LiveSegment(i) != 0.0 && (this.split.segments[i].BestSegment == 0.0 || this.split.LiveSegment(i) < this.split.segments[i].BestSegment))
                        {
                            if (Settings.Profile.RainbowSplits)
                            {
                                this.split.segments[i].TimeColor = colors.SegRainbow;
                            }
                            else
                            {
                                this.split.segments[i].TimeColor = colors.SegBestSegment;
                            };
                        }
                        else if (num3 < 0.0)
                        {
                            if (num2 < 0.0)
                            {
                                this.split.segments[i].TimeColor = colors.SegAheadGain;
                            }
                            else
                            {
                                this.split.segments[i].TimeColor = colors.SegAheadLoss;
                            }
                        }
                        else if (num2 > 0.0)
                        {
                            this.split.segments[i].TimeColor = colors.SegBehindLoss;
                        }
                        else
                        {
                            this.split.segments[i].TimeColor = colors.SegBehindGain;
                        }
                    }
                    // If the split was missed...
                    else if (this.split.segments[i].LiveTime == 0.0)
                    {
                        this.split.segments[i].TimeString = "-";
                        this.split.segments[i].TimeColor = colors.SegMissingTime;
                    }
                    // If there was no live time to compare splits to...
                    else if (this.split.CompTime(i) == 0.0)
                    {
                        this.split.segments[i].TimeString = this.timeFormatter(this.split.segments[i].LiveTime, TimeFormat.Short);
                        if (/*(i == 0 || this.split.segments[i - 1].LiveTime > 0.0) &&
                            (this.split.segments[i].BestSegment == 0.0) || this.split.LiveSegment(i) < this.split.segments[i].BestSegment*/
                            this.split.LiveSegment(i) != 0.0 && (this.split.segments[i].BestSegment == 0.0 || this.split.LiveSegment(i) < this.split.segments[i].BestSegment))
                        {
                            if (Settings.Profile.RainbowSplits)
                            {
                                this.split.segments[i].TimeColor = colors.SegRainbow;
                            }
                            else
                            {
                                this.split.segments[i].TimeColor = colors.SegBestSegment;
                            };
                        }
                        else
                        {
                            this.split.segments[i].TimeColor = colors.SegNewTime;
                        }
                    }
                }
                else if (i == this.split.LiveIndex)
                {
                    this.split.segments[i].TimeColor = colors.LiveSeg;
                    if (this.split.CompTime(i) > 0.0)
                    {
                        this.split.segments[i].TimeString = this.timeFormatter(this.split.CompTime(i), TimeFormat.Short); //changed
                    }
                    else
                    {
                        this.split.segments[i].TimeString = "-";
                    }
                }
                else
                {
                    this.split.segments[i].TimeColor = colors.FutureSegTime;
                    if (this.split.CompTime(i) > 0.0)
                    {
                        this.split.segments[i].TimeString = this.timeFormatter(this.split.CompTime(i), TimeFormat.Short);
                    }
                    else
                    {
                        this.split.segments[i].TimeString = "-";
                    }
                }
                this.split.segments[i].TimeWidth = MeasureDisplayStringWidth(this.split.segments[i].TimeString, this.timeFont);
            }

            this.painter.RequestBackgroundRedraw();
            base.Invalidate();

            // Updates the detailed view window columns things...
            // 
            if ((this.dview.segs.RowCount > 0) && (this.dview.finalSeg.RowCount > 1))
            {
                this.dview.Deltas.Clear();
                for (int j = 0; j < this.dview.segs.RowCount; ++j)
                {
                    DataGridViewRow row;
                    if (j < (this.dview.segs.RowCount - 1))
                        row = this.dview.segs.Rows[j + 1];
                    else
                        row = this.dview.finalSeg.Rows[1];

                    DataGridViewCell oldTimeCell = row.Cells[1];
                    DataGridViewCell bestTimeCell = row.Cells[2];
                    DataGridViewCell sumOfBestsTimeCell = row.Cells[3];
                    DataGridViewCell liveTimeCell = row.Cells[4];
                    DataGridViewCell deltaCell = row.Cells[5];

                    double oldTime = this.split.segments[j].OldTime;
                    double bestTime = this.split.segments[j].BestTime;
                    double sumOfBestsTime = this.split.SumOfBests(j);
                    double liveTime = this.split.segments[j].LiveTime;
                    double oldDelta = this.split.LastDelta(j);

                    double secs = this.split.RunDeltaAt(j);

                    // Puts oldTime in the "Old" column
                    if (oldTime > 0.0)
                        oldTimeCell.Value = this.timeFormatter(oldTime, TimeFormat.Short);
                    else if (this.split.LastSegment.OldTime > 0.0)
                        oldTimeCell.Value = "-";
                    else
                        oldTimeCell.Value = null;

                    // Puts bestTime in "Best" column
                    if (bestTime > 0.0)
                    {
                        bestTimeCell.Value = this.timeFormatter(bestTime, TimeFormat.Short);
                        if ((this.split.segments[j].BestSegment > 0.0) && Settings.Profile.DViewShowSegs)
                        {
                            object obj2 = bestTimeCell.Value;
                            bestTimeCell.Value = string.Concat(new object[] { obj2, " [", this.timeFormatter(this.split.segments[j].BestSegment, TimeFormat.Short), "]" });
                        }
                    }
                    else if (this.split.LastSegment.BestTime > 0.0)
                        bestTimeCell.Value = "-";
                    else
                        bestTimeCell.Value = null;

                    // Puts sumOfBestsTime in "Sum of Bests" column
                    if (sumOfBestsTime > 0.0)
                        sumOfBestsTimeCell.Value = this.timeFormatter(sumOfBestsTime, TimeFormat.Short);
                    else if (this.split.SumOfBests(this.split.LastIndex) > 0.0)
                        sumOfBestsTimeCell.Value = "-";
                    else
                        sumOfBestsTimeCell.Value = null;

                    // Puts liveTime in "Live" column
                    if (liveTime > 0.0)
                    {
                        liveTimeCell.Value = this.timeFormatter(liveTime, TimeFormat.Short);
                        if ((this.split.LiveSegment(j) > 0.0) && Settings.Profile.DViewShowSegs)
                        {
                            object obj3 = liveTimeCell.Value;
                            liveTimeCell.Value = string.Concat(new object[] { obj3, " [", this.timeFormatter(this.split.LiveSegment(j), TimeFormat.Short), "]" });
                        }
                    }
                    else if ((j < this.split.LiveIndex) && (this.timer.ElapsedTicks > 0L))
                        liveTimeCell.Value = "-";
                    else
                        liveTimeCell.Value = null;

                    // If the current row corresponds to the current split
                    if (j == this.split.LiveIndex)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = ColorSettings.Profile.UsedDViewSegHighlight;
                            cell.Style.ForeColor = ColorSettings.Profile.UsedDViewSegCurrentText;
                        }
                        deltaCell.Value = null;
                    }
                    else    // Else, apply general style...
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = Color.Black;
                            cell.Style.ForeColor = row.DefaultCellStyle.ForeColor;
                        }

                        // If the current row is before the current split and the timer is running
                        if ((j < this.split.LiveIndex) && (this.timer.ElapsedTicks > 0L))
                        {
                            if ((liveTime > 0.0) && (this.split.CompTime(j) > 0.0))
                            {
                                deltaCell.Value = this.timeFormatter(secs, TimeFormat.Delta);

                                liveTimeCell.Style.ForeColor = this.getDViewDeltaColor(secs, secs);
                                if (this.split.LiveSegment(j) > 0.0 && (this.split.segments[j].BestSegment == 0.0 || this.split.LiveSegment(j) < this.split.segments[j].BestSegment))
                                    deltaCell.Style.ForeColor = ColorSettings.Profile.UsedDViewSegBestSegment;
                                else
                                    deltaCell.Style.ForeColor = this.getDViewDeltaColor(secs, oldDelta);
                            }
                            else
                            {
                                deltaCell.Value = "n/a";
                                deltaCell.Style.ForeColor = ColorSettings.Profile.UsedDViewSegMissingTime;
                            }
                        }
                        else
                            deltaCell.Value = null;
                    }

                    if (secs != 0.0)
                        this.dview.Deltas.Add(secs);
                    else
                        this.dview.Deltas.Add(0.0);
                }

                if (this.split.RunTitle != "")
                    this.dview.segs.Rows[0].Cells[0].Value = this.split.RunTitle + "   Goal: " + this.split.RunGoal;
                else
                    this.dview.segs.Rows[0].Cells[0].Value = "Segment";
                this.dview.segs.DefaultCellStyle.SelectionForeColor = ColorSettings.Profile.UsedDViewSegCurrentText;

                if (this.split.ComparingType == Split.CompareType.Old)
                {
                    this.dview.segs.Columns[1].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegCurrentText;
                    this.dview.segs.Columns[2].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegDefaultText;
                    this.dview.segs.Columns[3].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegDefaultText;
                }
                else if (this.split.ComparingType == Split.CompareType.Best)
                {
                    this.dview.segs.Columns[1].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegDefaultText;
                    this.dview.segs.Columns[2].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegCurrentText;
                    this.dview.segs.Columns[3].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegDefaultText;
                }
                else
                {
                    this.dview.segs.Columns[1].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegDefaultText;
                    this.dview.segs.Columns[2].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegDefaultText;
                    this.dview.segs.Columns[3].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegCurrentText;
                }
                this.dview.segs.Columns[0].DefaultCellStyle.ForeColor = ColorSettings.Profile.UsedDViewSegDefaultText;
                this.dview.finalSeg.Columns[0].DefaultCellStyle.ForeColor = this.dview.segs.Columns[0].DefaultCellStyle.ForeColor;
                this.dview.finalSeg.Columns[1].DefaultCellStyle.ForeColor = this.dview.segs.Columns[1].DefaultCellStyle.ForeColor;
                this.dview.finalSeg.Columns[2].DefaultCellStyle.ForeColor = this.dview.segs.Columns[2].DefaultCellStyle.ForeColor;
                this.dview.finalSeg.Columns[3].DefaultCellStyle.ForeColor = this.dview.segs.Columns[3].DefaultCellStyle.ForeColor;

                this.dview.setDeltaPoints();
                this.dview.updateColumns();

                int num10 = Math.Max(Settings.Profile.DisplaySegs, 2);
                int liveIndex = this.split.LiveIndex;
                if (num10 < 3)
                {
                    int num13 = this.split.LiveIndex;
                }
                this.dview.segs.Height = Math.Max(2, Math.Min(num10, this.dview.segs.RowCount)) * this.dview.segs.RowTemplate.Height;
                if (this.dview.segs.RowCount > 1)
                {
                    int num11 = 3;
                    if (Settings.Profile.DisplaySegs < 4)
                    {
                        num11 = 2;
                    }
                    this.dview.segs.FirstDisplayedScrollingRowIndex = Math.Min((int)(this.dview.segs.RowCount - 1), (int)(1 + Math.Max(0, (this.split.LiveIndex - num10) + num11)));
                }
                this.dview.finalSeg.FirstDisplayedScrollingRowIndex = 1;
                for (int k = 1; k < this.dview.finalSeg.Columns.Count; k++)
                {
                    this.dview.segs.AutoResizeColumn(k, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
                    this.dview.finalSeg.AutoResizeColumn(k, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
                    this.dview.segs.Columns[k].Width = Math.Max(this.dview.segs.Columns[k].Width, this.dview.finalSeg.Columns[k].Width);
                    this.dview.finalSeg.Columns[k].Width = this.dview.segs.Columns[k].Width;
                }
            }
            this.dview.resetHeight();
        }

        private void updateDisplay()
        {
            if (Settings.Profile.CompareAgainst == 0)
                this.split.CompType = Split.CompareType.Fastest;
            else if (Settings.Profile.CompareAgainst == 1)
                this.split.CompType = Split.CompareType.Old;
            else if (Settings.Profile.CompareAgainst == 2)
                this.split.CompType = Split.CompareType.Best;
            else if (Settings.Profile.CompareAgainst == 3)
                this.split.CompType = Split.CompareType.SumOfBests;
            this.updateDetailed();
            base.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x312 && !this.modalWindowOpened)
            {
                switch (m.WParam.ToInt32())
                {
                    case 0x9d82:    // Split
                        this.timerControl();
                        break;

                    case 0x9d83:    // Pause
                        this.pauseResume();
                        break;

                    case 0x9d84:    // Stop
                        this.StopSplits();
                        break;

                    case 0x9d85:    // Reset
                        this.ResetSplits();
                        break;

                    case 0x9d86:    // Previous
                        this.prevStage();
                        break;

                    case 0x9d87:    // Next
                        this.nextStage();
                        break;

                    case 0x9d88:    // Switch Comparison Type
                        this.SwitchComparisonType();
                        break;

                    case 0x9d89:    // Toggle Global Hotkeys
                        Settings.Profile.EnabledHotkeys = !Settings.Profile.EnabledHotkeys;
                        this.setHotkeys();
                        this.painter.RequestBackgroundRedraw();
                        base.Invalidate();
                        break;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (!this.modalWindowOpened)
            {
                this.split.RunFile = ((string[])drgevent.Data.GetData(DataFormats.FileDrop, false)).First<string>();
                this.loadFile();
            }
            base.OnDragDrop(drgevent);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (!this.modalWindowOpened)
            {
                if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
                    drgevent.Effect = DragDropEffects.Copy;
                else
                    drgevent.Effect = DragDropEffects.None;
            }

            base.OnDragEnter(drgevent);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            this.clearHotkeys();
            Settings.Profile.WindowPosition = base.Location;
            Settings.Profile.LastFile = this.split.RunFile;
            Settings.Profile.Save();
            ColorSettings.Profile.Save();
            base.OnFormClosed(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int wParam = 0;
                int x = e.X;
                int y = e.Y;
                /*if (sender != this)
                {
                    x += ((Control)sender).Location.X;
                    y += ((Control)sender).Location.Y;
                }*/
                if ((this.currentDispMode == DisplayMode.Wide) && (Math.Abs((int)(x - ((this.clockRect.Right + this.wideSegWidth) - 3))) < 4))
                {
                    this.wideSegResizing = true;
                    this.wideSegResizeWidth = this.wideSegWidthBase;
                    this.wideSegX = x;
                    Cursor.Current = Cursors.SizeWE;
                }
                else if ((this.currentDispMode == DisplayMode.Wide) && (Math.Abs((int)(x - (base.Width - 0x75))) < 4))
                {
                    this.wideResizing = true;
                    this.wideResizingX = x;
                    Cursor.Current = Cursors.SizeWE;
                }
                else if ((this.currentDispMode == DisplayMode.Detailed) && (Math.Abs((int)(y - (this.clockRect.Top - 1))) < 4))
                {
                    this.detailResizing = true;
                    this.detailResizingY = y;
                    Cursor.Current = Cursors.SizeNS;
                }
                else if ((x >= (this.clockRect.Right - 5)) && (x <= this.clockRect.Right))
                {
                    if (((y >= (this.clockRect.Bottom - 5)) && (y <= this.clockRect.Bottom)) && (this.currentDispMode != DisplayMode.Wide))
                    {
                        Cursor.Current = Cursors.SizeNWSE;
                        wParam = 0x11;
                    }
                    else if (((y <= (this.clockRect.Top + 5)) && (y >= this.clockRect.Top)) && (this.currentDispMode != DisplayMode.Wide))
                    {
                        Cursor.Current = Cursors.SizeNESW;
                        wParam = 14;
                    }
                    else
                    {
                        Cursor.Current = Cursors.SizeWE;
                        wParam = 11;
                    }
                    this.MinimumSize = new Size((base.Width - this.clockRect.Width) + this.clockMinimumSize.Width, (base.Height - this.clockRect.Height) + this.clockMinimumSize.Height);
                    this.clockResize = true;
                }
                else if ((x <= (this.clockRect.Left + 5)) && (x >= this.clockRect.Left))
                {
                    if (((y >= (this.clockRect.Bottom - 5)) && (y <= this.clockRect.Bottom)) && (this.currentDispMode != DisplayMode.Wide))
                    {
                        Cursor.Current = Cursors.SizeNESW;
                        wParam = 0x10;
                    }
                    else if (((y <= (this.clockRect.Top + 5)) && (y >= this.clockRect.Top)) && (this.currentDispMode != DisplayMode.Wide))
                    {
                        Cursor.Current = Cursors.SizeNWSE;
                        wParam = 13;
                    }
                    else
                    {
                        Cursor.Current = Cursors.SizeWE;
                        wParam = 10;
                    }
                    this.MinimumSize = new Size((base.Width - this.clockRect.Width) + this.clockMinimumSize.Width, (base.Height - this.clockRect.Height) + this.clockMinimumSize.Height);
                    this.clockResize = true;
                }
                else if (((this.currentDispMode != DisplayMode.Wide) && (y >= (this.clockRect.Bottom - 5))) && (y <= this.clockRect.Bottom))
                {
                    Cursor.Current = Cursors.SizeNS;
                    wParam = 15;
                    this.MinimumSize = new Size((base.Width - this.clockRect.Width) + this.clockMinimumSize.Width, (base.Height - this.clockRect.Height) + this.clockMinimumSize.Height);
                    this.clockResize = true;
                }
                else if (((this.currentDispMode != DisplayMode.Wide) && (y <= (this.clockRect.Top + 5))) && (y >= this.clockRect.Top))
                {
                    Cursor.Current = Cursors.SizeNS;
                    wParam = 12;
                    this.MinimumSize = new Size((base.Width - this.clockRect.Width) + this.clockMinimumSize.Width, (base.Height - this.clockRect.Height) + this.clockMinimumSize.Height);
                    this.clockResize = true;
                }
                else
                    wParam = 2;

                if (wParam > 0)
                {
                    ReleaseCapture();
                    SendMessage(base.Handle, 0xa1, wParam, 0);
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.None)
                this.clockResize = false;

            int x = e.X;
            int y = e.Y;
            /*if (sender != this)
            {
                x += ((Control)sender).Location.X;
                y += ((Control)sender).Location.Y;
            }*/

            if (this.wideSegResizing)
            {
                this.wideSegWidthBase = Math.Max(60, this.wideSegResizeWidth + (x - this.wideSegX));
                this.setDisplay(DisplayMode.Wide);
            }

            else if (this.detailResizing)
            {
                if (y < (this.detailResizingY + this.segHeight))
                {
                    if ((y <= (this.detailResizingY - this.segHeight)) && (Settings.Profile.DisplaySegs > 2))
                    {
                        if (!Settings.Profile.DisplayBlankSegs && (Settings.Profile.DisplaySegs > this.split.Count))
                        {
                            Settings.Profile.DisplaySegs = Math.Min(this.split.Count - 1, 40);
                            this.updateDetailed();
                        }
                        else
                            this.showSegsDec();

                        this.detailResizingY -= this.segHeight;
                    }
                }
                else
                {
                    int num3;
                    if (Settings.Profile.DisplayBlankSegs)
                        num3 = 40;
                    else
                        num3 = Math.Min(this.split.Count, 40);

                    if (Settings.Profile.DisplaySegs < num3)
                    {
                        this.showSegsInc();
                        this.detailResizingY += this.segHeight;
                    }
                }
            }

            else if (this.wideResizing)
            {
                if (x < (this.wideResizingX + this.wideSegWidth))
                {
                    if ((x <= (this.wideResizingX - this.wideSegWidth)) && (Settings.Profile.WideSegs > 1))
                    {
                        if (!Settings.Profile.WideSegBlanks && (Settings.Profile.WideSegs > this.split.Count))
                        {
                            Settings.Profile.WideSegs = Math.Min(this.split.Count - 1, 20);
                        }
                        else
                        {
                            Settings settings2 = Settings.Profile;
                            settings2.WideSegs--;
                        }
                        this.setDisplay(DisplayMode.Wide);
                        this.wideResizingX -= this.wideSegWidth;
                    }
                }
                else
                {
                    int num4;
                    if (Settings.Profile.WideSegBlanks)
                    {
                        num4 = 20;
                    }
                    else
                    {
                        num4 = Math.Min(this.split.Count, 20);
                    }
                    if (Settings.Profile.WideSegs < num4)
                    {
                        Settings settings1 = Settings.Profile;
                        settings1.WideSegs++;
                        this.wideResizingX += this.wideSegWidth;
                        this.setDisplay(DisplayMode.Wide);
                    }
                }
            }

            else if ((this.currentDispMode == DisplayMode.Wide) && (Math.Abs((int)(x - ((this.clockRect.Right + this.wideSegWidth) - 3))) < 4))
                Cursor.Current = Cursors.SizeWE;

            else if ((this.currentDispMode == DisplayMode.Wide) && (Math.Abs((int)(x - (base.Width - 0x75))) < 4))
                Cursor.Current = Cursors.SizeWE;

            else if ((this.currentDispMode == DisplayMode.Detailed) && (Math.Abs((int)(y - (this.clockRect.Top - 1))) < 4))
                Cursor.Current = Cursors.SizeNS;

            else if ((x >= (this.clockRect.Right - 5)) && (x <= this.clockRect.Right))
            {
                if (((y >= (this.clockRect.Bottom - 5)) && (y <= this.clockRect.Bottom)) && (this.currentDispMode != DisplayMode.Wide))
                    Cursor.Current = Cursors.SizeNWSE;
                else if (((y <= (this.clockRect.Top + 5)) && (y >= this.clockRect.Top)) && (this.currentDispMode != DisplayMode.Wide))
                    Cursor.Current = Cursors.SizeNESW;
                else
                    Cursor.Current = Cursors.SizeWE;
            }

            else if ((x <= (this.clockRect.Left + 5)) && (x >= this.clockRect.Left))
            {
                if (((y >= (this.clockRect.Bottom - 5)) && (y <= this.clockRect.Bottom)) && (this.currentDispMode != DisplayMode.Wide))
                    Cursor.Current = Cursors.SizeNESW;
                else if (((y <= (this.clockRect.Top + 5)) && (y >= this.clockRect.Top)) && (this.currentDispMode != DisplayMode.Wide))
                    Cursor.Current = Cursors.SizeNWSE;
                else
                    Cursor.Current = Cursors.SizeWE;
            }

            else if (((y >= (this.clockRect.Bottom - 5)) && (y <= this.clockRect.Bottom)) && (this.currentDispMode != DisplayMode.Wide))
                Cursor.Current = Cursors.SizeNS;

            else if (((this.currentDispMode != DisplayMode.Wide) && (y <= (this.clockRect.Top + 5))) && (y >= this.clockRect.Top))
                Cursor.Current = Cursors.SizeNS;

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.clockResize = false;
            this.wideSegResizing = false;
            this.detailResizing = false;
            this.wideResizing = false;
            this.MinimumSize = new Size(0, 0);
            base.OnMouseUp(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.clockResize)
                this.clockRect.Size = (base.Size - this.MinimumSize) + this.clockMinimumSize;

            if (this.currentDispMode == DisplayMode.Detailed)
                this.detailPreferredSize = this.clockRect.Size;
            else if ((this.currentDispMode != DisplayMode.Wide) && (this.currentDispMode != DisplayMode.Null))
            {
                int width = this.clockRect.Width;
                if ((Settings.Profile.SegmentIcons > 1) && (this.currentDispMode == DisplayMode.Compact))
                    width -= this.clockMinimumSize.Width - this.clockMinimumSizeAbsolute.Width;

                Settings.Profile.ClockSize = new Size(width, this.clockRect.Height);
            }

            if (base.WindowState == FormWindowState.Maximized)
                base.WindowState = FormWindowState.Normal;

            base.OnResize(e);
        }


        // Functions created by Nitrofski
        // ------------------------------

        private void menuItemStartAt_Click(object sender, EventArgs e)
        {
            if (this.timer.IsRunning)
            {
                menuItemStartAt.Enabled = false;
                return;
            }

            StartAtDialog dialog = new StartAtDialog();
            base.TopMost = false;
            this.dview.TopMost = false;
            this.modalWindowOpened = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StartAt(Convert.ToInt64(this.timeParse(dialog.StartingTime) * 10000000.0), dialog.UseDelay);
                //      Not changed... Possible problems with locale settings...
            }

            base.TopMost = Settings.Profile.OnTop;
            this.dview.TopMost = Settings.Profile.DViewOnTop;
            this.modalWindowOpened = false;
        }

        private bool promptForSave()
        {
            if (this.split.UnsavedSplit)
            {
                this.modalWindowOpened = true;
                DialogResult result = MessageBoxEx.Show(
                    "スプリットを上書きしましたが、保存していません。\n" +
                    "スプリットを今保存しますか？",
                    "スプリット保存", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                this.modalWindowOpened = false;

                if (result == DialogResult.Cancel)
                    return false;
                if (result == DialogResult.Yes)
                {
                    if (this.split.RunFile == null)
                        this.saveAs();
                    else
                        this.saveFile();
                }
                this.split.UnsavedSplit = false;
            }

            else if (this.split.NeedUpdate(Settings.Profile.BestAsOverall))
            {
                this.modalWindowOpened = true;
                DialogResult result = MessageBoxEx.Show(
                    "前よりも速いタイムがあります。\n" +
                    "スプリットを上書き後、保存しますか？",
                    "スプリット保存", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                this.modalWindowOpened = false;

                if (result == DialogResult.Cancel)
                    return false;
                if (result == DialogResult.Yes)
                {
                    if (this.split.RunFile == null)
                        this.saveAs();
                    else
                        this.saveFile();
                }
            }
            return true;
        }

        private void ResetSplits()
        {
            if (this.split.NeedUpdate(Settings.Profile.BestAsOverall))
            {
                this.modalWindowOpened = true;
                DialogResult result = MessageBoxEx.Show(this,
                    "前よりも速いタイムがあります。\n" +
                    "それらを上書きしますか？",
                    "スプリット保存", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                this.modalWindowOpened = false;

                if (result == DialogResult.Yes)
                {
                    this.split.UpdateBest(Settings.Profile.BestAsOverall);
                    this.split.UnsavedSplit = true;
                }
                if (result != DialogResult.Cancel)
                {
                    this.InitializeDisplay();
                }
            }
            else
                this.InitializeDisplay();
        }

        private void StopSplits()
        {
            if (this.split.NeedUpdate(Settings.Profile.BestAsOverall))
            {
                this.split.UpdateBest(Settings.Profile.BestAsOverall);
                this.split.UnsavedSplit = true;
            }
            this.InitializeDisplay();
        }

        private void StartAt(long startingTicks, bool useDelay)
        {
            // If the refreshing stopwatch is running
            if (this.stopwatch.Enabled)
            {
                throw new InvalidOperationException(
                    "Trying to start the timer with a starting time, but timer is already running.");
            }

            // If the split aren't done, so if the timer isn't running
            else if (!this.split.Done && (this.startDelay == null))
            {
                // If the timer had not been started yet
                if (this.timer.ElapsedTicks == 0L)
                {
                    // If it wasn't running
                    if (!this.timer.IsRunning)
                    {
                        this.InitializeDisplay();
                    }
                    this.startTimer(startingTicks, useDelay);

                    this.menuItemStartAt.Enabled = false;
                    this.stopButton.Enabled = true;
                    this.resetButton.Enabled = true;
                }

                // If the timer was paused
                else
                {
                    throw new InvalidOperationException(
                    "Trying to start the timer with a starting time, but timer is paused.");
                }
                this.stopwatch.Enabled = true;
            }
        }

        public enum DisplayMode
        {
            Timer,
            Compact,
            Wide,
            Detailed,
            Null
        }

        public enum KeyModifiers
        {
            Alt = 1,
            Control = 2,
            None = 0,
            Shift = 4,
            Windows = 8
        }

        public enum TimeFormat
        {
            Seconds,
            Short,
            Long,
            Delta,
            DeltaShort
        }

        private class Painter
        {
            private WSplit wsplit;

            private string timeStringAbsPart;
            private string timeStringDecPart;

            SizeF clockTimeAbsSize;
            SizeF clockTimeDecSize;
            SizeF clockTimeTotalSize;

            float clockScale;

            private Bitmap background;
            private Bitmap bgImage;
            private Timer animatedBgTimer;
            private int currentAnimatedBackgroundFrame;

            private Color clockColor = Color.White;
            private Color clockGrColor = Color.White;
            private Color clockGrColor2 = Color.White;
            private Color clockPlainColor = Color.White;

            private bool bgRedrawRequested;

            private int runDelLength;
            private int runDeltaWidth;
            private bool runLosingTime;
            private int segDelLength;
            private int segDeltaWidth;
            private bool segLosingTime;
            private int segTimeLength;
            private int segTimeWidth;



            public Painter(WSplit wsplit)
            {
                this.wsplit = wsplit;
            }

            public void PrepareBackground()
            {
                this.bgImage = null;
                if (this.animatedBgTimer != null)
                {
                    this.animatedBgTimer.Dispose();
                    this.animatedBgTimer = null;
                    this.currentAnimatedBackgroundFrame = 0;
                }

                if (Settings.Profile.BackgroundImage)
                {
                    try
                    {
                        using (Bitmap bmp = new Bitmap(Settings.Profile.BackgroundImageFilename))
                        {
                            this.bgImage = bmp.Clone(Settings.Profile.BackgroundImageSelection, bmp.PixelFormat);
                        }

                        if (this.bgImage.FrameDimensionsList.Any(fd => fd.Equals(FrameDimension.Time.Guid))
                            && this.bgImage.GetFrameCount(FrameDimension.Time) > 1)
                        {
                            PropertyItem gifDelay = this.bgImage.GetPropertyItem(0x5100);

                            this.animatedBgTimer = new Timer();
                            this.animatedBgTimer.Interval = BitConverter.ToInt16(gifDelay.Value, 0) * 10;
                            this.animatedBgTimer.Tick += (o, e) =>
                                {
                                    ++this.currentAnimatedBackgroundFrame;
                                    if (this.currentAnimatedBackgroundFrame >= this.bgImage.GetFrameCount(FrameDimension.Time))
                                        this.currentAnimatedBackgroundFrame = 0;

                                    this.bgImage.SelectActiveFrame(FrameDimension.Time, currentAnimatedBackgroundFrame);
                                    this.RequestBackgroundRedraw();
                                };
                        }
                    }
                    catch (Exception e)
                    {
                        // If loading the image fails, we change de settings.
                        Settings.Profile.BackgroundImage = false;
                    }
                }
                // Prepare background
                this.RequestBackgroundRedraw();
            }

            public void RequestBackgroundRedraw()
            {
                this.bgRedrawRequested = true;
            }

            private void DrawBackground(Graphics graphics, float angle, int num5, int num8, int num13, int x)
            {
                // What is "angle?"
                // it's the angle m8...

                // If there is a need to redraw the background
                if (((this.background == null) || (this.background.Size != wsplit.Size)) || this.bgRedrawRequested)
                {
                    this.bgRedrawRequested = false;
                    GC.Collect();   // Hardcoded Garbage Collection? Mmh...

                    // Creating the bitmap
                    if ((this.background == null) || (this.background.Size != wsplit.Size))
                        this.background = new Bitmap(wsplit.Width, wsplit.Height);

                    Graphics bgGraphics = Graphics.FromImage(this.background);
                    bgGraphics.Clear(Color.Black);
                    bgGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    //
                    // If display mode is NOT Timer Only
                    //
                    if (wsplit.currentDispMode != DisplayMode.Timer)
                    {
                        Rectangle statusBarEctangle;
                        Rectangle headerTextRectangle;
                        Rectangle statusTextRectangle;
                        Rectangle timesaverectangle;
                        Rectangle sobrectangle;
                        Rectangle timesaverectangle1;
                        Rectangle sobrectangle1;
                        Rectangle timesaverectangle2;
                        Rectangle sobrectangle2;
                        Rectangle pbrectangle;
                        Rectangle pbrectangle1;
                        Rectangle pbrectangle2;
                        Rectangle bestrectangle;
                        Rectangle bestrectangle1;
                        Rectangle bestrectangle2;
                        Rectangle goalTextRectangle;

                        int ps_y, ts_y, sob_y, pb_y, best_y = 0;
                        ps_y = wsplit.clockRect.Bottom;
                        if (Settings.Profile.ShowPrevSeg) { ts_y = ps_y + 18; } else { ts_y = ps_y; };
                        if (Settings.Profile.ShowTimeSave) { sob_y = ts_y + 18; } else { sob_y = ts_y; };
                        if (Settings.Profile.ShowSoB) { pb_y = sob_y + 18; } else { pb_y = sob_y; };
                        if (Settings.Profile.PredPB) { best_y = pb_y + 18; } else { best_y = pb_y; };

                        if (wsplit.currentDispMode == DisplayMode.Wide)
                        {
                            statusBarEctangle = new Rectangle(wsplit.Width - 120, 0, 120, wsplit.Height);                       // Status bar
                            statusTextRectangle = new Rectangle(wsplit.Width - 119, wsplit.Height / 2, 118, wsplit.Height / 2); // Run status
                            timesaverectangle = new Rectangle(0, 0, 0, 0);
                            sobrectangle = new Rectangle(0, 0, 0, 0);
                            timesaverectangle1 = new Rectangle(0, 0, 0, 0);
                            sobrectangle1 = new Rectangle(0, 0, 0, 0);
                            timesaverectangle2 = new Rectangle(0, 0, 0, 0);
                            sobrectangle2 = new Rectangle(0, 0, 0, 0);
                            pbrectangle = new Rectangle(0, 0, 0, 0);
                            pbrectangle1 = new Rectangle(0, 0, 0, 0);
                            pbrectangle2 = new Rectangle(0, 0, 0, 0);
                            bestrectangle = new Rectangle(0, 0, 0, 0);
                            bestrectangle1 = new Rectangle(0, 0, 0, 0);
                            bestrectangle2 = new Rectangle(0, 0, 0, 0);

                            if ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal)
                            {
                                headerTextRectangle = new Rectangle(wsplit.Width - 119, (wsplit.Height / 4) - 4, 59, 12);          // Run title
                                statusTextRectangle = new Rectangle(headerTextRectangle.X + headerTextRectangle.Width, (wsplit.Height / 4) - 4, 59, 12); //Run goal
                                goalTextRectangle = new Rectangle(wsplit.Width - 119, wsplit.Height / 2, 118, wsplit.Height / 2); //Run status
                            }
                            else
                            {
                                headerTextRectangle = new Rectangle(wsplit.Width - 119, (wsplit.Height / 4) - 4, 118, 12);          // Run title
                                statusTextRectangle = new Rectangle(wsplit.Width - 119, wsplit.Height / 2, 118, wsplit.Height / 2); // Run status
                                goalTextRectangle = new Rectangle(0, 0, 0, 0);  //Nothing...
                            }
                        }
                        else if (wsplit.currentDispMode == DisplayMode.Detailed)
                        {
                            headerTextRectangle = new Rectangle(0, 0, 0, 0);

                            if (Settings.Profile.ShowPrevSeg)
                            {
                                statusBarEctangle = new Rectangle(0, ps_y, wsplit.Width, 18);
                                statusTextRectangle = new Rectangle(1, ps_y + 2, wsplit.Width - 2, 16);
                            }
                            else
                            {
                                statusBarEctangle = new Rectangle(0, ps_y - 18, wsplit.Width, 18);
                                statusTextRectangle = new Rectangle(1, ps_y + 2 - 18, wsplit.Width - 2, 16);
                            };

                            timesaverectangle = new Rectangle(0, ts_y, wsplit.Width, 18);
                            sobrectangle = new Rectangle(0, sob_y, wsplit.Width, 18);
                            timesaverectangle1 = new Rectangle(0, ts_y + 2, wsplit.Width - 2, 16);
                            sobrectangle1 = new Rectangle(0, sob_y + 2, wsplit.Width - 2, 16);
                            timesaverectangle2 = new Rectangle(0, ts_y + 2, wsplit.Width - 2, 16);
                            sobrectangle2 = new Rectangle(0, sob_y + 2, wsplit.Width - 2, 16);
                            pbrectangle = new Rectangle(0, pb_y, wsplit.Width, 18);
                            bestrectangle = new Rectangle(0, best_y, wsplit.Width, 18);
                            pbrectangle1 = new Rectangle(0, pb_y + 2, wsplit.Width - 2, 16);
                            bestrectangle1 = new Rectangle(0, best_y + 2, wsplit.Width - 2, 16);
                            pbrectangle2 = new Rectangle(0, pb_y + 2, wsplit.Width - 2, 16);
                            bestrectangle2 = new Rectangle(0, best_y + 2, wsplit.Width - 2, 16);
                            statusBarEctangle = new Rectangle(0, wsplit.clockRect.Bottom, wsplit.Width, 18);            // Status bar
                            headerTextRectangle = new Rectangle(0, 0, 0, 0);                                            // Nothing? Why?
                            statusTextRectangle = new Rectangle(1, wsplit.clockRect.Bottom + 2, wsplit.Width - 2, 16);  // Run status
                            goalTextRectangle = new Rectangle(0, 0, 0, 0);                                              // Nothing...
                        }
                        else
                        {
                            statusBarEctangle = new Rectangle(0, wsplit.clockRect.Bottom, wsplit.Width, 16);            // Status bar
                            headerTextRectangle = new Rectangle(1, 2, (wsplit.Width / 2) - 2, 13);                            // Segment name
                            statusTextRectangle = new Rectangle(1, wsplit.clockRect.Bottom + 2, wsplit.Width - 2, 14);  // Run status
                            timesaverectangle = new Rectangle(0, 0, 0, 0);
                            sobrectangle = new Rectangle(0, 0, 0, 0);
                            timesaverectangle1 = new Rectangle(0, 0, 0, 0);
                            sobrectangle1 = new Rectangle(0, 0, 0, 0);
                            timesaverectangle2 = new Rectangle(0, 0, 0, 0);
                            sobrectangle2 = new Rectangle(0, 0, 0, 0);
                            pbrectangle = new Rectangle(0, 0, 0, 0);
                            pbrectangle1 = new Rectangle(0, 0, 0, 0);
                            pbrectangle2 = new Rectangle(0, 0, 0, 0);
                            bestrectangle = new Rectangle(0, 0, 0, 0);
                            bestrectangle1 = new Rectangle(0, 0, 0, 0);
                            bestrectangle2 = new Rectangle(0, 0, 0, 0);

                            if ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal)
                            {
                                goalTextRectangle = new Rectangle(headerTextRectangle.X + headerTextRectangle.Width, 2, (wsplit.Width / 2) - 2, 13); // Run goal
                            }
                            else
                            {
                                goalTextRectangle = new Rectangle(0, 0, 0, 0); // Nothing...
                            }
                            // If the segment icon will be shown, the segment name have to be pushed right
                            if ((Settings.Profile.SegmentIcons > 0) && !wsplit.split.Done)
                            {
                                headerTextRectangle.Width -= 4 + (8 * (Settings.Profile.SegmentIcons + 1));
                                headerTextRectangle.X += 4 + (8 * (Settings.Profile.SegmentIcons + 1));
                            }
                        }

                        // If the background isn't black
                        if (!Settings.Profile.BackgroundBlack)
                        {
                            // Fill the status bar
                            if (Settings.Profile.BackgroundPlain)
                            {
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBackPlain), statusBarEctangle);
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBackPlain), timesaverectangle);
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBackPlain), sobrectangle);
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBackPlain), pbrectangle);
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBackPlain), bestrectangle);
                            }
                            else
                            {
                                bgGraphics.FillRectangle(new LinearGradientBrush(statusBarEctangle, ColorSettings.Profile.StatusBack, ColorSettings.Profile.StatusBack2, angle), statusBarEctangle);
                                bgGraphics.FillRectangle(new LinearGradientBrush(statusBarEctangle, ColorSettings.Profile.StatusBack, ColorSettings.Profile.StatusBack2, angle), timesaverectangle);
                                bgGraphics.FillRectangle(new LinearGradientBrush(statusBarEctangle, ColorSettings.Profile.StatusBack, ColorSettings.Profile.StatusBack2, angle), sobrectangle);
                                bgGraphics.FillRectangle(new LinearGradientBrush(statusBarEctangle, ColorSettings.Profile.StatusBack, ColorSettings.Profile.StatusBack2, angle), pbrectangle);
                                bgGraphics.FillRectangle(new LinearGradientBrush(statusBarEctangle, ColorSettings.Profile.StatusBack, ColorSettings.Profile.StatusBack2, angle), bestrectangle);
                            };
                            // Detailed mode - Draw the title bar
                            if (wsplit.currentDispMode == DisplayMode.Detailed)
                            {
                                int titleX = 0;
                                int titleY = 0;
                                int goalX = 0;
                                int goalY = 0;

                                if (((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle) && ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal) && !Settings.Profile.BackgroundPlain)
                                {
                                    bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack), titleX, titleY, wsplit.Width, 32);
                                    bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack2), titleX, titleY, wsplit.Width, 16);

                                }
                                else
                                {
                                    if ((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle)
                                    {
                                        if (Settings.Profile.BackgroundPlain)
                                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBackPlain), titleX, titleY, wsplit.Width, 16);
                                        else
                                        {
                                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack), titleX, titleY, wsplit.Width, 16);
                                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack2), titleX, titleY, wsplit.Width, 8);
                                        }
                                    }
                                    if ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal)
                                    {
                                        if ((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle)
                                        {
                                            goalY = 16;
                                        }
                                        if (Settings.Profile.BackgroundPlain)
                                        {
                                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBackPlain), 0, goalY, wsplit.Width, 16);
                                        }
                                        else
                                        {
                                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack), 0, goalY, wsplit.Width, 16);
                                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack2), 0, goalY, wsplit.Width, 8);
                                        }
                                    }
                                }
                            }

                            // Compact mode - Draw the title bar
                            else if (wsplit.currentDispMode == DisplayMode.Compact)
                            {
                                if (Settings.Profile.BackgroundPlain)
                                    bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBackPlain), 0, 0, wsplit.Width, 18);
                                else
                                {
                                    bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack), 0, 0, wsplit.Width, 15);
                                    bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.TitleBack2), 0, 0, wsplit.Width, 7);
                                }
                            }
                        }

                        // Refactor? Only used once, never changes
                        StringFormat format3 = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Trimming = StringTrimming.EllipsisCharacter
                        };

                        // Only used once, Alignment changes
                        StringFormat format4 = (StringFormat)format3.Clone();

                        if (wsplit.currentDispMode == DisplayMode.Compact)
                            format4.Alignment = StringAlignment.Far;

                        string s = "";
                        string statusText = "";

                        // The run is not started yet
                        if (wsplit.timer.ElapsedTicks == 0L)
                        {
                            if (wsplit.currentDispMode != DisplayMode.Wide)
                                format4.Alignment = StringAlignment.Far;

                            if (wsplit.startDelay != null)
                                statusText = "Delay";
                            else
                            {
                                statusText = "Ready";
                                if (Settings.Profile.ShowAttempts && ((wsplit.currentDispMode != DisplayMode.Detailed) || !Settings.Profile.ShowTitle))
                                    statusText += ", Attempt #" + (wsplit.split.AttemptsCount + 1);
                            }
                        }

                        // The run is done
                        else if (wsplit.split.Done)
                        {
                            if (wsplit.split.CompTime(wsplit.split.LastIndex) == 0.0)
                            {
                                if (wsplit.currentDispMode != DisplayMode.Wide)
                                    format4.Alignment = StringAlignment.Far;

                                statusText = "完走";
                            }
                            else if (wsplit.split.LastSegment.LiveTime < wsplit.split.CompTime(wsplit.split.LastIndex))
                                statusText = "新記録";
                            else
                                statusText = "完走";
                        }

                        // The run is going
                        else if ((wsplit.currentDispMode == DisplayMode.Compact) && (wsplit.split.CompTime(wsplit.split.LiveIndex) != 0.0))
                            statusText = wsplit.split.ComparingType.ToString() + ": " + wsplit.timeFormatter(wsplit.split.CompTime(wsplit.split.LiveIndex), TimeFormat.Long);
                        else if (this.segLosingTime)
                            statusText = "現区間";
                        else if (((wsplit.split.LiveIndex > 0) && (wsplit.split.segments[wsplit.split.LiveIndex - 1].LiveTime > 0.0)) && (wsplit.split.CompTime(wsplit.split.LiveIndex - 1) != 0.0))
                            statusText = "前区間";

                        // Detailed mode
                        if (wsplit.currentDispMode == DisplayMode.Detailed)
                        {
                            int goalTextY = 0;
                            if ((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle)
                            {
                                bgGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                                Rectangle rectangle7 = new Rectangle(0, 1, wsplit.Width, 17);
                                goalTextY = 16;

                                // Draws the hotkey toggle indicator
                                if (Settings.Profile.HotkeyToggleKey != Keys.None)
                                {
                                    if (Settings.Profile.EnabledHotkeys)
                                    {
                                        bgGraphics.FillRectangle(Brushes.GreenYellow, wsplit.Width - 10, 4, 6, 6);
                                    }
                                    else
                                    {
                                        bgGraphics.FillRectangle(Brushes.OrangeRed, wsplit.Width - 10, 4, 6, 6);
                                    }
                                    rectangle7.Width -= 10;
                                }

                                string str8 = "";
                                if (Settings.Profile.ShowAttempts)
                                {
                                    if (wsplit.timer.IsRunning)
                                    {
                                        str8 = "#" + wsplit.split.AttemptsCount + " / ";
                                    }
                                    else
                                    {
                                        str8 = "#" + (wsplit.split.AttemptsCount + 1) + " / ";
                                    }
                                }

                                StringFormat format5 = new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center,
                                    Trimming = StringTrimming.EllipsisCharacter
                                };

                                bgGraphics.DrawString(str8 + wsplit.split.RunTitle, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.TitleFore), rectangle7, format5);
                            }

                            if ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal)
                            {
                                bgGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                                Rectangle goalRectangle = new Rectangle(0, 19, wsplit.Width, 17);
                                goalRectangle.Y = goalTextY;

                                if (Settings.Profile.HotkeyToggleKey != Keys.None)
                                {
                                    goalRectangle.Width -= 10;
                                }

                                StringFormat format5 = new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center,
                                    Trimming = StringTrimming.EllipsisCharacter
                                };
                                bgGraphics.DrawString("Goal: " + wsplit.split.RunGoal, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.TitleFore), goalRectangle, format5);
                            }
                        }

                        else if (wsplit.split.Done)
                        {
                            if (wsplit.currentDispMode == DisplayMode.Wide)
                            {
                                s = statusText;
                                statusText = "最終区間";
                            }
                            else
                                s = "最終区間";
                        }
                        else if (wsplit.currentDispMode == DisplayMode.Compact)
                            s = wsplit.split.CurrentSegment.Name;
                        else if ((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle)
                            s = wsplit.split.RunTitle;
                        else
                            s = "Run";

                        if (wsplit.currentDispMode == DisplayMode.Compact)
                        {
                            headerTextRectangle.Width -= this.segDeltaWidth;
                            statusTextRectangle.Width -= this.runDeltaWidth;
                            statusTextRectangle.X += this.runDeltaWidth;
                        }
                        else
                        {
                            headerTextRectangle.Width -= this.runDeltaWidth;
                            statusTextRectangle.Width -= this.segDeltaWidth;
                        }

                        bgGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        bgGraphics.DrawString(s, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), headerTextRectangle, format3);   // To be verified, but it seems like this line writes fuck all in a negative rectangle when in Detailed mode...
                        bgGraphics.DrawString(statusText, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), statusTextRectangle, format4);
                        // u wote m8
                        StringFormat strleft = new StringFormat
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center,
                            Trimming = StringTrimming.EllipsisCharacter
                        };
                        StringFormat strright = new StringFormat
                        {
                            Alignment = StringAlignment.Far,
                            LineAlignment = StringAlignment.Center,
                            Trimming = StringTrimming.EllipsisCharacter
                        };
                        // text
                        string sobtext = "-";
                        string tstext = "0:00";
                        if (Settings.Profile.ShowSoB)
                        {
                            bgGraphics.DrawString("理論値", wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), sobrectangle1, strleft);
                            if (wsplit.split.SumOfBests(wsplit.split.LastIndex) > 0.0)
                            {
                                sobtext = wsplit.timeFormatter(wsplit.split.SumOfBests(wsplit.split.LastIndex), TimeFormat.Short);
                            };
                            bgGraphics.DrawString(sobtext, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), sobrectangle2, strright);
                        };//yeah
                        //int j;
                        double segtime = 0.0;
                        //segtime = wsplit.split.segments[wsplit.split.LiveIndex].BestTime;
                        if (wsplit.split.LiveIndex > 0 && (wsplit.split.LiveIndex <= wsplit.split.LastIndex))
                        {
                            segtime = wsplit.split.segments[wsplit.split.LiveIndex].BestTime;
                            segtime -= wsplit.split.segments[wsplit.split.LiveIndex - 1].BestTime;
                        }
                        else if (wsplit.split.LiveIndex == 0)
                        {
                            segtime = wsplit.split.segments[0].BestTime;
                        };
                        if ((segtime > 0.0) && (wsplit.split.segments[wsplit.split.LiveIndex].BestSegment > 0.0) && (segtime >= wsplit.split.segments[wsplit.split.LiveIndex].BestSegment))
                        {
                            tstext = wsplit.timeFormatter(Math.Abs(segtime - wsplit.split.segments[wsplit.split.LiveIndex].BestSegment), TimeFormat.Short);
                        };
                        if ((wsplit.split.LiveIndex > 0) && (wsplit.split.LiveIndex <= wsplit.split.LastIndex) && ((wsplit.split.segments[wsplit.split.LiveIndex].BestTime == 0.0) || (wsplit.split.segments[wsplit.split.LiveIndex - 1].BestTime == 0.0)))
                        {
                            tstext = "-";
                        };
                        if (Settings.Profile.ShowTimeSave)
                        {
                            bgGraphics.DrawString("更新可能", wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), timesaverectangle1, strleft);
                            bgGraphics.DrawString(tstext, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), timesaverectangle2, strright);
                        };
                        if (Settings.Profile.PredPB)
                        {
                            bgGraphics.DrawString("予想(自己べ)", wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), pbrectangle1, strleft);
                            if (wsplit.split.CompTime(wsplit.split.LastIndex) > 0.0)
                            {
                                string pbtime = "";
                                //if (wsplit.split.LiveIndex <= 0)
                                //{
                                //    pbtime = wsplit.timeFormatter(wsplit.split.CompTime(wsplit.split.LastIndex), TimeFormat.Short);
                                //}
                                //else
                                //{
                                pbtime = wsplit.timeFormatter(wsplit.split.CompTime(wsplit.split.LastIndex) + wsplit.split.LastDelta(wsplit.split.LiveIndex), TimeFormat.Short);
                                //};
                                bgGraphics.DrawString(pbtime, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), pbrectangle2, strright);
                            }
                            else
                            {
                                bgGraphics.DrawString("-", wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), pbrectangle2, strright);
                            };
                        };
                        if (Settings.Profile.PredBest)
                        {
                            bgGraphics.DrawString("予想(区間最速)", wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), bestrectangle1, strleft);
                            if (wsplit.split.SumOfBests(wsplit.split.LastIndex) > 0.0)
                            {
                                double best = wsplit.split.SumOfBests(wsplit.split.LastIndex);
                                if ((wsplit.split.LiveIndex > 0) && (wsplit.split.LiveIndex <= wsplit.split.LastIndex))
                                {
                                    int i;
                                    for (i = wsplit.split.LiveIndex; i >= 0; i--)
                                    {
                                        if (wsplit.split.segments[i].LiveTime != 0)
                                        {
                                            best += wsplit.split.segments[i].LiveTime - wsplit.split.SumOfBests(i);
                                            break;
                                        };
                                    };
                                };
                                string besttime = wsplit.timeFormatter(best, TimeFormat.Short);
                                bgGraphics.DrawString(besttime, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), bestrectangle2, strright);
                            }
                            else
                            {
                                bgGraphics.DrawString("-", wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), bestrectangle2, strright);
                            };
                        };
                        if (wsplit.currentDispMode != DisplayMode.Detailed && wsplit.split.RunGoal != "")
                        {
                            bgGraphics.DrawString("Goal: " + wsplit.split.RunGoal, wsplit.displayFont, new SolidBrush(ColorSettings.Profile.StatusFore), goalTextRectangle, format4);
                        }
                    }

                    //
                    // Wide or detailed modes
                    //
                    if ((wsplit.currentDispMode == DisplayMode.Wide) || (wsplit.currentDispMode == DisplayMode.Detailed))
                    {
                        Rectangle rectangle8;   // Yet another unnamed rectangle
                        int num16 = wsplit.clockRect.Right + 2;
                        int y = 0;

                        if (((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle) && ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal))
                        {
                            y += 32;
                        }
                        else if ((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle)
                            y += 18;

                        else if ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal)
                        {
                            y += 18;
                        }

                        if (wsplit.currentDispMode == DisplayMode.Wide)
                        {
                            rectangle8 = new Rectangle(num16, 0, (wsplit.Width - wsplit.clockRect.Width) - 122, wsplit.Height);
                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBack2), wsplit.clockRect.Right, 0, 2, wsplit.Height);
                        }

                        else
                            rectangle8 = new Rectangle(0, y, wsplit.Width, wsplit.clockRect.Bottom - 18);

                        if (!Settings.Profile.BackgroundBlack)
                        {
                            if (Settings.Profile.BackgroundPlain)
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.SegBackPlain), rectangle8);
                            else
                                bgGraphics.FillRectangle(new LinearGradientBrush(rectangle8, ColorSettings.Profile.SegBack, ColorSettings.Profile.SegBack2, angle), rectangle8);
                        }

                        // Flag2 = Show last
                        bool flag2 = false;
                        if (((num5 > 3) && (((num13 + num5) - 1) < wsplit.split.LastIndex)) && (((wsplit.currentDispMode == DisplayMode.Detailed) && Settings.Profile.ShowLastDetailed) || ((wsplit.currentDispMode == DisplayMode.Wide) && Settings.Profile.ShowLastWide)))
                            flag2 = true;

                        if (wsplit.currentDispMode == DisplayMode.Wide)
                        {
                            if (flag2)
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBack2), (wsplit.Width - 0x7a) - wsplit.wideSegWidth, 0, 2, wsplit.Height);
                            else
                                bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBack2), wsplit.Width - 0x7a, 0, 2, wsplit.Height);
                        }
                        else if (flag2)
                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBackPlain), 0, (wsplit.clockRect.Y - 3) - wsplit.segHeight, wsplit.Width, 3);
                        else if (Settings.Profile.BackgroundPlain || Settings.Profile.BackgroundBlack)
                            bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.StatusBackPlain), 0, wsplit.clockRect.Y - 3, wsplit.Width, 3);
                        else
                            bgGraphics.FillRectangle(new LinearGradientBrush(rectangle8, ColorSettings.Profile.StatusBack, ColorSettings.Profile.StatusBack2, 0f), 0, wsplit.clockRect.Y - 3, wsplit.Width, 3);

                        StringFormat format6 = new StringFormat
                        {
                            Trimming = StringTrimming.EllipsisCharacter,
                            LineAlignment = StringAlignment.Center
                        };

                        StringFormat format7 = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center
                        };

                        if (wsplit.currentDispMode != DisplayMode.Wide)
                        {
                            if (wsplit.segHeight > 24)
                                format6.Alignment = StringAlignment.Near;

                            format7.Alignment = StringAlignment.Far;
                        }

                        Rectangle rect = new Rectangle(0, 0, 0, 1);

                        int num18 = 0;
                        for (int i = num13; (num18 < num5) && (i <= wsplit.split.LastIndex); i++)
                        {
                            int segTimeWidth;
                            Rectangle rectangle10;
                            Rectangle rectangle11;
                            Rectangle rectangle12;
                            Rectangle rectangle13;

                            Image grayIcon;
                            ImageAttributes attributes;

                            if (((num18 + 1) >= num5) && flag2)
                            {
                                i = wsplit.split.LastIndex;
                                y += 3;
                                num16 += 2;
                            }

                            Brush brush3 = new SolidBrush(ColorSettings.Profile.FutureSegName);
                            string str9 = wsplit.split.segments[i].TimeString;
                            // thing //
                            string splittime = "";
                            if ((i < wsplit.split.LiveIndex) && (wsplit.split.segments[i].LiveTime > 0.0))
                            {
                                splittime = wsplit.timeFormatter(wsplit.split.segments[i].LiveTime, TimeFormat.Short);
                            }
                            else
                            {
                                splittime = "-";
                            };
                            // idk //
                            string name = wsplit.split.segments[i].Name;

                            if ((i == wsplit.split.LiveIndex) && this.runLosingTime)
                                segTimeWidth = this.segTimeWidth;
                            else
                                segTimeWidth = wsplit.split.segments[i].TimeWidth;

                            ColorMatrix newColorMatrix = new ColorMatrix
                            {
                                Matrix33 = 0.65f
                            };

                            if (wsplit.currentDispMode == DisplayMode.Wide)
                            {
                                rectangle10 = new Rectangle(num16, 0, wsplit.wideSegWidth, wsplit.Height);
                                rectangle11 = new Rectangle(num16 + 2, (wsplit.Height / 4) - 4, (wsplit.wideSegWidth - x) - 2, 12);
                                rectangle12 = new Rectangle(rectangle11.Left, wsplit.Height / 2, rectangle11.Width, wsplit.Height / 2);
                                rectangle13 = new Rectangle(rectangle11.Left + Settings.Profile.Width + 8, wsplit.Height / 2, rectangle11.Width, wsplit.Height / 2);
                            }

                            else
                            /* create rectangles here */
                            {
                                rectangle10 = new Rectangle(0, y, wsplit.Width, wsplit.segHeight);         //icon
                                rectangle11 = new Rectangle(x, y + 2, wsplit.Width - x, wsplit.segHeight); //name
                                rectangle12 = new Rectangle(x, y + 1, wsplit.Width - x, wsplit.segHeight); //delta
                                rectangle13 = new Rectangle(x - Settings.Profile.Width - 8, y + 1, wsplit.Width - x, wsplit.segHeight); //test

                                if (wsplit.segHeight <= 24)
                                {
                                    rectangle11.Width -= segTimeWidth + 2;
                                    rectangle11.Y = (y + (wsplit.segHeight / 2)) - 5;
                                    rectangle11.Height = 13;
                                }
                                else
                                {
                                    rectangle11.Y = y + 2;
                                    rectangle11.Height /= 2;
                                    rectangle12.Y = rectangle11.Bottom - 2;
                                    rectangle12.Height /= 2;
                                    rectangle13.Y = rectangle11.Bottom - 2;
                                    rectangle13.Height /= 2;
                                }
                            }
                            if ((i < wsplit.split.LiveIndex) && (wsplit.timer.ElapsedTicks > 0L))
                            {
                                brush3 = new SolidBrush(ColorSettings.Profile.PastSeg);
                                if (wsplit.split.segments[i].TimeColor == ColorSettings.Profile.SegRainbow && Settings.Profile.RainbowSplits)
                                {
                                    brush3 = new SolidBrush(ColorSettings.Profile.SegRainbow);
                                };
                            }
                            else if (i == wsplit.split.LiveIndex)
                            {
                                brush3 = new SolidBrush(ColorSettings.Profile.LiveSeg);
                                rect = rectangle10;
                                if (Settings.Profile.BackgroundPlain || Settings.Profile.BackgroundBlack)
                                {
                                    bgGraphics.FillRectangle(new SolidBrush(ColorSettings.Profile.SegHighlightPlain), rectangle10);
                                }
                                else
                                {
                                    bgGraphics.FillRectangle(new LinearGradientBrush(rectangle10, ColorSettings.Profile.SegHighlight, ColorSettings.Profile.SegHighlight2, angle), rectangle10);
                                }
                                newColorMatrix.Matrix33 = 1f;
                            }
                            if (x == 0)
                            {
                                goto Label_1F67;
                            }
                            if ((i < wsplit.split.LiveIndex) && (wsplit.timer.ElapsedTicks > 0L))
                            {
                                newColorMatrix.Matrix33 = 0.85f;
                                switch (x)
                                {
                                    case 0x10:
                                        grayIcon = wsplit.split.segments[i].GrayIcon16;
                                        goto Label_1EE9;

                                    case 0x18:
                                        grayIcon = wsplit.split.segments[i].GrayIcon24;
                                        goto Label_1EE9;

                                    case 0x20:
                                        grayIcon = wsplit.split.segments[i].GrayIcon32;
                                        goto Label_1EE9;
                                }
                                grayIcon = wsplit.split.segments[i].GrayIcon;
                            }
                            else
                            {
                                switch (x)
                                {
                                    case 0x10:
                                        grayIcon = wsplit.split.segments[i].Icon16;
                                        goto Label_1EE9;

                                    case 0x18:
                                        grayIcon = wsplit.split.segments[i].Icon24;
                                        goto Label_1EE9;

                                    case 0x20:
                                        grayIcon = wsplit.split.segments[i].Icon32;
                                        goto Label_1EE9;
                                }
                                grayIcon = wsplit.split.segments[i].Icon;
                            }
                        Label_1EE9:
                            attributes = new ImageAttributes();
                            attributes.SetColorMatrix(newColorMatrix);
                            Rectangle destRect = new Rectangle(rectangle10.X, rectangle10.Y, x, x);
                            if (wsplit.currentDispMode == DisplayMode.Wide)
                            {
                                destRect.X += wsplit.wideSegWidth - x;
                                destRect.Y += wsplit.Height - x;
                            }
                            bgGraphics.DrawImage(grayIcon, destRect, 0, 0, grayIcon.Width, grayIcon.Height, GraphicsUnit.Pixel, attributes);
                        Label_1F67:
                            bgGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            //test
                            if (Settings.Profile.SplitTimes && (i < wsplit.split.LiveIndex) && (x < 32) && wsplit.currentDispMode == DisplayMode.Detailed)
                            {
                                rectangle11.Width = rectangle13.Right - MeasureDisplayStringWidth(str9, wsplit.timeFont) - x - 4;
                            };
                            bgGraphics.DrawString(name, wsplit.displayFont, brush3, rectangle11, format6);
                            /* draw labels here */
                            if (i != wsplit.split.LiveIndex)
                            {
                                if (i < wsplit.split.LiveIndex && Settings.Profile.SplitTimes && (Settings.Profile.last != "-") && wsplit.timer.ElapsedTicks > 0L)
                                {
                                    bgGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    if (wsplit.split.segments[i].TimeColor == ColorSettings.Profile.SegRainbow && Settings.Profile.RainbowSplits)
                                    {
                                        bgGraphics.DrawString(splittime, wsplit.timeFont, new SolidBrush(ColorSettings.Profile.SegRainbow), rectangle12, format7);
                                    }
                                    else
                                    {
                                        bgGraphics.DrawString(splittime, wsplit.timeFont, new SolidBrush(ColorSettings.Profile.SegPastTime), rectangle12, format7);
                                    };
                                    bgGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    if (wsplit.split.CompTime(i) == 0.0) { str9 = "-"; };
                                    bgGraphics.DrawString(str9, wsplit.timeFont, new SolidBrush(wsplit.split.segments[i].TimeColor), rectangle13, format7);
                                }
                                else
                                {
                                    bgGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    bgGraphics.DrawString(str9, wsplit.timeFont, new SolidBrush(wsplit.split.segments[i].TimeColor), rectangle12, format7);
                                };
                            }
                            num16 += wsplit.wideSegWidth;
                            y += wsplit.segHeight;
                            num18++;
                        }

                        if (wsplit.split.LiveRun && !wsplit.split.Done)
                        {
                            Pen pen = new Pen(new SolidBrush(ColorSettings.Profile.SegHighlightBorder));
                            if (wsplit.currentDispMode == DisplayMode.Wide)
                            {
                                rect.Height--;
                                bgGraphics.DrawRectangle(pen, rect);
                            }
                            else
                            {
                                bgGraphics.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top);
                                bgGraphics.DrawLine(pen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);
                            }
                        }
                    }

                    // Code for drawing clock back has been moved to another function. Current method's signature is temporary
                    this.DrawClockBack(angle, num8, bgGraphics);

                    if (((x > 0) && (wsplit.currentDispMode == DisplayMode.Compact)) && (wsplit.split.LiveRun && !wsplit.split.Done))
                    {
                        Image icon;
                        switch (x)
                        {
                            case 0x10:
                                icon = wsplit.split.CurrentSegment.Icon16;
                                break;

                            case 0x18:
                                icon = wsplit.split.CurrentSegment.Icon24;
                                break;

                            case 0x20:
                                icon = wsplit.split.CurrentSegment.Icon32;
                                break;

                            default:
                                icon = wsplit.split.CurrentSegment.Icon;
                                break;
                        }
                        Rectangle rectangle14 = new Rectangle(3, 3, x, x);
                        bgGraphics.DrawImage(icon, rectangle14, 0, 0, icon.Width, icon.Height, GraphicsUnit.Pixel);
                        bgGraphics.DrawRectangle(new Pen(new SolidBrush(wsplit.DarkenColor(this.clockColor, 0.7))), 3, 3, icon.Width, icon.Height);
                    }
                }

                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(this.background, 0, 0);
                graphics.CompositingMode = CompositingMode.SourceOver;
            }

            public void PaintAll(Graphics graphics)
            {
                TimeSpan span2;
                ColorSettings settings = ColorSettings.Profile;
                bool flag = false;
                double totalMilliseconds = wsplit.timer.Elapsed.TotalMilliseconds;


                if (((Settings.Profile.FallbackPreference == 1) || (Settings.Profile.FallbackPreference == 2)) && (totalMilliseconds > 0.0))
                {
                    double num2 = Math.Abs(wsplit.timer.driftMilliseconds);

                    if ((num2 > 500.0) && ((num2 / totalMilliseconds) > 5.5555555555555558E-05))
                    {
                        if (Settings.Profile.FallbackPreference == 2)
                            wsplit.timer.useFallback = true;

                        else
                            flag = true;
                    }

                    else if (Settings.Profile.FallbackPreference == 2)
                    {
                        wsplit.timer.useFallback = false;
                    }
                }

                TimeSpan elapsed = wsplit.timer.Elapsed;
                double num3 = wsplit.split.SegDelta(elapsed.TotalSeconds, wsplit.split.LiveIndex);
                double secs = wsplit.split.RunDelta(elapsed.TotalSeconds, wsplit.split.LiveIndex);
                int num5 = 0;
                float angle;
                if (Settings.Profile.HGrad)
                {
                    angle = 0f;
                }
                else
                {
                    angle = 270f;
                };

                if (wsplit.currentDispMode == DisplayMode.Wide)
                {
                    num5 = wsplit.displaySegsWide();
                    //angle = 90f;
                }
                else
                    num5 = wsplit.detailSegCount();

                if ((wsplit.split.StartDelay != 0) && (wsplit.timer.ElapsedTicks == 0L))
                {
                    span2 = TimeSpan.FromMilliseconds((double)wsplit.split.StartDelay);
                    if (wsplit.startDelay != null)
                    {
                        span2 -= DateTime.UtcNow - wsplit.offsetStartTime;
                    }
                }
                else if (wsplit.split.Done)
                    span2 = TimeSpan.FromSeconds(wsplit.split.LastSegment.LiveTime);
                else
                    span2 = elapsed;

                if (span2.TotalHours >= 100.0)
                {
                    clockTimeAbsSize = MeasureTimeStringMax("888:88:88", (Settings.Profile.DigitalClock) ? wsplit.digitLarge : wsplit.clockLarge, graphics);
                    this.timeStringAbsPart = string.Format("{0:000}:{1:00}:{2:00}", Math.Floor(span2.TotalHours) % 1000.0, span2.Minutes, span2.Seconds);
                }
                else
                {
                    clockTimeAbsSize = MeasureTimeStringMax("88:88:88", (Settings.Profile.DigitalClock) ? wsplit.digitLarge : wsplit.clockLarge, graphics);
                    if (span2.TotalHours >= 1.0)
                        this.timeStringAbsPart = string.Format("{0:0}:{1:00}:{2:00}", Math.Floor(span2.TotalHours), span2.Minutes, span2.Seconds);
                    else if (span2.TotalMinutes >= 1.0)
                        this.timeStringAbsPart = string.Format("{0}:{1:00}", span2.Minutes, span2.Seconds);
                    else
                        this.timeStringAbsPart = string.Format("{0}", span2.Seconds);
                }

                if (Settings.Profile.DigitalClock)
                {
                    // If the refresh interval is greater than 42, only 1 digit is shown after the decimal
                    /*if (wsplit.stopwatch.Interval > 42)
                        this.timeStringAbsPart = this.timeStringAbsPart.PadLeft(9, ' ');*/

                    this.timeStringAbsPart = this.timeStringAbsPart.PadLeft(8, ' ');
                    if (((wsplit.split.StartDelay != 0) && (wsplit.timer.ElapsedTicks == 0L)) && (this.timeStringAbsPart.Substring(0, 1) == " "))
                    {
                        this.timeStringAbsPart = "-" + this.timeStringAbsPart.Substring(1, this.timeStringAbsPart.Length - 1);
                    }
                }
                else if (((wsplit.split.StartDelay != 0) && (wsplit.timer.ElapsedTicks == 0L)) && ((span2.TotalHours < 10.0) || ((span2.TotalHours < 100.0) && (wsplit.stopwatch.Interval > 42))))
                    this.timeStringAbsPart = "-" + this.timeStringAbsPart;

                // If the number of hours is greater or equal to 100 or the refresh interval is greater than 42, show only 1 digit after the decimal
                if ((span2.TotalHours >= 100.0) || (wsplit.stopwatch.Interval > 42))
                {
                    this.clockTimeDecSize = MeasureTimeStringMax("8", (Settings.Profile.DigitalClock) ? wsplit.digitMed : wsplit.clockMed, graphics);
                    this.timeStringDecPart = string.Format("{0:0}", Math.Floor((double)(((double)span2.Milliseconds) / 100.0)));
                }
                else
                {
                    this.clockTimeDecSize = MeasureTimeStringMax("88", (Settings.Profile.DigitalClock) ? wsplit.digitMed : wsplit.clockMed, graphics);
                    this.timeStringDecPart = string.Format("{0:00}", Math.Floor((double)(((double)span2.Milliseconds) / 10.0)));
                }

                clockTimeTotalSize = new SizeF(clockTimeAbsSize.Width + clockTimeDecSize.Width,
                    Math.Max(clockTimeAbsSize.Height, clockTimeDecSize.Height));

                int x = 0;

                if (Settings.Profile.SegmentIcons >= 1)
                    x = (Settings.Profile.SegmentIcons + 1) * 8;

                int num8 = 0;

                if (((x > 16) && (wsplit.currentDispMode == DisplayMode.Compact)) && (wsplit.split.LiveRun && !wsplit.split.Done))
                    num8 = x + 6;

                //clockScale = Math.Min((float)(((float)(wsplit.clockRect.Width - num8)) / 124f), (float)(((float)wsplit.clockRect.Height) / 26f));
                clockScale = Math.Min((wsplit.clockRect.Width - num8) / clockTimeTotalSize.Width, wsplit.clockRect.Height / clockTimeTotalSize.Height);

                Color clockColor = this.clockColor;
                Color dViewClockColor = new Color();
                if (wsplit.timer.IsRunning)
                {
                    if (wsplit.split.LiveRun)
                    {
                        if (wsplit.split.Done)
                        {
                            if ((wsplit.split.LastSegment.LiveTime < wsplit.split.CompTime(wsplit.split.LastIndex)) || (wsplit.split.CompTime(wsplit.split.LastIndex) == 0.0))
                            {
                                clockColor = ColorSettings.Profile.RecordFore;
                                this.clockGrColor = ColorSettings.Profile.RecordBack;
                                this.clockGrColor2 = ColorSettings.Profile.RecordBack2;
                                this.clockPlainColor = ColorSettings.Profile.RecordBackPlain;

                                dViewClockColor = ColorSettings.Profile.UsedDViewRecord;
                            }
                            else
                            {
                                clockColor = ColorSettings.Profile.FinishedFore;
                                this.clockGrColor = ColorSettings.Profile.FinishedBack;
                                this.clockGrColor2 = ColorSettings.Profile.FinishedBack2;
                                this.clockPlainColor = ColorSettings.Profile.FinishedBackPlain;

                                dViewClockColor = ColorSettings.Profile.UsedDViewFinished;
                            }
                        }
                        else if (wsplit.flashDelay != null)
                        {
                            clockColor = ColorSettings.Profile.Flash;
                            dViewClockColor = ColorSettings.Profile.DViewFlash;
                        }
                        else if (wsplit.split.CompTime() == 0.0)
                        {
                            clockColor = ColorSettings.Profile.AheadFore;
                            this.clockGrColor = ColorSettings.Profile.AheadBack;
                            this.clockGrColor2 = ColorSettings.Profile.AheadBack2;
                            this.clockPlainColor = ColorSettings.Profile.AheadBackPlain;

                            dViewClockColor = ColorSettings.Profile.UsedDViewAhead;
                        }
                        else if (elapsed.TotalSeconds < wsplit.split.CompTime())
                        {
                            if (num3 < 0.0)
                            {
                                clockColor = ColorSettings.Profile.AheadFore;
                                this.clockGrColor = ColorSettings.Profile.AheadBack;
                                this.clockGrColor2 = ColorSettings.Profile.AheadBack2;
                                this.clockPlainColor = ColorSettings.Profile.AheadBackPlain;

                                dViewClockColor = ColorSettings.Profile.UsedDViewAhead;
                            }
                            else
                            {
                                clockColor = ColorSettings.Profile.AheadLosingFore;
                                this.clockGrColor = ColorSettings.Profile.AheadLosingBack;
                                this.clockGrColor2 = ColorSettings.Profile.AheadLosingBack2;
                                this.clockPlainColor = ColorSettings.Profile.AheadLosingBackPlain;

                                dViewClockColor = ColorSettings.Profile.UsedDViewAheadLosing;
                            }
                        }
                        else if (num3 < 0.0)
                        {
                            clockColor = ColorSettings.Profile.BehindFore;
                            this.clockGrColor = ColorSettings.Profile.BehindBack;
                            this.clockGrColor2 = ColorSettings.Profile.BehindBack2;
                            this.clockPlainColor = ColorSettings.Profile.BehindBackPlain;

                            dViewClockColor = ColorSettings.Profile.UsedDViewBehind;
                        }
                        else
                        {
                            clockColor = ColorSettings.Profile.BehindLosingFore;
                            this.clockGrColor = ColorSettings.Profile.BehindLosingBack;
                            this.clockGrColor2 = ColorSettings.Profile.BehindLosingBack2;
                            this.clockPlainColor = ColorSettings.Profile.BehindLosingBackPlain;

                            dViewClockColor = ColorSettings.Profile.UsedDViewBehindLosing;
                        }
                    }
                    else
                    {
                        clockColor = ColorSettings.Profile.WatchFore;
                        this.clockGrColor = ColorSettings.Profile.WatchBack;
                        this.clockGrColor2 = ColorSettings.Profile.WatchBack2;
                        this.clockPlainColor = ColorSettings.Profile.WatchBackPlain;
                    }
                }
                else if (wsplit.timer.ElapsedTicks > 0L)
                {
                    clockColor = ColorSettings.Profile.Paused;
                    dViewClockColor = ColorSettings.Profile.UsedDViewPaused;
                }
                else if (wsplit.split.StartDelay != 0)
                {
                    clockColor = ColorSettings.Profile.DelayFore;
                    this.clockGrColor = ColorSettings.Profile.DelayBack;
                    this.clockGrColor2 = ColorSettings.Profile.DelayBack2;
                    this.clockPlainColor = ColorSettings.Profile.DelayBackPlain;

                    dViewClockColor = ColorSettings.Profile.UsedDViewDelay;
                }
                else if (wsplit.split.LiveRun)
                {
                    clockColor = ColorSettings.Profile.AheadFore;
                    this.clockGrColor = ColorSettings.Profile.AheadBack;
                    this.clockGrColor2 = ColorSettings.Profile.AheadBack2;
                    this.clockPlainColor = ColorSettings.Profile.AheadBackPlain;

                    dViewClockColor = ColorSettings.Profile.UsedDViewAhead;
                }
                else
                {
                    clockColor = ColorSettings.Profile.WatchFore;
                    this.clockGrColor = ColorSettings.Profile.WatchBack;
                    this.clockGrColor2 = ColorSettings.Profile.WatchBack2;
                    this.clockPlainColor = ColorSettings.Profile.WatchBackPlain;

                    dViewClockColor = ColorSettings.Profile.UsedDViewAhead;
                }

                if (clockColor != this.clockColor)
                {
                    this.RequestBackgroundRedraw();
                    this.clockColor = clockColor;
                }

                Brush brush = new SolidBrush(this.clockColor);
                wsplit.dview.clockColor = new SolidBrush(dViewClockColor);

                if (span2.TotalHours >= 100.0)
                    wsplit.dview.clockText = string.Format("{0:000}:{1:00}:{2:00.00}", Math.Floor((double)(span2.TotalHours % 1000.0)), span2.Minutes, span2.Seconds + (Math.Floor((double)(((float)span2.Milliseconds) / 10f)) / 100.0));
                else if (span2.TotalHours >= 1.0)
                    wsplit.dview.clockText = string.Format("{0:0}:{1:00}:{2:00.00}", Math.Floor((double)(span2.TotalHours % 1000.0)), span2.Minutes, span2.Seconds + (Math.Floor((double)(((float)span2.Milliseconds) / 10f)) / 100.0));
                else
                    wsplit.dview.clockText = string.Format("{0:00}:{1:00.00}", span2.Minutes, span2.Seconds + (Math.Floor((double)(((float)span2.Milliseconds) / 10f)) / 100.0));

                wsplit.dview.Invalidate();

                Rectangle layoutRectangle = new Rectangle();
                Rectangle rectangle2 = new Rectangle();
                StringFormat format = new StringFormat();
                StringFormat format2 = new StringFormat();

                string text = "";
                string str4 = "";

                double num10 = 0.0;
                Brush white = Brushes.White;

                if (wsplit.currentDispMode != DisplayMode.Timer)
                {
                    if (wsplit.currentDispMode == DisplayMode.Wide)
                    {
                        layoutRectangle = new Rectangle(wsplit.Width - 119, 2, 119, wsplit.Height / 2);
                        rectangle2 = new Rectangle(wsplit.Width - 119, layoutRectangle.Bottom - 2, 119, wsplit.Height / 2);
                    }
                    else if (wsplit.currentDispMode == DisplayMode.Detailed)
                    {
                        layoutRectangle = new Rectangle(0, 0, 0, 0);
                        rectangle2 = new Rectangle(1, wsplit.clockRect.Bottom + 2, wsplit.Width - 1, 16);
                    }
                    else
                    {
                        rectangle2 = new Rectangle(1, 2, wsplit.Width - 1, 13);
                        layoutRectangle = new Rectangle(1, wsplit.clockRect.Bottom + 2, wsplit.Width - 1, 14);
                        if ((Settings.Profile.SegmentIcons > 0) && !wsplit.split.Done)
                        {
                            rectangle2.Width -= 2 + x;
                            rectangle2.X += 2 + x;
                        }
                    }

                    format.LineAlignment = StringAlignment.Center;
                    format.Trimming = StringTrimming.EllipsisCharacter;
                    format.Alignment = StringAlignment.Far;
                    format2 = (StringFormat)format.Clone();

                    if (wsplit.currentDispMode == DisplayMode.Compact)
                        format2.Alignment = StringAlignment.Near;

                    if (num3 <= 0.0)
                        this.segLosingTime = false;

                    if (wsplit.timer.ElapsedTicks != 0L)
                    {
                        if (wsplit.split.Done)
                        {
                            if (wsplit.split.CompTime(wsplit.split.LastIndex) != 0.0)
                            {
                                num10 = wsplit.split.SegDelta(wsplit.split.LastSegment.LiveTime, wsplit.split.LastIndex);
                                text = wsplit.timeFormatter(num10, TimeFormat.DeltaShort);
                            }
                        }
                        // If we are losing time on the current segment...
                        else if (num3 > 0.0)
                        {
                            num10 = num3;   // The number to be written becomes the current segment delta
                            text = wsplit.timeFormatter(num10, TimeFormat.DeltaShort);

                            // If we just started losing time, we indicate we are and we ask for a redraw of the timer background since color has changed
                            if (!this.segLosingTime)
                            {
                                this.segLosingTime = true;
                                this.RequestBackgroundRedraw();
                            }
                        }

                        // If we aren't losing time on the current segment and if the current segment isn't the first segment...
                        else if (wsplit.split.LiveIndex > 0)
                        {
                            // The number to be written becomes the previous segment delta:
                            num10 = wsplit.split.SegDelta(wsplit.split.segments[wsplit.split.LiveIndex - 1].LiveTime, wsplit.split.LiveIndex - 1);

                            // Though, if the previous split was skipped or if the previous split had no time, we don't write anything...
                            // wsplit will probably change.
                            if ((wsplit.split.segments[wsplit.split.LiveIndex - 1].LiveTime > 0.0) && (wsplit.split.CompTime(wsplit.split.LiveIndex - 1) != 0.0))
                            {
                                text = wsplit.timeFormatter(num10, TimeFormat.DeltaShort);
                            }
                        }
                    }

                    // If we're not in the Detailed display mode
                    if (wsplit.currentDispMode != DisplayMode.Detailed)
                    {
                        // If splits are done
                        if (wsplit.split.Done)
                        {
                            if (wsplit.split.CompTime(wsplit.split.LastIndex) != 0.0)
                            {
                                double num11 = wsplit.split.RunDeltaAt(wsplit.split.LastIndex);
                                str4 = wsplit.timeFormatter(num11, TimeFormat.Delta);
                                if (num11 < 0.0)
                                {
                                    white = new SolidBrush(ColorSettings.Profile.RecordFore);
                                }
                                else
                                {
                                    white = new SolidBrush(ColorSettings.Profile.FinishedFore);
                                }
                            }
                        }

                        // If we are losing time on the current segment...
                        else if (num3 > 0.0)
                        {
                            // Format the run delta in str4
                            str4 = wsplit.timeFormatter(secs, TimeFormat.Delta);

                            if (secs < 0.0) // If ahead overall | Is wsplit part of the code even useful?
                            {
                                white = new SolidBrush(ColorSettings.Profile.SegAheadGain);
                            }
                            else            // If behind overall
                            {
                                white = new SolidBrush(ColorSettings.Profile.SegBehindLoss);
                            }
                        }

                        // If there is a previous split time and there is a time to compare it to...
                        else if (((wsplit.split.LiveIndex > 0) && (wsplit.split.segments[wsplit.split.LiveIndex - 1].LiveTime > 0.0)) && (wsplit.split.CompTime(wsplit.split.LiveIndex - 1) != 0.0))
                        {
                            // Stores the run delta at the previous split in num12
                            double num12 = wsplit.split.RunDeltaAt(wsplit.split.LiveIndex - 1);
                            str4 = wsplit.timeFormatter(num12, TimeFormat.Delta);
                            if (num12 < 0.0)
                            {
                                white = new SolidBrush(ColorSettings.Profile.SegAheadLoss);
                            }
                            else
                            {
                                white = new SolidBrush(ColorSettings.Profile.SegBehindGain);
                            }
                        }
                    }

                    if (text.Length != this.segDelLength)
                    {
                        this.segDelLength = text.Length;
                        this.segDeltaWidth = MeasureDisplayStringWidth(text, wsplit.timeFont);
                        this.RequestBackgroundRedraw();
                    }

                    if (str4.Length != this.runDelLength)
                    {
                        this.runDelLength = str4.Length;
                        this.runDeltaWidth = MeasureDisplayStringWidth(str4, wsplit.timeFont);
                        this.RequestBackgroundRedraw();
                    }
                }

                int num13 = 0;
                int num14 = (wsplit.split.LiveIndex - num5) + 2;
                int liveIndex = wsplit.split.LiveIndex;
                if (num5 >= 2)
                {
                    liveIndex--;
                }
                if (((wsplit.currentDispMode == DisplayMode.Detailed) && Settings.Profile.ShowLastDetailed) || ((wsplit.currentDispMode == DisplayMode.Wide) && Settings.Profile.ShowLastWide))
                {
                    num14++;
                }
                num13 = Math.Max(0, Math.Min(liveIndex, Math.Min((wsplit.split.LastIndex - num5) + 1, num14)));
                Rectangle rectangle3 = new Rectangle();
                Color timeColor = wsplit.split.CurrentSegment.TimeColor;
                string timeString = wsplit.split.CurrentSegment.TimeString;

                // If in wide or detailed desplay mode and if not done...
                if (((wsplit.currentDispMode == DisplayMode.Wide) || (wsplit.currentDispMode == DisplayMode.Detailed)) && !wsplit.split.Done)
                {
                    this.runLosingTime = false; // Not losing time... ?

                    // If there is a time to compare to and one current segment delta or run delta is greater than 0.0
                    if ((wsplit.split.CompTime() > 0.0) && ((num3 > 0.0) || (secs > 0.0)))
                    {
                        this.runLosingTime = true;  // Losing time...
                        // Formats run delta in timeString:
                        //timeString = wsplit.timeFormatter(secs, TimeFormat.Delta);
                    }
                    // If losing time and in detailed display mode, and if Segment height (so Icon size) is smaller or equal to 24 pixels and length has changed...
                    if ((this.runLosingTime && (wsplit.currentDispMode == DisplayMode.Detailed)) && ((wsplit.segHeight <= 0x18) && (timeString.Length != this.segTimeLength)))
                    {
                        this.segTimeLength = timeString.Length;
                        this.segTimeWidth = MeasureDisplayStringWidth(timeString, wsplit.timeFont);
                        this.RequestBackgroundRedraw();
                    }

                    // If in the Wide display mode...
                    if (wsplit.currentDispMode == DisplayMode.Wide)
                        rectangle3 = new Rectangle((wsplit.clockRect.Right + (wsplit.wideSegWidth * (wsplit.split.LiveIndex - num13))) + 4, wsplit.Height / 2, (wsplit.wideSegWidth - x) - 2, wsplit.Height / 2);
                    // Otherwise...
                    else
                    {
                        // Builds a rectangle for the live segment time
                        rectangle3 = new Rectangle(x, wsplit.segHeight * (wsplit.split.LiveIndex - num13), wsplit.Width - x, wsplit.segHeight);
                        // Moves the rectangle down if we have to show the run title

                        if (((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle) && ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal))
                        {
                            rectangle3.Y += 0x20;
                        }
                        else if ((wsplit.split.RunTitle != "") && Settings.Profile.ShowTitle)
                        {
                            rectangle3.Y += 0x12;
                        }
                        else if ((wsplit.split.RunGoal != "") && Settings.Profile.ShowGoal)
                        {
                            rectangle3.Y += 0x12;
                        }
                        // If greater than 24 pixels icons
                        if (wsplit.segHeight > 0x18)
                        {
                            // Goes to the second half of the rectangle
                            rectangle3.Height /= 2;
                            rectangle3.Y += rectangle3.Height;
                        }
                        // Otherwise, moves down 1 pixel.
                        else
                        {
                            rectangle3.Y++;
                        }
                    }
                }

                // Temporary signature of the method
                this.DrawBackground(graphics, angle, num5, num8, num13, x);

                // Code for drawing clock fore has been moved to another function. Current method's signature is temporary
                this.DrawClockFore(graphics, timeStringAbsPart, ref timeStringDecPart, flag, num8, ref brush);

                if (wsplit.currentDispMode != DisplayMode.Timer)
                {
                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    //graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    // At this point...
                    // num3 -> Current segment delta
                    // num10 -> What has to be written in the status bar

                    // If the time to write in the status bar is a new best segment...
                    /* apparently this always runs m8 */
                    if (Settings.Profile.ShowPrevSeg)
                    {
                        if (!this.segLosingTime && wsplit.split.LiveIndex > 0 &&
                            wsplit.split.LiveSegment(wsplit.split.LiveIndex - 1) != 0.0 && (wsplit.split.segments[wsplit.split.LiveIndex - 1].BestSegment == 0.0 ||
                            wsplit.split.LiveSegment(wsplit.split.LiveIndex - 1) < wsplit.split.segments[wsplit.split.LiveIndex - 1].BestSegment))
                        {
                            if (Settings.Profile.RainbowSplits)
                            {
                                graphics.DrawString(text, wsplit.timeFont, new SolidBrush(ColorSettings.Profile.SegRainbow), rectangle2, format);
                            }
                            else
                            {
                                graphics.DrawString(text, wsplit.timeFont, new SolidBrush(ColorSettings.Profile.SegBestSegment), rectangle2, format);
                            };
                        }
                        // Else, if The time to write in the status bar is a time loss...
                        else if (num10 > 0.0)
                        {
                            graphics.DrawString(text, wsplit.timeFont, new SolidBrush(ColorSettings.Profile.SegBehindLoss), rectangle2, format);
                        }
                        // Otherwise (the time is a time gain but not a best segment), or there is not time to write...
                        else
                        {
                            graphics.DrawString(text, wsplit.timeFont, new SolidBrush(ColorSettings.Profile.SegAheadGain), rectangle2, format);
                        }
                    };
                    if (wsplit.currentDispMode != DisplayMode.Detailed)
                    {
                        graphics.DrawString(str4, wsplit.timeFont, white, layoutRectangle, format2);
                    }
                }
                if (((wsplit.currentDispMode == DisplayMode.Wide) || (wsplit.currentDispMode == DisplayMode.Detailed)) && !wsplit.split.Done)
                {
                    StringFormat format9 = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center
                    };

                    if (wsplit.currentDispMode == DisplayMode.Wide)
                        format9.Alignment = StringAlignment.Near;
                    else
                        format9.Alignment = StringAlignment.Far;

                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    //graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    graphics.DrawString(timeString, wsplit.timeFont, new SolidBrush(timeColor), rectangle3, format9);
                }
            }

            private void DrawClockBack(float angle, int num8, Graphics bgGraphics)
            {
                if (!Settings.Profile.BackgroundBlack)
                {
                    if (Settings.Profile.BackgroundPlain)
                        bgGraphics.FillRectangle(new SolidBrush(this.clockPlainColor), wsplit.clockRect);
                    else
                    {
                        bgGraphics.FillRectangle(new LinearGradientBrush(wsplit.clockRect, this.clockGrColor, this.clockGrColor2, angle), wsplit.clockRect);
                        if ((angle == 0f) && Settings.Profile.ClockAccent)
                            bgGraphics.FillRectangle(new SolidBrush(Color.FromArgb(0x56, this.clockGrColor2)), wsplit.clockRect.X, wsplit.clockRect.Y, wsplit.clockRect.Width, wsplit.clockRect.Height / 2);

                        if (Settings.Profile.DigitalClock)
                        {
                            bgGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            bgGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                            bgGraphics.SmoothingMode = SmoothingMode.HighQuality;

                            // Change according to what was done in that DrawClockFore method
                            //bgGraphics.TranslateTransform(num8 + (((wsplit.clockRect.Width - (clockScale * 124f)) - num8) / 2f), wsplit.clockRect.Top + ((wsplit.clockRect.Height - (clockScale * 26f)) / 2f));
                            //bgGraphics.ScaleTransform(clockScale, clockScale);


                            bgGraphics.TranslateTransform(num8 + ((wsplit.clockRect.Width - (this.clockScale * this.clockTimeTotalSize.Width) - num8) / 2f),
                                wsplit.clockRect.Top + ((wsplit.clockRect.Height - (this.clockScale * this.clockTimeTotalSize.Height)) / 2f));
                            bgGraphics.ScaleTransform(clockScale, clockScale);

                            Brush brush4 = new SolidBrush(Color.FromArgb(86, this.clockGrColor2));
                            bgGraphics.DrawString("88:88:88".PadLeft(timeStringAbsPart.Length, '8'), wsplit.digitLarge, brush4, 0f, 0.15f);

                            if (Settings.Profile.ShowDecimalSeparator)
                            {
                                bgGraphics.DrawString(wsplit.decimalChar, wsplit.digitMed, brush4, (112 - clockTimeDecSize.Width), 4.5f);
                                bgGraphics.DrawString("".PadRight(this.timeStringDecPart.Length, '8'), wsplit.digitMed, brush4, (119 - clockTimeDecSize.Width), 4.5f);
                            }

                            else
                                bgGraphics.DrawString("".PadRight(this.timeStringDecPart.Length, '8'), wsplit.digitMed, brush4, (117.5f - clockTimeDecSize.Width), 4.5f);

                            bgGraphics.ResetTransform();
                        }
                    }
                }
            }

            // Current methods signature is temporary. Most of the parameters will temporary become class members or properties.
            private void DrawClockFore(Graphics graphics, string timeStringAbsPart, ref string timeStringDecPart, bool inaccuraciesDetected, int num8, ref Brush brush)
            {
                // Transforms the Graphics object in order to draw the clock time full-sized
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                //graphics.TranslateTransform(num8 + (((wsplit.clockRect.Width - (clockScale * 124f)) - num8) / 2f), wsplit.clockRect.Top + ((wsplit.clockRect.Height - (clockScale * 26f)) / 2f));
                graphics.TranslateTransform(num8 + ((wsplit.clockRect.Width - (this.clockScale * this.clockTimeTotalSize.Width) - num8) / 2f),
                    wsplit.clockRect.Top + ((wsplit.clockRect.Height - (this.clockScale * this.clockTimeTotalSize.Height)) / 2f));
                graphics.ScaleTransform(clockScale, clockScale);

                // If using DigitalClock font
                if (Settings.Profile.DigitalClock)
                {
                    // The drawing of the digital clock font could have been done dynamically, but since the result wasn't
                    // exactly centered vertically, I decided to hardcode values that give the best looking result.
                    // Since this option is used by many, I had to make sure it looked nice.
                    graphics.DrawString(timeStringAbsPart, wsplit.digitLarge, brush, 0f, 0.15f);

                    if (inaccuraciesDetected)
                    {
                        timeStringDecPart = "".PadRight(timeStringDecPart.Length, '?');
                        brush = new SolidBrush(ColorSettings.Profile.Flash);
                    }

                    if (Settings.Profile.ShowDecimalSeparator)
                    {
                        graphics.DrawString(wsplit.decimalChar, wsplit.digitMed, brush, clockTimeAbsSize.Width - 7, 4.5f);
                        graphics.DrawString(timeStringDecPart, wsplit.digitMed, brush, clockTimeAbsSize.Width, 4.5f);
                        /*graphics.DrawString(wsplit.decimalChar, wsplit.digitMed, brush, (112 - clockTimeDecSize.Width), 4.5f);
                        graphics.DrawString(timeStringDecPart, wsplit.digitMed, brush, (119 - clockTimeDecSize.Width), 4.5f);*/

                    }
                    else
                        graphics.DrawString(timeStringDecPart, wsplit.digitMed, brush, (clockTimeAbsSize.Width), 4.5f);
                    //graphics.DrawString(timeStringDecPart, wsplit.digitMed, brush, (117.5f - clockTimeDecSize.Width), 4.5f);
                }

                // If the font used for the clock is any font but the default digital clock font, the display is done dynamically, according to font measurements. 
                else
                {
                    // Calculates de relative baseline height of both fonts used in the display so that they can be aligned correctly
                    float largeBaseline = wsplit.clockLarge.Size * wsplit.clockLarge.FontFamily.GetCellAscent(wsplit.clockLarge.Style) / wsplit.clockLarge.FontFamily.GetEmHeight(wsplit.clockLarge.Style);
                    float mediumBaseline = wsplit.clockMed.Size * wsplit.clockMed.FontFamily.GetCellAscent(wsplit.clockMed.Style) / wsplit.clockMed.FontFamily.GetEmHeight(wsplit.clockMed.Style);

                    RectangleF clockTimeAbsRectF = new RectangleF(0f, 0f, this.clockTimeAbsSize.Width, this.clockTimeAbsSize.Height);
                    RectangleF clockTimeDecRectF;

                    if (Settings.Profile.ShowDecimalSeparator)
                    {
                        clockTimeDecRectF = new RectangleF(clockTimeAbsRectF.Right - 7, clockTimeAbsRectF.Top + (largeBaseline - mediumBaseline), this.clockTimeDecSize.Width, this.clockTimeDecSize.Height);

                        graphics.DrawString(wsplit.decimalChar, wsplit.clockMed, brush, clockTimeDecRectF);
                        clockTimeDecRectF.X = clockTimeAbsRectF.Right;
                    }
                    else
                        clockTimeDecRectF = new RectangleF(clockTimeAbsRectF.Right - 2, clockTimeAbsRectF.Top + (largeBaseline - mediumBaseline), this.clockTimeDecSize.Width, this.clockTimeDecSize.Height);

                    StringFormat format = new StringFormat { Alignment = StringAlignment.Far };
                    graphics.DrawString(timeStringAbsPart, wsplit.clockLarge, brush, clockTimeAbsRectF, format);

                    if (inaccuraciesDetected)
                    {
                        timeStringDecPart = "".PadRight(timeStringDecPart.Length, '?');
                        brush = new SolidBrush(ColorSettings.Profile.Flash);
                    }

                    format.Alignment = StringAlignment.Near;
                    graphics.DrawString(timeStringDecPart, wsplit.clockMed, brush, clockTimeDecRectF, format);
                }

                graphics.ResetTransform();
            }

            private static SizeF MeasureTimeStringMax(string timeString, Font font, Graphics graphics)
            {
                SizeF max = new SizeF(0, font.Height);
                for (int i = 0; i <= 9; ++i)
                {
                    SizeF temp = graphics.MeasureString(timeString.Replace('8', (char)(i + '0')), font);
                    if (temp.Width > max.Width)
                        max.Width = temp.Width;
                }

                return max;
            }
        }
    }
}

