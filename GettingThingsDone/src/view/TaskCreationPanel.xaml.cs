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

namespace GettingThingsDone.src.view
{
    /// <summary>
    /// Logique d'interaction pour TaskCreationPanel.xaml
    /// </summary>
    public partial class TaskCreationPanel : UserControl
    {
        public TaskCreationPanel()
        {
            InitializeComponent();
        }

        private void DueDateChecked(object sender, RoutedEventArgs e)
        {
            DueDatePicker.IsEnabled = true;
        }

        private void DueDateUnchecked(object sender, RoutedEventArgs e)
        {
            DueDatePicker.IsEnabled = false;
        }

        public Task CreateTask()
        {
            if (DueDateBox.IsChecked.Value)
                return new SingleTask(TitleText.Text, DescText.Text, DueDatePicker.SelectedDate.Value);
            else
                return new SingleTask(TitleText.Text, DescText.Text);
        }
    }
}
