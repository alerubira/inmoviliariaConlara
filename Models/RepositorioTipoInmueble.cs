using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace Inmobiliaria.Models
{
    public class RepositorioTipoInmueble
    {
        private readonly string connectionString;

        public RepositorioTipoInmueble(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")  ?? string.Empty;
        }

        public int Alta(TipoInmueble ti)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO TipoInmueble
                    ( Nombre)
                    VALUES ( @nombre);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", ti.Nombre);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    ti.IdTipoInmueble = res;
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM TipoInmueble WHERE IdTipoInmueble = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(TipoInmueble ti)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE TipoInmueble SET 
                        Nombre=@nombre
                        WHERE IdTipoInmueble = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", ti.Nombre);
                    command.Parameters.AddWithValue("@id", ti.IdTipoInmueble);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<TipoInmueble> ObtenerTodos()
        {
            var res = new List<TipoInmueble>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdTipoInmueble, Nombre
                            FROM TipoInmueble
                            ORDER BY Nombre";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var ti = new TipoInmueble
                        {
                            IdTipoInmueble  = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Nombre  = reader["Nombre"].ToString()  ?? string.Empty
                        };
                        res.Add(ti);
                    }
                    connection.Close();
                }
            }
               return res;
        }
           
        public TipoInmueble? ObtenerPorId(int id)
        {
            TipoInmueble? ti = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdTipoInmueble, Nombre
                            FROM TipoInmueble
                            WHERE IdTipoInmueble = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ti = new TipoInmueble
                        {
                            IdTipoInmueble = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Nombre = reader["Nombre"].ToString() ?? string.Empty
                        };
                    }
                    connection.Close();
                }
            }
            return ti;
        }
    }
}
        
    