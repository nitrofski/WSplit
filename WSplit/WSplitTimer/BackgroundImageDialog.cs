using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using WSplitTimer.Properties;

namespace WSplitTimer
{
    public partial class BackgroundImageDialog : Form
    {
        private WSplit wsplit;

        Bitmap image;
        RectangleF imageDisplayRectangle;
        Timer animationTimer;
        int currentFrame;

        Rectangle selectionRectangle;
        RectangleF selectionDisplayRectangle;

        float scale;

        bool imageLoaded = false;
        String imageFilename = "";

        Brush grayScreen = new SolidBrush(Color.FromArgb(128, Color.LightGray));

        // Members related to the PictureBox events management
        bool scrolling = false;
        Point scrollingCursorStartPoint;
        PointF scrollingImageStartPoint;
        PointF scrollingSelectionStartPoint;

        bool moving = false;
        Point movingCursorStartPoint;
        Point movingUnscaledSelectionStartPoint;

        bool resizingLeft = false;
        bool resizingRight = false;
        bool resizingTop = false;
        bool resizingBottom = false;
        Rectangle resizingSelectionStartRect;

        bool overLeft = false;
        bool overRight = false;
        bool overTop = false;
        bool overBottom = false;

        public BackgroundImageDialog()
        {
            InitializeComponent();

            // The following prevents the mouse wheel event from having any effect on the trackbars:
            this.trackBarZoom.MouseWheel += ((o, e) => ((HandledMouseEventArgs)e).Handled = true);
            this.trackBarOpacity.MouseWheel += ((o, e) => ((HandledMouseEventArgs)e).Handled = true);

            this.picBoxImageSelectionModifier.MouseWheel += picBoxImageSelectionModifier_MouseWheel;
        }

        public DialogResult ShowDialog(Form caller, WSplit wsplit)
        {
            this.wsplit = wsplit;
            
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
            this.checkBoxUseImageBg.Checked = Settings.Profile.BackgroundImage;
            this.changeUsedImage(Settings.Profile.BackgroundImageFilename, false);
            this.selectionRectangle = Settings.Profile.BackgroundImageSelection;
            if (this.image != null)
            {
                if (this.selectionRectangle == Rectangle.Empty)
                    this.ResetSelection();
                this.FitZoom();
            }

            this.trackBarOpacity.Value = Settings.Profile.BackgroundOpacity;
            if (Settings.Profile.BackgroundPlain)
                this.radioButtonPlain.Checked = true;
            else if (Settings.Profile.BackgroundBlack)
                this.radioButtonBlack.Checked = true;
            else
                this.radioButtonDefault.Checked = true;
        }

        private void RestoreDefaults()
        {
            Settings.Profile.Reset();
            Settings.Profile.FirstRun = false;
            this.PopulateSettings();
        }

        public void ApplyChanges()
        {
            Settings.Profile.BackgroundImage = this.checkBoxUseImageBg.Checked;
            Settings.Profile.BackgroundImageFilename = this.imageFilename;
            Settings.Profile.BackgroundImageSelection = this.selectionRectangle;

            Settings.Profile.BackgroundOpacity = this.trackBarOpacity.Value;
            Settings.Profile.BackgroundPlain = this.radioButtonPlain.Checked;
            Settings.Profile.BackgroundBlack = this.radioButtonBlack.Checked;
        }

