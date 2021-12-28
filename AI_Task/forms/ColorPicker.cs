using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_Task.forms
{
    public partial class ColorPicker : Form
    {
        Color _color = Color.Empty;
        ColorsRedOrange _colorsRedOrange;

        public ColorPicker(ColorsRedOrange colorsRedOrange)
        {
            InitializeComponent();
            _colorsRedOrange = colorsRedOrange;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _color = colorDialog.Color;

                richTextBox1.Text += "\n";
                richTextBox1.Text += "\n";
                richTextBox1.Text += _color.ToString() + "\n";
                richTextBox1.Text += CmykColor.ConvertRgbToCmyk(_color).ToString();

                richTextBox2.Text += "Red: " + _colorsRedOrange.CalcPossibilityRed(_color).ToString() + "\n";
                richTextBox2.Text += "Orange: " + _colorsRedOrange.CalcPossibilityOrange(_color).ToString() + "\n\n";
            }
        }
    }
}
