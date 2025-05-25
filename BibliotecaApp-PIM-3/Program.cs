using System;
using System.Windows.Forms;
using BibliotecaApp.Forms;

namespace BibliotecaApp{
    internal static class Program{
        [STAThread]
        static void Main(){
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
