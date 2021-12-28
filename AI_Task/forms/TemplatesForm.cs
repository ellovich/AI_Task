using ImagerLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace AI_Task
{
    public partial class TemplatesForm : Form
    {
        TemplatesTask _templateTask;
        int _myImgSize = 160;

        public TemplatesForm(TemplatesTask signsTask)
        {
            InitializeComponent();
            TemplateViewElement.s_Size = 64;
            pb_myImg.Image = Imager.GetWhiteBitmap(_myImgSize, _myImgSize);

            InitForm(signsTask);
        }

        void InitForm(TemplatesTask signsTask)
        {
            _templateTask = signsTask;

            panel_signs.Controls.Clear();
            foreach (var t in _templateTask.Templates)
                panel_signs.Controls.Add(new TemplateViewElement(t));

            UpdateModelAndView();
        }


        #region UPDATING

        void UpdateModelAndView()
        {
            UpdateModel();
            UpdateView();
        }

        void UpdateModel()
        {
            _templateTask.UpdatePossibilities((Bitmap)pb_myImg.Image);
        }

        void UpdateView(bool isMouseDown = false)
        {
            foreach (TemplateViewElement tve in panel_signs.Controls)
                tve.UpdateState();

            if (!isMouseDown && sortAuto) // sorting shouldn`t be called in MouseUp event
                SortPossibilities();

            //    pb_DebugImage.Image = Imager.CreateTwiceScaledGrayImage(
            //        pb_myImg.Image, _myImgSize, Template.s_ImageRez);


            pb_DebugImage.Image = Imager.CreateTwiceScaledImage(
                     pb_myImg.Image, _myImgSize, Template.s_ImageRez);


            pb_DebugImage2.Image = _templateTask.GetWinnerImage(_myImgSize);

            l_Answer.Text = _templateTask.GetWinner().Name;
        }

        #endregion


        #region PAINTING

        System.Drawing.Point? _prevPos = null;
        Pen _pen = new Pen(Color.Black, 14);
        Color _prevColor = Color.Black;

        private void pb_MyImg_MouseDown(object sender, MouseEventArgs e)
        {
            _pen.Color = (e.Button == MouseButtons.Left) ? _prevColor : Color.White;
            _prevPos = new System.Drawing.Point(e.X, e.Y);
            pb_MyImg_MouseMove(sender, e);
        }

        private void pb_MyImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (_prevPos != null)
            {
                using (Graphics g = Graphics.FromImage(pb_myImg.Image))
                {
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                    g.DrawLine(_pen, _prevPos.Value.X, _prevPos.Value.Y, e.X, e.Y);
                }
                pb_myImg.Invalidate();
                _prevPos = new System.Drawing.Point(e.X, e.Y);
            } // model is updating in OnPaint event
        }

        private void pb_MyImg_Paint(object sender, PaintEventArgs e)
        {
            UpdateModel();
            UpdateView(true);
        }

        private void pb_MyImg_MouseUp(object sender, MouseEventArgs e)
        {
            _prevPos = null;
            UpdateView(); // model is updating in pb_MyImg OnPaint event
        }

        private void b_ChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog
            {
                Color = _pen.Color
            };

            if (MyDialog.ShowDialog() == DialogResult.OK)
                _pen.Color = _prevColor = MyDialog.Color;
        }

        private void b_Erase_Click(object sender, EventArgs e)
        {
            pb_myImg.Image = Imager.GetWhiteBitmap(_myImgSize, _myImgSize);
            UpdateView(); // model is updating in pb_MyImg OnPaint event
        }

        #endregion


        #region PICTURE LOADING

        void LoadImage(string filePath)
        {
            try
            {
                Image image = new Bitmap(filePath);
                pb_myImg.Image = Imager.Crop(image, new Rectangle(0, 0, image.Width, image.Height));
                pb_myImg.Image = Imager.Resize(image, _myImgSize, _myImgSize);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private void b_LoadTemplates_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _templateTask.InitTemplates(fbd.SelectedPath);
                InitForm(_templateTask);
            }
        }

        private void b_LoadFile_Click(object sender, EventArgs e)
        {
            string codecFilter = "Image Files|";
            foreach (var codec in ImageCodecInfo.GetImageEncoders())
                codecFilter += codec.FilenameExtension + ";";

            var dialog = new OpenFileDialog
            {
                Title = "Open Image",
                Filter = codecFilter
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadImage(dialog.FileName);
                UpdateModelAndView();
            }
            dialog.Dispose();
        }

        private void TemplatesForm_DragDrop(object sender, DragEventArgs e)
        {
            int x = PointToClient(new System.Drawing.Point(e.X, e.Y)).X;
            int y = PointToClient(new System.Drawing.Point(e.X, e.Y)).Y;

            // loading templates
            if (x >= panel_signs.Location.X &&
                x <= panel_signs.Location.X + panel_signs.Width &&
                y >= panel_signs.Location.Y &&
                y <= panel_signs.Location.Y + panel_signs.Height)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                    try
                    {
                        _templateTask.InitTemplates(path);
                        InitForm(_templateTask);
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
            }

            // loading image
            else if (x >= pb_myImg.Location.X &&
                x <= pb_myImg.Location.X + pb_myImg.Width &&
                y >= pb_myImg.Location.Y &&
                y <= pb_myImg.Location.Y + pb_myImg.Height)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null)
                {
                    LoadImage(files[0]);
                    UpdateModelAndView();
                }
            }
        }

        private void TemplatesForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #endregion


        #region SORTING

        bool sortAuto = false;
        void SortPossibilities(bool byDescending = true)
        {
            panel_signs.Controls.Clear();

            var tfes = new List<TemplateViewElement>();
            foreach (var t in _templateTask.Templates)
                tfes.Add(new TemplateViewElement(t));

            if (byDescending)
                tfes = tfes.OrderByDescending(tfe => tfe.Template.Possibility).ToList();

            panel_signs.Controls.AddRange(tfes.ToArray());
        }

        private void cb_SortAuto_CheckedChanged(object sender, EventArgs e)
        {
            sortAuto = cb_SortAuto.Checked;
            SortPossibilities();
        }

        private void b_SortTemplates_Click(object sender, EventArgs e)
        {
            SortPossibilities();
        }

        private void b_ResetSorting_Click(object sender, EventArgs e)
        {
            SortPossibilities(false);
        }

        #endregion


        private void cb_ConsiderInvreted_CheckedChanged(object sender, EventArgs e)
        {
            Template.s_ConsiderInverted = cb_ConsiderInvreted.Checked;
            UpdateModelAndView();
        }

        private void TemplatesForm_Activated(object sender, EventArgs e)
        {
            _templateTask.Templates.ForEach(t => t.UpdatePixelsDegreesOfTruth());
            UpdateModelAndView();
        }

        private void TemplatesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

    public class TemplateViewElement : FlowLayoutPanel
    {
        public static int s_Size { get; set; }

        public Template Template { get; private set; }

        Label _label;
        PictureBox _pictureBox;
        TextProgressBar _progressBar;

        public TemplateViewElement(Template template)
        {
            Template = template;

            Size = new Size(s_Size + 6, s_Size + 60);

            _label = new Label
            {
                Text = template.Name
            };
            Controls.Add(_label);

            _pictureBox = new PictureBox
            {
                Image = Imager.Resize(template.Image, s_Size, s_Size),
                Padding = new Padding(0, 0, 0, 0),
                Size = new Size(s_Size, s_Size),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(_pictureBox);

            _progressBar = new TextProgressBar
            {
                Size = new Size(s_Size, 25),
                Value = (int)template.Possibility,
                Maximum = 100,
                VisualMode = ProgressBarDisplayMode.CustomText,
                CustomText = template.Possibility.ToString()
            };
            Controls.Add(_progressBar);

            UpdateState();
        }

        public void UpdateState()
        {
            double curPossibility = Template.Possibility;
            curPossibility = Math.Round(curPossibility, 3);

            Color bgcolor = Color.FromArgb(
                Convert.ToInt32(255 - (curPossibility * 255)), // R
                Convert.ToInt32(curPossibility * 255),         // G
                0);                                            // B
            BackColor = bgcolor;

            _progressBar.ProgressColor = bgcolor;
            _progressBar.Value = (int)(curPossibility * 100);
            _progressBar.CustomText = curPossibility.ToString();
        }
    }
}