﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Browser(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog filterFiles = new System.Windows.Forms.OpenFileDialog();
                filterFiles.Filter = "csv files(*.csv)|*.csv| All Files(*.*)|*.*";

                if (filterFiles.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //string lokacijaSlike = filterFiles.FileName; ovo file name daje putanju ja msm tj trebalo bi
                    //string rez = new DirectoryInfo(filterFiles.FileName).Parent.Name;
                    //rez += "/";
                    //rez += new DirectoryInfo(filterFiles.FileName).Name;

                    //BitmapImage pom = new BitmapImage();
                    //pom.BeginInit();
                    //pom.UriSource = new Uri(rez, UriKind.Relative);
                    //pom.EndInit();

                    //slicica.Stretch = Stretch.Fill;

                    //slicica.Source = pom;
                    //// textBoxIme.Text = slicica.Source.ToString();

                }

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}