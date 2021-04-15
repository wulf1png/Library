using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    class MySqlHelper
    {
        private static string stringConnection;
        private static MySqlDataAdapter MySql_dataAdapter;
        private static MySqlConnection MySql_connect;
        private static MySqlCommand MySql_command;

        /// <summary>
        /// Возвращает текущую виртуальную таблицу.
        /// </summary>
        public static DataTable Table { get; private set; }

        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public static string DataBaseConnection
        {
            set
            {
                stringConnection = value;

                //Строка для подключение к базе данных.
                MySql_connect = new MySqlConnection(stringConnection);

                //Открываем соединение с базой данных.
                MySql_connect.Open();
            }
        }

        /// <summary>
        /// Выполняет запрос к базе данных
        /// </summary>
        /// <param name="script">Скрипт запроса(INSERT, UPDATE, DELETE)</param>
        public static void Execut(string script)
        {
            try
            {
                //Открываем соединение с базой данных.
                MySql_connect.Open();
                MySql_command = MySql_connect.CreateCommand();
                MySql_command.CommandText = script;
                MySql_command.ExecuteNonQuery();
                CloseDB();
            }
            catch (Exception ex) { CloseDB(); MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// Выполняет запрос к базе данных и возвращает DataTable
        /// </summary>
        /// <param name="script">Скрипт запроса(INSERT, UPDATE, DELETE)</param>
        /// <param name="SelectScript">Скрипт запроса(SELECT)</param>
        /// <returns></returns>
        public static DataTable Execut(string script, string SelectScript)
        {
            try
            {
                MySql_connect.Open();
                MySql_command = MySql_connect.CreateCommand();
                MySql_command.CommandText = script;
                MySql_command.ExecuteNonQuery();
                return RunQuery(SelectScript);
            }
            catch (Exception ex) { CloseDB(); MessageBox.Show(ex.Message); return Table; }
        }

        /// <summary>
        /// Выполняет запрос к базе данных и возвращает DataTable
        /// </summary>
        /// <param name="script">Скрипт запроса(SELECT)</param>
        public static DataTable RunQuery(string script)
        {
            try
            {
                MySql_dataAdapter = new MySqlDataAdapter(script, MySql_connect);
                Table = new DataTable();
                MySql_dataAdapter.Fill(Table);
                CloseDB();
            }
            catch (Exception ex) { CloseDB(); MessageBox.Show(ex.Message); }

            return Table;

        }

        /// <summary>
        /// Возвращает значение ячейки.
        /// </summary>
        /// <param name="col">Индекс столбца</param>
        /// <param name="row">Индекс строки</param>
        /// <returns></returns>
        public static object GetCellValue(int col, int row)
        {
            return Table.Rows.Count != 0 ? Table.Rows[row].ItemArray[col] : 0;
        }

        /// <summary>Закрывает соединение с базой данных
        /// 
        /// </summary>
        public static void CloseDB()
        {
            //Закрываем соединение с бд
            if (MySql_connect != null)
                MySql_connect.Close();
        }
    }
}
