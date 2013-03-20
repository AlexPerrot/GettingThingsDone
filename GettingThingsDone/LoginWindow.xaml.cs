using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ShowCredentials(object Sender, EventArgs e) {
            System.Console.WriteLine(string.Format("Credentials : {0}, {1}", loginBox.Text, passBox.Password));
        }

        private void Login(object sender, EventArgs e)
        {
            Window win = App.Current.MainWindow;
            win.Show();
            this.Close();
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
