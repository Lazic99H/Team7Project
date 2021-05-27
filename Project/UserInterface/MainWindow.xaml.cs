using System;
using System.Collections.Generic;
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
                    string lokacijaSlike = filterFiles.FileName;// ovdje je kompletna putanja C//users//programs//Faks//prog_2018_02_03.csv npr
                    string fileName = new DirectoryInfo(filterFiles.FileName).Name;//ovo je prog_2018_02_03.csv brutalno
                    csvFileName.Text = fileName;
                    //bool validacija = Validation.ValidateFileName(fileName);
                    //fileName += "/";
                    //fileName += new DirectoryInfo(filterFiles.FileName).Name;
                    //BitmapImage pom = new BitmapImage();
                    //pom.BeginInit();
                    //pom.UriSource = new Uri(fileName, UriKind.Relative);
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

        private void Button_Click_Load(object sender, RoutedEventArgs e)
        {
            if(csvFileName.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Chose a file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //else{} pozove funkciju iz FileWriter.... i obrise to iz csvFileName
        }

        private void Button_Click_Find(object sender, RoutedEventArgs e)
        {
            if(endDate.Text.Equals("") || startDate.Text.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Sva polja moraju biti ispravno popunjena", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if (idText.Text.Trim().Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Sva polja moraju biti popunjena", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string to = startDate.Text;
                string end = endDate.Text;
                string[] tos = to.Split('.');
                string[] ends = end.Split('.');
                if (int.Parse(tos[0]) >= int.Parse(ends[0]))
                {
                    System.Windows.Forms.MessageBox.Show("Pocetni datum mora biti manji od krajnjeg datuma", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            
        }
    }
}
