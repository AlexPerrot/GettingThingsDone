﻿using GettingThingsDone.src.data;
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
            IUser user = dbProvider.IdManager.GetUser(dbProvider.Database.Users.First().Id); // dummy user for now
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
    }
}
