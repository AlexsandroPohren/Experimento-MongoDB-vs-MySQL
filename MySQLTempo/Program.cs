using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLTempo
{
    public class Program
    {
        // CONEXÃO
        string[] lines = System.IO.File.ReadAllLines(@"C:\teste\teste.txt"); //Abre o arquivo
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        server = "localhost";
        database = "nome do banco";
        uid = "usuario";
        password = "senha";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
           database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        connection = new MySqlConnection(connectionString);

        //INSERIR

        string query = "INSERT INTO nomeDaTAbela (PRIMEIRO CAMPO, SEGUNDO CAMPO) VALUES('VALOR DO PRIMEIRO CAMPO', 'VALOR DO SEGUNDO CAMPO')";
        MySqlCommand cmd = new MySqlCommand(query, connection);


        //BUSCA

        string query = "SELECT * FROM nomeDaTAbela WHERE nomeDoCampo = 'VALOR DA BUSCA'";
    }
}

        