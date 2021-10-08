using System;
using System.Windows.Forms;

namespace AI_Task
{
    public partial class MainForm : Form
    {
        Fff _fff;
        ChartManager _chartManager;

        public MainForm()
        {
            InitializeComponent();

            //Console.SetOut(new TextBoxStreamWriter(resTB));
            speedChart.ChartAreas[0].AxisX.RoundAxisValues();

            _fff = new Fff();
            _chartManager = new ChartManager(_fff, speedChart);
            showChB.Items.AddRange(_chartManager.GetSeriesLabels());
        }

        #region CALC BUTTONS

        private void calcForEachFuncB_Click(object sender, EventArgs e)
        {
            if (IsValueEnteredCorrectly())
            {
                _chartManager.HideAllButFuncs();
                OutputResult("Значения точки X = " + _fff.XToCheck + " для функций: ", _fff.GetFuncs().ToArray());
                _chartManager.Draw("X");
            }
        }

        private void calcForDiffB_Click(object sender, EventArgs e)
        {
            if (IsValueEnteredCorrectly())
            {
                _chartManager.HideAllButFuncs();
                _chartManager.Draw("diff");
                OutputResult("Значение точки X = " + _fff.XToCheck + " для функции пересечения: ", _fff.GetDiff());
            }
        }

        private void calcForUnionB_Click(object sender, EventArgs e)
        {
            if (IsValueEnteredCorrectly())
            {
                _chartManager.HideAllButFuncs();
                _chartManager.Draw("union");
                OutputResult("Значение точки X = " + _fff.XToCheck + " для функции объединения: ", _fff.GetUnion());
            }
        }

        private void calcCenterOfMaxB_Click(object sender, EventArgs e)
        {

        }

        #endregion

        bool IsValueEnteredCorrectly()
        {
            if (valueTB.Text != string.Empty)
            {
                double value = Convert.ToDouble(valueTB.Text);

                if (0.0 <= value && value <= 120.0)
                {
                    _fff.XToCheck = value;
                    return true;
                }
            }
            else
            {
                MessageBox.Show("X должен лежать в диапозоне от 0 до 120!");
            }

            return false;
        }

        void OutputResult(string text, params Func[] funcs)
        {
            _chartManager.Draw("X");

            resTB.Text += text;
            Console.WriteLine(text);

            foreach (var func in funcs)
                if (func.ExistsIn(_fff.XToCheck))
                {
                    Console.WriteLine(func.Name + ": " + func.FindValueIn(_fff.XToCheck));
                    resTB.Text += ("\n" + func.Name + ": " + func.FindValueIn(_fff.XToCheck) + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("не существует");
                    resTB.Text += ("\n" + "не существует" + Environment.NewLine);
                }
        }

        private void clearAllB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
        }

        private void showChB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            //resTB.Text = "";

            foreach (var item in showChB.CheckedItems)
                _chartManager.Draw((string)item);
        }

        private void valueTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b') 
                return;
            else
                e.Handled = true;
        }
    }
}