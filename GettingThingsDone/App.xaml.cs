﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        private GTDSystem gtd = new GTDSystem();
        public GTDSystem GTD { get { return gtd; } }

    }
}
