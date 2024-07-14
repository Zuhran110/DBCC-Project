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
using WpfApp6.Views.General_Manager;

namespace WpfApp6.Views.Store_Keeper
{
    /// <summary>
    /// Interaction logic for ok.xaml
    /// </summary>
    public partial class ok : Page
    {
        public Report SelectedReport
        {
            get; set;
        }

        public ok(Report report)
        {
            InitializeComponent();
            SelectedReport = report;
            DataContext = SelectedReport;
        }

        public ok()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Approve_Btn(object sender, RoutedEventArgs e)
        {
        }

        private void Decline_Btn(object sender, RoutedEventArgs e)
        {
        }
    }
}