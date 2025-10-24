using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioPropietario
    {
        private readonly string connectionString;

        public RepositorioPropietario(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty; ;
        }

        public int Alta(Propietario p)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Propietario
                    (Nombre, Apellido, Dni, Telefono, eMail, Clave, existe)
                    VALUES (@nombre, @apellido, @dni, @telefono, @email, @clave,@existe);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono ?? "");
                    command.Parameters.AddWithValue("@email", p.email);
                    command.Parameters.AddWithValue("@clave", p.Clave);
                    command.Parameters.AddWithValue("@existe", 1);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.IdPropietario = res;
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(Propietario p)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE Propietario
                    SET  Apellido=@apellido,Nombre=@nombre, Dni=@dni, Telefono=@telefono, eMail=@email, existe=@existe
                    WHERE IdPropietario = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono ?? "");
                    command.Parameters.AddWithValue("@email", p.email);
                    //command.Parameters.AddWithValue("@clave", p.Clave);
                    command.Parameters.AddWithValue("@existe",0);
                    command.Parameters.AddWithValue("@id", p.IdPropietario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(Propietario p)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE Propietario
                    SET  Apellido=@apellido,Nombre=@nombre, Dni=@dni, Telefono=@telefono, eMail=@email, existe=@existe
                    WHERE IdPropietario = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono ?? "");
                    command.Parameters.AddWithValue("@email", p.email);
                    //command.Parameters.AddWithValue("@clave", p.Clave);
                    command.Parameters.AddWithValue("@existe",1);
                    command.Parameters.AddWithValue("@id", p.IdPropietario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Propietario> ObtenerTodos()
        {
            IList<Propietario> res = new List<Propietario>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, eMail, Clave FROM Propietario WHERE existe = 1";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Propietario p = new Propietario
                        {
                            IdPropietario = reader.GetInt32("IdPropietario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                            email = reader.GetString("eMail"),
                            Clave = reader.GetString("Clave"),
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Propietario? ObtenerPorId(int id)
        {
            Propietario? p = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, eMail, Clave 
                    FROM Propietario WHERE IdPropietario=@id && existe = 1";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            IdPropietario = reader.GetInt32("IdPropietario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                            email = reader.GetString("eMail"),
                            Clave = reader.GetString("Clave"),
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }
         public Propietario? ObtenerPorDni(String dni)
        {
            Propietario? p = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, eMail, Clave 
                    FROM Propietario WHERE Dni=@dni && existe = 1";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@dni", dni);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            IdPropietario = reader.GetInt32("IdPropietario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                            email = reader.GetString("eMail"),
                            Clave = reader.GetString("Clave"),
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }
         public Propietario? ObtenerPorEmail(String eMail)
        {
            Propietario? p = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, eMail, Clave 
                    FROM Propietario WHERE eMail=@eMail && existe = 1";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@eMail", eMail);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            IdPropietario = reader.GetInt32("IdPropietario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Dni = reader.GetString("Dni"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString("Telefono"),
                            email = reader.GetString("eMail"),
                            Clave = reader.GetString("Clave"),
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }

        public IList<Propietario> BuscarPorFraccionApellido(string fraccion)
        {
            IList<Propietario> res = new List<Propietario>();

            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"
                            SELECT IdPropietario, Nombre, Apellido
                            FROM Propietario
                            WHERE Apellido LIKE @fraccion && existe = 1;
                        ";

                using (var command = new MySqlCommand(sql, connection))
                {
                    // Agregamos los comodines para LIKE
                    command.Parameters.AddWithValue("@fraccion", "%" + fraccion + "%");

                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Propietario p = new Propietario
                        {
                            IdPropietario = reader.GetInt32("IdPropietario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido")
                        };
                        res.Add(p);
                    }

                    connection.Close();
                }
            }

            return res;
        }

    }    
    
}