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
        int _recognitionElement_size = 180;


        public TemplatesForm(TemplatesTask signsTask)
        {
            InitializeComponent();
            TemplateViewElement.s_Size = 64;
            pb_RecognitionElement.Image = GetWhiteBitmap();
            FormInit(signsTask);
        }

        void FormInit(TemplatesTask signsTask)
        {
            _templateTask = signsTask;

            panel_signs.Controls.Clear();
            foreach (var t in _templateTask.Templates)
                panel_signs.Controls.Add(new TemplateViewElement(t));

            _templateTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image); // model update

            if (sortAuto)
                SortByPossibility(); // view update
        }


        private void b_LoadTemplates_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _templateTask.Init(fbd.SelectedPath);
                FormInit(_templateTask);
            }
        }

        private void pb_RecognitionElement_Paint(object sender, PaintEventArgs e)
        {   // calls when picture has changed 
            // sorting is called only after painting (see MouseUp event)
            _templateTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image);

            foreach (TemplateViewElement tfe in panel_signs.Controls)
                tfe.UpdateState();

            pb_DebugImage.Image = _templateTask.CreateScaledDebugPicture(
                pb_RecognitionElement.Image, _recognitionElement_size);
            pb_DebugImage2.Image = _templateTask.CreateColoredDebugWinnerPicture(_recognitionElement_size);
        }

        private void cb_ConsiderInvreted_CheckedChanged(object sender, EventArgs e)
        {
            Template.s_ConsiderInverted = cb_ConsiderInvreted.Checked;
            _templateTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image);

            if (sortAuto)
                SortByPossibility();
        }

        Bitmap GetWhiteBitmap()
        {
            Bitmap bmp = new Bitmap(_recognitionElement_size, _recognitionElement_size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            return bmp;
        }


        #region PAINTING

        System.Drawing.Point? _prevPos = null;
        Pen _pen = new Pen(Color.Black, 14);
        Color _prevColor = Color.Black;

        private void pb_RecognitionElement_MouseDown(object sender, MouseEventArgs e)
        {
            _pen.Color = (e.Button == MouseButtons.Left) ? _prevColor : Color.White;
            _prevPos = new System.Drawing.Point(e.X, e.Y);
            pb_RecognitionElement_MouseMove(sender, e);
        }

        private void pb_RecognitionElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (_prevPos != null)
            {
                using (Graphics g = Graphics.FromImage(pb_RecognitionElement.Image))
                {
                    g.DrawLine(_pen, _prevPos.Value.X, _prevPos.Value.Y, e.X, e.Y);
                }
                pb_RecognitionElement.Invalidate();
                _prevPos = new System.Drawing.Point(e.X, e.Y);
            }
        }

        private void pb_RecognitionElement_MouseUp(object sender, MouseEventArgs e)
        {  // model is updating in MouseMove event
            _prevPos = null;

            if (sortAuto)
                SortByPossibility();
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
            pb_RecognitionElement.Image = GetWhiteBitmap();
        }

        #endregion


        #region PICTURE LOADING

        void LoadImage(string filePath)
        {
            try
            {
                Image image = new Bitmap(filePath);
                image = Imager.Resize(image, _recognitionElement_size, _recognitionElement_size);
                pb_RecognitionElement.Image = image;

                _templateTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image);

                if (sortAuto)
                    SortByPossibility();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
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
                LoadImage(dialog.FileName);
            dialog.Dispose();
        }

        private void TemplatesForm_DragDrop(object sender, DragEventArgs e)
        {
            // loading image
            int x = PointToClient(new System.Drawing.Point(e.X, e.Y)).X;
            int y = PointToClient(new System.Drawing.Point(e.X, e.Y)).Y;

            if (x >= pb_RecognitionElement.Location.X &&
                x <= pb_RecognitionElement.Location.X + pb_RecognitionElement.Width &&
                y >= pb_RecognitionElement.Location.Y &&
                y <= pb_RecognitionElement.Location.Y + pb_RecognitionElement.Height)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                LoadImage(files[0]);
            }

            // loading templates
            else if (x >= panel_signs.Location.X &&
                x <= panel_signs.Location.X + panel_signs.Width &&
                y >= panel_signs.Location.Y &&
                y <= panel_signs.Location.Y + panel_signs.Height)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                    try
                    {
                        _templateTask.Init(path);
                        FormInit(_templateTask);

                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
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
        void SortByPossibility()
        {
            panel_signs.Controls.Clear();

            var tfes = new List<TemplateViewElement>();
            foreach (var t in _templateTask.Templates)
                tfes.Add(new TemplateViewElement(t));
            tfes = tfes.OrderByDescending(tfe => tfe.Template.Possibility).ToList();

            panel_signs.Controls.AddRange(tfes.ToArray());
        }

        private void cb_SortAuto_CheckedChanged(object sender, EventArgs e)
        {
            sortAuto = cb_SortAuto.Checked;
            if (sortAuto)
                SortByPossibility();
        }

        private void b_SortTemplates_Click(object sender, EventArgs e)
        {
            SortByPossibility();
        }

        private void b_ResetSorting_Click(object sender, EventArgs e)
        {
            panel_signs.Controls.Clear();
            foreach (var t in _templateTask.Templates)
                panel_signs.Controls.Add(new TemplateViewElement(t));
        }

        #endregion
    }

    public class TemplateViewElement : FlowLayoutPanel
    {
        public static int s_Size = 105; // sets in Form

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
            curPossibility = Math.Round(curPossibility, 2);

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