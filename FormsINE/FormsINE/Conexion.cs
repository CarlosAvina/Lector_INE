using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FormsINE
{
    public class Conexion
    {
        Form1 frm = new Form1();
        public Persona GetAll(string id)
        {
            try
            {
                Persona persona = new Persona();
                DataRow row;
                var Conjunto = new DataSet();
                var conexion = new MySqlConnection("Server=localhost;Database=ine;Uid=root;Pwd=;SslMode=none");
                conexion.Open();
                var Consultar = new MySqlDataAdapter("SELECT * FROM ine.persona WHERE id = '" + id + "'", conexion);
                Consultar.Fill(Conjunto, "Datos");
                conexion.Close();

                row = Conjunto.Tables["Datos"].Rows[0];
                persona.ine_id = row["id"].ToString();
                persona.Nombre = row["nombre"].ToString();
                persona.FechaNacimiento = (DateTime)row["fecha_nacimiento"];
                string str = (string)row["imagen"];
                persona.Imagen = frm.StringToImage(str);

                return persona;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public void InsertTransaction(Transaction transaction)
        {
            try
            {
                Persona persona = new Persona();
                var conexion = new MySqlConnection("Server=localhost;Database=ine;Uid=root;Pwd=;SslMode=none");
                conexion.Open();
                var Consultar = new MySqlCommand($"INSERT INTO ine.transaction (id, ine_id, date_transaction, hour_transaction, status) VALUES ({transaction.id},'{transaction.persona.ine_id}','{transaction.date_transaction}','{transaction.hour_transaction}', {transaction.status});", conexion);
                Consultar.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public int GetTransactions(string fecha)
        {
            var dataset = new DataSet();
            try
            {
                var conexion = new MySqlConnection("Server=localhost;Database=ine;Uid=root;Pwd=;SslMode=none");
                conexion.Open();
                var consulta = new MySqlDataAdapter($"SELECT * FROM ine.transaction WHERE date_transaction = '{fecha}' and status = 1", conexion);
                conexion.Close();

                consulta.Fill(dataset, "Datos");

                return dataset.Tables["Datos"].Rows.Count;
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);
                return 0;
            }
        }
    }
}
