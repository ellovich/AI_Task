using System;
using System.Windows.Forms;

namespace AI_Task
{
    public partial class MainForm : Form
    {
        FuncsManager _funcsManager;
        ChartManager _chartManager;

        public MainForm()
        {
            InitializeComponent();

            //Console.SetOut(new TextBoxStreamWriter(resTB));
            _funcsManager = new FuncsManager();
            _chartManager = new ChartManager(_funcsManager, chart);
            showChB.Items.AddRange(_chartManager.GetSeriesLabels());
        }

        #region CALC BUTTONS
        private void calcForEachFuncB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();

            if (IsValueEnteredCorrectly())
            {
                _chartManager.Draw("X");
                OutputResult("Значения точки X = " + _funcsManager.XToCheck + " для функций: ", _funcsManager.GetFuncs().ToArray());
            }
        }

        private void calcForDiffB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();

            if (IsValueEnteredCorrectly())
            {
                _chartManager.Draw("diff");
                OutputResult("Значение точки X = " + _funcsManager.XToCheck + " для функции пересечения: ", _funcsManager.GetDiff());
            }
        }

        private void calcForUnionB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();

            if (IsValueEnteredCorrectly())
            {
                _chartManager.Draw("union");
                OutputResult("Значение точки X = " + _funcsManager.XToCheck + " для функции объединения: ", _funcsManager.GetUnion());
            }
        }

        private void calcCenterOfMaxB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            _chartManager.Draw("unionMaxAverage");
            OutputResult("Среднее значение точек максимума: " + _funcsManager.GetUnionMaxAverage());
        }
        #endregion

        private bool IsValueEnteredCorrectly()
        {
            double value = Convert.ToDouble(valueTB.Text);

            if (0.0 <= value && value <= 120.0) 
            {
                _funcsManager.XToCheck = value;
                return true;
            }
            else 
            { 
                MessageBox.Show("X должен лежать в диапозоне от 0 до 120!");
                return false;
            }
        }

        private void OutputResult(string text, params Func[] funcs)
        {
            resTB.Text += text + Environment.NewLine;
            Console.WriteLine(text);

            foreach (var func in funcs)
                if (func.ExistsIn(_funcsManager.XToCheck))
                {
                    Console.WriteLine(func.Name + ": " + func.FindValueIn(_funcsManager.XToCheck)); 
                    resTB.Text += (func.Name + ": " + func.FindValueIn(_funcsManager.XToCheck) + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine(func.Name + ": не существует");
                    resTB.Text += (func.Name + ": не существует" + Environment.NewLine);
                }
        }

        private void clearAllB_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
        }

        private void showChB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();

            foreach (var item in showChB.CheckedItems)
                _chartManager.Draw((string)item);
        }

        private void valueTB_KeyPress(object sender, KeyPressEventArgs e)
        { // в текстбокс разрешается вводить только числа
            if (char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b') 
                return;
            else
                e.Handled = true;
        }
    }
}