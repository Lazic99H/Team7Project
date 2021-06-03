﻿using DataAccess.Model;
using FileWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using DataCache;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static BindingList<Consumption> consumptions
        {
            get; set;
        }

        public static int counter = 0;

        public static BindingList<string> Countrys
        {
            get; set;
        }

        public static FileWriter.Program writeFunk = new FileWriter.Program();
        //public static DataCache.Program = new DataCache.Program();
        public static DataCache.DataCacheFunctions dataCacheFunctions = new DataCacheFunctions();
        public static DataCache.Validate validate = new Validate();



        public MainWindow()
        {
            consumptions = new BindingList<Consumption>();
            Countrys = new BindingList<string>();
            foreach (var temp in writeFunk.ReadAllCountrys())
            {
                Countrys.Add(temp);
            }
            InitializeComponent();
            DataContext = this;//cuvena linija koda
            
        }

        string fileLoaction = "";

        private void Button_Click_Browser(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog filterFiles = new System.Windows.Forms.OpenFileDialog();
                filterFiles.Filter = "csv files(*.csv)|*.csv| All Files(*.*)|*.*";

                if (filterFiles.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileLoaction = filterFiles.FileName;// ovdje je kompletna putanja C//users//programs//Faks//prog_2018_02_03.csv npr
                    string fileName = new DirectoryInfo(filterFiles.FileName).Name;//ovo je prog_2018_02_03.csv brutalno
                    csvFileName.Text = fileName;
                }

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click_Load(object sender, RoutedEventArgs e)
        {
            if (csvFileName.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Chose a file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                string check = writeFunk.Write(fileLoaction, csvFileName.Text);
                if (check == "dateExists")
                {
                    System.Windows.Forms.MessageBox.Show("Date already exists for that day and region!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (check == "good")
                {
                    Countrys = new BindingList<string>();
                    foreach (var temp in writeFunk.ReadAllCountrys())
                    {
                        Countrys.Add(temp);
                    }
                    System.Windows.Forms.MessageBox.Show("Data has been successfully added", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Some of the hours are missing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                csvFileName.Text = "";
                fileLoaction = "";
            }
        }
        private void Button_Click_Find(object sender, RoutedEventArgs e)
        {
            string messagee = validate.ValidateEntry(startDate.Text, endDate.Text, idText.Text.Trim());
            if (messagee == "")
            {
                counter++;
                if (counter != 1)
                {
                    consumptions.Clear();
                }

                List<List<DataAccess.Model.Consumption>> lista = dataCacheFunctions.CheckForQueries(startDate.Text, endDate.Text, idText.Text);
                if (lista == null)
                {
                    noContentLabel.Content = "There are no data with this request.";
                }
                else
                {
                    noContentLabel.Content = "";
                    foreach (List<DataAccess.Model.Consumption> item in lista)
                    {
                        foreach (DataAccess.Model.Consumption item2 in item)
                        {
                            consumptions.Add(item2);
                        }
                    }
                    //dataCacheFunctions.Deamon();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(messagee, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


    }
}
