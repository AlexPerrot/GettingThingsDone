using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using GettingThingsDone.Properties;
using GettingThingsDone;
using GettingThingsDone.data;
using GettingThingsDone.src.model;

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        private GTDSystem gtd = new GTDSystem();
        public GTDSystem GTD { get { return gtd; } }

        public GettingThingsDone.DataClassesDataContext DB { get { return new DataClassesDataContext(); } }

        private Users admin;
        public Users Admin { get { return admin; } }

        private IGTDFactory factory = new DBGTDFactory();
        public IGTDFactory Factory { get { return factory; } }

        public App()
            : base()
        {
            admin = this.DB.Users.Single(item => item.Username == "admin");
        }

        private void Application_Exit_1(object sender, ExitEventArgs e)
        {
            DB.SubmitChanges();
        }
    }
}
