using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            string lUser = Login.Text;
            string pUser = pbPassword.Password;
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length > 0) //проверка на ввод логина   
            {
                if (pbPassword.Password.Length > 0) //проверка на ввод пароля       
                {
                    MySqlConnection connection = new MySqlConnection("server=enkavzg1.beget.tech;username=enkavzg1_var8;password=7Iw&ppuQ;database=enkavzg1_var8");

                    using (connection)
                    {
                        string lUser = Login.Text;
                        string pUser = pbPassword.Password;
                        connection.Open();
                        DataTable Table = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        MySqlCommand command = new MySqlCommand(
                          "SELECT IDRole FROM Users WHERE `Login` = @uL AND `Password` = @uP;",
                          connection);
                        command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = lUser;
                        command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pUser;
                        adapter.SelectCommand = command;
                        adapter.Fill(Table);
                        if (Table.Rows.Count > 0)
                        {
                            object read = command.ExecuteScalar();

                            if (read.ToString() != "0")
                            {
                                if (read.ToString() == "Преподаватель")
                                {
                                    connection.Close();
                                    connection.Open();
                                    ListOfBooks DOL = new ListOfBooks();
                                    this.Hide();
                                    DOL.ShowDialog();
                                    this.Show();

                                    string comman = $"SELECT Role FROM Users WHERE `Login` = '{Login.Text}' AND `Password` = '{Login.Text}';";
                                    MySqlDataAdapter da = new MySqlDataAdapter(comman, connection);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    DOL.TDOL.Content = dt.Rows[0]["Role"].ToString();

                                    connection.Close();
                                    
                                    connection.Open();
                                    string comma = $"SELECT Name FROM Users WHERE `Login` = '{Login.Text}' AND `Password` = '{Login.Text}';";
                                    MySqlDataAdapter das = new MySqlDataAdapter(comma, connection);
                                    DataTable dts = new DataTable();
                                    das.Fill(dts);
                                    DOL.TDOL.Content = dt.Rows[0]["Name"].ToString();



                                    //string commands = $"SELECT Photo FROM Users WHERE `Login` = '{Login.Text}' AND `Password` = '{Login.Text}';";
                                    //MySqlDataAdapter daa = new MySqlDataAdapter(commands, connection);
                                    //DataTable dtt = new DataTable();
                                    //daa.Fill(dtt);
                                    //string photo = dtt.Rows[0]["Photo"].ToString();
                                    //BitmapImage bimage = new BitmapImage();
                                    //bimage.BeginInit();
                                    //bimage.UriSource = new Uri(photo, UriKind.Relative);
                                    //bimage.EndInit();
                                    //DOL.Photo.ImageSource = bimage;


                                    

                                    Close();
                                }
                                else
                                {
                                    MySqlCommand rolePR = new MySqlCommand(
                          "SELECT Role FROM Users WHERE `Login` = @uLd AND `Password` = @uPd;",
                          connection);
                                    rolePR.Parameters.Add("@uLd", MySqlDbType.VarChar).Value = lUser;
                                    rolePR.Parameters.Add("@uPd", MySqlDbType.VarChar).Value = pUser;
                                    //adapter.SelectCommand = rolePR;
                                    //adapter.Fill(Table);
                                    ListOfBooks DOL = new ListOfBooks();
                                    DOL.TDOL.Content = (string)rolePR.ExecuteScalar();


                                    MySqlCommand namePR = new MySqlCommand(
                          "SELECT Name FROM Users WHERE `Login` = @uLl AND `Password` = @uPp;",
                          connection);
                                    namePR.Parameters.Add("@uLl", MySqlDbType.VarChar).Value = lUser;
                                    namePR.Parameters.Add("@uPp", MySqlDbType.VarChar).Value = pUser;
                                    //adapter.SelectCommand = rolePR;
                                    //adapter.Fill(Table);
                                    DOL.NDOL.Content = (string)namePR.ExecuteScalar();

                                    this.Hide();
                                    DOL.ShowDialog();
                                    this.Show();
                                    Close();
                                }
                            }
                           
                        }
                        else MessageBox.Show("Пользователь не найден");

                        //
                        
                    }
                   
                }
                else MessageBox.Show("Введите пароль"); //ошибка, если пароль не введен
            }
            else MessageBox.Show("Введите логин"); //ошибка, если логин не введен
        }
    }
}
