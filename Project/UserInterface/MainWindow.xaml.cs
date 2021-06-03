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
using System.Reflection;

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
            Countrys.Clear();
            foreach (var temp in writeFunk.ReadAllCountrys())
            {
                Countrys.Add(temp);
            }
            InitializeComponent();
            DataContext = this;//cuvena linija koda
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            if (!File.Exists(startupPath + "\\prog_2018_05_11.csv"))
                Extract("UserInterface", startupPath, "Resources", "prog_2018_05_11.csv");
            if (!File.Exists(startupPath + "\\prog_2020_05_10.csv"))
                Extract("UserInterface", startupPath, "Resources", "prog_2020_05_10.csv");
            if (!File.Exists(startupPath + "\\prog_2020_05_12.csv"))
                Extract("UserInterface", startupPath, "Resources", "prog_2020_05_12.csv");
            if (!File.Exists(startupPath + "\\prog_2020_05_13.csv"))
                Extract("UserInterface", startupPath, "Resources", "prog_2020_05_13.csv");
            if (!File.Exists(startupPath + "\\prog_2020_05_15.csv"))
                Extract("UserInterface", startupPath, "Resources", "prog_2020_05_15.csv");
            if (!File.Exists(startupPath + "\\prog_2020_05_16.csv"))
                Extract("UserInterface", startupPath, "Resources", "prog_2020_05_16.csv");
            if (!File.Exists(startupPath + "\\prog_2020_05_17.csv"))
                Extract("UserInterface", startupPath, "Resources", "prog_2020_05_17.csv");
        //    string path = System.IO.Path.GetFullPath("prog_2018_05_11.csv");
        }

        string fileLoaction = "";

        public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
                using (BinaryReader r = new BinaryReader(s))
                    using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
                        using (BinaryWriter w = new BinaryWriter(fs))
                            w.Write(r.ReadBytes((int)s.Length));

        }


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
                    Countrys.Clear();
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

                List<List<DataAccess.Model.Consumption>> lista = dataCacheFunctions.GetData(startDate.Text, endDate.Text, idText.Text);
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
