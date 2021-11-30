using System;
using System.Windows.Forms;

namespace AI_Task
{
    public partial class ChartForm : Form
    {
        ChartManager _chartManager;
        FuncsManager _funcsManager;

        public ChartForm(string name, FuncsManager funcsManager)
        {
            InitializeComponent();
            this.Text = name;

            _funcsManager = funcsManager;
            _chartManager = new ChartManager(_funcsManager, chart);
            cb_Show.Items.AddRange(_chartManager.GetSeriesLabels());
            b_EndEditing.Visible = false;
        }


        void OutputResult(string text, params Func[] funcs)
        {
            tb_Res.Text += text + Environment.NewLine;
            Console.WriteLine(text);

            foreach (var func in funcs)
            {
                if (func.ExistsIn(_funcsManager.XToCheck))
                {
                    Console.WriteLine(func.Name + ": " + func.FindValueIn(_funcsManager.XToCheck));
                    tb_Res.Text += (func.Name + ": " + func.FindValueIn(_funcsManager.XToCheck) + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine(func.Name + ": не существует");
                    tb_Res.Text += (func.Name + ": не существует" + Environment.NewLine);
                }
                Console.WriteLine(new string('=', 10));
                tb_Res.Text += new string('=', 10) + Environment.NewLine;
            }
        }

        public void SetChartSizes(double xMin, double xMax, double yMin, double yMax)
        {
            _chartManager.SetSizes(xMin, xMax, yMin, yMax);
        }


        #region CALC BUTTONS
        private void b_CalcForEachFunc_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            _chartManager.Draw("X");
            OutputResult("Значения точки X = " + _funcsManager.XToCheck + " для функций: ", _funcsManager.GetFuncs().ToArray());
        }

        private void b_CalcForDiff_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            _chartManager.Draw("diff");
            OutputResult("Значение точки X = " + _funcsManager.XToCheck + " для функции пересечения: ", _funcsManager.GetDiff());
        }

        private void b_CalcForUnion_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            _chartManager.Draw("union");
            OutputResult("Значение точки X = " + _funcsManager.XToCheck + " для функции объединения: ", _funcsManager.GetUnion());
        }

        private void b_CalcCenterOfMax_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            _chartManager.Draw("unionMaxAverage");
            OutputResult("Среднее значение точек максимума: " + _funcsManager.GetUnionMaxAverage());
        }
        #endregion


        #region FUNCS EDITING MODE

        private void b_EditFuncs_Click(object sender, EventArgs e)
        {
            _chartManager.EnterEditingMode();
            b_EditFuncs.Enabled = false;
            b_EndEditing.Show();
        }

        private void chart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _chartManager.EnterEditingMode();
            b_EditFuncs.Enabled = false;
            b_EndEditing.Show();
        }

        private void b_EndEditing_Click(object sender, EventArgs e)
        {
            _chartManager.EndEditingMode();
            b_EditFuncs.Enabled = true;
            b_EndEditing.Hide();
            _funcsManager.RecalculateAll();
        }

        #endregion


        private void b_HideAllButFuncs_Click(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
        }

        private void cb_Show_SelectedIndexChanged(object sender, EventArgs e)
        {
            _chartManager.HideAllButFuncs();
            foreach (var item in cb_Show.CheckedItems)
                _chartManager.Draw((string)item);
        }

        private void tb_Value_KeyPress(object sender, KeyPressEventArgs e)
        {   // textbox accepts only digits
            if (char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b')
                return;
            else
                e.Handled = true;
        }

        private void tb_Value_Leave(object sender, EventArgs e)
        {
            _funcsManager.XToCheck = Convert.ToDouble(tb_Value.Text);
        }
    }
}