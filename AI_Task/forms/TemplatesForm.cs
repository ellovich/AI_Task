using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AI_Task
{
    public partial class TemplatesForm : Form
    {
        TemplatesTask _signsTask;

        public TemplatesForm(TemplatesTask signsTask)
        {
            InitializeComponent();
            TemplateFormElement.s_Size = 100;
            pb_RecognitionElement.Image = GetWhiteBitmap();
            Init(signsTask);
        }

        public void Init(TemplatesTask signsTask)
        {
            _signsTask = signsTask;

            panel_signs.Controls.Clear();
            foreach (var t in _signsTask.Templates)
                panel_signs.Controls.Add(new TemplateFormElement(t));

            _signsTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image);
        }

        private void pb_RecognitionElement_Paint(object sender, PaintEventArgs e)
        {
            _signsTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image);

            foreach (TemplateFormElement tfe in panel_signs.Controls)
                tfe.UpdateState();
        }

        private void b_LoadTemplates_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _signsTask.Init(fbd.SelectedPath);
                _signsTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image);
                Init(_signsTask);
            }
        }


        #region PAINTING

        System.Drawing.Point? _prevPos = null;
        Pen _pen = new Pen(Color.Black, 12);
        Color _prevColor = Color.Black;

        private Bitmap GetWhiteBitmap()
        {
            Bitmap bmp = new Bitmap(pb_RecognitionElement.Size.Width, pb_RecognitionElement.Size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            return bmp;
        }

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
        {
            _prevPos = null;
            if (sortAuto)
                SortByPossibility();
        }

        private void b_ChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.Color = _pen.Color;

            if (MyDialog.ShowDialog() == DialogResult.OK)
                _pen.Color = _prevColor = MyDialog.Color;
        }

        private void b_Erase_Click(object sender, EventArgs e)
        {
            pb_RecognitionElement.Image = GetWhiteBitmap();
        }

        #endregion


        #region PICTURE LOADING

        private void b_LoadFile_Click(object sender, System.EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "Open Image";
            dialog.Filter = "png files (*.png)|*.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = new Bitmap(dialog.FileName);
                image = Imager.Resize(image, pb_RecognitionElement.Width, pb_RecognitionElement.Height, false);
                pb_RecognitionElement.Image = image;

                if (cb_ConsiderInvreted.Checked)
                    SortByPossibility();
            }

            dialog.Dispose();
        }

        private void SignsForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void SignsForm_DragDrop(object sender, DragEventArgs e)
        {
            int x = PointToClient(new System.Drawing.Point(e.X, e.Y)).X;
            int y = PointToClient(new System.Drawing.Point(e.X, e.Y)).Y;

            if (x >= pb_RecognitionElement.Location.X &&
                x <= pb_RecognitionElement.Location.X + pb_RecognitionElement.Width &&
                y >= pb_RecognitionElement.Location.Y &&
                y <= pb_RecognitionElement.Location.Y + pb_RecognitionElement.Height)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                Image image = Image.FromFile(files[0]);
                image = Imager.Resize(image, pb_RecognitionElement.Width, pb_RecognitionElement.Height, false);
                pb_RecognitionElement.Image = image;

                if (cb_ConsiderInvreted.Checked)
                    SortByPossibility();
            }
        }

        #endregion


        #region SORTING

        bool sortAuto = false;
        void SortByPossibility()
        {
            panel_signs.Controls.Clear();

            var tfes = new List<TemplateFormElement>();
            foreach (var t in _signsTask.Templates)
                tfes.Add(new TemplateFormElement(t));
            tfes.Sort();
            tfes.Reverse();

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
            foreach (var t in _signsTask.Templates)
                panel_signs.Controls.Add(new TemplateFormElement(t));
        }

        #endregion

        private void cb_ConsiderInvreted_CheckedChanged(object sender, EventArgs e)
        {
            TemplatesTask.s_ConsiderInverted = cb_ConsiderInvreted.Checked;
            _signsTask.UpdatePossibilities((Bitmap)pb_RecognitionElement.Image);

            if (sortAuto)
                SortByPossibility();
        }
    }

    public class TemplateFormElement : FlowLayoutPanel, IComparer<TemplateFormElement>, IComparable<TemplateFormElement>
    {
        public static int s_Size;

        Template _template;
        Label _label;
        PictureBox _pictureBox;
        TextProgressBar _progressBar;


        public TemplateFormElement(Template template)
        {
            _template = template;

            Size = new Size(s_Size + 6, s_Size + 60);

            _label = new Label
            {
                Text = template.Name
            };
            Controls.Add(_label);

            _pictureBox = new PictureBox
            {
                Image = Imager.Resize(template.Image, s_Size, s_Size, false),
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
            double curPossibility = _template.Possibility;
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


        public int CompareTo(TemplateFormElement obj)
        {
            TemplateFormElement t1 = obj;
            return Compare(this, t1);
        }

        public int Compare(TemplateFormElement x, TemplateFormElement y)
        {
            Template t1 = x._template;
            Template t2 = y._template;
            return t1.CompareTo(t2);
        }
    }
}