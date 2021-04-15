using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для ListOfBooks.xaml
    /// </summary>
    public partial class ListOfBooks : Window
    {
        public ListOfBooks()
        {
            InitializeComponent();
            FillGrid();
            Coll();
     
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что желаете выйти?", "Внимание", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MainWindow Log = new MainWindow();
                    this.Hide();
                    Log.ShowDialog();
                    this.Show();
                    Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SearchBooks dob = new SearchBooks(this);

            dob.ShowDialog();

            
        }

        private void Btn_Full(object sender, RoutedEventArgs e)
        {
            DataGrid datagrid = ((Button)sender).CommandParameter as DataGrid;
            var selectedRow = datagrid.SelectedItem;
            var selectedIndex = datagrid.SelectedIndex;


        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void FillGrid()
        {
            string sql = "SELECT Photo, NameBook, Author, Year FROM Books";

            using (MySqlConnection connection = new MySqlConnection("server=enkavzg1.beget.tech;username=enkavzg1_var8;password=7Iw&ppuQ;database=enkavzg1_var8"))
            {

                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds, "LoadDataBinding");
                    datagrid.DataContext = ds;

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                connection.Close();


            }
        }

        public void Coll()
        {
            int coll = datagrid.Items.Count;
            ListColl.Content = $"Книг в списке - {coll}";
        }

        private void Btn_Dell(object sender, RoutedEventArgs e)
        {



        }

        private void Btn_Clear(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT Photo, NameBook, Author, Year FROM Books";

            using (MySqlConnection connection = new MySqlConnection("server=enkavzg1.beget.tech;username=enkavzg1_var8;password=7Iw&ppuQ;database=enkavzg1_var8"))
            {

                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds, "LoadDataBinding");
                    datagrid.DataContext = ds;
                   

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                connection.Close();
                Coll();

            }
        }
    }
}
