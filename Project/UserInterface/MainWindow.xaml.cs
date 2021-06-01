using DataAccess.Model;
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

        public static BindingList<string> Countrys
        {
            get; set;
        }

        public static Program writeFunk = new Program();


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

            if (endDate.Text.Equals("") || startDate.Text.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("All fields must be filled in correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (idText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("All fields must be filled in correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string to = startDate.Text;
                string end = endDate.Text;
                string[] tos = to.Split('/');
                string[] ends = end.Split('/');
                if (int.Parse(tos[2]) > int.Parse(ends[2]))
                {
                    System.Windows.Forms.MessageBox.Show("Starting date must be lower then ending date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (int.Parse(tos[2]) == int.Parse(ends[2]))
                {
                    if (int.Parse(tos[0]) > int.Parse(ends[0]))
                    {
                        System.Windows.Forms.MessageBox.Show("Starting date must be lower then ending date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (int.Parse(tos[0]) == int.Parse(ends[0]) && int.Parse(tos[1]) > int.Parse(ends[1]))
                    {
                        System.Windows.Forms.MessageBox.Show("Starting date must be lower then ending date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        DataCache.DataCacheFunctions dataCacheFunctions = new DataCacheFunctions();
                        List<List<DataAccess.Model.Consumption>> lista = dataCacheFunctions.CheckForQueries(to, end, idText.Text);
                        foreach (List<DataAccess.Model.Consumption> item in lista)
                        {
                            foreach (DataAccess.Model.Consumption item2 in item)
                            {
                                consumptions.Add(item2);
                            }
                        }
                    }
                }
                else
                {
                    DataCache.DataCacheFunctions dataCacheFunctions = new DataCacheFunctions();
                    List<List<DataAccess.Model.Consumption>> lista = dataCacheFunctions.CheckForQueries(to, end, idText.Text);
                    foreach (List<DataAccess.Model.Consumption> item in lista)
                    {
                        foreach (DataAccess.Model.Consumption item2 in item)
                        {
                            consumptions.Add(item2);
                        }
                    }
                }
            }


        }


    }
}
