using System;
using System.Windows.Forms;

namespace UnhandledDeadlockApp
{
    static class Program
    {
        private static Form1 _form1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            _form1 = new Form1();
            Application.Run(_form1);
        }
        
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _form1.SynchronizationContext.Send(a => MessageBox.Show(_form1, @"Application will shutdown!" + e.ExceptionObject.ToString(), @"The end!"), null);
        }
    }
}