        private void changeUsedImage(string filename, bool newSelection = true)
        {
            // Image is gonna be changed.
            // The current image and values related to it are set back to default values
            this.image = null;
            this.imageLoaded = false;

            if (this.animationTimer != null)
            {
                this.animationTimer.Dispose();
                this.animationTimer = null;
                this.currentFrame = 0;
            }

            if (filename == "")
            {
                this.imageFilename = filename;
                this.textBoxImagePath.ForeColor = SystemColors.WindowText;
                this.textBoxImagePath.Text = "No image selected";

                this.labelZoom.Enabled = false;
                this.labelZoomDisplay.Enabled = false;
                this.trackBarZoom.Enabled = false;
                this.buttonZoomFit.Enabled = false;
                this.buttonAutoSelect.Enabled = false;
                this.buttonResetSelect.Enabled = false;
                this.picBoxImageSelectionModifier.Enabled = false;
            }
            else
            {
                try
                {
                    this.image = new Bitmap(filename);
                    this.imageFilename = filename;
                    this.textBoxImagePath.ForeColor = SystemColors.WindowText;
                    this.textBoxImagePath.Text = filename;

                    // If it's an animated image, sets up the animation correctly.
                    if (this.image.FrameDimensionsList.Any(fd => fd.Equals(FrameDimension.Time.Guid))
                        && this.image.GetFrameCount(FrameDimension.Time) > 1)
                    {
                        PropertyItem gifDelay = this.image.GetPropertyItem(0x5100);

                        animationTimer = new Timer();
                        animationTimer.Interval = BitConverter.ToInt16(gifDelay.Value, 0) * 10;
                        animationTimer.Tick += (o, e) =>
                            {
                                ++this.currentFrame;
                                if (this.currentFrame >= this.image.GetFrameCount(FrameDimension.Time))
                                    this.currentFrame = 0;

                                this.image.SelectActiveFrame(FrameDimension.Time, currentFrame);
                                this.picBoxImageSelectionModifier.Invalidate();
                            };
                        animationTimer.Start();
                    }

                    this.labelZoom.Enabled = true;
                    this.labelZoomDisplay.Enabled = true;
                    this.trackBarZoom.Enabled = true;
                    this.buttonZoomFit.Enabled = true;
                    this.buttonAutoSelect.Enabled = true;
                    this.buttonResetSelect.Enabled = true;
                    this.picBoxImageSelectionModifier.Enabled = true;

                    if (newSelection)
                    {
                        ResetSelection();
                        FitZoom();
                    }

                    imageLoaded = true;
                }
                catch (Exception)
                {
                    this.imageFilename = "";
                    this.textBoxImagePath.ForeColor = Color.Red;
                    this.textBoxImagePath.Text = "Cannot load image: " + filename;

                    this.labelZoom.Enabled = false;
                    this.labelZoomDisplay.Enabled = false;
                    this.trackBarZoom.Enabled = false;
                    this.buttonZoomFit.Enabled = false;
                    this.buttonAutoSelect.Enabled = false;
                    this.buttonResetSelect.Enabled = false;
                    this.picBoxImageSelectionModifier.Enabled = false;
                }
            }

            this.picBoxImageSelectionModifier.Invalidate();
        }

        private void FitZoom()
        {
            // Automatically sets up the optimal way to display the picture and the selection

            // First creates a rectangle that contains both the image and the selection rectangle,
            // at 100% scale, with (0;0) being the top-left corner of the image
            Rectangle contentRectangle = new Rectangle();
            contentRectangle.X = Math.Min(0, this.selectionRectangle.X);
            contentRectangle.Y = Math.Min(0, this.selectionRectangle.Y);
            contentRectangle.Width = Math.Max(this.image.Width, this.selectionRectangle.X + this.selectionRectangle.Width) - contentRectangle.X;
            contentRectangle.Height = Math.Max(this.image.Height, this.selectionRectangle.Y + this.selectionRectangle.Height) - contentRectangle.Y;

            // Calculates the scale needed to show the whole picture. Scale is at least 5%, and at most 200%
            this.scale = Math.Max(5, 39800 / (new int[] { contentRectangle.Width, contentRectangle.Height, 199 }).Max()) / 100f;
            this.trackBarZoom.Value = (int)(100 * this.scale);

            // Sets the display rectangles according to the previously calculated scale
            PointF contentDisplayPosition = new PointF(
                (398 - contentRectangle.Width * this.scale) / 2f,
                (398 - contentRectangle.Height * this.scale) / 2f);

            this.imageDisplayRectangle.Width = this.image.Width * this.scale;
            this.imageDisplayRectangle.Height = this.image.Height * this.scale;
            this.imageDisplayRectangle.X = -contentRectangle.X * this.scale + contentDisplayPosition.X;
            this.imageDisplayRectangle.Y = -contentRectangle.Y * this.scale + contentDisplayPosition.Y;

            this.selectionDisplayRectangle.Width = this.selectionRectangle.Width * this.scale;
            this.selectionDisplayRectangle.Height = this.selectionRectangle.Height * this.scale;
            this.selectionDisplayRectangle.X = (this.selectionRectangle.X - contentRectangle.X) * this.scale + contentDisplayPosition.X;
            this.selectionDisplayRectangle.Y = (this.selectionRectangle.Y - contentRectangle.Y) * this.scale + contentDisplayPosition.Y;

            // Tell the PictureBox it needs to refresh
            this.picBoxImageSelectionModifier.Invalidate();
        }

