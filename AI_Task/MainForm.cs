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
            _chartManager.HideAllButFuncs();
            CalcAndDrawValueForFunc("Значения точки X для функций: ", _fff.GetFuncs().ToArray());
        }

        private void calcForDiffB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            _chartManager.Draw("diff");
            CalcAndDrawValueForFunc("Значение точки X для функции пересечения", _fff.GetDiff());
        }

        private void calcForUnionB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            _chartManager.Draw("union");
            CalcAndDrawValueForFunc("Значение точки X для функции объединения", _fff.GetUnion());
        }

        private void calcCenterOfMaxB_Click(object sender, EventArgs e)
        {

        }

        #endregion

        bool ValueIsCorrect(double value)
        {
            if (0.0 <= value && value <= 120.0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("X должен лежать в диапозоне от 0 до 120!");
                return false;
            }
        }

        void CalcAndDrawValueForFunc(string text, params Func[] funcs)
        {
            if (valueTB.Text != string.Empty)
                if (ValueIsCorrect(Convert.ToDouble(valueTB.Text)))
                {
                    _fff.XToCheck = Convert.ToDouble(valueTB.Text);
                               
                    _chartManager.Draw("X");

                    Console.WriteLine(text);

                    foreach (var func in funcs)
                        if (func.ExistsIn(_fff.XToCheck))
                            Console.WriteLine(func.Name + ": " + func.FindValueIn(_fff.XToCheck));             
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