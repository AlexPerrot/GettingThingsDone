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

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        private GTDSystem gtd = new GTDSystem();
        public GTDSystem GTD { get { return gtd; } }

        private GettingThingsDone.DataClassesDataContext db = new DataClassesDataContext(Settings.Default.gtdLocalConnectionString);
        public GettingThingsDone.DataClassesDataContext DB { get { return db; } }

        private Users admin;
        public Users Admin { get { return admin; } }


        public App()
            : base()
        {
            admin = db.Users.Single(item => item.Username == "admin");
        }
    }
}
