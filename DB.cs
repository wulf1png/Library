using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library
{
    class DB
    {
        //соединяемся с БД
        MySqlConnection connection = new MySqlConnection("server=enkavzg1.beget.tech;username=enkavzg1_var8;password=7Iw&ppuQ;database=enkavzg1_var8");

        public void openConnection() //функция с открытием соединения(не используется пока)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closedConnection() //функция с закрытием(не используется пока)
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection() //функция подключения бд(используется)
        {
            return connection;

        }

    }
}