        private void ResetSelection()
        {
            // For the selection rectangle, (0;0) is the top-left corner of the image.
            this.selectionRectangle = new Rectangle(0, 0, image.Width, image.Height);
            this.selectionDisplayRectangle = this.imageDisplayRectangle;

            // Tell the PictureBox it needs to refresh
            this.picBoxImageSelectionModifier.Invalidate();
        }

        private void ZoomOnPoint(PointF zoomPoint, float newScale)
        {
            // The zoomPoint must remain in the same spot
            // First find what the zoomPoint is in 100% scale for both the image and the selection
            PointF imageFixPoint = new PointF(((zoomPoint.X - this.imageDisplayRectangle.X) / this.scale) * newScale,
                                              ((zoomPoint.Y - this.imageDisplayRectangle.Y) / this.scale) * newScale);
            PointF selectionFixPoint = new PointF(((zoomPoint.X - this.selectionDisplayRectangle.X) / this.scale) * newScale,
                                                  ((zoomPoint.Y - this.selectionDisplayRectangle.Y) / this.scale) * newScale);

            // We can safely change the scale now
            this.scale = newScale;

            // Then, change the display rectangles to fit the needs
            // Point imageFixPoint has to be at zoomPoint
            this.imageDisplayRectangle.X = zoomPoint.X - imageFixPoint.X;
            this.imageDisplayRectangle.Y = zoomPoint.Y - imageFixPoint.Y;
            this.imageDisplayRectangle.Width = image.Width * this.scale;
            this.imageDisplayRectangle.Height = image.Height * this.scale;

            this.selectionDisplayRectangle.X = zoomPoint.X - selectionFixPoint.X;
            this.selectionDisplayRectangle.Y = zoomPoint.Y - selectionFixPoint.Y;
            this.selectionDisplayRectangle.Width = selectionRectangle.Width * this.scale;
            this.selectionDisplayRectangle.Height = selectionRectangle.Height * this.scale;

            // Tell the PictureBox it needs to refresh
            this.picBoxImageSelectionModifier.Invalidate();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
                changeUsedImage(this.openFileDialog.FileName);
        }

        private void picBoxImageSelectionModifier_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (this.imageLoaded)
            {
                e.Graphics.DrawImage(this.image, this.imageDisplayRectangle);

                // Draw 4 semi-transparent rectangles around the selection rectangle:
                e.Graphics.FillRectangle(grayScreen, new RectangleF(
                    0, 0, this.picBoxImageSelectionModifier.ClientSize.Width,
                    this.selectionDisplayRectangle.Top));
                e.Graphics.FillRectangle(grayScreen, new RectangleF(
                    0, this.selectionDisplayRectangle.Top, this.selectionDisplayRectangle.Left,
                    this.picBoxImageSelectionModifier.ClientSize.Height - this.selectionDisplayRectangle.Top));
                e.Graphics.FillRectangle(grayScreen, new RectangleF(
                    this.selectionDisplayRectangle.Right, this.selectionDisplayRectangle.Top,
                    this.picBoxImageSelectionModifier.ClientSize.Width - this.selectionDisplayRectangle.Right,
                    this.picBoxImageSelectionModifier.ClientSize.Height - this.selectionDisplayRectangle.Top));
                e.Graphics.FillRectangle(grayScreen, new RectangleF(
                    this.selectionDisplayRectangle.Left, this.selectionDisplayRectangle.Bottom,
                    this.selectionDisplayRectangle.Width,
                    this.picBoxImageSelectionModifier.ClientSize.Height - this.selectionDisplayRectangle.Bottom));

                // There is no method for drawing a single RectangleF...
                e.Graphics.DrawRectangles(Pens.Red, new RectangleF[] { this.selectionDisplayRectangle });
            }

