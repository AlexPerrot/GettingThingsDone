﻿using System;
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

        private IGTDFactory factory = new DBGTDFactory(new LocalDatabaseProvider());
        public IGTDFactory Factory { get { return factory; } }

        public App()
            : base()
        {
            admin = this.DB.Users.Single(item => item.Username == "admin");
            gtd = Factory.makeSystem();
        }

        private void Application_Exit_1(object sender, ExitEventArgs e)
        {
            DB.SubmitChanges();
        }
    }
}
