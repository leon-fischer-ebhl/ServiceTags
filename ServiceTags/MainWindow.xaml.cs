using Microsoft.Win32;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServiceTags
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

        private void selectTextFileBtn_Click(object sender, RoutedEventArgs e)
        {
            handleSelectFileBtn();
        }

        private void handleSelectFileBtn()
        {
            string filePath = selectTextFile();
            List<string> filteredServiceTags = new List<string>();
            if (!String.IsNullOrWhiteSpace(filePath))
            filteredServiceTags = filterServiceTags(filePath);
            foreach (string line in filteredServiceTags) serviceTagsTb.Text += line + "\n";
            serviceTagsTb.Text = serviceTagsTb.Text.Trim();
        }

        private List<string> filterServiceTags(string file)
        {
            List<string> result = new List<string>();
            List<string> filtered = new List<string>();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        result.Add(sr.ReadLine());
                    }
                }
            }
            foreach (string line in result)
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("Service Tag"))
                    filtered.Add(line.Replace("Service Tag:","").Trim());
            return filtered;
        }

        private string selectTextFile()
        {
            serviceTagsTb.Text = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                return filePath;
            }
            return "";
        }

        private void serviceTagsTb_Click(object sender, MouseButtonEventArgs e)
        {
            serviceTagsTb.SelectAll();
            serviceTagsTb.Copy();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
