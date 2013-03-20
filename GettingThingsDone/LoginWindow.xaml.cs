using GettingThingsDone.src.data;
using GettingThingsDone.src.model;
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
            IDatabaseProvider dbProvider = App.Current.Properties["DBProvider"] as IDatabaseProvider;
            DataClassesDataContext db = dbProvider.Database;

            //recup des infos
            string userName = this.loginBox.Text;
            string password = this.passBox.Password;

            //récupération de l'utilisateur en base
            Users dbUser = null;
            try
            {
                dbUser = db.Users.Single(usr => usr.Username == userName);
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show("No user with this username", "Sorry");
                return;
            }

            if (!(dbUser.Password == password))
            {
                MessageBox.Show("Wrong password", "Try again");
                return;
            }

            IUser user = dbProvider.IdManager.GetUser(dbUser.Id);
            IGTDFactory factory = App.Current.Properties["Factory"] as IGTDFactory;
            App.Current.Properties["GTD"] = factory.makeSystem(user);

            Window win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void EnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login(sender, e);
        }
    }
}
