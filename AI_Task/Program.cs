using System;
using System.Windows.Forms;

namespace AI_Task
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    /// <summary>
    /// сравнение вещественных чисел с допущением 0.00001
    /// </summary>
    public static class D
    {
        public static bool Eq(double d1, double d2)
        {
            return Math.Abs(d1 - d2) <= 0.00001;
        }
    }
}
