using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Логика взаимодействия для SearchBooks.xaml
    /// </summary>
    public partial class SearchBooks : Window
    {
        ListOfBooks books;
        public SearchBooks(ListOfBooks bb)
        {
            InitializeComponent();
            this.books = bb;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BookName.Text.Length > 0) //проверка на ввод названия  
            {
                if (Author.Text.Length > 0) //проверка на ввод автора      
                {
                    if (Year.Text.Length > 0) //проверка на ввод года      
                    {

                        MySqlConnection connection = new MySqlConnection("server=enkavzg1.beget.tech;username=enkavzg1_var8;password=7Iw&ppuQ;database=enkavzg1_var8;Allow User Variables=True");

                        using (connection)
                        {
                            string bookname = BookName.Text;
                            string author = Author.Text;
                            string year = Year.Text;
                            connection.Open();
                            DataTable Table = new DataTable();
                            MySqlDataAdapter adapter = new MySqlDataAdapter();
                            MySqlCommand command = new MySqlCommand(
                              "SELECT * FROM Books WHERE `NameBook` = @uLh AND Year = @uTh AND Author = @uPh",
                              connection);
                            command.Parameters.Add("@uLh", MySqlDbType.VarChar).Value = bookname;
                            command.Parameters.Add("@uPh", MySqlDbType.VarChar).Value = author;
                            command.Parameters.Add("@uTh", MySqlDbType.Int32).Value = year;
                            adapter.SelectCommand = command;
                            adapter.Fill(Table);
                            if (Table.Rows.Count > 0)
                            {

                                using (connection)
                                {
                                    try
                                    {
                                        MySqlCommand cmd = new MySqlCommand($"SELECT Photo, NameBook, Author, Year FROM Books WHERE NameBook = '{BookName.Text}' AND Year = '{Year.Text}' AND Author = '{Author.Text}'", connection);
                                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                                        DataSet ds = new DataSet();
                                        adp.Fill(ds, "LoadDataBinding");
                                        books.datagrid.DataContext = ds;
                                    }
                                    catch (MySqlException ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }

                                    connection.Close();
                                    Coll();
                                    Close();
                                    
                                }
                            }
                            else
                            {
                                MessageBox.Show("Книга не найдена");
                            }
                        }
                    }
                    else MessageBox.Show("Введите год");
                }
                else MessageBox.Show("Введены не все данные");
            }
            else MessageBox.Show("Введены не все данные");
        }

        public void Coll()
        {
            int coll = books.datagrid.Items.Count;
            books.ListColl.Content = $"Книг в списке - {coll}";
            Debug.WriteLine(coll);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
        private void Year_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Year_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

    }
}
