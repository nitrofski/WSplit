namespace WSplitTimer.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings)SettingsBase.Synchronized(new Settings()));

        public static Settings Profile
        {
            get { return defaultInstance; }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("True")]
        public bool BestAsOverall
        {
            get { return (bool) this["BestAsOverall"]; }
            set { this["BestAsOverall"] = value; }
        }

        [DefaultSettingValue("True"), DebuggerNonUserCode, UserScopedSetting]
        public bool ClockAccent
        {
            get { return (bool) this["ClockAccent"]; }
            set { this["ClockAccent"] = value; }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("240, 50")]
        public Size ClockSize
        {
            get { return (Size) this["ClockSize"]; }
            set { this["ClockSize"] = value; }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("0")]
        public int CompareAgainst
        {
            get { return (int) this["CompareAgainst"]; }
            set { this["CompareAgainst"] = value; }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("True")]
        public bool DigitalClock
        {
            get { return (bool) this["DigitalClock"]; }
            set { this["DigitalClock"] = value; }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("False")]
        public bool DisplayBlankSegs
        {
            get { return (bool) this["DisplayBlankSegs"]; }
            set { this["DisplayBlankSegs"] = value; }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("3")]
        public int DisplayMode
        {
            get { return (int) this["DisplayMode"]; }
            set { this["DisplayMode"] = value; }
        }

        [UserScopedSetting, DefaultSettingValue("10"), DebuggerNonUserCode]
        public int DisplaySegs
        {
            get { return (int) this["DisplaySegs"]; }
            set { this["DisplaySegs"] = value; }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("500")]
        public int DoubleTapGuard
        {
            get { return (int) this["DoubleTapGuard"]; }
            set { this["DoubleTapGuard"] = value; }
        }

        [UserScopedSetting, DefaultSettingValue("False"), DebuggerNonUserCode]
        public bool DViewDeltaMarks
        {
            get { return (bool) this["DViewDeltaMarks"]; }
            set { this["DViewDeltaMarks"] = value; }
        }

        [DefaultSettingValue("False"), DebuggerNonUserCode, UserScopedSetting]
        public bool DViewOnTop
        {
            get { return (bool) this["DViewOnTop"]; }
            set { this["DViewOnTop"] = value; }
        }

        [DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
        public bool DViewShowOld
        {
            get { return (bool) this["DViewShowOld"]; }
            set { this["DViewShowOld"] = value; }
        }

        [DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
        public bool DViewShowBest
        {
            get { return (bool)this["DViewShowBest"]; }
            set { this["DViewShowBest"] = value; }
        }

        [DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
        public bool DViewShowSumOfBests
        {
            get { return (bool)this["DViewShowSumOfBests"]; }
            set { this["DViewShowSumOfBests"] = value; }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("True")]
        public bool DViewShowComp
        {
            get { return (bool) this["DViewShowComp"]; }
            set { this["DViewShowComp"] = value; }
        }

        [UserScopedSetting, DefaultSettingValue("True"), DebuggerNonUserCode]
        public bool DViewShowDeltas
        {
            get { return (bool) this["DViewShowDeltas"]; }
            set { this["DViewShowDeltas"] = value; }
        }

        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool DViewShowLive
        {
            get { return (bool) this["DViewShowLive"]; }
            set { this["DViewShowLive"] = value; }
        }

        [UserScopedSetting, DefaultSettingValue("False"), DebuggerNonUserCode]
        public bool DViewShowSegs
        {
            get { return (bool) this["DViewShowSegs"]; }
            set { this["DViewShowSegs"] = value; }
        }

        [DefaultSettingValue("2"), DebuggerNonUserCode, UserScopedSetting]
        public int FallbackPreference
        {
            get { return (int) this["FallbackPreference"]; }
            set { this["FallbackPreference"] = value; }
        }

        [DebuggerNonUserCode, DefaultSettingValue("True"), UserScopedSetting]
        public bool FirstRun
        {
            get { return (bool) this["FirstRun"]; }
            set { this["FirstRun"] = value; }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string LastFile
        {
            get { return (string) this["LastFile"]; }
            set { this["LastFile"] = value; }
        }

        [DebuggerNonUserCode, DefaultSettingValue("True"), UserScopedSetting]
        public bool LoadMostRecent
        {
            get { return (bool) this["LoadMostRecent"]; }
            set { this["LoadMostRecent"] = value; }
        }

        [DefaultSettingValue("False"), DebuggerNonUserCode, UserScopedSetting]
        public bool OnTop
        {
            get { return (bool) this["OnTop"]; }
            set { this["OnTop"] = value; }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("1")]
        public double Opacity
        {
            get { return (double) this["Opacity"]; }
            set { this["Opacity"] = value; }
        }

        [UserScopedSetting, DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />"), DebuggerNonUserCode]
        public StringCollection RecentFiles
        {
            get { return (StringCollection) this["RecentFiles"]; }
            set { this["RecentFiles"] = value; }
        }

        [UserScopedSetting, DefaultSettingValue("15"), DebuggerNonUserCode]
        public int RefreshRate
        {
            get { return (int) this["RefreshRate"]; }
            set { this["RefreshRate"] = value; }
        }

        [DefaultSettingValue("True"), DebuggerNonUserCode, UserScopedSetting]
        public bool SaveWindowPos
        {
            get { return (bool) this["SaveWindowPos"]; }
            set { this["SaveWindowPos"] = value; }
        }

        [DefaultSettingValue("0"), DebuggerNonUserCode, UserScopedSetting]
        public int SegmentIcons
        {
            get { return (int) this["SegmentIcons"]; }
            set { this["SegmentIcons"] = value; }
        }

        [DefaultSettingValue("False"), DebuggerNonUserCode, UserScopedSetting]
        public bool ShowAttempts
        {
            get { return (bool) this["ShowAttempts"]; }
            set { this["ShowAttempts"] = value; }
        }

        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool ShowDecimalSeparator
        {
            get { return (bool) this["ShowDecimalSeparator"]; }
            set { this["ShowDecimalSeparator"] = value; }
        }

        [DebuggerNonUserCode, DefaultSettingValue("True"), UserScopedSetting]
        public bool ShowLastDetailed
        {
            get { return (bool) this["ShowLastDetailed"]; }
            set { this["ShowLastDetailed"] = value; }
        }

        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool ShowLastWide
        {
            get { return (bool) this["ShowLastWide"]; }
            set { this["ShowLastWide"] = value; }
        }

        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool ShowTitle
        {
            get { return (bool) this["ShowTitle"]; }
            set { this["ShowTitle"] = value; }
        }

        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool ShowGoal
        {
            get { return (bool)this["ShowGoal"];  }
            set { this["ShowGoal"] = value; }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("False")]
        public bool WideSegBlanks
        {
            get { return (bool) this["WideSegBlanks"]; }
            set { this["WideSegBlanks"] = value; }
        }

        [DefaultSettingValue("3"), UserScopedSetting, DebuggerNonUserCode]
        public int WideSegs
        {
            get { return (int) this["WideSegs"]; }
            set { this["WideSegs"] = value; }
        }

        [DefaultSettingValue("20, 20"), UserScopedSetting, DebuggerNonUserCode]
        public Point WindowPosition
        {
            get { return (Point) this["WindowPosition"]; }
            set { this["WindowPosition"] = value; }
        }

        [DefaultSettingValue("Calibri"), UserScopedSetting, DebuggerNonUserCode]
        public String FontFamilySegments
        {
            get { return (string)this["FontFamilySegments"]; }
            set { this["FontFamilySegments"] = value; }
        }

        [DefaultSettingValue("1"), UserScopedSetting, DebuggerNonUserCode]
        public float FontMultiplierSegments
        {
            get { return (float)this["FontMultiplierSegments"]; }
            set { this["FontMultiplierSegments"] = value; }
        }

        [DefaultSettingValue("Microsoft Sans Serif"), UserScopedSetting, DebuggerNonUserCode]
        public String FontFamilyDView
        {
            get { return (string)this["FontFamilyDView"]; }
            set { this["FontFamilyDView"] = value; }
        }

        //
        // Hotkey related settings
        //
        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("False")]
        public bool EnabledHotkeys
        {
            get { return (bool)this["EnabledHotkeys"]; }
            set { this["EnabledHotkeys"] = value; }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("None")]
        public Keys HotkeyToggleKey
        {
            get { return (Keys)this["HotkeyToggleKey"]; }
            set { this["HotkeyToggleKey"] = value; }
        }

        [DebuggerNonUserCode, DefaultSettingValue("None"), UserScopedSetting]
        public Keys NextKey
        {
            get { return (Keys)this["NextKey"]; }
            set { this["NextKey"] = value; }
        }

        [DefaultSettingValue("None"), UserScopedSetting, DebuggerNonUserCode]
        public Keys PauseKey
        {
            get { return (Keys)this["PauseKey"]; }
            set { this["PauseKey"] = value; }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("None")]
        public Keys PrevKey
        {
            get { return (Keys)this["PrevKey"]; }
            set { this["PrevKey"] = value; }
        }

        [DefaultSettingValue("None"), UserScopedSetting, DebuggerNonUserCode]
        public Keys ResetKey
        {
            get { return (Keys)this["ResetKey"]; }
            set { this["ResetKey"] = value; }
        }

        [DefaultSettingValue("None"), UserScopedSetting, DebuggerNonUserCode]
        public Keys SplitKey
        {
            get { return (Keys)this["SplitKey"]; }
            set { this["SplitKey"] = value; }
        }

        [DefaultSettingValue("None"), UserScopedSetting, DebuggerNonUserCode]
        public Keys StopKey
        {
            get { return (Keys)this["StopKey"]; }
            set { this["StopKey"] = value; }
        }

        [DefaultSettingValue("None"), UserScopedSetting, DebuggerNonUserCode]
        public Keys CompTypeKey
        {
            get { return (Keys)this["CompTypeKey"]; }
            set { this["CompTypeKey"] = value; }
        }

        //
        // Background related settings
        //

        [DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
        public bool BackgroundBlack
        {
            get { return (bool)this["BackgroundBlack"]; }
            set { this["BackgroundBlack"] = value; }
        }

        [DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
        public bool BackgroundPlain
        {
            get { return (bool)this["BackgroundPlain"]; }
            set { this["BackgroundPlain"] = value; }
        }

        [DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
        public bool BackgroundImage
        {
            get { return (bool)this["BackgroundImage"]; }
            set { this["BackgroundImage"] = value; }
        }

        [DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
        public String BackgroundImageFilename
        {
            get { return (String)this["BackgroundImageFilename"]; }
            set { this["BackgroundImageFilename"] = value; }
        }

        [DefaultSettingValue("0, 0, 0, 0"), UserScopedSetting, DebuggerNonUserCode]
        public Rectangle BackgroundImageSelection
        {
            get { return (Rectangle)this["BackgroundImageSelection"]; }
            set { this["BackgroundImageSelection"] = value; }
        }

        [DefaultSettingValue("100"), UserScopedSetting, DebuggerNonUserCode]
        public int BackgroundOpacity
        {
            get { return (int)this["BackgroundOpacity"]; }
            set { this["BackgroundOpacity"] = value; }
        }
    }
}