            // If the picture box is disabled, a semi-transparent dray screen is drawn on it
            if (!this.picBoxImageSelectionModifier.Enabled)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.LightGray)),
                    this.picBoxImageSelectionModifier.ClientRectangle);
            }
        }

        private void checkBoxUseImageBg_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxImageBg.Enabled = this.checkBoxUseImageBg.Checked;

            this.trackBarOpacity.Enabled = this.checkBoxUseImageBg.Checked;
            this.labelOpacity.Enabled = this.checkBoxUseImageBg.Checked;
            this.labelOpacityDisplay.Enabled = this.checkBoxUseImageBg.Checked;

            bool radioButtonsState = !this.checkBoxUseImageBg.Checked || this.trackBarOpacity.Value != 0;
            this.radioButtonDefault.Enabled = radioButtonsState;
            this.radioButtonPlain.Enabled = radioButtonsState;
            this.radioButtonBlack.Enabled = radioButtonsState;
        }

        private void trackBarOpacity_ValueChanged(object sender, EventArgs e)
        {
            bool valueNotZero = this.trackBarOpacity.Value != 0;
            this.radioButtonDefault.Enabled = valueNotZero;
            this.radioButtonPlain.Enabled = valueNotZero;
            this.radioButtonBlack.Enabled = valueNotZero;

            this.labelOpacityDisplay.Text = (valueNotZero) ? this.trackBarOpacity.Value + "%" : "Transparent";
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            ZoomOnPoint(new PointF(
                this.picBoxImageSelectionModifier.ClientSize.Width / 2f,
                this.picBoxImageSelectionModifier.ClientSize.Height / 2f),
                this.trackBarZoom.Value / 100f);
        }

        private void trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            this.labelZoomDisplay.Text = this.trackBarZoom.Value + "%";
        }

        private void buttonZoomFit_Click(object sender, EventArgs e)
        {
            FitZoom();
        }

        private void buttonResetSelect_Click(object sender, EventArgs e)
        {
            ResetSelection();
        }

        private void buttonAutoSelect_Click(object sender, EventArgs e)
        {
            this.selectionRectangle.Size = this.wsplit.Size;
            this.selectionRectangle.X = this.image.Width / 2 - this.selectionRectangle.Width / 2;
            this.selectionRectangle.Y = this.image.Height / 2 - this.selectionRectangle.Height / 2;

            this.selectionDisplayRectangle.Width = this.selectionRectangle.Width * this.scale;
            this.selectionDisplayRectangle.Height = this.selectionRectangle.Height * this.scale;
            this.selectionDisplayRectangle.X = this.selectionRectangle.X * this.scale + this.imageDisplayRectangle.X;
            this.selectionDisplayRectangle.Y = this.selectionRectangle.Y * this.scale + this.imageDisplayRectangle.Y;

            this.picBoxImageSelectionModifier.Invalidate();
        }

        private void picBoxImageSelectionModifier_MouseEnter(object sender, EventArgs e)
        {
            if (!this.picBoxImageSelectionModifier.Focused)
                this.picBoxImageSelectionModifier.Focus();
        }

        private void picBoxImageSelectionModifier_MouseLeave(object sender, EventArgs e)
        {
            if (this.picBoxImageSelectionModifier.Focused)
                this.picBoxImageSelectionModifier.Parent.Focus();
        }

        private void picBoxImageSelectionModifier_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!this.moving)
            {
                int newValue = Math.Max(
                    Math.Min((e.Delta / 30) + this.trackBarZoom.Value, trackBarZoom.Maximum),
                    trackBarZoom.Minimum);

                this.trackBarZoom.Value = newValue;
                ZoomOnPoint(e.Location, newValue / 100f);
            }
        }

        private void picBoxImageSelectionModifier_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.scrolling = true;
                this.scrollingCursorStartPoint = e.Location;
                this.scrollingImageStartPoint = this.imageDisplayRectangle.Location;
                this.scrollingSelectionStartPoint = this.selectionDisplayRectangle.Location;

                this.picBoxImageSelectionModifier.Cursor = Cursors.SizeAll;
            }

            else if (e.Button == MouseButtons.Left)
            {
                if (!this.scrolling)
                {
                    // Check if the mouse is over any of the sides of the rectangle
                    if (overLeft)
                    {
                        this.resizingSelectionStartRect = this.selectionRectangle;
                        this.resizingLeft = true;

                        if (overTop)
                        {
                            this.resizingTop = true;
                            this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNWSE;
                        }
                        else if (overBottom)
                        {
                            this.resizingBottom = true;
                            this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNESW;
                        }
                        else
                            this.picBoxImageSelectionModifier.Cursor = Cursors.SizeWE;
                    }

                    else if (overRight)
                    {
                        this.resizingSelectionStartRect = this.selectionRectangle;
                        this.resizingRight = true;

                        if (overTop)
                        {
                            this.resizingTop = true;
                            this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNESW;
                        }
                        else if (overBottom)
                        {
                            this.resizingBottom = true;
                            this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNWSE;
                        }
                        else
                            this.picBoxImageSelectionModifier.Cursor = Cursors.SizeWE;
                    }

                    else if (overTop)
                    {
                        this.resizingSelectionStartRect = this.selectionRectangle;
                        this.resizingTop = true;
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNS;
                    }
                    else if (overBottom)
                    {
                        this.resizingSelectionStartRect = this.selectionRectangle;
                        this.resizingBottom = true;
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNS;
                    }

                    // At last, if it's over none of the borders, check if the cursor is in the rectangle for movement
                    else if (this.selectionDisplayRectangle.Left < e.X && e.X < this.selectionDisplayRectangle.Right
                        && this.selectionDisplayRectangle.Top < e.Y && e.Y < this.selectionDisplayRectangle.Bottom)
                    {
                        this.moving = true;
                        this.movingCursorStartPoint = e.Location;
                        this.movingUnscaledSelectionStartPoint = this.selectionRectangle.Location;
                    }
                }
            }
        }

        private void picBoxImageSelectionModifier_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop every interaction with the picture box
            if (e.Button == MouseButtons.Middle)
                this.scrolling = false;

            else if (e.Button == MouseButtons.Left)
            {
                this.resizingLeft = false;
                this.resizingRight = false;
                this.resizingTop = false;
                this.resizingBottom = false;
                this.moving = false;
            }
        }

        private void picBoxImageSelectionModifier_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.scrolling)
            {
                SizeF cursorLocationDifference = new SizeF(
                    this.scrollingCursorStartPoint.X - e.X, this.scrollingCursorStartPoint.Y - e.Y);

                // Move both display rectangles
                this.imageDisplayRectangle.Location = PointF.Subtract(this.scrollingImageStartPoint, cursorLocationDifference);
                this.selectionDisplayRectangle.Location = PointF.Subtract(this.scrollingSelectionStartPoint, cursorLocationDifference);

                this.picBoxImageSelectionModifier.Invalidate();
            }

            else if (this.moving)
            {
                // For the sake of limiting movement, the modifications have to first be applied to the
                // unscaled selection rectangle.
                this.selectionRectangle.X = this.movingUnscaledSelectionStartPoint.X - (int)((this.movingCursorStartPoint.X - e.X) / this.scale);
                this.selectionRectangle.Y = this.movingUnscaledSelectionStartPoint.Y - (int)((this.movingCursorStartPoint.Y - e.Y) / this.scale);

                if (this.selectionRectangle.Right < 5)
                    this.selectionRectangle.X = -this.selectionRectangle.Width + 5;
                else if (this.selectionRectangle.Left > this.image.Width - 5)
                    this.selectionRectangle.X = this.image.Width - 5;

                if (this.selectionRectangle.Bottom < 5)
                    this.selectionRectangle.Y = -this.selectionRectangle.Height + 5;
                else if (this.selectionRectangle.Top > this.image.Height - 5)
                    this.selectionRectangle.Y = this.image.Height - 5;

                // Calculate the new selection display rectangle from the selection rectangle
                this.selectionDisplayRectangle.X = (this.selectionRectangle.X * this.scale) + this.imageDisplayRectangle.X;
                this.selectionDisplayRectangle.Y = (this.selectionRectangle.Y * this.scale) + this.imageDisplayRectangle.Y;

                this.picBoxImageSelectionModifier.Invalidate();
            }

            else if (this.resizingLeft || this.resizingRight || this.resizingTop || this.resizingBottom)
            {
                // If the Shift key is pressed, keep the aspect ratio
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    if (this.resizingLeft || this.resizingRight)
                    {
                        float newOldRatio = Math.Max((this.resizingLeft)
                                ? Math.Max(this.resizingSelectionStartRect.Right - (e.X - this.imageDisplayRectangle.X) / this.scale,
                                    this.resizingSelectionStartRect.Right - this.image.Width + 5f)
                                : Math.Max((e.X - this.selectionDisplayRectangle.X) / this.scale, -this.resizingSelectionStartRect.X + 5f),
                            5f) / this.resizingSelectionStartRect.Width;

                        // Change the behavior depending on what borders are also being resized
                        if (this.resizingTop)
                        {
                            newOldRatio = Math.Max(newOldRatio, new float[] {
                                    this.resizingSelectionStartRect.Bottom - (e.Y - this.imageDisplayRectangle.Y) / this.scale,
                                    this.resizingSelectionStartRect.Bottom - this.image.Height + 5f, 5f
                                }.Max() / this.resizingSelectionStartRect.Height);

                            this.selectionRectangle.Y = this.resizingSelectionStartRect.Bottom -
                                (int)(this.resizingSelectionStartRect.Height * newOldRatio);
                        }

                        else if (this.resizingBottom)
                        {
                            newOldRatio = Math.Max(newOldRatio, new float[] {
                                    (e.Y - this.selectionDisplayRectangle.Y) / this.scale,
                                    -this.resizingSelectionStartRect.Y + 5f, 5f
                                }.Max() / this.resizingSelectionStartRect.Height);
                        }

                        else
                        {
                            newOldRatio = Math.Max(newOldRatio, new float[] {
                                    this.resizingSelectionStartRect.Height - 2 * ((this.image.Height - 5f) - this.resizingSelectionStartRect.Top),
                                    this.resizingSelectionStartRect.Height - 2 * (this.resizingSelectionStartRect.Bottom - 5f), 5f
                                }.Max() / this.resizingSelectionStartRect.Height);

                            this.selectionRectangle.Y = this.resizingSelectionStartRect.Y + (this.resizingSelectionStartRect.Height -
                                (int)(this.resizingSelectionStartRect.Height * newOldRatio)) / 2;
                        }

                        // The Width and Height calculation is the same for all cases
                        this.selectionRectangle.Width = (int)(this.resizingSelectionStartRect.Width * newOldRatio);
                        this.selectionRectangle.Height = (int)(this.resizingSelectionStartRect.Height * newOldRatio);

                        if (this.resizingLeft)
                            this.selectionRectangle.X = this.resizingSelectionStartRect.Right - this.selectionRectangle.Width;
                    }

                    // Here, it's either top or bottom, without the other borders
                    else
                    {
                        float newOldRatio = Math.Max(
                            Math.Max(
                                (this.resizingTop)
                                    ? Math.Max(this.resizingSelectionStartRect.Bottom - (e.Y - this.imageDisplayRectangle.Y) / this.scale,
                                        this.resizingSelectionStartRect.Bottom - this.image.Height + 5f)
                                    : Math.Max((e.Y - this.selectionDisplayRectangle.Y) / this.scale, -this.resizingSelectionStartRect.Y + 5f),
                                5f) / this.resizingSelectionStartRect.Height,
                            new float[] {
                                this.resizingSelectionStartRect.Width - 2 * ((this.image.Width - 5f) - this.resizingSelectionStartRect.Left),
                                this.resizingSelectionStartRect.Width - 2 * (this.resizingSelectionStartRect.Right - 5f), 5f
                            }.Max() / this.resizingSelectionStartRect.Width);



                        this.selectionRectangle.X = this.resizingSelectionStartRect.X + (this.resizingSelectionStartRect.Width -
                            (this.selectionRectangle.Width = (int)(this.resizingSelectionStartRect.Width * newOldRatio))) / 2;
                        this.selectionRectangle.Height = (int)(this.resizingSelectionStartRect.Height * newOldRatio);

                        if (this.resizingTop)   // This line only applies to Top
                            this.selectionRectangle.Y = this.resizingSelectionStartRect.Bottom - this.selectionRectangle.Height;
                    }
                }
                // Change size without caring about the aspect ratio
                else
                {
                    // Left side or more
                    if (this.resizingLeft)
                    {
                        int previousX = this.selectionRectangle.X;
                        this.selectionRectangle.X = (int)((e.X - this.imageDisplayRectangle.X) / this.scale);
                        if (this.selectionRectangle.Left >= this.image.Width)
                            this.selectionRectangle.X = this.image.Width - 1;
                        this.selectionRectangle.Width -= this.selectionRectangle.X - previousX;
                    }

                    // Right side or more
                    else if (this.resizingRight)
                    {
                        this.selectionRectangle.Width = (int)((e.X - this.selectionDisplayRectangle.X) / this.scale);
                        if (this.selectionRectangle.Right <= 0)
                            this.selectionRectangle.Width = -this.selectionRectangle.X + 1;
                    }

                    // Top side or more
                    if (this.resizingTop)
                    {
                        int previousY = this.selectionRectangle.Y;
                        this.selectionRectangle.Y = (int)((e.Y - this.imageDisplayRectangle.Y) / this.scale);
                        if (this.selectionRectangle.Top >= this.image.Height)
                            this.selectionRectangle.Y = this.image.Height - 1;
                        this.selectionRectangle.Height -= this.selectionRectangle.Y - previousY;
                    }

                    // Bottom side or more
                    else if (this.resizingBottom)
                    {
                        this.selectionRectangle.Height = (int)((e.Y - this.selectionDisplayRectangle.Y) / this.scale);
                        if (this.selectionRectangle.Bottom <= 0)
                            this.selectionRectangle.Height = -this.selectionRectangle.Y + 1;
                    }

                    // Apply size limitations
                    if (this.selectionRectangle.Width < 5)
                    {
                        if (this.resizingLeft)
                            this.selectionRectangle.X = this.selectionRectangle.Right - 5;
                        this.selectionRectangle.Width = 5;
                    }

                    if (this.selectionRectangle.Height < 5)
                    {
                        if (this.resizingTop)
                            this.selectionRectangle.Y = this.selectionRectangle.Bottom - 5;
                        this.selectionRectangle.Height = 5;
                    }
                }


                // Convert selection rectangle to selection display rectangle
                this.selectionDisplayRectangle.X = (this.selectionRectangle.X * this.scale) + this.imageDisplayRectangle.X;
                this.selectionDisplayRectangle.Y = (this.selectionRectangle.Y * this.scale) + this.imageDisplayRectangle.Y;
                this.selectionDisplayRectangle.Width = this.selectionRectangle.Width * this.scale;
                this.selectionDisplayRectangle.Height = this.selectionRectangle.Height * this.scale;

                this.picBoxImageSelectionModifier.Invalidate();
            }

            else
            {
                // Assume the cursor may not be over any border anymore
                this.overLeft = false;
                this.overTop = false;
                this.overBottom = false;
                this.overRight = false;

                // Check if the cursor is over any border of the selection rectangle
                if (selectionDisplayRectangle.Left - 5 <= e.X && e.X <= selectionDisplayRectangle.Left + 5
                    && selectionDisplayRectangle.Top - 5 <= e.Y && e.Y <= selectionDisplayRectangle.Bottom + 5)
                    overLeft = true;

                else if (selectionDisplayRectangle.Right - 5 <= e.X && e.X <= selectionDisplayRectangle.Right + 5
                    && selectionDisplayRectangle.Top - 5 <= e.Y && e.Y <= selectionDisplayRectangle.Bottom + 5)
                    overRight = true;


                if (selectionDisplayRectangle.Left - 5 <= e.X && e.X <= selectionDisplayRectangle.Right + 5
                    && selectionDisplayRectangle.Top - 5 <= e.Y && e.Y <= selectionDisplayRectangle.Top + 5)
                    overTop = true;

                else if (selectionDisplayRectangle.Left - 5 <= e.X && e.X <= selectionDisplayRectangle.Right + 5
                    && selectionDisplayRectangle.Bottom - 5 <= e.Y && e.Y <= selectionDisplayRectangle.Bottom + 5)
                    overBottom = true;

                // Sets the cursor according to what border it is over.
                if (overLeft)
                {
                    if (overTop)
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNWSE;
                    else if (overBottom)
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNESW;
                    else
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeWE;
                }

                else if (overRight)
                {
                    if (overTop)
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNESW;
                    else if (overBottom)
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNWSE;
                    else
                        this.picBoxImageSelectionModifier.Cursor = Cursors.SizeWE;
                }

                else if (overTop || overBottom)
                    this.picBoxImageSelectionModifier.Cursor = Cursors.SizeNS;

                else    // If it is over none of the borders, default cursor
                    this.picBoxImageSelectionModifier.Cursor = Cursors.Default;
            }
        }

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBoxEx.Show(this,
                "Help Window",
                "Help", MessageBoxButtons.OK);
        }

        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.github.com/Nitrofski/WSplit");
        }
    }
}
