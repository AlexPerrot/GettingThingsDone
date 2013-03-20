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
using GettingThingsDone.src.model;
using GettingThingsDone.src.data;

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IGTDSystem gtd;
        public IGTDSystem GTD { get { return gtd; } }

        public GettingThingsDone.DataClassesDataContext DB { get { return new DataClassesDataContext(); } }

        private Users admin;
        public Users Admin { get { return admin; } }

        IDatabaseProvider dbProvider = new LocalDatabaseProvider();

        private IGTDFactory factory;
        public IGTDFactory Factory { get { return factory; } }

        public App()
            : base()
        {
            factory = new DBGTDFactory(dbProvider);
            admin = this.DB.Users.Single(item => item.Username == "admin");
            gtd = Factory.makeSystem(dbProvider.IdManager.GetUser(admin.Id));

            this.Properties["Factory"] = factory;
            this.Properties["Admin"] = admin;
            this.Properties["GTD"] = gtd;
            this.Properties["DBProvider"] = dbProvider;
        }

        private void Application_Exit_1(object sender, ExitEventArgs e)
        {
            DB.SubmitChanges();
        }
    }
}
