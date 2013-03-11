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
using System.Windows.Shapes;

namespace GettingThingsDone.src.view
{
    /// <summary>
    /// Logique d'interaction pour TaskCreationWindow.xaml
    /// </summary>
    public partial class TaskCreationWindowWithContext : Window
    {
        public IEnumerable<TaskList> Contexts { get { return (App.Current as App).GTD.Contexts; } }

        public TaskCreationWindowWithContext()
        {
            InitializeComponent();
        }

        public static void NewTaskDialog()
        {
            TaskCreationWindowWithContext win = new TaskCreationWindowWithContext();
            if (win.ShowDialog().Value)
            {
                Task t = win.CreationPanel.CreateTask();
                TaskList l = win.ContextSelect.SelectedItem as TaskList;
                l.AddTask(t);
            }
        }
        
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
