﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GettingThingsDone.src.model;

namespace GettingThingsDone.src.view
{
    /// <summary>
    /// Logique d'interaction pour ProjectPanel.xaml
    /// </summary>
    public partial class ProjectsPanel : UserControl
    {

        public List<Project> Projects { get; set; }

        public ProjectsPanel()
        {
            InitializeComponent();

            this.Projects = new List<Project>();
            this.Projects.Add(new Project("mon projet 1", "ma desc 1", null, false, DateTimeOffset.Now));
            this.Projects.Add(new Project("mon projet 2", "ma desc 2", null, false, DateTimeOffset.Now));

            this.DataContext = this;
        }
    }
}
