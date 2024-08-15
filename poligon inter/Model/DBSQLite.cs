using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace poligon_inter.Model
{
    public class DBSQLite
    {

        private SqliteConnection s_conn;

        public DBSQLite(string path, string dbase)
        {
            //yyy robić pusty ??
            //bo jest potrzebny taki w którym wskazujemy bazę 
            // i by się przydała jakaś zmienna wskazująca  na katalog z danymi programu
            string CS;

            CS = "Data Source=" + path + "\\" + dbase + ".db";
            s_conn = new SqliteConnection(CS);
            s_conn.Open();

            DbuildDb(dbase);

        }

        private void DbuildDb(bool update = false,string name = "")
        {
            // to ma tworzyć automatycznie tabele w nowej bazie lub dodawać nowe jak nie istnieją
            // na zasadzie create if not exist
            //s_conn.Open();
            //var param = "jakaś baza";
            DbuildDb(name);
        }
        private void DbuildDb(string name = "")
        {
            // to ma tworzyć automatycznie tabele w nowej bazie lub dodawać nowe jak nie istnieją
            // na zasadzie create if not exist
            //s_conn.Open();
            //var param = "jakaś baza";
            var command = s_conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS katalogi (path , id  INTEGER PRIMARY KEY)";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS pliki (nazwa varchar(255),rozszezenie varchar(5), id_katalogu, usuniety bool, MD5 , id  INTEGER PRIMARY KEY)";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS slowniki (nazwa,wartosc , id  INTEGER PRIMARY KEY)";
            command.ExecuteNonQuery();
            command.CommandText = String.Format(@"insert into slowniki (nazwa,wartosc) values ('DBname','{0}')", name);
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS katerorie (nazwa,id_rodzica,wartosc , id  INTEGER PRIMARY KEY)";
            command.ExecuteNonQuery();
            command.CommandText = String.Format(@"insert into kategorie (nazwa,wartosc) values ('DBname','{0}')", name);
            command.ExecuteNonQuery();
        }

        public void AddPassword(string password)
        {
            string pass = Tools.CalculateMD5Sting(password);
            var command = s_conn.CreateCommand();
            command.CommandText = String.Format(@"insert into slowniki (nazwa,wartosc) values (pass,'{0}')", pass);
            command.ExecuteNonQuery();
        }

        public void addFile(string nazwa, string path, string MD5 = "")
        {
            string id = string.Empty;
            var command = s_conn.CreateCommand();
            command.CommandText = String.Format("SELECT id FROM katalogi WHERE path='{0}'", path);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader["id"].ToString();
                //MessageBox.Show("id: " + reader["id"]);
            }
            reader.Close();
            if ((id == string.Empty) || (id == null))
            {
                command.CommandText = String.Format(@"insert into katalogi (path) values ('{0}')", path);
                command.ExecuteNonQuery();
                command.CommandText = String.Format("SELECT id FROM katalogi WHERE path='{0}'", path);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader["id"].ToString();
                    //MessageBox.Show("id: " + reader["id"]);
                }
                reader.Close();
            }


            command.CommandText = String.Format(@"insert into pliki (nazwa,id_katalogu,usuniety,rozszezenie,MD5) values ('{0}','{1}',false,'','{2}')", nazwa, id, MD5);
            var ile = command.ExecuteNonQuery();
            if (ile < 1) MessageBox.Show(" chyba coś poszło nie tak, ilość dodana: " + ile);
        }

        public void addFile(string CombinePath)
        {

        }


        public int FileExists(string nazwa)
        {

            return 0;
        }

        public bool CatalogExists(string path)
        {
            return false;
        }

        public bool MD5Exists(string MD5)
        {
            return false;
        }
    }
}

